﻿using Microsoft.EntityFrameworkCore;
using PublisherJobInfra.Infra.Entities;

namespace PublisherJobInfra.Infra.Data
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=MARINA\SQLEXPRESS;Initial Catalog=Alunos;Integrated Security=True;TrustServerCertificate=True");
        }
        public DbSet<UserEntity> User { get; set; }
    }
}
