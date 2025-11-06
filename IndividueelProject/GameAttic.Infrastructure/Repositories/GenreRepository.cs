using GameAttic.Application;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GameAttic.Infrastructure
{
    public class GenreRepository : BaseDb, IGenreRepository
    {
        public GenreRepository() : base() { }
        public bool AddGenre(GenreDto genre)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new SqlCommand("dbo.spGenre_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GenreId", genre.Id));
                command.Parameters.Add(new SqlParameter("@Name", genre.Name));

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

        public List<GenreDto> GetAllGenres()
        {
            List<GenreDto> genres = new List<GenreDto>();
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("dbo.spGenre_GetAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new GenreDto()
                    {
                        Id = reader.GetGuid("GenreId"),
                        Name = reader.GetString("Name")
                    });
                }
            }
            catch (SqlException)
            {
                CloseConnection();
                return genres;
            }

            CloseConnection();
            return genres;
        }

        public GenreDto? GetGenreById(Guid id)
        {
            OpenConnection();

            SqlCommand command = new("dbo.spGenre_GetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@GenreId", id));

            using SqlDataReader reader = command.ExecuteReader();
            GenreDto? genre = null;

            if (reader.HasRows)
            {
                reader.Read();
                genre = new GenreDto()
                {
                    Id = reader.GetGuid("GenreId"),
                    Name = reader.GetString("Name")
                };
            }

            CloseConnection();
            return genre;
        }

        public List<GenreDto> GetGenresByGame(Guid gameId)
        {
            List<GenreDto> genres = new List<GenreDto>();
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("dbo.spSelectGenresForGame", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("GameId", gameId));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new GenreDto()
                    {
                        Id = reader.GetGuid("GenreId"),
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
            return genres;
        }

        public bool EditGenre(GenreDto genre)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spGenre_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GenreId", genre.Id));
                command.Parameters.Add(new SqlParameter("@Name", genre.Name));

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

        public bool DeleteGenre(Guid id)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spGenre_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GenreId", id));

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
