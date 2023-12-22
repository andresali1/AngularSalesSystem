using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SalesSystem.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly DbsalesContext _dbContext;

        public GenericRepository(DbsalesContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method to get on the records of a model of given filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel model = await _dbContext.Set<TModel>().FirstOrDefaultAsync(filter);
                return model;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to create a record of a model in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to edit a record of a model in BD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Edit(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to delete a record of a model in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to get an IQueryable of a model given one filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IQueryable<TModel>> Consult(Expression<Func<TModel, bool>> filter = null)
        {
            try
            {
                IQueryable<TModel> queryModel = filter == null ? _dbContext.Set<TModel>() : _dbContext.Set<TModel>().Where(filter);
                return queryModel;
            }
            catch
            {
                throw;
            }
        }
    }
}
