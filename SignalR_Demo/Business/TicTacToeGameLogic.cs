using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_Demo.Business
{
    public class TicTacToeGameLogic
    {
        private List<string> WinningCombinations
        {
            get
            {
                List<string> combinations = new List<string>()
                {
                    "1,2,3",
                    "4,5,6",
                    "7,8,9",
                    "1,4,7",
                    "2,5,8",
                    "3,6,9",
                    "1,5,9",
                    "3,5,7"
                };
                return combinations;
            }
        }

        public bool PlayerHasWon(IEnumerable<string> moves)
        {
            bool playerHasWon = false;
            if (moves != null && moves.Count() > 0)
            {
                WinningCombinations.ForEach(c =>
                {
                    if (!playerHasWon)
                    {
                        var comb = c.Split(',');
                        playerHasWon = comb.All(e => moves.Any(p => p.Equals(e)));
                    }
                });
            }

            return playerHasWon;
        }

        public bool PlayerHasTied(IEnumerable<string> player1Moves, IEnumerable<string> player2Moves)
        {
            if (player1Moves != null && player2Moves != null)
            {
                if (player1Moves.Count() + player2Moves.Count() == 9)
                    return true;
            }
            return false;
        }
    }
}