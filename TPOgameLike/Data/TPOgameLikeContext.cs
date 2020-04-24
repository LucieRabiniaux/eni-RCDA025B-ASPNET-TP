using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TPOgameLike_BO.Entities;

namespace TPOgameLike.Data
{
    public class TPOgameLikeContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TPOgameLikeContext() : base("name=TPOgameLikeContext")
        {
        }

        public System.Data.Entity.DbSet<TPOgameLike_BO.Entities.Planet> Planets { get; set; }

        public System.Data.Entity.DbSet<TPOgameLike_BO.Entities.SolarSystem> SolarSystems { get; set; }

        public System.Data.Entity.DbSet<TPOgameLike_BO.Entities.Resource> Resources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //gestion des relations entre entités


            //propriétés à ne pas persister en BDD
            //NB : privilégier les annotations à la fluent API -> annotation [NotMapped] sur les propriétés concernées
            //modelBuilder.Entity<Building>().Ignore(b => b.CellNb);

            base.OnModelCreating(modelBuilder);
        }

    }
}
