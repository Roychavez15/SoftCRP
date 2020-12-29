using SoftCRP.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Repositories
{
    public class IturanRepository : IIturanRepository
    {
        private readonly DataContext _context;

        public IturanRepository(
            DataContext context)
        {
            _context = context;
        }

    }
}
