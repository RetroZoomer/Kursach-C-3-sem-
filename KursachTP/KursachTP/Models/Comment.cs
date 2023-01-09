using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachTP.Models
{
    public class Comment
    {
        private int userID;
        public static int UserID
        {
            get
            {
                return User.UserID;
            }
            set
            {
                User.UserID = value;
            }
        }
        private string text;
        public static string Text
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }
        public static void EddingComment()
        {
            //Console.WriteLine("Работает2");
        }
        public static void DeleteComment()
        {
            //Console.WriteLine("Работает23");
        }
    }
}
