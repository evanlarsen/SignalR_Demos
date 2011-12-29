using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR_Demo.Models;

namespace SignalR_Demo.Business
{
    public class GameManager : IGameManager
    {
        private List<GameRoom> _GameRooms { get; set; }
        private TicTacToeGameLogic _GameLogic;

        public GameManager()
        {
            this._GameRooms = new List<GameRoom>();
            this._GameLogic = new TicTacToeGameLogic();
        }

        public bool AnyOpenRooms()
        {
            if (_GameRooms.Count > 0)
                return _GameRooms.Any(p => p.GamesCurrentState == GameRoom.GameStates.LookingForAnotherPlayer);
            else
                return false;
        }

        public void CreateRoom(Player player)
        {
            GameRoom newRoom = new GameRoom();
            newRoom.FirstUser = player;
            newRoom.GroupId = _GameRooms.Count().ToString();
            newRoom.GamesCurrentState = GameRoom.GameStates.LookingForAnotherPlayer;

            _GameRooms.Add(newRoom);
        }

        public GameRoom JoinOpenRoom(Player player)
        {
            GameRoom openRoom = _GameRooms.First(room => room.GamesCurrentState == GameRoom.GameStates.LookingForAnotherPlayer);
            openRoom.SecondUser = player;
            openRoom.GamesCurrentState = GameRoom.GameStates.GameInProgress;
            return openRoom;
        }

        public void PlayerMove(Player player, string move)
        {
            Player currentPlayer = GetPlayer(player);

            if (currentPlayer.Moves == null)
                currentPlayer.Moves = new List<string>();

            currentPlayer.Moves.Add(move);
        }

        public Player GetOpponent(Player player)
        {
            GameRoom room = GetRoom(player);
            Player opponent = null;
            
            if (room != null)
            {
                if (room.FirstUser.id == player.id)
                {
                    opponent = room.SecondUser;
                }
                else
                {
                    opponent = room.FirstUser;
                }
            }

            return opponent;
        }

        public Player GetPlayer(Player player)
        {
            GameRoom room = GetRoom(player);
            Player currentPlayer = null;

            if (room != null)
            {
                if (room.FirstUser.id == player.id)
                {
                    currentPlayer = room.FirstUser;
                }
                else
                {
                    currentPlayer = room.SecondUser;
                }
            }
            return currentPlayer;
        }

        public GameRoom GetRoom(Player player)
        {
            return _GameRooms.FirstOrDefault(r => 
                {
                    bool isRoom = false;
                
                    if (r.FirstUser != null && r.FirstUser.id == player.id)
                        isRoom = true;

                    if (r.SecondUser != null && r.SecondUser.id == player.id)
                        isRoom = true;

                    return isRoom;
                    
                });
        }

        public bool DidPlayerWin(Player player)
        {
            bool playerWon = false;
            Player currentPlayer = GetPlayer(player);

            if (_GameLogic.PlayerHasWon(currentPlayer.Moves))
            {
                playerWon = true;
                RemoveRoom(player);
            }

            return playerWon;
        }

        public bool DidPlayerTie(Player player)
        {
            bool playerTied = false;
            Player currentPlayer = GetPlayer(player);
            Player opponent = GetOpponent(player);
            if (_GameLogic.PlayerHasTied(currentPlayer.Moves, opponent.Moves))
            {
                playerTied = true;
                RemoveRoom(player);
            }
            return playerTied;
        }

        public void RemoveRoom(Player player)
        {
            GameRoom room = GetRoom(player);

            if (room != null)
                _GameRooms.Remove(room);
        }

    }
}