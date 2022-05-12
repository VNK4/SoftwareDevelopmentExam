using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Region
    {
        [Key]
        public int ID { get; private set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        private Region()
        {
            
        }

        public Region(string name)
        {
            Name = name;
        }
    }
}
