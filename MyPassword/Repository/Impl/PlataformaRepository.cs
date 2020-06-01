using MyPassword.Entity;
using MyPassword.Repository.Contract;
using MyPassword.Repository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public void Delete(int PlataformaId)
        {
            var plataforma = _context.Plataformas.Find(PlataformaId);
            _context.Plataformas.Remove(plataforma);
            _context.SaveChanges();
        }

        public IList<Plataforma> GetAll()
        {
            return _context.Plataformas.ToList();
        }

        public Plataforma GetById(int PlataformaId)
        {
            return _context.Plataformas.Find(PlataformaId);
        }

        public int InsertOrUpdate(Plataforma plataforma)
        {
            var _plataforma = _context.Plataformas.InsertOrUpdateOnSubmit(x => x.PlataformaId == plataforma.PlataformaId);
            _plataforma.Nome = plataforma.Nome;
            _plataforma.URL = plataforma.URL;
            _plataforma.DataCadastro = _plataforma.PlataformaId == 0 ? DateTime.Now : _plataforma.DataCadastro;
            _context.SaveChanges();
            return plataforma.PlataformaId;
        }
    }
}
