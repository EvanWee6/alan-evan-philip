using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class WormMovement : MonoBehaviour
{
	
	private Vector2Int gridMoveDirection;
	private Vector2Int gridPosition;
	private float gridMoveTimer;
	private float gridMoveTimerMax;

	private int snakeBodySize;
	private List<Vector2Int> snakeMovePositionList;

	private void Awake() {
		gridPosition = new Vector2Int(0,0);
		gridMoveTimerMax = .2f;
		gridMoveTimer = gridMoveTimerMax;
		gridMoveDirection = new Vector2Int(1,0);

		snakeBodySize = 1;
		snakeMovePositionList = new List<Vector2Int>();
	}	

    void Update() {
		
		HandleInput();	
		HandleGridMove();

	}

	private void HandleInput() {

		if (Input.GetKeyDown(KeyCode.W)) {
			if (gridMoveDirection.y != -1) {
				gridMoveDirection.y = 1;
				gridMoveDirection.x = 0;
			}
		}
		else if (Input.GetKeyDown(KeyCode.S)) {
			if (gridMoveDirection.y != +1) {
				gridMoveDirection.y = -1;
				gridMoveDirection.x = 0;
			}
		}
		else if (Input.GetKeyDown(KeyCode.A)) {
			if (gridMoveDirection.x != +1) {
				gridMoveDirection.y = 0;
				gridMoveDirection.x = -1;
			}
		}
		else if (Input.GetKeyDown(KeyCode.D)) {
			if (gridMoveDirection.x != -1) {
				gridMoveDirection.y = 0;
				gridMoveDirection.x = +1;
			}
		}
	}

	private void HandleGridMove() {

		gridMoveTimer += Time.deltaTime;
		if (gridMoveTimer >= gridMoveTimerMax) {
			gridPosition += gridMoveDirection;
			gridMoveTimer -= gridMoveTimerMax;
			snakeMovePositionList.Insert(0, gridPosition);

			if (snakeMovePositionList.Count  >= snakeBodySize + 1) {
				snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
			}
			for (int i = 0; i < snakeMovePositionList.Count; i++) {
				Vector2Int snakeMovePosition = snakeMovePositionList[i];
				World_Sprite worldSprite = World_Sprite.Create(new Vector3(snakeMovePosition.x, snakeMovePosition.y), Vector3.one * 0.5f, Color.white);
				FunctionTimer.Create(worldSprite.DestroySelf, gridMoveTimerMax);
			}

			transform.position = new Vector3(gridPosition.x,gridPosition.y);
			transform.eulerAngles = new Vector3(0, 0, RotateSprite(gridMoveDirection) - 90);
		}
	}

	private float RotateSprite(Vector2Int dir) {
		float n = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		if (n < 0) {
			n += 360;
		}

		return n;
	}

}
