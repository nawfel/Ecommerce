using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(string sort)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderby(x=>x.Name);
            

            if(!string.IsNullOrWhiteSpace(sort)){
                switch(sort){
                    case "priceAsc":
                    AddOrderby(p=>p.Price);
                    break;
                    case "priceDesc":
                    AddOrderbyDesc(p=>p.Price);
                    break;
                    default :
                    AddOrderby(n=>n.Name);
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