using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using KursachTP.Models;

namespace KursachTP.DAO
{
    public class WorkDAO : ConnectDAO
    {
        public List<User> Record()
        {
            Connect();
            string sql = "SELECT id_user,name,lastname,userdescription,birthday,pol,login," +
                "password,phone FROM User";
            MySqlCommand command = new MySqlCommand(sql, connection);

            MySqlDataReader reader = command.ExecuteReader();

            Users.users.Clear();

            while (reader.Read())
            {
                Users.users.Add(new User(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7), 
                    reader.GetString(8)));
            }

            Disconnect();
            return Users.users;
        }
        [HttpGet]
        public void GetPerson(User user)
        {
            Connect();
            string sql = "INSERT INTO USER(name,lastname,userdescription,birthday,pol,login," +
                "password,phone) VALUES (@user.name, @user.lastname,  @user.userdescription, @user.birthday, " +
                "@user.pol, @user.login, @user.password, @user.phone)";
            MySqlCommand comanda = new MySqlCommand(sql, connection);

            comanda.Parameters.AddWithValue("user.name", user.Name);
            comanda.Parameters.AddWithValue("user.lastname", user.LastName);
            comanda.Parameters.AddWithValue("user.userdescription", user.UserDescription);
            comanda.Parameters.AddWithValue("user.birthday", user.Birthday);
            comanda.Parameters.AddWithValue("user.pol", user.Pol);
            comanda.Parameters.AddWithValue("user.login", user.Login);
            comanda.Parameters.AddWithValue("user.password", user.Password);
            comanda.Parameters.AddWithValue("user.phone", user.Phone);

            comanda.ExecuteNonQuery();
            Disconnect();
        }
        public void DeleteById(int id)
        {
            Connect();
            string sql = "DELETE FROM User WHERE id_user = @id;";


            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();

            Disconnect();
        }
        public User UserInfo(int userid) 
        {
            Connect();

            string sql = "SELECT * FROM User WHERE id_user like (@userid);";
            User person = null;

            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("userid", userid);

            command.ExecuteNonQuery();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                person = new User(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7),
                    reader.GetString(8));
            }

            Disconnect();
            return person;
        }
        public User UsersInfoIndex(int userid)
        {
            Connect();

            string sql = "SELECT * FROM User WHERE id_user > (@userid);";
            User person = null;

            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("userid", userid);

            command.ExecuteNonQuery();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                person = new User(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7),
                    reader.GetString(8));
            }

            Disconnect();
            return person;
        }
        public void UpZn(User user)
        {
            Connect();
            string sql = "UPDATE USER SET id_user = @id_user, name = @name, lastname = @lastname, userdescription = @userdescription," +
                " birthday = @birthday, pol = @pol, login = @login, password = @password, phone=@phone WHERE id_user = @id_user ;";

            MySqlCommand comanda = new MySqlCommand(sql, connection);

            comanda.Parameters.AddWithValue("id_user", user.UserID);
            comanda.Parameters.AddWithValue("name", user.Name);
            comanda.Parameters.AddWithValue("lastname", user.LastName);
            comanda.Parameters.AddWithValue("userdescription", user.UserDescription);
            comanda.Parameters.AddWithValue("birthday", user.Birthday);
            comanda.Parameters.AddWithValue("pol", user.Pol);
            comanda.Parameters.AddWithValue("login", user.Login);
            comanda.Parameters.AddWithValue("password", user.Password);
            comanda.Parameters.AddWithValue("phone", user.Phone);

            comanda.ExecuteNonQuery();

            Disconnect();
        }

    }
}
