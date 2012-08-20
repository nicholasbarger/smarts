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
        public SmartsDbContext() : base("SmartsDbContext") { }

        public DbSet<Asset> Assets { get; set; }
    }
}