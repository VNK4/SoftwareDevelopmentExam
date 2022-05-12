using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int ID { get; private set; }

        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [Required, Range(18, 81)]
        public int Age { get; set; }

        [Required, MaxLength(20)]
        public string Username { get; set; }

        [Required, MaxLength(20)]
        public string Email { get; set; }

        [Required, MaxLength(70)]
        public string Password { get; set; }

        public IEnumerable<User> Friends { get; set; }

        public IEnumerable<Interest> Interests { get; set; }
        

        private User()
        {
            
        }

        public User(string firstName, string lastName, int age, string username, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
