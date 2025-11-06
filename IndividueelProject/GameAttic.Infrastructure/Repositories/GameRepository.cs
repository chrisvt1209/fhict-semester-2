using GameAttic.Application;
using GameAttic.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GameAttic.Infrastructure
{
    public class GameRepository : BaseDb, IGameRepository
    {
        public GameRepository() : base() { }

        public bool AddGame(CreateGameDto game)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spGame_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GameId", game.GameId));
                command.Parameters.Add(new SqlParameter("@PlatformId", game.PlatformId));
                command.Parameters.Add(new SqlParameter("@GenreId", game.GenreId));
                command.Parameters.Add(new SqlParameter("@Title", game.Title));
                command.Parameters.Add(new SqlParameter("@ReleaseDate", game.ReleaseDate));
                command.Parameters.Add(new SqlParameter("@Price", game.Price));
                command.Parameters.Add(new SqlParameter("@ImageUrl", game.ImageUrl));

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

        public List<GameDto> GetAllGames(GameFilterDto gameFilter)
        {
            string sortOrder = gameFilter.IsAsc ? "ASC" : "DESC";
            string sortColumn = gameFilter.OrderColumn.ToString() ?? GameOrderColumn.Title.ToString();
            List<GameDto> games = new List<GameDto>();
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("dbo.spGetFilteredGames", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                AddSqlParameter(command, "@Title", gameFilter.Title);
                AddSqlParameter(command, "@PlatformId", gameFilter.PlatformId);
                AddSqlParameter(command, "@GenreId", gameFilter.GenreId);
                AddSqlParameter(command, "@SortColumn", sortColumn);
                AddSqlParameter(command, "@SortOrder", sortOrder);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid gameId = reader.GetGuid("GameId");
                    string? imageUrl = reader.IsDBNull("ImageUrl") ? null : reader.GetString("ImageUrl");
                    games.Add(new GameDto()
                    {
                        Id = gameId,
                        Title = reader.GetString("Title"),
                        ReleaseDate = DateOnly.FromDateTime(reader.GetDateTime("ReleaseDate")),
                        Price = reader.GetDecimal("Price"),
                        ImageUrl = imageUrl
                    });
                }
            }
            catch (SqlException)
            {
                CloseConnection();
                return games;
            }

            CloseConnection();
            return games;
        }

        public GameDto? GetGameById(Guid id)
        {
            OpenConnection();

            SqlCommand command = new("dbo.spGame_GetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@GameId", id));

            using SqlDataReader reader = command.ExecuteReader();
            GameDto? game = null;

            if (reader.HasRows)
            {
                reader.Read();
                string? imageUrl = reader.IsDBNull("ImageUrl") ? null : reader.GetString("ImageUrl");
                game = new GameDto()
                {
                    Id = reader.GetGuid("GameId"),
                    Title = reader.GetString("Title"),
                    ReleaseDate = DateOnly.FromDateTime(reader.GetDateTime("ReleaseDate")),
                    Price = reader.GetDecimal("Price"),
                    ImageUrl = imageUrl
                };
            }

            CloseConnection();
            return game;
        }

        public bool EditGame(GameDto game)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spGame_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GameId", game.Id));
                command.Parameters.Add(new SqlParameter("@Title", game.Title));
                command.Parameters.Add(new SqlParameter("@ReleaseDate", game.ReleaseDate));
                command.Parameters.Add(new SqlParameter("@Price", game.Price));
                command.Parameters.Add(new SqlParameter("@ImageUrl", game.ImageUrl));

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

        public bool DeleteGame(Guid id)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spGame_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GameId", id));

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

        public bool AddPlatformToGame(Guid gameId, Guid platformId)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spGame_Platform_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GameId", gameId));
                command.Parameters.Add(new SqlParameter("@PlatformId", platformId));

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

        public bool AddGenreToGame(Guid gameId, Guid genreId)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spGame_Genre_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GameId", gameId));
                command.Parameters.Add(new SqlParameter("@GenreId", genreId));

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

        public bool RemovePlatformFromGame(Guid gameId, Guid platformId)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spGame_Platform_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GameId", gameId));
                command.Parameters.Add(new SqlParameter("@PlatformId", platformId));

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

        public bool RemoveGenreFromGame(Guid gameId, Guid genreId)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new("dbo.spGame_Genre_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(new SqlParameter("@GameId", gameId));
                command.Parameters.Add(new SqlParameter("@GenreId", genreId));

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
