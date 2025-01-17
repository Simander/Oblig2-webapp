﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Kunder
    {
        [Key]
        public int ID { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Telefonnr { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Epost { get; set; }
        public Byte[] Password { get; set; }
        public virtual Poststeder Poststeder { get; set; }
    }
    public class Poststeder { 
        public string Postnr { get; set; }
        public string Poststed { get; set; }
        public virtual List<Kunder> Kunder { get; set; }
    }

    public class AdminBrukere
    {
        [Key]
        public int ID { get; set; }

        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Telefonnr { get; set; }
        public string Epost { get; set; }
        public Byte[] Password { get; set; }
        public bool Superadmin { get; set; }
    }

    public class Varer
    {
        [Key]
        public int ID { get; set; }
        public string Varenavn { get; set; }
        public string Pris { get; set; }
        public string Kvantitet { get; set; }
        public string Beskrivelse { get; set; }
        public int ProdusentId { get; set; }
        public virtual Produsenter Produsenter { get; set; }
        public int KategoriId { get; set; }
        public virtual Kategorier Kategorier { get; set; }
    }
    public class Kategorier
    {
        [Key]
        public int ID { get; set; }
        public string Navn { get; set; }
        public virtual List<Varer> Varer { get; set; }
    }
    public class Produsenter
    {
        [Key]
        public int ID { get; set; }
        public string Navn { get; set; }
        public virtual List<Varer> Varer { get; set; }
    }

    public class Bestillinger
    {
        [Key]
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal PrisTotal { get; set; }
        public virtual List<Ordrelinjer> Ordrelinjer { get; set; }
        public int KundeId { get; set; }        //referanse til kundens id
        public Kunder Kunder { get; set; }
    }

    public class Ordrelinjer
    {
        [Key]
        public int ID { get; set; }
        public int ProduktId { get; set; }  //referanse til varens id
        public Varer Vare { get; set; }
        public int Kvantitet { get; set; }
        public int Bestillingsnr { get; set; }  //referans til bestillingens Id
        public Bestillinger Bestilling { get; set; }
    }
    
    public class DBContext : DbContext
    {
        public DBContext()
            : base("name=WebStoreDb")
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Kunder> Kunder { get; set; }
        public DbSet<Poststeder> Poststeder { get; set; }

        public DbSet<Varer> Varer { get; set; }
        public DbSet<Produsenter> Produsenter { get; set; }
        public DbSet<Kategorier> Kategorier { get; set; }

        public DbSet<AdminBrukere> AdminBrukere { get; set; }

        public DbSet<Bestillinger> Bestillinger { get; set; }
        public DbSet<Ordrelinjer> Ordrelinjer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poststeder>()
                .HasKey(p => p.Postnr);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
