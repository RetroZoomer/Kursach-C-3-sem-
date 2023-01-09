using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachTP.Models
{
    public class User : Person
    {
        private int userID;
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
        }
        private string description;
        public static string Description
        {
            get
            {
                return Description;
            }
            set
            {
                Description = value;
            }
        }
        //private  photo;
        private Boolean sex;
        public static Boolean Sex
        {
            get
            {
                return Sex;
            }
            set
            {
                Sex = value;
            }
        }
        private string login;
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
    }
}
