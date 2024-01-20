// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using CodeMonkey.Utils;
// using CodeMonkey;

// public class WormMovement : MonoBehaviour
// {

// 	private Vector2Int gridMoveDirection;
// 	private Vector2Int gridPosition;
// 	private float gridMoveTimer;
// 	private float gridMoveTimerMax;
// 	private LevelGrid levelGrid;
// 	private int wormBodySize;
// 	private List<Vector2Int> snakeMovePositionList;

// 	private List<Transform> wormBodyTransformList;


// 	public void Setup(LevelGrid levelGrid) {
// 		this.levelGrid = levelGrid;
// 	}

// 	public void ChangeSpeed(string type)
// 	{
// 		if (type == "point")
// 		{
// 			if ((gridMoveTimerMax - 0.05) > 0.04)
// 			{
// 				gridMoveTimerMax -= 0.05f;

// 				if (wormBodySize <= 5) {
// 					wormBodySize += 1;
// 					CreateWormBody();
// 				}
// 			}

// 		}
// 	}

// 	public float GetSpeed()
// 	{
// 		return gridMoveTimerMax;
// 	}

// 	private void Awake() {
// 		gridPosition = new Vector2Int(0,0);
// 		gridMoveTimerMax = 1f;
// 		gridMoveTimer = gridMoveTimerMax;
// 		gridMoveDirection = new Vector2Int(1,0);

// 		wormBodySize = 1;
// 		snakeMovePositionList = new List<Vector2Int>();

// 		wormBodyTransformList = new List<Transform>();
// 	}	

//    void Update() {

// 		HandleInput();	
// 		HandleGridMove();

// 	}

// 	private void HandleInput() {

// 		if (Input.GetKeyDown(KeyCode.W)) {
// 			if (gridMoveDirection.y != -1) {
// 				gridMoveDirection.y = 1;
// 				gridMoveDirection.x = 0;
// 			}
// 		}
// 		else if (Input.GetKeyDown(KeyCode.S)) {
// 			if (gridMoveDirection.y != +1) {
// 				gridMoveDirection.y = -1;
// 				gridMoveDirection.x = 0;
// 			}
// 		}
// 		else if (Input.GetKeyDown(KeyCode.A)) {
// 			if (gridMoveDirection.x != +1) {
// 				gridMoveDirection.y = 0;
// 				gridMoveDirection.x = -1;
// 			}
// 		}
// 		else if (Input.GetKeyDown(KeyCode.D)) {
// 			if (gridMoveDirection.x != -1) {
// 				gridMoveDirection.y = 0;
// 				gridMoveDirection.x = +1;
// 			}
// 		}
// 	}

// 	private void HandleGridMove() {

// 		gridMoveTimer += Time.deltaTime;
// 		if (gridMoveTimer >= gridMoveTimerMax) {

//            gridMoveTimer -= gridMoveTimerMax;
//            snakeMovePositionList.Insert(0, gridPosition);

//            gridPosition += gridMoveDirection;



//            if (snakeMovePositionList.Count  >= wormBodySize + 1) {
// 				snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
// 			}
// 			//for (int i = 0; i < snakeMovePositionList.Count; i++) {
// 				//Vector2Int snakeMovePosition = snakeMovePositionList[i];

//                //World_Sprite worldSprite = World_Sprite.Create(new Vector3(snakeMovePosition.x, snakeMovePosition.y), Vector3.one * 0.5f, Color.white);
// 				//FunctionTimer.Create(worldSprite.DestroySelf, gridMoveTimerMax);

//            //}



// 			transform.position = new Vector3(gridPosition.x,gridPosition.y);
// 			transform.eulerAngles = new Vector3(0, 0, RotateSprite(gridMoveDirection) - 90);

// 			for (int i=0;i < wormBodyTransformList.Count; i++) {
// 				Vector3 wormBodyPosition = new Vector3(snakeMovePositionList[i].x, snakeMovePositionList[i].y);
// 				wormBodyTransformList[i].transform.eulerAngles = new Vector3(0,0,RotateSprite(gridMoveDirection) - 90);
// 				wormBodyTransformList[i].position = wormBodyPosition;
// 			}

// 			//levelGrid.SnakeMoved(gridPosition);
// 		}
// 	}

// 	private void CreateWormBody() {
// 		GameObject wormBody = new GameObject("WormBody", typeof(SpriteRenderer));
// 		wormBody.GetComponent<SpriteRenderer>().sprite = GameAssets.i.wormBody;
// 		wormBody.transform.localScale = new Vector3(2,2,2);
// 		wormBodyTransformList.Add(wormBody.transform);
// 	}

// 	private float RotateSprite(Vector2Int dir) {
// 		float n = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
// 		if (n < 0) {
// 			n += 360;
// 		}

// 		return n;
// 	}

// 	public List<Vector2Int> GetWormGridPositionList() {
// 		List<Vector2Int> gridPositionList = new List<Vector2Int>() {gridPosition};
// 		gridPositionList.AddRange(snakeMovePositionList);
// 		return gridPositionList;
// 	}

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class WormMovement : MonoBehaviour
{

    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private LevelGrid levelGrid;
    private int snakeBodySize;
    private List<Vector2Int> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    public void ChangeSpeed(string type)
    {
        if (type == "point")
        {
            if ((gridMoveTimerMax - 0.05) > 0.04)
            {
                gridMoveTimerMax -= 0.05f;

                if (snakeBodySize <= 5)
                {
                    snakeBodySize += 1;
                    CreateSnakeBodyPart();
                }
            }

        }
    }

    private void Awake()
    {
        gridPosition = new Vector2Int(0,0);
        gridMoveTimerMax = .5f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);

        snakeMovePositionList = new List<Vector2Int>();
        snakeBodySize = 0;

        snakeBodyPartList = new List<SnakeBodyPart>();
    }

    private void Update()
    {
        HandleInput();
        HandleGridMovement();
    }

    public float GetSpeed()
    {
        return gridMoveTimerMax;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gridMoveDirection.y != -1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = +1;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gridMoveDirection.y != +1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (gridMoveDirection.x != +1)
            {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (gridMoveDirection.x != -1)
            {
                gridMoveDirection.x = +1;
                gridMoveDirection.y = 0;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            snakeMovePositionList.Insert(0, gridPosition);

            gridPosition += gridMoveDirection;

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            /*
            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList) 
            {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (gridPosition == snakeBodyPartGridPosition)
                {
                    //game over!!
                    CMDebug.TextPopup("dead", transform.position); 
                }
            }
            */
            //this is needed later for when worm dies (but snakeMovePosition currently doesn't exist, so uncomment later on when needed)

            /*for (int i = 0; i < snakeMovePositionList.Count; i++) {
                Vector2Int snakeMovePosition = snakeMovePositionList[i];
                World_Sprite worldSprite = World_Sprite.Create(new Vector3(snakeMovePosition.x, snakeMovePosition.y), Vector3.one * .5f, Color.white);
                FunctionTimer.Create(worldSprite.DestroySelf, gridMoveTimerMax);
            }*/

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);

            UpdateSnakeBodyParts();
        }
    }

    private void CreateSnakeBodyPart()
    {
        SnakeBodyPart newBodyPart = new SnakeBodyPart(snakeBodyPartList.Count);
        newBodyPart.SetGridPosition(gridPosition);
        // snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
        snakeBodyPartList.Add(newBodyPart);
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            snakeBodyPartList[i].SetGridPosition(snakeMovePositionList[i]);
            // if (i < snakeMovePositionList.Count - 1)
            // {
            //     Vector2Int direction = snakeMovePositionList[i] - snakeMovePositionList[i + 1];
            //     snakeBodyPartList[i].SetRotation(GetAngleFromVector(direction));
            // }

            if (gridMoveDirection.x == 0 && gridMoveDirection.y == 1) {
                snakeBodyPartList[i].SetRotate("UP");
            }
            else if (gridMoveDirection.x == 0 && gridMoveDirection.y == -1) {
                snakeBodyPartList[i].SetRotate("DOWN");
            }
            else if (gridMoveDirection.x == -1 && gridMoveDirection.y == 0) {
                snakeBodyPartList[i].SetRotate("LEFT");
            }
            else if (gridMoveDirection.x == 1 && gridMoveDirection.y == 0) {
                snakeBodyPartList[i].SetRotate("RIGHT");
            }
            
        }
    } 


    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    // Return the full list of positions occupied by the snake: Head + Body
    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }

    private class SnakeBodyPart
    {

        private Vector2Int gridPosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.wormBody;
            //snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -1 - bodyIndex;
            snakeBodyGameObject.transform.localScale = new Vector3(2, 2, 2);
            transform = snakeBodyGameObject.transform;
        }

        public void SetGridPosition(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
        /*
        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }
        */
        //this is needed later for when worm dies (but snakeMovePosition currently doesn't exist, so uncomment later on when needed)

        public void SetRotation(float angle)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public void SetRotate(string direction) {
            if (direction == "UP" || direction == "DOWN") {
                transform.eulerAngles = new Vector3(0,0,180);
            }
            else {
                transform.eulerAngles = new Vector3(0,0,90);
            }
        }

    }

}

