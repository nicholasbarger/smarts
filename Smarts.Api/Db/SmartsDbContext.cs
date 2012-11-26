using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Smarts.Api.Models;

namespace Smarts.Api.Db
{
    /// <summary>
    /// The EF db context for the smarts database.
    /// </summary>
    internal class SmartsDbContext : DbContext
    {
        public SmartsDbContext() : base("SmartsDbContext") 
        {
            base.Configuration.ProxyCreationEnabled = false;
            base.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityEvent> ActivityEvents { get; set; }
        public DbSet<Resource> Assets { get; set; }
        public DbSet<ResourceType> AssetTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Plan> Curriculums { get; set; }
        public DbSet<Topic> Subjects { get; set; }
        public DbSet<WebUser> WebUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Asset>()
            //    .HasMany(a => a.SubjectAssociations)
            //    .WithMany(a => a.Asset)
            //    .Map(a =>
            //    {
            //        a.ToTable("MapAssetToSubject");
            //        a.MapLeftKey("AssetId");
            //        a.MapRightKey("Hashtag");
            //    });
        }
    }
}