﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewProject
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UP_0101Entities3 : DbContext
    {
        private static UP_0101Entities3 _context;

        public static UP_0101Entities3 GetContext()
        {
            if (_context == null) _context = new UP_0101Entities3();
            return _context;
        }
        public UP_0101Entities3()
            : base("name=UP_0101Entities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<AppStatus> AppStatus { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<DeffectType> DeffectType { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }
    }
}
