using Microsoft.EntityFrameworkCore;
using SmartManager.Domain.Entities;
using SmartManager.Entities;
using SmartManager.Infra.Context;
using SmartManager.Infra.Interfaces;

namespace SmartManager.Infra.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository {

        private readonly SmartManagerContext _context;

        public ProductRepository(SmartManagerContext context) : base(context)
        {
            _context = context;
        }


    }
}