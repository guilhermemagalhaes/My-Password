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

        public void Delete(int SenhaId)
        {
            var senha = _context.Senhas.Find(SenhaId);
            _context.Senhas.Remove(senha);
            _context.SaveChanges();
        }

        public IList<Senha> GetAll()
        {
            return _context.Senhas.ToList();
        }

        public Senha GetById(int SenhaId)
        {
            return _context.Senhas.Find(SenhaId);
        }

        public int InsertOrUpdate(Senha senha)
        {
            var _senha = _context.Senhas.FirstOrDefault(x => x.SenhaId == senha.SenhaId);

            if (_senha == null)
                _senha = new Senha();

            _senha.PlataformaId = senha.PlataformaId;
            _senha.Usuario = senha.Usuario;
            _senha.SenhaDesc = senha.SenhaDesc;
            _senha.DataCadastro = _senha.SenhaId == 0 ? DateTime.Now : _senha.DataCadastro;

            _context.Add(_senha);
            _context.SaveChanges();

            return _senha.SenhaId;
        }
    }
}
