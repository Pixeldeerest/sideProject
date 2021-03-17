using RPGCharacterCreatorServer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RPGCharacterCreatorServer.DAO
{
    public class PlayerSqlDAO : IPlayerDAO
    {

        private string connectionString;

        public PlayerSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("Select * from Player", conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Player player = RowToPlayer(rdr);
                    players.Add(player);
                }
            }
            return players;
        }

        public Player GetPlayerById(int playerId)
        {
            Player player = new Player();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("Select * from Player where PlayerId = @playerId", conn);
                cmd.Parameters.AddWithValue("@playerId", playerId);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    player = RowToPlayer(rdr);
                }
            }

            return player;
        }

        public bool CreateNewPlayer(Player newPlayer)
        {
            int playerIdentity;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //TODO Maybe want to make the FK from Class PlayerName instead? How to make the two tables reference eachother and easy to add into?
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Player (PlayerName, PlayerRace, ClassId, PlayerStatId, Height, Age, Pronouns, Personality, HairColor, Eyecolor, UniqueFeatures)
                            VALUES(@playerName,
                            @playerRace,
                            (Select ClassId from Class Where ClassId = @classId),
                            (Select PlayerStatId from PlayerStat Where PlayerId = @playerStatId), 
                            @height,
	                        @age,
	                        @pronouns,
	                        @personality,
	                        @hairColor,
	                        @eyeColor,
	                        @uniqueFeatures); SELECT @@IDENTITY", conn);
                cmd.Parameters.AddWithValue("@playerName", newPlayer.PlayerName);
                cmd.Parameters.AddWithValue("@playerRace", newPlayer.PlayerRace);
                cmd.Parameters.AddWithValue("@classId", newPlayer.ClassId); //TODO How to do this???
                cmd.Parameters.AddWithValue("@playerStatId", newPlayer.PlayerStatId); //TODO How to do this???
                cmd.Parameters.AddWithValue("@height", newPlayer.Height);
                cmd.Parameters.AddWithValue("@age", newPlayer.Age);
                cmd.Parameters.AddWithValue("@prnouns", newPlayer.Pronouns);
                cmd.Parameters.AddWithValue("@personality", newPlayer.Personality);
                cmd.Parameters.AddWithValue("@hairColor", newPlayer.HairColor);
                cmd.Parameters.AddWithValue("@eyeColor", newPlayer.EyeColor);
                cmd.Parameters.AddWithValue("@uniqueFeatures", newPlayer.UniqueFeatures);
                playerIdentity = Convert.ToInt32(cmd.ExecuteScalar());
                newPlayer.PlayerId = playerIdentity;

                if (playerIdentity > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdatePlayer(string intendedColumn, string intendedUpdatedData, string playerName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE player SET @intendedColumn = @intendedUpdatedData WHERE playerName = @playerName);", conn);
                //can I make it so that the players can choose from a list of possible columns, and the @intendedColumn parameter is filled in for them?
                cmd.Parameters.AddWithValue("@intendedColumn", intendedColumn);
                cmd.Parameters.AddWithValue("@intendedUpdatedData", intendedUpdatedData);
                cmd.Parameters.AddWithValue("@playerName", playerName);

                int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<PlayerNameAndStats> GetPlayerDataAndStats()
        {
            List<PlayerNameAndStats> listOfPlayerInfo = new List<PlayerNameAndStats>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                   Select p.PlayerName, p.PlayerRace, c.ClassName, ps.Strength, ps.Dexterity, ps.Constitution, ps.Intelligence, ps.Wisdom, ps.Charisma 
                   FROM Player P
                   JOIN PlayerStat ps ON ps.playerId = p.playerId
                   JOIN Class c ON p.ClassId = c.ClassId", conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    PlayerNameAndStats playerNandS = RowToPlayerNameAndStats(rdr);
                    listOfPlayerInfo.Add(playerNandS);
                }

                //How to do this sql statement and rowToObject? Make a new model?
            }

            return listOfPlayerInfo;
        }

        public PlayerNameAndStats GetPlayerDataFromPlayerId(int playerId)
        {
            PlayerNameAndStats playerNandS = new PlayerNameAndStats();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                   Select p.PlayerName, p.PlayerRace, c.ClassName, ps.Strength, ps.Dexterity, ps.Constitution, ps.Intelligence, ps.Wisdom, ps.Charisma 
                   FROM Player P
                   JOIN PlayerStat ps ON ps.playerId = p.playerId
                   JOIN Class c ON p.ClassId = c.ClassId
                   WHERE p.playerId = @playerId", conn);
                cmd.Parameters.AddWithValue("@playerId", playerId);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    playerNandS = RowToPlayerNameAndStats(rdr);
                }
            }

            return playerNandS;
        }

        private Player RowToPlayer(SqlDataReader rdr)
        {
            Player player = new Player();
            player.Age = Convert.ToInt32(rdr["Age"]);
            player.ClassId = Convert.ToInt32(rdr["ClassId"]);
            player.EyeColor = Convert.ToString(rdr["EyeColor"]);
            player.HairColor = Convert.ToString(rdr["HairColor"]);
            player.Height = Convert.ToInt32(rdr["Height"]);
            player.Personality = Convert.ToString(rdr["Personality"]);
            player.PlayerId = Convert.ToInt32(rdr["PlayerId"]);
            player.PlayerName = Convert.ToString(rdr["PlayerName"]);
            player.PlayerRace = Convert.ToString(rdr["PlayerRace"]);
            player.PlayerStatId = Convert.ToInt32(rdr["PlayerStatId"]);
            player.UniqueFeatures = Convert.ToString(rdr["UniqueFeatures"]);
            return player;
        }

        private PlayerNameAndStats RowToPlayerNameAndStats(SqlDataReader rdr)
        {
            PlayerNameAndStats playerNandS = new PlayerNameAndStats();
            playerNandS.PlayerName = Convert.ToString(rdr["p.PlayerName"]);
            playerNandS.PlayerRace = Convert.ToString(rdr["p.PlayerRace"]);
            playerNandS.ClassName = Convert.ToString(rdr["c.ClassName"]);
            playerNandS.Dexterity = Convert.ToInt32(rdr["ps.Dexterity"]);
            playerNandS.Charisma = Convert.ToInt32(rdr["ps.Charisma"]);
            playerNandS.Constitution = Convert.ToInt32(rdr["ps.Contitution"]);
            playerNandS.Intelligence = Convert.ToInt32(rdr["ps.Intelligence"]);
            playerNandS.Strength = Convert.ToInt32(rdr["ps.Strength"]);
            playerNandS.Wisdom = Convert.ToInt32(rdr["ps.Wisdom"]);
            return playerNandS;
        }

    }
}
