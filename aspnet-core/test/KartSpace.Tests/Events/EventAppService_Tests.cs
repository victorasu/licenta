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
        public async Task GetEvents_Test()
        {
            //Act
            var output = await _eventAppService.GetAllAsync(new PagedEventResultRequestDto{MaxResultCount = 20, SkipCount = 0}); //invalid
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
    }
}