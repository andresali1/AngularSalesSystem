using AutoMapper;
using SalesSystem.DTO;
using SalesSystem.Model;
using System.Globalization;

namespace SalesSystem.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region UserRole
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region AppUser
            CreateMap<AppUser, AppUserDTO>()
                .ForMember(destination =>
                    destination.RoleDescription,
                    opt => opt.MapFrom(origin => origin.Role.RoleName)
                )
                .ForMember(destination =>
                    destination.IsActive,
                    opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );

            CreateMap<AppUser, SessionDTO>()
                .ForMember(destination =>
                    destination.RoleDescription,
                    opt => opt.MapFrom(origin => origin.Role.RoleName)
                );

            CreateMap<AppUserDTO, AppUser>()
                .ForMember(destination =>
                    destination.Role,
                    opt => opt.Ignore()
                )
                .ForMember(destination =>
                    destination.IsActive,
                    opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );
            #endregion

            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion

            #region Product
            CreateMap<Product, ProductDTO>()
                .ForMember(destination =>
                    destination.CategoryDescription,
                    opt => opt.MapFrom(origin => origin.Category.CategoryName)
                )
                .ForMember(destination =>
                    destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.IsActive,
                    opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );

            CreateMap<ProductDTO, Product>()
                .ForMember(destination =>
                    destination.Category,
                    opt => opt.Ignore()
                )
                .ForMember(destination =>
                    destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.IsActive,
                    opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );
            #endregion

            #region Sale
            CreateMap<Sale, SaleDTO>()
                .ForMember(destination =>
                    destination.TotalText,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.RecordDate,
                    opt => opt.MapFrom(origin => origin.RecordDate.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<SaleDTO, Sale>()
                .ForMember(destination =>
                    destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TotalText, new CultureInfo("es-CO")))
                );
            #endregion

            #region SaleDetail
            CreateMap<SaleDetail, SaleDetailDTO>()
                .ForMember(destination =>
                    destination.ProductDescription,
                    opt => opt.MapFrom(origin => origin.Product.ProductName)
                )
                .ForMember(destination =>
                    destination.PriceText,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.TotalText,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO")))
                );

            CreateMap<SaleDetailDTO, SaleDetail>()
                .ForMember(destination =>
                    destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.PriceText, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TotalText, new CultureInfo("es-CO")))
                );
            #endregion

            #region Report
            CreateMap<SaleDetail, ReportDTO>()
                .ForMember(destination =>
                    destination.RecordDate,
                    opt => opt.MapFrom(origin => origin.Sale.RecordDate.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destination =>
                    destination.DocumentNumber,
                    opt => opt.MapFrom(origin => origin.Sale.DocumentNumber)
                )
                .ForMember(destination =>
                    destination.PaymentType,
                    opt => opt.MapFrom(origin => origin.Sale.PaymentType)
                )
                .ForMember(destination =>
                    destination.SaleTotal,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Sale.Total.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.Product,
                    opt => opt.MapFrom(origin => origin.Product.ProductName)
                )
                .ForMember(destination =>
                    destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO")))
                );
            #endregion
        }
    }
}
