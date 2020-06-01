using MyPassword.Entity;
using MyPassword.Repository.Contract;
using MyPassword.Repository.Impl;
using MyPassword.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services.Impl
{
    public class PlataformaService : IPlataformaService
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public PlataformaService(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;
        }

        public int InsertOrUpdate(Plataforma plataforma)
        {
            return _plataformaRepository.InsertOrUpdate(plataforma);
        }

        public Plataforma GetById(int PlataformaId)
        {
            return _plataformaRepository.GetById(PlataformaId);
        }

        public IList<Plataforma> GetAll()
        {
            return _plataformaRepository.GetAll();
        }

        public void Delete(int PlataformaId)
        {
            _plataformaRepository.Delete(PlataformaId);
        }
    }
}
