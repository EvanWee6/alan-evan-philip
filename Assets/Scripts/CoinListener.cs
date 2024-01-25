using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CoinListener : MonoBehaviour
{

	private int Coins;
    public ScoreScript scoreScript;


    void Start() {
		this.Coins = 0;
	}

	public void WriteCoins() {
		string path = "coins.txt";

		int previousCoins = ReadCoins();
		int newCoins = Coins + previousCoins;
		File.WriteAllText(path, newCoins.ToString());
        ScoreScript scorescript = GetComponent<ScoreScript>();
        if (scorescript != null)
        {
            scorescript.UpdateCoinsText(newCoins);
        }
    }

	public int ReadCoins() {
		string path = "coins.txt";

		if (File.Exists(path)) {
			string coinsTxt = File.ReadAllText(path);
			int coins;
			if (int.TryParse(coinsTxt, out coins)) {
				return coins;
			}
		}
		return 0;
	}

	public int GetCoins() {
		return Coins;
	}
	public void AddCoin()
	{
		Coins += 1;
		if (scoreScript != null)
		{
			scoreScript.UpdateCoinsText(Coins);

		}
    }
}
