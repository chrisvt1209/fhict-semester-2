using GameAttic.Application;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GameAttic.Infrastructure
{
    public class UserRepository : BaseDb, IUserRepository
    {
        public UserRepository() : base() { }
        public bool AddUser(RegistrationUserDto user)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spUser_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@UserId", user.Id));
                command.Parameters.Add(new SqlParameter("@DisplayName", user.DisplayName));
                command.Parameters.Add(new SqlParameter("@Username", user.Username));
                command.Parameters.Add(new SqlParameter("@Password", user.Password));
                command.Parameters.Add(new SqlParameter("@Email", user.Email));

                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                CloseConnection();
                return false;
            }

            CloseConnection();
            return true;
        }

        public List<UserDto> GetAllUsers()
        {
            List<UserDto> users = new List<UserDto>();
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("dbo.spUser_GetAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserDto()
                    {
                        Id = reader.GetGuid("UserId"),
                        DisplayName = reader.GetString("DisplayName"),
                        Email = reader.GetString("Email"),
                        Role = reader.GetInt32("Role")
                    });
                }
            }
            catch (SqlException)
            {
                CloseConnection();
                return users;
            }

            CloseConnection();
            return users;
        }

        public UserDto? GetUserById(Guid id)
        {
            OpenConnection();

            SqlCommand command = new("dbo.spUser_GetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@UserId", id));

            using SqlDataReader reader = command.ExecuteReader();
            UserDto? user = null;

            if (reader.HasRows)
            {
                reader.Read();
                user = new UserDto()
                {
                    Id = reader.GetGuid("UserId"),
                    Email = reader.GetString("Email"),
                    DisplayName = reader.GetString("DisplayName"),
                    Role = reader.GetInt32("Role")
                };
            }

            CloseConnection();

            return user;
        }

        public bool EditUser(RegistrationUserDto user)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new("dbo.spUser_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@UserId", user.Id));
                command.Parameters.Add(new SqlParameter("@Username", user.Username));
                command.Parameters.Add(new SqlParameter("@Password", user.Password));
                command.Parameters.Add(new SqlParameter("@Email", user.Email));
                command.Parameters.Add(new SqlParameter("@DisplayName", user.DisplayName));

                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                CloseConnection();
                return false;
            }

            CloseConnection();
            return true;
        }

        public bool DeleteUser(Guid id)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spUser_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@UserId", id));

                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                CloseConnection();
                return false;
            }

            CloseConnection();
            return true;
        }

        public int GetRole(Guid roleId)
        {
            try
            {
                int role;
                OpenConnection();
                SqlCommand command = new("dbo.spUser_SelectRole", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@RoleId", roleId));

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    role = reader.GetInt32("Role");
                }
                else
                {
                    role = -1;
                }
                CloseConnection();
                return role;

            }
            catch (SqlException)
            {
                CloseConnection();
                throw;
            }
        }

        public Guid? Login(string username, string password)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("dbo.spUser_Login", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@Username", username));
                command.Parameters.Add(new SqlParameter("@Password", password));

                using SqlDataReader reader = command.ExecuteReader();

                Guid? guid = null;
                while (reader.Read())
                {
                    guid = reader.GetGuid("UserId");
                }

                return guid;
            }
            catch (SqlException)
            {
                CloseConnection();
                return null;
            }
        }
    }
}
