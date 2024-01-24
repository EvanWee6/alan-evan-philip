using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawning : MonoBehaviour
{

    public GameObject Worm;
    public GameObject Score;
	public GameObject Coins;
    public GameObject GoldenApple;

	private Apple apple;

    [SerializeField] public AudioSource appleEatSoundEffect;

    void Start()
    {

		apple = CreateApple();

        apple.transform.position = new Vector3Int(3, 0);
        apple.transform.localScale = new Vector3(3, 3);
    }

	private Apple CreateApple() {

        Apple apple = new Apple();
        apple.AppleInit();
		return apple;
	}

    // Update is called once per frame
    void Update()
    {

        
        if (Worm.transform.position.x == apple.transform.position.x && Worm.transform.position.y == apple.transform.position.y)
        {

            appleEatSoundEffect.Play();

            Score.GetComponent<ScoreListener>().UpdateScore();
            Debug.Log(Score.GetComponent<ScoreListener>().GetScore());

			Coins.GetComponent<CoinListener>().AddCoin();
			Debug.Log($"coins: {Coins.GetComponent<CoinListener>().GetCoins()}");

            Worm.GetComponent<WormMovement>().ChangeSpeed("point");
            Debug.Log(Worm.GetComponent<WormMovement>().GetSpeed());

			
			apple.changePosition();

            GoldenApple.GetComponent<GoldenAppleScript>().ChanceSpawn();

        }
        
    }
}
