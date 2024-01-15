using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
	
	private Vector2Int gridMoveDirection;
	private Vector2Int gridPosition;
	private float gridMoveTimer;
	private float gridMoveTimerMax;

	private void Awake() {
		gridPosition = new Vector2Int(0,0);
		gridMoveTimerMax = .2f;
		gridMoveTimer = gridMoveTimerMax;
		gridMoveDirection = new Vector2Int(1,0);
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
