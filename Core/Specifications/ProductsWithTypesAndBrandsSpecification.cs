using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams param)
        : base(x =>
        (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search)) &&
         (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId) &&
         (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId)
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderby(x => x.Name);
            ApplyPaging(param.PageSize * (param.PageIndex - 1), param.PageSize);

            if (!string.IsNullOrWhiteSpace(param.Sort))
            {
                switch (param.Sort)
                {
                    case "priceAsc":
                        AddOrderby(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderbyDesc(p => p.Price);
                        break;
                    default:
                        AddOrderby(n => n.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}