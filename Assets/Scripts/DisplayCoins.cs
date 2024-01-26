using UnityEngine;
using TMPro;

public class DisplayCoins : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    void Start()
    {
        // Retrieve the coins from PlayerPrefs
        int savedCoins = PlayerPrefs.GetInt("SavedCoins", 0);

        // Display the coins on the screen
        coinsText.text = $"Coins: {savedCoins}";
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
