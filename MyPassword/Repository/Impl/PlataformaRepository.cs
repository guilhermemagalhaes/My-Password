using MyPassword.Entity;
using MyPassword.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPassword.Repository.Impl
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly MyPasswordContext _context;
        public PlataformaRepository(MyPasswordContext context)
        {
            _context = context;
        }

        public IList<Plataforma> Get()
        {
            return _context.Plataformas.ToList();
        }

        public void Post(Plataforma plataforma)
        {
            _context.Add(plataforma);
            _context.SaveChanges();
        }
    }
}
