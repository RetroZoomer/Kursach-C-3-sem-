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
using System.Data;


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
        
        public List<Profile> RecordOprName(string name)
        {
            Connect();
            string sql = "SELECT name,lastname,userdescription,birthday,pol,id_user FROM User where name LIKE @name or Login LIKE @name";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("name", name);

            MySqlDataReader reader = command.ExecuteReader();

            Profiles.profiles.Clear();

            while (reader.Read())
            {
                Profiles.profiles.Add(new Profile(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
            }

            Disconnect();
            return Profiles.profiles;
        }

        public List<Profile> RecordOprID(int id)
        {
            Connect();
            string sql = "SELECT name,lastname,userdescription,birthday,pol,id_user FROM User where id_user LIKE @id";
            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("id", id);

            MySqlDataReader reader = command.ExecuteReader();

            Profiles.profiles.Clear();

            while (reader.Read())
            {
                Profiles.profiles.Add(new Profile(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
            }

            Disconnect();
            return Profiles.profiles;
        }

        public List<Post> ListPost(bool vbr)
        {
            Connect();
            string sql = null;
            if (vbr == true)
            {   // Все посты
                sql = "SELECT post.id_post,post.id_user, posttitle,postdescription,starttime," +
                    "hide,user.name,user.lastname,user.login,hobbypost.id_hobby FROM post " +
                    "INNER JOIN user ON post.id_user = user.id_user  " +
                    "JOIN hobbypost on hobbypost.id_post = post.id_post  order by starttime ;";
            }
            else
            {   // Только видемые посты
                sql = "SELECT post.id_post,post.id_user, posttitle,postdescription,starttime," +
                    "hide,user.name,user.lastname,user.login,hobbypost.id_hobby FROM post " +
                    "INNER JOIN user ON post.id_user = user.id_user  " +
                    "JOIN hobbypost on hobbypost.id_post = post.id_post  where hide = true " +
                    "order by starttime ;";
            }

            MySqlCommand command = new MySqlCommand(sql, connection);

            MySqlDataReader reader = command.ExecuteReader();

            Posts.posts.Clear();

            while (reader.Read())
            {
                Posts.posts.Add(new Post(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7), 
                    reader.GetString(8), reader.GetString(9)));
            }

            Disconnect();
            return Posts.posts;
        }

        public List<Warning> ListWarning()
        {
            Connect();


            string sql = "SELECT id_warning, post.id_user, user.Name, user.LastName, warningDescription, warningTime, warning.id_post " +
                "FROM Warning " +
                "INNER JOIN post ON post.id_post = warning.id_post " +
                "INNER JOIN user ON user.id_user = post.id_user ORDER BY warning.WarningTime DESC;";

            


            MySqlCommand command = new MySqlCommand(sql, connection);
            

            MySqlDataReader reader = command.ExecuteReader();
            

            Warnings.warnings.Clear();

            while (reader.Read())
            {
                Warnings.warnings.Add(new Warning(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6)));
            }

            Disconnect();
            return Warnings.warnings;
        }

        public string WarningCount(int id)
        {
            Connect();
            string sql = "SELECT count(id_post) FROM Warning WHERE id_post = @id;";

            string res = null;

            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("id", id);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                res = reader.GetString(0);
            }
            Disconnect();

            return res;
        }

        public List<Friend> ListFriends(int id,string namesuser,bool pyt)
        {
            Connect();

            string sql = "SELECT friends.id_user2, name,lastname,birthday,pol,phone FROM friends " +
                "join user on user.id_user=friends.id_user2 where friends.id_user = @id union " +
                "SELECT friends.id_user2, name,lastname,birthday,pol,phone FROM friends " +
                "join user on user.id_user=friends.id_user where friends.id_user2 LIKE @id"; // Друзья
            string sql3 = "select  DISTINCT id_user, name,lastname,birthday,pol,phone from user " +
                "where lastname in (SELECT lastname FROM friends" +
                " join user on user.id_user = friends.id_user2 where friends.id_user = @id union" +
                " SELECT lastname FROM friends join user on user.id_user = friends.id_user " +
                "where friends.id_user2 = @id) and name LIKE @name; "; // Друзья по имени
            string sql2 = "select DISTINCT id_user, name,lastname,birthday,pol,phone from user " +
                "where lastname not in (SELECT lastname FROM friends"+
           " join user on user.id_user = friends.id_user2 where friends.id_user = @id union"+
            " SELECT lastname FROM friends join user on user.id_user = friends.id_user where friends.id_user2 = @id) and id_user <> @id; "; // неДрузья

            if (!pyt) // Вывод недрузей при поиске
            {
                MySqlCommand command2 = new MySqlCommand(sql2, connection);
                command2.Parameters.AddWithValue("id", id);
                MySqlDataReader reader = command2.ExecuteReader();
                Friends.friends.Clear();

                while (reader.Read())
                {
                    Friends.friends.Add(new Friend(reader.GetString(0), reader.GetString(1),
                        reader.GetString(2), reader.GetString(3), reader.GetString(4),
                        reader.GetString(5)));
                }
            }
            else
            {// Вывод друзей 
                MySqlCommand command;
                if (namesuser == null)
                {
                    command = new MySqlCommand(sql, connection);
                }
                else
                {
                    command = new MySqlCommand(sql3, connection);
                    command.Parameters.AddWithValue("@name", "%" + namesuser+ "%");
                }
                command.Parameters.AddWithValue("id", id);

                MySqlDataReader reader = command.ExecuteReader();
                Friends.friends.Clear();

                while (reader.Read())
                {
                    Friends.friends.Add(new Friend(reader.GetString(0), reader.GetString(1),
                        reader.GetString(2), reader.GetString(3), reader.GetString(4),
                        reader.GetString(5)));
                }
            }
            Disconnect();
            return Friends.friends;
        }
        [HttpGet]
        public void GetPerson(User user)
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
        public void GetFr(int id_user, int id_fr)
        {
            Connect();
            string sql = "INSERT INTO friends(id_user, id_user2) " +
                "VALUES (@id, @id2)";
            MySqlCommand comanda = new MySqlCommand(sql, connection);

            comanda.Parameters.AddWithValue("id", id_user);
            comanda.Parameters.AddWithValue("id2", id_fr);

            comanda.ExecuteNonQuery();
            Disconnect();
        }
        public int PostID(string postTitle)
        {
            Connect();
            string sql = "SELECT id_post " +
                "FROM Post WHERE postTitle like (@postTitle);";
            int res = 0;

            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("postTitle", postTitle);
            command.ExecuteNonQuery();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                res = Convert.ToInt32(reader.GetString(0));
                /*
                postic = new Post(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7),
                    reader.GetString(8), reader.GetString(9));*/
            }
            Disconnect();
            return res;
        }
        public void GetPost(Post post, int id)
        {
            Connect();
            string sql = "INSERT INTO POST(id_user, posttitle,postdescription,hide,starttime) " +
                "VALUES (@post.id_user, @post.posttitle,  @post.postdescription, " +
                "@post.hide, NOW())";

            MySqlCommand comanda = new MySqlCommand(sql, connection);


            comanda.Parameters.AddWithValue("post.id_user", id);

            comanda.Parameters.AddWithValue("post.posttitle", post.PostTitle);
            comanda.Parameters.AddWithValue("post.postdescription", post.PostDescription);
            comanda.Parameters.AddWithValue("post.hide", true);
            string ttl = post.PostTitle;
            int hbb = Convert.ToInt32(post.HobbyID);

            comanda.ExecuteNonQuery();

            Disconnect();

            DoID(ttl,hbb);
        }
        public void DoID(string ttl, int hbb)
        {
            int pst = PostID(ttl);
            Connect();
            string sql1 = "insert into hobbypost(id_post, id_hobby) values(@id_post,@id_hobby)";
            MySqlCommand comanda1 = new MySqlCommand(sql1, connection);
            comanda1.Parameters.AddWithValue("id_hobby", hbb);
            comanda1.Parameters.AddWithValue("id_post", pst);
            comanda1.ExecuteNonQuery();
            Disconnect();
        }
        public void GetWarning(int id_post, int id_us, string description)
        {
            Connect();
            string sql = "INSERT INTO Warning(id_post, id_user,warningdescription,warningtime) " +
                "VALUES (@warning.id_post, @warning.id_user,  @warning.warningdescription, " +
                "@warning.warningtime)";
            MySqlCommand comanda = new MySqlCommand(sql, connection);

            DateTime dateTime = DateTime.Now;

            comanda.Parameters.AddWithValue("warning.id_post", id_post);
            comanda.Parameters.AddWithValue("warning.id_user", id_us);
            comanda.Parameters.AddWithValue("warning.warningdescription", description);
            comanda.Parameters.AddWithValue("warning.warningtime", dateTime);

            comanda.ExecuteNonQuery(); //Срабатывает исключение
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

        public void DeleteFriendID(int id,int id_user)
        {
            Connect();
            string sql = "DELETE FROM friends WHERE id_user = @id and id_user2 = @id2 or id_user = @id2 and id_user2 = @id;";

            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("id2", id_user);
            command.ExecuteNonQuery();
            Disconnect();
        }

        public void DeleteByIdPost(int id)
        {
            Connect();
            string sql = "DELETE FROM Post WHERE id_post = @id;";
            string sql1 = "DELETE FROM HobbyPost WHERE id_post = @id;";

            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlCommand command1 = new MySqlCommand(sql1, connection);

            command.Parameters.AddWithValue("id", id);
            command1.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
            command1.ExecuteNonQuery();

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

        public void UpUserZn(User user)
        {
            Connect();
            string sql = "UPDATE USER SET name = @name, lastname = @lastname, userdescription = @userdescription," +
                " birthday = @birthday, pol = @pol, password= IF(password=@password,@password,@password2), phone=@phone" +
                " WHERE id_user = @id_user ;";

            MySqlCommand comanda = new MySqlCommand(sql, connection);

            comanda.Parameters.AddWithValue("id_user", user.UserID);
            comanda.Parameters.AddWithValue("name", user.Name);
            comanda.Parameters.AddWithValue("lastname", user.LastName);
            comanda.Parameters.AddWithValue("userdescription", user.UserDescription);
            comanda.Parameters.AddWithValue("birthday", user.Birthday);
            comanda.Parameters.AddWithValue("pol", user.Pol);
            comanda.Parameters.AddWithValue("password2", HashPasswordHelper.HashPassword(user.Password));
            comanda.Parameters.AddWithValue("password", user.Password);
            comanda.Parameters.AddWithValue("phone", user.Phone);

            comanda.ExecuteNonQuery();

            Disconnect();
        }

        public void UpPost(Post post)
        {
            Connect();
            string sql = "UPDATE POST SET id_post = @id_post,id_user = @id_user, posttitle = @posttitle, postdescription = @postdescription, starttime = @starttime, hide = @hide WHERE id_post = @id_post ;";
            string sql1 = "UPDATE HOBBYPOST SET id_hobby = @id_hobby WHERE id_post = @id_post ;";

            MySqlCommand comanda = new MySqlCommand(sql, connection);
            MySqlCommand comanda1 = new MySqlCommand(sql1, connection);

            comanda.Parameters.AddWithValue("id_post", post.PostID);
            comanda.Parameters.AddWithValue("id_user", post.UserID);
            comanda.Parameters.AddWithValue("posttitle", post.PostTitle);
            comanda.Parameters.AddWithValue("postdescription", post.PostDescription);
            comanda.Parameters.AddWithValue("starttime", post.StartTime);
            comanda.Parameters.AddWithValue("hide", post.Hide);


            comanda1.Parameters.AddWithValue("id_hobby", post.HobbyID);
            comanda1.Parameters.AddWithValue("id_post", post.PostID);

            comanda.ExecuteNonQuery();
            comanda1.ExecuteNonQuery();
            Disconnect();
        }

        public void UpPostU(Post post)
        {
            Connect();
            string sql = "UPDATE POST SET posttitle = @posttitle," +
                " postdescription = @postdescription" +
                " WHERE id_post = @id;";

            MySqlCommand comanda = new MySqlCommand(sql, connection);
            comanda.Parameters.AddWithValue("id", post.PostID);
            comanda.Parameters.AddWithValue("posttitle", post.PostTitle);
            comanda.Parameters.AddWithValue("postdescription", post.PostDescription);

            comanda.ExecuteNonQuery();
            Disconnect();
        }

        public Post PostInfo(int userid)
        {
            Connect();

            string sql = "SELECT post.id_post,post.id_user, posttitle,postdescription," +
                "starttime,hide,user.name,user.lastname,user.login,hobbypost.id_hobby " +
                "FROM Post INNER JOIN user ON post.id_user = user.id_user " +
                "JOIN hobbypost on hobbypost.id_post = post.id_post WHERE post.id_post like (@userid);";
            Post postic = null;

            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("userid", userid);
            command.ExecuteNonQuery();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                postic = new Post(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7),
                    reader.GetString(8), reader.GetString(9));
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
        public string GetID(string login)
        {
            Connect();
            string sql = "SELECT id_user FROM USER WHERE login like (@login);";

            MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("login", login);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                login = reader.GetString(0);
            }
            Disconnect();
            return login;

        }
    }
}
