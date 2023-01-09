using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachTP.Models
{
    public class Person
    {
        private string name;
        public static string Name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
        private string lastname;
        public static string LastName
        {
            get
            {
                return LastName;
            }
            set
            {
                LastName = value;
            }
        }
        private string phoneNumber;
        public static string PhoneNumber
        {
            get
            {
                return PhoneNumber;
            }
            set
            {
                PhoneNumber = value;
            }
        }
        private string email;
        public static string Email
        {
            get
            {
                return Email;
            }
            set
            {
                Email = value;
            }
        }
        private string birthday;
        public static DateTime Birthday
        {
            get
            {
                return Birthday;
            }
            set
            {
                Birthday = value;
            }
        }
        public static void Registration()
        {
            //Console.WriteLine("Работает");
        }
    }
}
