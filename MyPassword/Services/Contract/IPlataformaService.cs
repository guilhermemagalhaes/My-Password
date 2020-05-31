using MyPassword.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services.Contract
{
    public interface IPlataformaService
    {
        IList<Plataforma> GetPlataformas();

        void AdicionarPlataforma(Plataforma plataforma);
    }
}
