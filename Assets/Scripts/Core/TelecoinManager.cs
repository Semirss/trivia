using UnityEngine;

namespace HowX.Core
{
    /// <summary>
    /// Manages the player's persistent Telecoin balance.
    /// Coins are saved to PlayerPrefs and survive app restarts.
    /// </summary>
    public static class TelecoinManager
    {
        private const string PREFS_KEY = "HowX_Telecoins";
        public const int COINS_PER_CORRECT_ANSWER = 10;

        /// <summary>Total coins the player has earned across all sessions.</summary>
        public static int TotalCoins
        {
            get => PlayerPrefs.GetInt(PREFS_KEY, 0);
            private set
            {
                PlayerPrefs.SetInt(PREFS_KEY, value);
                PlayerPrefs.Save();
                GameEvents.OnCoinsChanged?.Invoke(value);
            }
        }

        /// <summary>Award coins and persist immediately.</summary>
        public static void AddCoins(int amount)
        {
            if (amount <= 0) return;
            TotalCoins += amount;
        }

        /// <summary>Spend coins. Returns true if the player had enough.</summary>
        public static bool SpendCoins(int amount)
        {
            if (TotalCoins < amount) return false;
            TotalCoins -= amount;
            return true;
        }

        /// <summary>Check affordability without spending.</summary>
        public static bool CanAfford(int amount) => TotalCoins >= amount;
    }
}
