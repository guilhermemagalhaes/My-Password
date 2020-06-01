using MyPassword.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services.Contract
{
    public interface IPlataformaService
    {
        IList<Plataforma> GetAll();

        Plataforma GetById(int PlataformaId);

        int InsertOrUpdate(Plataforma plataforma);

        void Delete(int PlataformaId);
    }
}
