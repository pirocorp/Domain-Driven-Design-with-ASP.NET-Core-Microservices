﻿namespace CarRentalSystem.Startup.Specs
{
    using System.Linq;
    using Application.Features.CarAds.Queries.Search;
    using Domain.Models.CarAds;
    using FakeItEasy;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using Web.Features;
    using Xunit;

    public class CarAdsControllerSpecs
    {
        [Fact]
        public void GetShouldHaveCorrectAttributes()
            => MyController<CarAdsController>
                .Calling(c => c.Get(With.Default<SearchCarAdsQuery>()))

                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Get));

        [Theory]
        [InlineData(2)]
        public void GetShouldReturnAllCarAdsWithoutAQuery(int totalCarAds)
            => MyPipeline
                .Configuration()
                .ShouldMap("/CarAds")

                .To<CarAdsController>(c => c.Get(With.Empty<SearchCarAdsQuery>()))
                .Which(instance => instance
                    .WithData(A.CollectionOfDummy<CarAd>(totalCarAds)))

                .ShouldReturn()
                .ActionResult<SearchCarAdsOutputModel>(result => result
                    .Passing(model => model
                        .CarAds.Count().Should().Be(totalCarAds)));

        [Fact]
        public void GetShouldReturnAvailableCarAdsWithoutAQuery()
            => MyPipeline
                .Configuration()
                .ShouldMap("/CarAds")

                .To<CarAdsController>(c => c.Get(With.Empty<SearchCarAdsQuery>()))
                .Which(instance => instance
                    .WithData(CarAdFakes.Data.GetCarAds()))

                .ShouldReturn()
                .ActionResult<SearchCarAdsOutputModel>(result => result
                    .Passing(model => model
                        .CarAds.Count().Should().Be(10)));

        [Theory]
        [InlineData("Manufacturer10")]
        public void GetShouldReturnFilteredCarAdsWithQuery(string manufacturer)
            => MyPipeline
                .Configuration()
                .ShouldMap($"/CarAds?{nameof(manufacturer)}={manufacturer}")

                .To<CarAdsController>(c => c.Get(new SearchCarAdsQuery { Manufacturer = manufacturer }))
                .Which(instance => instance
                    .WithData(CarAdFakes.Data.GetCarAds()))

                .ShouldReturn()
                .ActionResult<SearchCarAdsOutputModel>(result => result
                    .Passing(model =>
                    {
                        model.CarAds.Count().Should().Be(1);
                        model.CarAds.First().Manufacturer.Should().Be(manufacturer);
                    }));
    }
}
