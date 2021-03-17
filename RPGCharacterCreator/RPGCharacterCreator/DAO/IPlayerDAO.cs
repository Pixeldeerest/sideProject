using RPGCharacterCreatorServer.Models;
using System.Collections.Generic;

namespace RPGCharacterCreatorServer.DAO
{
    public interface IPlayerDAO
    {
        bool CreateNewPlayer(Player newPlayer);
        List<Player> GetAllPlayers();
        Player GetPlayerById(int playerId);
        List<PlayerNameAndStats> GetPlayerDataAndStats();
        PlayerNameAndStats GetPlayerDataFromPlayerId(int playerId);
        bool UpdatePlayer(string intendedColumn, string intendedUpdatedData, string playerName);
    }
}