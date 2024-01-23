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

    // private class Apple
    // {

    //     public Vector2Int gridPosition;
    //     public Transform transform;

    //     GameObject appleGameObject;

    //     public void RedApple()
    //     {
    //         appleGameObject = new GameObject("Apple", typeof(SpriteRenderer));
    //         appleGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.redApple;
    //         appleGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    //         appleGameObject.transform.localScale = new Vector3(2, 2, 2);
    //         transform = appleGameObject.transform;   
    //     }

    //     void SetGridPosition(Vector2Int gridPosition)
    //     {
    //         this.gridPosition = gridPosition;
    //         transform.position = new Vector3(gridPosition.x, gridPosition.y);
    //     }


    //     public void changePosition()
    //     {
    //         transform.position = new Vector3Int((Random.Range(-15, 19)), (Random.Range(-9, 9)));
    //     }
    // }

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
