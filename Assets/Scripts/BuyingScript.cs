using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class BuyingScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skin;
    private int coinsRequired = 100;

    public void Buy()
    {
        int savedCoins = PlayerPrefs.GetInt("SavedCoins", 0);

        // Check if the user has enough coins to make the purchase
        if (savedCoins >= coinsRequired)
        {
            // Deduct 100 coins
            savedCoins -= coinsRequired;

            // Update the PlayerPrefs with the new coin value
            PlayerPrefs.SetInt("SavedCoins", savedCoins);
            PlayerPrefs.Save();

            // Update the skins.txt file
            string path = "skins.txt";
            File.WriteAllText(path, skin.text);

            Debug.Log($"Buying {skin.text}. Remaining coins: {savedCoins}");
        }
        else
        {
            // User doesn't have enough coins, display a message or take appropriate action
            Debug.Log("Not enough coins for the purchase.");
        }
    }
}
