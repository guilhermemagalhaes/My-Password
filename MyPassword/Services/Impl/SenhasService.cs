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
        private readonly IPlataformaService _plataformaService;

        public SenhasService(ISenhasRepository senhasRepository, IPlataformaService plataformaService)
        {
            _senhasRepository = senhasRepository;
            _plataformaService = plataformaService;
        }

        public int InsertOrUpdate(PlataformaSenha plataformaSenha)
        {
            if(plataformaSenha.Plataforma.Nome != null)
                plataformaSenha.Senha.PlataformaId = _plataformaService.InsertOrUpdate(plataformaSenha.Plataforma);

            return _senhasRepository.InsertOrUpdate(plataformaSenha.Senha);
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
