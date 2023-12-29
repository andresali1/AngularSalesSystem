using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model;
using System.Globalization;

namespace SalesSystem.BLL.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<SaleDetail> _saleDetailRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IGenericRepository<SaleDetail> saleDetailRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to register a new sale
        /// </summary>
        /// <returns></returns>
        public async Task<SaleDTO> Register(SaleDTO model)
        {
            try
            {
                var generatedSale = await _saleRepository.Register(_mapper.Map<Sale>(model));

                if (generatedSale.SaleId == 0)
                    throw new TaskCanceledException("Sale couldn't be created");

                return _mapper.Map<SaleDTO>(generatedSale);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to get sale history by params
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="saleNumber"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<SaleDTO>> History(string searchBy, string saleNumber, string beginDate, string endDate)
        {
            IQueryable<Sale> query = await _saleRepository.Consult();
            var resultList = new List<Sale>();

            try
            {
                if (searchBy == "date")
                {
                    DateTime begin_date = DateTime.ParseExact(beginDate, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime end_date = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-CO"));

                    resultList = await query.Where(s =>
                        s.RecordDate.Value.Date >= begin_date.Date &&
                        s.RecordDate.Value.Date <= end_date.Date
                    ).Include(s => s.SaleDetails)
                    .ThenInclude(p => p.Product)
                    .ToListAsync();
                }
                else
                {
                    resultList = await query.Where(s =>
                        s.DocumentNumber == saleNumber
                    ).Include(s => s.SaleDetails)
                    .ThenInclude(p => p.Product)
                    .ToListAsync();
                }
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<SaleDTO>>(resultList);
        }

        /// <summary>
        /// Method to get the sale report by date range
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<ReportDTO>> Report(string beginDate, string endDate)
        {
            IQueryable<SaleDetail> query = await _saleDetailRepository.Consult();
            var resultList = new List<SaleDetail>();

            try
            {
                DateTime begin_date = DateTime.ParseExact(beginDate, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime end_date = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-CO"));

                resultList = await query
                .Include(p => p.Product)
                .Include(s => s.Sale)
                .Where(sd =>
                    sd.Sale.RecordDate.Value.Date >= begin_date &&
                    sd.Sale.RecordDate.Value.Date <= end_date
                ).ToListAsync();
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReportDTO>>(resultList);
        }
    }
}
