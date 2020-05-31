using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Entity
{
    public class MyPasswordContext  : DbContext
    {
        public MyPasswordContext(DbContextOptions<MyPasswordContext> options): base(options)
        {

        }

        public DbSet<Plataforma> Plataformas { get; set; }
        public DbSet<Senha> Senhas { get; set; }
    }
}
