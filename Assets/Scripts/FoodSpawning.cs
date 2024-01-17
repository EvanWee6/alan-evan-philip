using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawning : MonoBehaviour
{

    public GameObject Apple;
    public GameObject Worm;
    public GameObject Score;
	public GameObject Coins;

    // Start is called before the first frame update
    void Start()
    {
        Apple.transform.position = new Vector3Int(3, 0);
        Apple.transform.localScale = new Vector3(3, 3);
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Worm.transform.position.x == Apple.transform.position.x && Worm.transform.position.y == Apple.transform.position.y)
        {
            Score.GetComponent<ScoreListener>().UpdateScore();
            Debug.Log(Score.GetComponent<ScoreListener>().GetScore());

			Coins.GetComponent<CoinListener>().AddCoin();
			Debug.Log($"coins: {Coins.GetComponent<CoinListener>().GetCoins()}");

            Worm.GetComponent<WormMovement>().ChangeSpeed("point");
            Debug.Log(Worm.GetComponent<WormMovement>().GetSpeed());




            Apple.transform.position = new Vector3Int((Random.Range(-15, 19)),(Random.Range(-9, 9)));
        }
        
    }
}
