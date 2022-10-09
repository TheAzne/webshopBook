using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category {get;}
        ICoverTypeRepository CoverType {get;}
        IProductRepository Product {get;}
        ICompanyRepository Company {get;}
        IApplicationUserRepository ApplicationUser {get;}
        IShoppingCartRepository ShoppingCart {get;}
        IOrderHeaderRepository OrderHeader {get;}
        IOrderDetailRepository OrderDetail {get;}



        void Save();

    }
}