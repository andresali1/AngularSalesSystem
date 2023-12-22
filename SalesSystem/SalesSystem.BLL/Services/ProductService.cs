using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get all products in DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductDTO>> List()
        {
            try
            {
                var productQuery = await _productRepository.Consult();
                var productList = productQuery.Include(cat => cat.Category).ToList();
                return _mapper.Map<List<ProductDTO>>(productList);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to create a product in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductDTO> Create(ProductDTO model)
        {
            try
            {
                var createdProduct = await _productRepository.Create(_mapper.Map<Product>(model));

                if(createdProduct.ProductId == 0)
                    throw new TaskCanceledException("Product couldn't be created");

                return _mapper.Map<ProductDTO>(createdProduct);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to update a product in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Edit(ProductDTO model)
        {
            try
            {
                var productModel = _mapper.Map<Product>(model);
                var foundProduct = await _productRepository.Get(p => p.ProductId == productModel.ProductId);

                if(foundProduct == null)
                    throw new TaskCanceledException("Product doesn't exist");

                foundProduct.ProductName = productModel.ProductName;
                foundProduct.CategoryId = productModel.CategoryId;
                foundProduct.Stock = productModel.Stock;
                foundProduct.Price = productModel.Price;
                foundProduct.IsActive = productModel.IsActive;

                bool response = await _productRepository.Edit(foundProduct);

                if (!response)
                    throw new TaskCanceledException("Product couldn't be updated");

                return response;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to delete a product from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            try
            {
                var foundProduct = await _productRepository.Get(p => p.ProductId == id);

                if (foundProduct == null)
                    throw new TaskCanceledException("Product doesn't exist");

                bool response = await _productRepository.Delete(foundProduct);

                if (!response)
                    throw new TaskCanceledException("Product couldn't be deleted");

                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
