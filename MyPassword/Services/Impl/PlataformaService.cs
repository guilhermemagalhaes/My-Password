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

        public void AdicionarPlataforma(Plataforma plataforma)
        {
            _plataformaRepository.Post(plataforma);
        }

        public IList<Plataforma> GetPlataformas()
        {
            return _plataformaRepository.Get();
        }
    }
}
