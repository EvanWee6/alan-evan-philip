using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class LevelGrid {
	
	private Vector2Int foodGridPosition;
	private int width;
	private int height;

	private string spriteNames = "Apple";
	private Sprite[] sprites;
	private SpriteRenderer spriteR;



	public LevelGrid(int width, int height) {
		this.width = width;
		this.height = height;

		SpawnFood();

		FunctionPeriodic.Create(SpawnFood, 1f);
	}

	void Start()
	{
        spriteR = spriteR.gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNames);

    }

    private void SpawnFood() {

		foodGridPosition = new Vector2Int(Random.Range(0,width), Random.Range(0,height));

		GameObject foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
		spriteR.sprite = sprites[0];
		foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);

	}    

	public void SnakeMoved(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == foodGridPosition) {
			Object.Destroy(sprites[0]);
            SpawnFood();
        }
    }
}
