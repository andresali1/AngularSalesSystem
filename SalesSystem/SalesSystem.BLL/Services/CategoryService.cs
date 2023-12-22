using AutoMapper;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get all the categories
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryDTO>> List()
        {
            try
            {
                var categoryList = await _categoryRepository.Consult();
                return _mapper.Map<List<CategoryDTO>>(categoryList.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
