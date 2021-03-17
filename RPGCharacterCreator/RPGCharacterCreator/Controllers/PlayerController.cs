using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGCharacterCreatorServer.DAO;
using RPGCharacterCreatorServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGCharacterCreatorServer.Controllers
{
    [Route("/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private IPlayerDAO playerDAO;
        private IUserDAO userDAO;

        public PlayerController(IPlayerDAO playerDAO, IUserDAO userDAO)
        {
            this.playerDAO = playerDAO;
            this.userDAO = userDAO;
        }

        [HttpGet]
        public ActionResult<List<Player>> GetPlayers()
        {
            List<Player> players = playerDAO.GetAllPlayers();
            if (players == null)
            {
                return NotFound();
            }
            else
            {
                return players;
            }
        }

        [HttpGet("{playerId}/players")]
        public ActionResult<Player> GetPlayerById(int playerId)
        {
            Player player = playerDAO.GetPlayerById(playerId);
            if (player == null)
            {
                return NotFound();
            }
            else
            {
                return player;
            }
        }

        [HttpPost]
        public ActionResult CreatePlayer(Player player)
        {
            playerDAO.CreateNewPlayer(player);
            return Ok();
        }

        //[HttpPut]
        //public ActionResult UpdatePlayer()

        [HttpGet("/player/playerData")]
        public ActionResult<List<PlayerNameAndStats>> GetPlayerNameAndStats()
        {
            List<PlayerNameAndStats> listOfData = playerDAO.GetPlayerDataAndStats();
            if (listOfData == null)
            {
                return NotFound();
            }
            else
            {
                return listOfData;
            }
        }

        [HttpGet("/player/{playerId}/playerData")]
        public ActionResult<PlayerNameAndStats> GetPlayerDataFromId(int playerId)
        {
            PlayerNameAndStats playerData = playerDAO.GetPlayerDataFromPlayerId(playerId);
            if (playerData == null)
            {
                return NotFound();
            }
            else
            {
                return playerData;
            }
        }

    }
}
