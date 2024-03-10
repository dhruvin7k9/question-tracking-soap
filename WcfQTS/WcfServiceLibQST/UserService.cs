using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceLibQTS;
using System.Configuration; 

namespace WcfServiceLibQTS
{
    public class UserService : IUserService
    {
        readonly string CnnStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QTS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        List<User> IUserService.GetUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(query, cnn);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        username = reader.GetString(1),
                        password = reader.GetString(2),
                        email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    };

                    users.Add(user);
                }
            }

            return users;
        }

        User IUserService.GetUserById(int id)
        {
            SqlConnection cnn = new SqlConnection(CnnStr);
            string query = "SELECT * FROM Users WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlParameter p = new SqlParameter("@id", id);
            cmd.Parameters.Add(p);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            User user = new User();
            while (reader.Read())
            {
                user.Id = reader.GetInt32(0);
                user.username = reader.GetString(1);
                user.password = reader.GetString(2);
                user.email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
            }

            reader.Close();
            cnn.Close();

            return user;
        }

        User IUserService.GetUserByUserName(string username)
        {
            SqlConnection cnn = new SqlConnection(CnnStr);
            string query = "SELECT * FROM Users WHERE username=@username";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlParameter p = new SqlParameter("@username", username);
            cmd.Parameters.Add(p);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            User user = new User();
            while (reader.Read())
            {
                user.Id = reader.GetInt32(0);
                user.username = reader.GetString(1);
                user.password = reader.GetString(2);
                user.email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
            }

            reader.Close();
            cnn.Close();

            return user;
        }

        void IUserService.DeleteUser(int id)
        {
            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "DELETE FROM Users WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@id", id);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        void IUserService.UpdateUser(int id, User user)
        {
            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "UPDATE Users SET username = @userName, password = @password, email = @email WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@userName", user.username);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(user.email) ? (object)DBNull.Value : user.email);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        void IUserService.CreateUser(User user)
        {
            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "INSERT INTO Users (username, password, email) VALUES (@userName, @password, @email)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@userName", user.username);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(user.email) ? (object)DBNull.Value : user.email);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
