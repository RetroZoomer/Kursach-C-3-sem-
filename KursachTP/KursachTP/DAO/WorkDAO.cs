using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using KursachTP.Models;
using KursachTP.Controllers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;


namespace KursachTP.DAO
{
    public class WorkDAO : ConnectDAO
    {
        public List<User> Record(int vbr, int id_user, int id_user2, string name)
        {
            string sql;
            Connect();
            MySqlCommand command;
            if (vbr == 1)
            {
                sql = "SELECT id_user,name,lastname,userdescription,birthday,pol,login," +
                "password,phone,rol FROM User";
                command = new MySqlCommand(sql, connection);
            }
            else if (vbr == 2)
            {
                sql = "SELECT id_user,name,lastname,userdescription,birthday,pol,login," +
                "password,phone,rol FROM User where id_user BETWEEN @id_user and @id_user2";
                command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("id_user", id_user);
                if (id_user2 < 1)
                {
                    id_user2 = id_user + 100;
                }
                command.Parameters.AddWithValue("id_user2", id_user2);
            }
            else
            {
                sql = "SELECT id_user,name,lastname,userdescription,birthday,pol,login," +
                "password,phone,rol FROM User where name LIKE @name or Login LIKE @name";
                command = new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("name", "%" + name + "%");
            }

            MySqlDataReader reader = command.ExecuteReader();

            Users.users.Clear();

            while (reader.Read())
            {
                Users.users.Add(new User(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7),
                    reader.GetString(8), reader.GetString(9)));
            }
            Disconnect();
            return Users.users;
        }
        /*public List<User> RecordOprId(int id_user,int id_user2)
        {
            Connect();
            string sql = "SELECT id_user,name,lastname,userdescription,birthday,pol,login," +
                "password,phone FROM User where id_user BETWEEN @id_user and @id_user2";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("id_user", id_user);
            if (id_user2 < 1)
            {
                id_user2 = id_user + 100;
            }
            command.Parameters.AddWithValue("id_user2", id_user2);

            MySqlDataReader reader = command.ExecuteReader();

            Users.users.Clear();

            while (reader.Read())
            {
                Users.users.Add(new User(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7),
                    reader.GetString(8), reader.GetString(9)));
            }

            Disconnect();
            return Users.users;
        }
        public List<User> RecordOprName(string name)
        {
            Connect();
            string sql = "SELECT id_user,name,lastname,userdescription,birthday,pol,login," +
                "password,phone FROM User where name LIKE @name or Login LIKE @name";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("name", "%" + name + "%");

            MySqlDataReader reader = command.ExecuteReader();

            Users.users.Clear();

            while (reader.Read())
            {
                Users.users.Add(new User(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7),
                    reader.GetString(8), reader.GetString(9)));
            }

            Disconnect();
            return Users.users;
        }*/
        
        public List<Post> ListPost()
        {
            Connect();
            string sql = "SELECT id_post,post.id_user, posttitle,postdescription,starttime,hide,user.name,user.lastname " +
                "FROM post " + "INNER JOIN user ON post.id_user = user.id_user;"; // where id_user BETWEEN @id_user and @id_user2";
            MySqlCommand command = new MySqlCommand(sql, connection);
            //command.Parameters.AddWithValue("id_user", id_user);
            MySqlDataReader reader = command.ExecuteReader();

            Posts.posts.Clear();

            while (reader.Read())
            {
                Posts.posts.Add(new Post(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7)));
            }

            Disconnect();
            return Posts.posts;
        }

        [HttpGet]
        public async void GetPerson(User user)
        {
            Connect();
            string sql = "INSERT INTO USER(name,lastname,userdescription,birthday,pol,login," +
                "password,phone,rol) VALUES (@user.name, @user.lastname,  @user.userdescription, @user.birthday, " +
                "@user.pol, @user.login, @user.password, @user.phone, @user.rol)";
            MySqlCommand comanda = new MySqlCommand(sql, connection);

            comanda.Parameters.AddWithValue("user.name", user.Name);
            comanda.Parameters.AddWithValue("user.lastname", user.LastName);
            comanda.Parameters.AddWithValue("user.userdescription", user.UserDescription);
            comanda.Parameters.AddWithValue("user.birthday", user.Birthday);
            comanda.Parameters.AddWithValue("user.pol", user.Pol);
            comanda.Parameters.AddWithValue("user.login", user.Login);
            comanda.Parameters.AddWithValue("user.password", HashPasswordHelper.HashPassword(user.Password));
            comanda.Parameters.AddWithValue("user.phone", user.Phone);
            comanda.Parameters.AddWithValue("user.rol", "UserIS");

            comanda.ExecuteNonQuery();
            Disconnect();
        }

        public void GetPost(Post post)
        {
            Connect();
            string sql = "INSERT INTO POST(id_user, posttitle,postdescription,starttime,hide) " +
                "VALUES (@post.id_user, @post.posttitle,  @post.postdescription, @post.starttime, " +
                "@post.hide)";
            MySqlCommand comanda = new MySqlCommand(sql, connection);

            comanda.Parameters.AddWithValue("post.id_user", post.UserID);
            comanda.Parameters.AddWithValue("post.posttitle", post.PostTitle);
            comanda.Parameters.AddWithValue("post.postdescription", post.PostDescription);
            comanda.Parameters.AddWithValue("post.starttime", post.StartTime);
            comanda.Parameters.AddWithValue("post.hide", true);

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

        public void DeleteByIdPost(int id)
        {
            Connect();
            string sql = "DELETE FROM Post WHERE id_post = @id;";

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
                    reader.GetString(8), reader.GetString(9));
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
                    reader.GetString(8), reader.GetString(9));
            }
            Disconnect();
            return person;
        }
        public void UpZn(User user)
        {
            Connect();
            string sql = "UPDATE USER SET id_user = @id_user, name = @name, lastname = @lastname, userdescription = @userdescription," +
                " birthday = @birthday, pol = @pol, login = @login, password = @password, phone=@phone, rol=@rol" +
                " WHERE id_user = @id_user ;";

            MySqlCommand comanda = new MySqlCommand(sql, connection);

            comanda.Parameters.AddWithValue("id_user", user.UserID);
            comanda.Parameters.AddWithValue("name", user.Name);
            comanda.Parameters.AddWithValue("lastname", user.LastName);
            comanda.Parameters.AddWithValue("userdescription", user.UserDescription);
            comanda.Parameters.AddWithValue("birthday", user.Birthday);
            comanda.Parameters.AddWithValue("pol", user.Pol);
            comanda.Parameters.AddWithValue("login", user.Login);
            comanda.Parameters.AddWithValue("password", HashPasswordHelper.HashPassword(user.Password));
            comanda.Parameters.AddWithValue("phone", user.Phone);
            comanda.Parameters.AddWithValue("rol", user.Rol);

            comanda.ExecuteNonQuery();
            Disconnect();
        }

        public void UpPost(Post post)
        {
            Connect();
            string sql = "UPDATE POST SET id_post = @id_post,id_user = @id_user, posttitle = @posttitle," +
                " postdescription = @postdescription," +
                " starttime = @starttime, hide = @hide  WHERE id_post = @id_post ;";

            MySqlCommand comanda = new MySqlCommand(sql, connection);
            comanda.Parameters.AddWithValue("id_post", post.PostID);
            comanda.Parameters.AddWithValue("id_user", post.UserID);
            comanda.Parameters.AddWithValue("posttitle", post.PostTitle);
            comanda.Parameters.AddWithValue("postdescription", post.PostDescription);
            comanda.Parameters.AddWithValue("starttime", post.StartTime);
            comanda.Parameters.AddWithValue("hide", post.Hide);
           
            comanda.ExecuteNonQuery();
            Disconnect();
        }

        public Post PostInfo(int userid)
        {
            Connect();

            string sql = "SELECT id_post,post.id_user, posttitle,postdescription,starttime,hide,user.name,user.lastname FROM Post " +
                "INNER JOIN user ON post.id_user = user.id_user WHERE post.id_post like (@userid);";
            Post postic = null;

            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("userid", userid);
            command.ExecuteNonQuery();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                postic = new Post(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7));
            }

            Disconnect();
            return postic;
        }
        public Boolean YesNoData(User user)
        {
            Connect();
            string sql = "SELECT * FROM USER WHERE login like (@login) and password like (@password)";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("login", user.Login);
            command.Parameters.AddWithValue("password", HashPasswordHelper.HashPassword(user.Password));

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                Disconnect();
                return (true);
            }
            else
            {
                Disconnect();
                return (false);
            }
        }

        public string GetRole(User user)
        {
            Connect();
            string sql = "SELECT rol FROM USER WHERE login like (@login);";

            user.Rol = null;

            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("login", user.Login);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                user.Rol = reader.GetString(0);
            }
            Disconnect();

            if (user.Rol == "UserIS")
            {
                return ("UserIS");
            }
            else
            {
                return ("AdminIS");
            }
        }

    }
}
