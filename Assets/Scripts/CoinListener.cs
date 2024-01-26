using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CoinListener : MonoBehaviour
{
    private int Coins = 0; 
    public ScoreScript scoreScript;

    void Start()
    {
        // Retrieve the coins from PlayerPrefs when the scene starts
        Coins = PlayerPrefs.GetInt("SavedCoins", Coins);
        UpdateCoins();
    }

    public void WriteCoins()
    {
        string path = "coins.txt";

        // Append the new coins to the existing file content
        File.AppendAllText(path, Coins.ToString() + "\n");

        // Save the coins to PlayerPrefs for the next update
        PlayerPrefs.SetInt("SavedCoins", Coins);
        PlayerPrefs.Save();

        UpdateCoins();
    }

    public int ReadCoins()
    {
        string path = "coins.txt";

        if (File.Exists(path))
        {
            // Read all lines in the file
            string[] lines = File.ReadAllLines(path);

            if (lines.Length > 0)
            {
                // Get the last line, which contains the most recent total coins
                string coinsTxt = lines[lines.Length - 1];
                int coins;
                if (int.TryParse(coinsTxt, out coins))
                {
                    return coins;
                }
            }
        }
        return 0;
    }

    public int GetCoins()
    {
        return Coins;
    }

    public void AddCoin()
    {
        Coins += 1;

        // Save the updated coins to PlayerPrefs
        PlayerPrefs.SetInt("SavedCoins", Coins);
        PlayerPrefs.Save();

        UpdateCoins();

        if (scoreScript != null)
        {
            scoreScript.UpdateCoinsText(Coins);
        }
    }

    private void UpdateCoins()
    {
        // Optionally, you can perform additional actions when the coin value is updated
        Debug.Log($"Updated coins: {Coins}");
    }
}