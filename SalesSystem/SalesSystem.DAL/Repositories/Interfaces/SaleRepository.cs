using SalesSystem.DAL.DBContext;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.Model;

namespace SalesSystem.DAL.Repositories.Interfaces
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly DbsalesContext _dbContext;

        public SaleRepository(DbsalesContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method to Create a new SaleDetail and its operations in BD
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Sale> Register(Sale model)
        {
            Sale generatedSale = new Sale();

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                foreach (SaleDetail sd in model.SaleDetails)
                {
                    Product foundProduct = _dbContext.Products.Where(p => p.ProductId == sd.ProductId).First();
                    foundProduct.Stock = foundProduct.Stock - sd.Amount;
                    _dbContext.Products.Update(foundProduct);
                }
                await _dbContext.SaveChangesAsync();

                DocumentNumber correlative = _dbContext.DocumentNumbers.First();
                correlative.LastNumber = correlative.LastNumber + 1;
                correlative.RecordDate = DateTime.Now;
                _dbContext.DocumentNumbers.Update(correlative);
                await _dbContext.SaveChangesAsync();

                int digitsAmount = 4;
                string zeros = string.Concat(Enumerable.Repeat("0", digitsAmount));
                string saleNumber = zeros + correlative.LastNumber.ToString();
                saleNumber = saleNumber.Substring(saleNumber.Length - digitsAmount, digitsAmount);
                model.DocumentNumber = saleNumber;
                await _dbContext.Sales.AddAsync(model);
                await _dbContext.SaveChangesAsync();

                generatedSale = model;

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            return generatedSale;
        }
    }
}
