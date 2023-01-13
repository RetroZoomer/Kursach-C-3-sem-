using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KursachTP.Models
{
    public class User : Person
    {
        /*private int userID;
        public static int UserID
        {
            get
            {
                return UserID;
            }
            set
            {
                UserID = value;
            }
        }*/

        /*private string userdescription;
        public static string UserDescription
        {
            get
            {
                return UserDescription;
            }
            set
            {
                UserDescription = value;
            }
        }*/
        //private  photo;
        private Boolean sex;
        /*public static String Pol
        {
            get
            {
                return Pol;
            }
            set
            {
                Pol = value;
            }
        }*/
        /*private string login;
        public static string Login
        {
            get
            {
                return Login;
            }
            set
            {
                Login = value;
            }
        }
        private string password;
        public static string Password
        {
            get
            {
                return Password;
            }
            set
            {
                Password = value;
            }
        }
        private string phone;
        public static string Phone
        {
            get
            {
                return Phone;
            }
            set
            {
                Phone = value;
            }
        }*/

        public User(/*string f0,*/ string f1, string f2, string f3, string f4, string f5, string f6, string f7, string f8)
        {
            //UserId = Convert.ToInt32(f0);
            Name = f1;
            LastName = f2;
            UserDescription = f3;
            Birthday = Convert.ToDateTime(f4);
            Pol = Convert.ToBoolean(f5);
            Login = f6;
            Password = f7;
            Phone = f8;
        }
        public User()
        {

        }
        [Required]
        public int? UserID { get; set; }
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

        //[Range(1, 150, ErrorMessage = "Incorrect age")]
        //public int Age { get; set; }
    }
    public static class Users
    {
        public static List<User> users = new List<User>();
    }
}

