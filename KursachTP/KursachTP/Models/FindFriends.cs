using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KursachTP.Models;

namespace KursachTP.Models
{
    public class FindFriends
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
        }
        public static void SkipPerson()
        {
            //Console.WriteLine("Работает2");
        }
        public static void AddFriend()
        {
            //Console.WriteLine("Работает2");
        }
    }
}
