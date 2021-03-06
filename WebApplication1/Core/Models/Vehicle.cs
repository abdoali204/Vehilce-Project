using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Core.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id {get;set;}
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        [StringLength(255)]
        public string ConcatName {get;set;}

        [StringLength(255)]
        public string ConcatEmail {get;set;}
        [Required]
        [StringLength(255)]
        public string ConcatPhone {get;set;}
        public DateTime LastUpdate {get;set;}


        public ICollection<VehicleFeature> Features { get; set; }

        public ICollection<Photo> Photos{get;set;}

        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }
    }

 
}