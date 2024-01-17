using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinListener : MonoBehaviour
{
    
	private int Coins = 0;

	public int GetCoins() {
		return Coins;
	}

	public void AddCoin() {
		Coins += 1;
	}

}
