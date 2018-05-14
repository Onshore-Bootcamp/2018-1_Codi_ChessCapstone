using ChessBLL.Models;

namespace ChessBLL
{
    public class GameStats
    {
        public static int WinLossRatio(int wins, int asWhite, int asBlack)
        {
            int wlr;
            int totalGames;

            totalGames = asBlack + asWhite;
            wlr = wins / totalGames;
            return wlr;
        }

        public static int Losses(int wins, int asWhite, int asBlack)
        {
            int losses;
            int totalGames;

            totalGames = asBlack + asWhite;
            losses = totalGames - wins;
            return losses;
        }
    }
}
