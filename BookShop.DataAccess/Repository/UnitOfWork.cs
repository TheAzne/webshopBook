using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.DataAccess.Repository.IRepository;

namespace BookShop.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);


        }
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; set; }

        public IShoppingCartRepository ShoppingCart { get; set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}