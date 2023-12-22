using AutoMapper;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model;
using System.Globalization;

namespace SalesSystem.BLL.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public DashBoardService(ISaleRepository saleRepository, IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get all the sales by given params
        /// </summary>
        /// <param name="saleTable"></param>
        /// <param name="daysAmountMinus"></param>
        /// <returns></returns>
        private IQueryable<Sale> GetSales(IQueryable<Sale> saleTable, int daysAmountMinus)
        {
            DateTime? lastDate = saleTable.OrderByDescending(s => s.RecordDate).Select(s => s.RecordDate).First();
            lastDate = lastDate.Value.AddDays(daysAmountMinus);

            return saleTable.Where(s => s.RecordDate.Value.Date >= lastDate.Value.Date);
        }

        /// <summary>
        /// Method to get the number of sale of the last week
        /// </summary>
        /// <returns></returns>
        private async Task<int> LastWeekSalesTotal()
        {
            int total = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.Consult();

            if(_saleQuery.Count() > 0)
            {
                var saleTable = GetSales(_saleQuery, -7);
                total = saleTable.Count();
            }

            return total;
        }

        /// <summary>
        /// Method to get the the las week income for sales
        /// </summary>
        /// <returns></returns>
        private async Task<string> LastWeekIncomeTotal()
        {
            decimal result = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.Consult();

            if (_saleQuery.Count() > 0)
            {
                var saleTable = GetSales(_saleQuery, -7);
                result = saleTable.Select(s => s.Total).Sum(s => s.Value);
            }

            return Convert.ToString(result, new CultureInfo("es-CO"));
        }

        /// <summary>
        /// Method to get the total of products
        /// </summary>
        /// <returns></returns>
        private async Task<int> ProductTotal()
        {
            IQueryable<Product> _productQuery = await _productRepository.Consult();
            int total = _productQuery.Count();
            return total;
        }

        /// <summary>
        /// Method to get the last week sales
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<string, int>> LastWeekSales()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            IQueryable<Sale> _saleQuery = await _saleRepository.Consult();

            if(_saleQuery.Count() > 0)
            {
                var saleTable = GetSales(_saleQuery, -7);
                result = saleTable
                    .GroupBy(s => s.RecordDate.Value.Date).OrderBy(g => g.Key)
                    .Select(sd => new { date = sd.Key.ToString("dd/MM/yyyy"), total = sd.Count() })
                    .ToDictionary(keySelector : r => r.date, elementSelector: r => r.total);
            }

            return result;
        }

        /// <summary>
        /// Method to get the sumary information for the dashboard page
        /// </summary>
        /// <returns></returns>
        public async Task<DashBoardDTO> Summary()
        {
            DashBoardDTO vmDashBoard = new DashBoardDTO();

            try
            {
                vmDashBoard.SalesTotal = await LastWeekSalesTotal();
                vmDashBoard.IncomeTotal = await LastWeekIncomeTotal();
                vmDashBoard.ProductTotal = await ProductTotal();

                List<WeekSaleDTO> weekSaleList = new List<WeekSaleDTO>();

                foreach(KeyValuePair<string, int> item in await LastWeekSales())
                {
                    weekSaleList.Add(new WeekSaleDTO()
                    {
                        Date = item.Key,
                        Total = item.Value
                    });
                }

                vmDashBoard.LastWeekSales = weekSaleList;
            }
            catch
            {
                throw;
            }

            return vmDashBoard;
        }
    }
}
