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

        public void AdicionarSenha(Senha senha)
        {
            _senhasRepository.Post(senha);
        }

        public IList<Senha> GetSenhas()
        {
            return _senhasRepository.Get();
        }
    }
}
