using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.MultiTenancy;
using KartSpace.Authorization.Users;
using KartSpace.Merchandise;
using KartSpace.Purchases.Dto;
using KartSpace_MLPredictor;
using Microsoft.AspNetCore.Mvc;

namespace KartSpace.Purchases
{
    public class PurchaseAppService : AsyncCrudAppService<Purchase, PurchaseDto, int, PagedPurchaseResultRequestDto, PurchaseDto, PurchaseDto, EntityDto<int>, EntityDto<int>>, IPurchaseAppService
    {
        private readonly IRepository<Purchase, int> _purchaseRepository;
        private readonly IRepository<Merch, int> _merchRepository;
        private readonly UserManager _userManager;
        private readonly IRepository<User, long> _userRepository;
        private readonly IMerchAppService _merchAppService;


        public PurchaseAppService(
            IRepository<Purchase, int> purchaseRepository,
            IRepository<Merch, int> merchRepository,
            UserManager userManager,
            IRepository<User, long> userRepository, 
            IMerchAppService merchAppService)
            : base(purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
            _merchRepository = merchRepository;
            _userManager = userManager;
            _userRepository = userRepository;
            _merchAppService = merchAppService;
            LocalizationSourceName = KartSpaceConsts.LocalizationSourceName;
        }

        public async Task BuyMerchAsync(BuyMerchDto input)
        {
            var userId = AbpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);

            user.PhoneNumber = input.PhoneNumber;

            await _userManager.UpdateAsync(user);

            var newPurchase = new Purchase
            {
                MerchId = input.Id,
                UserId = user.Id,
                CreationDateTime = DateTime.Now,
                StareComanda = TipStareComanda.Plasata
            };

            await _purchaseRepository.InsertAsync(newPurchase);
        }

        public async Task EditStateAsync(EditStateDto input)
        {
            var purchase = await _purchaseRepository.GetAsync(input.PurchaseId);

            purchase.StareComanda = input.NouaStareComanda;

            await _purchaseRepository.UpdateAsync(purchase);
        }

        [HttpGet]
        public async Task<PagedResultDto<PurchaseResultDto>> DisplayPurchases(PagedPurchaseResultRequestDto input, TipStareComanda stareComanda)
        {
            var merchQuery = _merchRepository.GetAll();
            var purchasesQuery = _purchaseRepository.GetAll();
            var usersQuery = _userRepository.GetAll();
            
            using (UnitOfWorkManager.Current.SetTenantId(null))
            {
                usersQuery = _userRepository.GetAll();
            

                var query = from purchase in purchasesQuery
                            join merch in merchQuery on purchase.MerchId equals merch.Id
                            join user in usersQuery on purchase.UserId equals user.Id
                            select new
                            {
                                purchase,
                                merch,
                                user
                            };

                var queryRez = await AsyncQueryableExecuter.ToListAsync(query);
                

                var selectiveQueryRez = queryRez
                    .Where(x => (AbpSession.MultiTenancySide == MultiTenancySides.Tenant) ?
                        x.merch.TenantId == AbpSession.TenantId : x.purchase.UserId == AbpSession.UserId)
                    .ToList();

                var purchasesList = selectiveQueryRez.Select(x =>
                {
                    var lista = new PurchaseResultDto
                    {
                        Id = x.purchase.Id,
                        ProductName = x.merch.Name,
                        Price = x.merch.Price,
                        UserFullName = x.user.FullName,
                        UserPhoneNumber = x.user.PhoneNumber,
                        CreationDate = x.purchase.CreationDateTime,
                        CreationDateString = x.purchase.CreationDateTime.ToString("dd/MM/yyyy hh:mm"),
                        TipStareComanda = x.purchase.StareComanda,
                        StareComandaName = GetDisplayName(x.purchase.StareComanda)
                    };
                    return lista;
                }
                ).ToList();

                var purchasesFilter = purchasesList
                    .WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
                    x => x.ProductName.Contains(input.Keyword)
                         || x.UserFullName.Contains(input.Keyword)
                         || x.UserPhoneNumber.Contains(input.Keyword)
                         || x.CreationDateString.Contains(input.Keyword)
                         || x.Price.ToString().Contains(input.Keyword)
                         )
                    .Where(x=>x.TipStareComanda == stareComanda)
                    .ToList();

                var purchasesDisplay = new PagedResultDto<PurchaseResultDto>(purchasesFilter.Count, purchasesFilter);
                return purchasesDisplay;
            }
        }

        /// <summary>
        /// Based on last purchase, recommends first most confident prediction
        /// If there is no last purchase, recommends two most popular products on the market
        /// </summary>
        /// <returns>Paged result since data is to be displayed in table</returns>
        public async Task<PagedResultDto<PurchaseRecommendationDto>> GetRecommendationsAsync()
        {
            var lastPurchaseOrEmpty = _purchaseRepository
                .GetAll()
                .OrderByDescending(x=>x.CreationDateTime)
                .FirstOrDefault(x => x.UserId == AbpSession.UserId.Value);

            if (lastPurchaseOrEmpty == null)
            {
                var mostPurchasedQuery = _purchaseRepository.GetAll()
                    .GroupBy(m => m.MerchId)
                    .OrderByDescending(mg => mg.Count())
                    .Take(2)
                    .Select(m => m.Key);

                var queryRez = await AsyncQueryableExecuter.ToListAsync(mostPurchasedQuery);

                var recList = queryRez.Select(x =>
                    {
                        var merch = _merchRepository.Get(x);

                        var lista = new PurchaseRecommendationDto
                        {
                            Name = merch.Name,
                            Price = merch.Price,
                            CategoryName = _merchAppService.GetDisplayName(merch.Category),
                            Description = merch.Description
                        };

                        return lista;
                    }
                ).ToList();

                var recDisplay = new PagedResultDto<PurchaseRecommendationDto>(recList.Count, recList);
                return recDisplay;
            }

            var lastProduct = _merchRepository.GetAll().First(x=> x.Id == lastPurchaseOrEmpty.MerchId);

            //Load sample data
            var sampleData = new ProductRecommender.ModelInput()
            {
                MerchId = lastProduct.Id,
                Category = (int)lastProduct.Category,
                BoughtWithCategory = (int)TipMerch.Accesorii,
            };

            //Load model and predict output
            var result = ProductRecommender.Predict(sampleData);

            var predictedId = (int)result.PredictedLabel;
            var recommended = _merchRepository.Get(predictedId);
            var displayRecommendation = new PurchaseRecommendationDto
            {
                Name = recommended.Name,
                Description = recommended.Description,
                CategoryName = _merchAppService.GetDisplayName(recommended.Category),
                Price = recommended.Price
            };

            List<PurchaseRecommendationDto> displayList = new() {displayRecommendation};


            return new PagedResultDto<PurchaseRecommendationDto>(1, displayList);
        }

        public string GetDisplayName(TipStareComanda enumValue)
        {
            string displayName;

            displayName = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            return displayName;
        }
    }
}