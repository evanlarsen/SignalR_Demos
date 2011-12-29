using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_Demo.Models
{
    public class GameRoom
    {
        public Player FirstUser { get; set; }
        public Player SecondUser { get; set; }

        public string GroupId { get; set; }

        public enum GameStates
        {
            LookingForAnotherPlayer,
            GameInProgress,
        }

        public GameStates GamesCurrentState { get; set; }
    }
}