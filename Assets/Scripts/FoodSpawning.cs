using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawning : MonoBehaviour
{

    public GameObject Worm;
    public GameObject Score;
	public GameObject Coins;

	Apple apple;

    [SerializeField] public AudioSource appleEatSoundEffect;

    // Start is called before the first frame update
    void Start()
    {

		apple = CreateApple();

        apple.transform.position = new Vector3Int(3, 0);
        apple.transform.localScale = new Vector3(3, 3);
    }

	private Apple CreateApple() {
		
		Apple apple = new Apple();
		apple.RedApple();

		//apple.SetGridPosition(new Vector2Int((Random.Range(-15, 19)),(Random.Range(-9, 9))));

		return apple;
	}

	private class Apple { 
			
	    public Vector2Int gridPosition;
        public Transform transform;

		GameObject appleGameObject;

        public void RedApple()
        {
            appleGameObject = new GameObject("Apple", typeof(SpriteRenderer));
            appleGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.redApple;
            appleGameObject.transform.localScale = new Vector3(2, 2, 2);
            transform = appleGameObject.transform;
        }

		public void GoldenApple() {
            appleGameObject = new GameObject("GoldenApple", typeof(SpriteRenderer));
            appleGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.goldenApple;
            appleGameObject.transform.localScale = new Vector3(2, 2, 2);
            transform = appleGameObject.transform;
		}

		public void SetGridPosition(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }



        public void changePosition()
        {
            if (appleGameObject.GetComponent<SpriteRenderer>().sprite == GameAssets.i.redApple)
            {
                appleGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.goldenApple;
                appleGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                transform.position = new Vector3Int((Random.Range(-15, 19)), (Random.Range(-9, 9)), 0);
            }
            else if (appleGameObject.GetComponent<SpriteRenderer>().sprite == GameAssets.i.goldenApple)
            {
                appleGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.redApple;
                appleGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                transform.position = new Vector3Int((Random.Range(-15, 19)), (Random.Range(-9, 9)), 0);
            }
        }

		

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

            //Apple.transform.position = new Vector3Int((Random.Range(-15, 19)),(Random.Range(-9, 9)));
        }
        
    }
}
