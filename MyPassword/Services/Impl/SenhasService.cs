using MyPassword.Entity;
using MyPassword.Repository.Contract;
using MyPassword.Repository.Impl;
using MyPassword.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services.Impl
{
    public class SenhasService : ISenhasService
    {
        private readonly ISenhasRepository _senhasRepository;

        public SenhasService(ISenhasRepository senhasRepository)
        {
            _senhasRepository = senhasRepository;
        }

        public int InsertOrUpdate(Senha senha)
        {
            return _senhasRepository.InsertOrUpdate(senha);
        }

        public IList<Senha> GetAll()
        {
            return _senhasRepository.GetAll();
        }

        public Senha GetById(int SenhaId)
        {
            return _senhasRepository.GetById(SenhaId);
        }

        public void Delete(int SenhaId)
        {
            _senhasRepository.Delete(SenhaId);
        }
    }
}
