using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;
using SignalR_Demo.Models;
using SignalR_Demo.Business;
using SignalR.Infrastructure;

// need to make so the game room is removed from list if the person re-subscribes

namespace SignalR_Demo.Hubs
{
    public class TicTacToeHub : Hub, IDisconnect
    {
        private readonly IGameManager _GameManager;

        public TicTacToeHub(IGameManager gameManager)
        {
            _GameManager = gameManager;
        }

        public void Subscribe()
        {
            Player player = new Player() { id = Context.ClientId };
            //looking for room
            if (_GameManager.AnyOpenRooms())
            {
                //room found, joining room
                Caller.alertMsg("Room found, joining room...");

                GameRoom openRoom = _GameManager.JoinOpenRoom(player);
                
                // begin game
                Clients[openRoom.FirstUser.id].alertMsg("Game has begun, your 'X'");
                Clients[openRoom.FirstUser.id].beginGame('x');
                Clients[openRoom.SecondUser.id].alertMsg("Game has begun, your 'O'");
                Clients[openRoom.SecondUser.id].beginGame('o');
            }
            else
            {
                // no rooms found, waiting for another player
                Caller.alertMsg("No rooms found, creating room..");
                _GameManager.CreateRoom(player);
                Caller.alertMsg("Waiting for another player to join...");
            }
        }

        public void MakeMove(string move)
        {
            Player player = new Player() { id = Context.ClientId };
            _GameManager.PlayerMove(player, move);
            Player opponent = _GameManager.GetOpponent(player);

            Clients[opponent.id].setOpponentsMove(move);

            if (_GameManager.DidPlayerWin(player))
            {
                Caller.youWin();
                Clients[opponent.id].youLose();
            }
            else if (_GameManager.DidPlayerTie(player))
            {
                Caller.youTied();
                Clients[opponent.id].youTied();
            }
        }

        public void Disconnect()
        {
            Player player = new Player() { id = Context.ClientId };
            Player opponent = _GameManager.GetOpponent(player);

            if (opponent != null)
                Clients[opponent.id].opponentQuit();


            _GameManager.RemoveRoom(player);
        }


        //private string GetCookieValue(string key)
        //{
        //    HttpCookie cookie = Context.Cookies[key];
        //    return cookie != null ? HttpUtility.UrlDecode(cookie.Value) : null;
        //}
    }
}