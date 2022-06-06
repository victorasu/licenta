using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using KartSpace.Authorization.Users;
using KartSpace.Merchandise.Dto;
using KartSpace.Purchases;
using Microsoft.AspNetCore.Mvc;

namespace KartSpace.Merchandise
{
    public class MerchAppService : AsyncCrudAppService<Merch, MerchDto, int, PagedMerchResultRequestDto, MerchDto, MerchDto, EntityDto<int>, EntityDto<int>>, IMerchAppService
    {
        private readonly IRepository<Merch, int> _merchRepository;
        private readonly UserManager _userManager;
        private readonly IRepository<Purchase, int> _purchaseRepository;

        public MerchAppService(
            IRepository<Merch, int> merchRepository,
            UserManager userManager,
            IRepository<Purchase, int> purchaseRepository)
            : base(merchRepository)
        {
            _merchRepository = merchRepository;
            _userManager = userManager;
            _purchaseRepository = purchaseRepository;
            LocalizationSourceName = KartSpaceConsts.LocalizationSourceName;
        }

        public override async Task<MerchDto> CreateAsync(MerchDto input)
        {
            var merch = ObjectMapper.Map<Merch>(input);

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException(L("UnauthorizedAction"), L("CantCreateAsHost"));
            }

            merch.TenantId = AbpSession.TenantId.Value;

            var merchData = await _merchRepository.InsertAsync(merch);

            return ObjectMapper.Map<MerchDto>(merchData);
        }

        public override async Task<MerchDto> UpdateAsync(MerchDto input)
        {
            var merch = ObjectMapper.Map<Merch>(input);

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException(L("UnauthorizedAction"), L("CantCreateAsHost"));
            }

            merch.TenantId = AbpSession.TenantId.Value;

            var merchData = await _merchRepository.UpdateAsync(merch);

            return ObjectMapper.Map<MerchDto>(merchData);
        }

        protected override IQueryable<Merch> CreateFilteredQuery(PagedMerchResultRequestDto input)
        {
            var query = _merchRepository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
                x=>x.Name.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword)
                || x.Price.ToString().Contains(input.Keyword));

            return query;
        }

        public async Task<PagedResultDto<MerchResultDto>> GetMerchList(PagedMerchResultRequestDto input, TipMerch category)
        {
            var produse = CreateFilteredQuery(input);

            var query = from produs in produse
                        where category.Equals(TipMerch.Alege) ? true : produs.Category.Equals(category)
                        select produs;

            //query = query
            //    .Skip(input.SkipCount)
            //    .Take(input.MaxResultCount);

            var queryRez = await AsyncQueryableExecuter.ToListAsync(query);

            var merchList = queryRez.Select(x =>
                {
                    var lista = ObjectMapper.Map<MerchResultDto>(x);
                    lista.CategoryName = GetDisplayName(lista.Category);
                    return lista;
                }
            ).ToList();

            var merchPaginated = merchList
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var merchDisplay = new PagedResultDto<MerchResultDto>(merchList.Count, merchPaginated);

            return merchDisplay;
        }

        public string GetDisplayName(TipMerch enumValue)
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