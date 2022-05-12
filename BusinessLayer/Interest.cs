﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Interest
    {
        [Key]
        public int ID { get; private set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public IEnumerable<User> Users { get; set; }

        [ForeignKey("Region")]
        public int RegionID { get; set; }
        
        public Region Region { get; set; }

        private Interest()
        {

        }

        public Interest(string name, Region region)
        {
            Name = name;
            Region = region;
        }
    }
}
