using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using KartSpace.Events;
using KartSpace.Events.Dto;

namespace KartSpace.Tests
{
    public class EventAppService_Tests : KartSpaceTestBase
    {
        private readonly IEventAppService _eventAppService;

        public EventAppService_Tests()
        {
            _eventAppService = Resolve<IEventAppService>();
        }

        [Fact]
        public async Task GetEvents_Test_BlackBox()
        {
            //Act
            var output = await _eventAppService.GetAllAsync(new PagedEventResultRequestDto{MaxResultCount = 20, SkipCount = 0});
            //Act
            output.Items.Where(x => x.EndTime.Value.Month > 12 || x.StartTime.Month > 12).ToList().Count.ShouldBe(0);

            //Act
            output.Items.Where(x => x.EndTime.Value.Month <= 0 || x.StartTime.Month <= 0).ToList().Count.ShouldBe(0);

            //Act
            output.Items.Where(x => x.EndTime.Value.Month > 0 || x.StartTime.Month > 0).ToList().Count.ShouldBeGreaterThan(0);

            //Act
            output.Items.Where(x => x.EndTime.Value.Month < 12 || x.StartTime.Month < 12).ToList().Count.ShouldBeGreaterThan(0);


            //Act
            output.Items.Where(x => x.EndTime.Value.Month == 13  || x.StartTime.Month == 13).ToList().Count.ShouldBe(0);

            //Act
            output.Items.Where(x => x.EndTime.Value.Month == 00 || x.StartTime.Month == 00).ToList().Count.ShouldBe(0);

            //Act
            output.Items.Where(x => x.EndTime.Value.Month == 12 || x.StartTime.Month == 12).ToList().Count.ShouldBeGreaterThan(0);

            //Act
            output.Items.Where(x => x.EndTime.Value.Month == 01 || x.StartTime.Month == 01).ToList().Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetEvents_Test_WhiteBox()
        {
            //road1 - tc1 => 2(F)
            var output = await _eventAppService
                .GetAllAsync(new PagedEventResultRequestDto { Keyword = null, MaxResultCount = 20, SkipCount = 0 });
            output.Items.Count.ShouldBe(6);

            //road2 - tc2 => 2(T), 4(T)
            output = await _eventAppService
                .GetAllAsync(new PagedEventResultRequestDto { Keyword = "Etapa", MaxResultCount = 20, SkipCount = 0 });
            output.Items.ShouldNotBeEmpty();

            //road3 - tc3 => 2(T), 4(F), 5(T)
            output = await _eventAppService
                .GetAllAsync(new PagedEventResultRequestDto { Keyword = "CNK Juniori - Circuit Adancata", MaxResultCount = 20, SkipCount = 0 });
            output.Items.ShouldNotBeEmpty();

            //road4 - tc4 => 2(T), 4(F), 5(F), 6(T)
            output = await _eventAppService
                .GetAllAsync(new PagedEventResultRequestDto { Keyword = "2022-01-23", MaxResultCount = 20, SkipCount = 0 });
            output.Items.ShouldNotBeEmpty();

            //road5 - tc5 => 2(T), 4(F), 5(F), 6(F), 7
            output = await _eventAppService
                .GetAllAsync(new PagedEventResultRequestDto { Keyword = "2022-04-23", MaxResultCount = 20, SkipCount = 0 });
            output.Items.ShouldNotBeEmpty();
        }
    }
}