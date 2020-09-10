﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Core.Models;

namespace WebApplication1.Presistence
{
    public class VegaDbContext : DbContext
    {
        
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }     
        public DbSet<Photo> Photos { get; set; }     
        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => new {vf.VehicleId , vf.FeatureId});
        }
    }
}
