using System;
using SignalR_Demo.Models;
namespace SignalR_Demo.Business
{
    public interface IGameManager
    {
        bool AnyOpenRooms();
        void CreateRoom(Player player);
        bool DidPlayerTie(Player player);
        bool DidPlayerWin(Player player);
        Player GetOpponent(Player player);
        GameRoom JoinOpenRoom(Player player);
        void PlayerMove(Player player, string move);
        void RemoveRoom(Player player);
    }
}
