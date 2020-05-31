using MyPassword.Entity;
using MyPassword.Repository.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPassword.Repository.Impl
{
    public class SenhasRepository : ISenhasRepository
    {
        private readonly MyPasswordContext _context;
        public SenhasRepository(MyPasswordContext context)
        {
            _context = context;
        }

        public IList<Senha> Get()
        {
            return _context.Senhas.ToList();
        }

        public void Post(Senha senha)
        {
            _context.Add(senha);
            _context.SaveChanges();
        }
    }
}
