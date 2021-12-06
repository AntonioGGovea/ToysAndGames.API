using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToysAndGames.API;
using ToysAndGames.Tests.UnitTests.Fixtures;
using Xunit;

namespace ToysAndGames.Tests.IntegrationTests
{
    [Collection("Product")]
    public class ProductsIntegrationTest
    {
        private readonly ProductFixture _productFixture;
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductsIntegrationTest(ProductFixture productFixture)
        {
            _productFixture = productFixture;
            _factory = new WebApplicationFactory<Startup>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("/1")]
        [InlineData("/2")]
        public async Task GetProducts_ReturnsAllProducts(string routeParams)
        {
            // arrange
            var client = _factory.CreateDefaultClient();

            // act
            var response = await client.GetAsync($"/api/products{routeParams}");

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateProduct_GivenInvalidName_ReturnsBadRequest()
        {
            // arrange
            var client = _factory.CreateDefaultClient();
            var product = _productFixture.CreateProductDto;
            product.Name = null;
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            // act
            var response = await client.PostAsync("/api/products", content);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData(1001)]
        [InlineData(0)]
        public async Task UpdateProduct_GivenOutOfRangePrice_ReturnsBadRequest(int price)
        {
            // arrange
            var client = _factory.CreateDefaultClient();
            var product = _productFixture.ProductDTO;
            product.Price = price;
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            // act
            var response = await client.PatchAsync($"api/products/{product.Id}", content);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
