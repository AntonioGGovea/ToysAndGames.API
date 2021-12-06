using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ToysAndGames.Services.Contracts;
using AutoMapper;
using ToysAndGames.API.Controllers;
using ToysAndGames.Tests.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using ToysAndGames.Data.Models;

namespace ToysAndGames.Tests.UnitTests
{
    [Collection("Product")]
    public class ProductsTest
    {
        private readonly Mock<IProductsRepository> _productRepo;
        private readonly Mock<IMapper> _mapper;
        private readonly ProductsController _controller;
        private readonly ProductFixture _prodFixture;

        public ProductsTest(ProductFixture prodFixture)
        {
            _productRepo = new Mock<IProductsRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new ProductsController(_productRepo.Object, _mapper.Object);
            _prodFixture = prodFixture;
        }

        ////////////// GET ////////////
        [Fact]
        [Trait("Category", "Get")]
        public void GetProducts_ReturnsProductsList()
        {
            // arrange
            var productList = _prodFixture.products;
            _productRepo.Setup(x => x.GetProducts(null, null)).Returns(productList);

            // act
            var response = _controller.GetProducts() as ObjectResult;

            // assert
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(productList, response.Value);
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetProducts_RepositoryException_ReturnsStatusCode500()
        {
            // arrange
            var exceptionMsg = "db error";
            _productRepo.Setup(x => x.GetProducts(null, null)).Throws(new Exception(exceptionMsg));

            // act
            var response = _controller.GetProducts() as ObjectResult;

            // assert
            Assert.Equal(500, response.StatusCode);
            Assert.Equal(exceptionMsg, response.Value);
        }

        ////////////// CREATE ////////////
        [Fact]
        [Trait("Category", "Create")]
        public void CreateProduct_CreatesNewProd_WithValidData()
        {
            // arrange
            var product = _prodFixture.product1;
            var productDto = _prodFixture.CreateProductDto;
            _productRepo.Setup(x => x.CreateProduct(product)).Returns(true);
            _mapper.Setup(x => x.Map<Product>(productDto)).Returns(product);

            // act
            var response = _controller.CreateProduct(productDto) as ObjectResult;

            //assert
            Assert.Equal(201, response.StatusCode);
            Assert.Equal(product, response.Value);
        }

        [Fact]
        [Trait("Category", "Create")]
        public void CreateProduct_GivenInvalidName_ReturnsCode500()
        {
            // arrange
            var product = _prodFixture.product1;
            var productDto = _prodFixture.CreateProductDto;
            productDto.Name = null;
            product.Name = null;

            _productRepo.Setup(x => x.CreateProduct(product)).Returns(false);
            _mapper.Setup(x => x.Map<Product>(productDto)).Returns(product);

            // act
            var response = _controller.CreateProduct(productDto) as ObjectResult;

            // assert
            Assert.Equal(500, response.StatusCode);
        }

        ////////////// UPDATE ////////////
        [Fact]
        [Trait("Category", "Update")]
        public void UpdateProduct_UpdatesAProd_WithValidData()
        {
            // arrange
            var product = _prodFixture.product1;
            var productDto = _prodFixture.ProductDTO;
            _productRepo.Setup(x => x.UpdateProduct(product)).Returns(true);
            _mapper.Setup(x => x.Map<Product>(productDto)).Returns(product);

            // act
            var response = _controller.UpdateProduct(productDto.Id ,productDto) as ObjectResult;

            // assert
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(product, response.Value);
        }

        [Fact]
        [Trait("Category", "Update")]
        public void UpdateProduct_GivenOutOfRangeAge_ReturnsError()
        {
            // arrange
            int age = 101;
            var product = _prodFixture.product1;
            var productDto = _prodFixture.ProductDTO;
            product.AgeRestriction = age;
            productDto.AgeRestriction = 101;

            _productRepo.Setup(x => x.UpdateProduct(product)).Returns(false);
            _mapper.Setup(x => x.Map<Product>(productDto)).Returns(product);

            // act
            var response = _controller.UpdateProduct(productDto.Id, productDto) as ObjectResult;

            // assert
            Assert.Equal(500, response.StatusCode);
        }

        ////////////// DELETE ////////////
        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteProduct_GivenValidProduct_RemovesIt()
        {
            // arrange
            var product = _prodFixture.product1;
            var productDto = _prodFixture.ProductDTO;
            _productRepo.Setup(x => x.DeleteProduct(product)).Returns(true);
            _mapper.Setup(x => x.Map<Product>(productDto)).Returns(product);

            // act
            var result = _controller.DeleteProduct(productDto.Id, productDto) as StatusCodeResult;

            // assert
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteProduct_GivenInvaildId_ReturnsBadRequest()
        {
            // arrange
            int id = 0;
            string ErrorMessage = "Id from route doesn't match the body object's Id";
            var productDto = _prodFixture.ProductDTO;

            // act
            var response = _controller.DeleteProduct(id, productDto) as ObjectResult;

            // assert
            Assert.Equal(400, response.StatusCode);
            Assert.Equal(ErrorMessage, response.Value);
        }
    }
}
