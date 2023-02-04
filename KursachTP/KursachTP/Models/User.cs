using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KursachTP.Models
{
    public class User : Person
    {
        public User(string f0, string f1, string f2, string f3, string f4, string f5, string f6, string f7, string f8, string f9)
        {
            UserID = Convert.ToInt32(f0);
            Name = f1;
            LastName = f2;
            UserDescription = f3;
            Birthday = Convert.ToDateTime(f4);
            Pol = Convert.ToBoolean(f5);
            Login = f6;
            Password = f7;
            Phone = f8;
            Rol = f9;
        }
        public User()
        {

        }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserDescription { get; set; }
        [Required]
        public DateTime? Birthday { get; set; }
        [Required]
        public bool? Pol { get; set; }
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Rol { get; set; }
        //[Range(1, 150, ErrorMessage = "Incorrect age")]
        //public int Age { get; set; }
    }
    public static class Users
    {
        public static List<User> users = new List<User>();
    }
}

