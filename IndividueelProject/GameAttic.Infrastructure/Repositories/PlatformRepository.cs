using GameAttic.Application;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GameAttic.Infrastructure
{
    public class PlatformRepository : BaseDb, IPlatformRepository
    {
        public PlatformRepository() : base() { }

        public bool AddPlatform(PlatformDto platform)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new SqlCommand("dbo.spPlatform_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@PlatformId", platform.Id));
                command.Parameters.Add(new SqlParameter("@Name", platform.Name));

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

        public List<PlatformDto> GetAllPlatforms()
        {
            List<PlatformDto> platforms = new List<PlatformDto>();
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("dbo.spPlatform_GetAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    platforms.Add(new PlatformDto()
                    {
                        Id = reader.GetGuid("PlatformId"),
                        Name = reader.GetString("Name")
                    });
                }
            }
            catch (SqlException)
            {
                CloseConnection();
                return platforms;
            }

            CloseConnection();
            return platforms;
        }

        public PlatformDto? GetPlatformById(Guid id)
        {
            OpenConnection();

            SqlCommand command = new("dbo.spPlatform_GetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@PlatformId", id));

            using SqlDataReader reader = command.ExecuteReader();
            PlatformDto? platform = null;

            if (reader.HasRows)
            {
                reader.Read();
                platform = new PlatformDto()
                {
                    Id = reader.GetGuid("PlatformId"),
                    Name = reader.GetString("Name")
                };
            }

            CloseConnection();
            return platform;
        }

        public List<PlatformDto> GetPlatformsByGame(Guid gameId)
        {
            List<PlatformDto> platforms = new List<PlatformDto>();
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("dbo.spSelectPlatformsForGame", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GameId", gameId));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    platforms.Add(new PlatformDto()
                    {
                        Id = reader.GetGuid("PlatformId"),
                        Name = reader.GetString("Name")
                    });
                }
            }
            catch (SqlException)
            {
                CloseConnection();
                throw;
            }

            CloseConnection();
            return platforms;
        }

        public bool EditPlatform(PlatformDto platform)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spPlatform_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@PlatformId", platform.Id));
                command.Parameters.Add(new SqlParameter("@Name", platform.Name));

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

        public bool DeletePlatform(Guid id)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spPlatform_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@PlatformId", id));

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
    }
}
