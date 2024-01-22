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
// 	private float speed;
// 	private LevelGrid levelGrid;
// 	private int wormBodySize;
// 	private List<Vector2Int> WormMovePositionList;

// 	private List<Transform> wormBodyTransformList;


// 	public void Setup(LevelGrid levelGrid) {
// 		this.levelGrid = levelGrid;
// 	}

// 	public void ChangeSpeed(string type)
// 	{
// 		if (type == "point")
// 		{
// 			if ((speed - 0.05) > 0.04)
// 			{
// 				speed -= 0.05f;

// 				if (wormBodySize <= 5) {
// 					wormBodySize += 1;
// 					CreateWormBody();
// 				}
// 			}

// 		}
// 	}

// 	public float GetSpeed()
// 	{
// 		return speed;
// 	}

// 	private void Awake() {
// 		gridPosition = new Vector2Int(0,0);
// 		speed = 1f;
// 		gridMoveTimer = speed;
// 		gridMoveDirection = new Vector2Int(1,0);

// 		wormBodySize = 1;
// 		WormMovePositionList = new List<Vector2Int>();

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
// 		if (gridMoveTimer >= speed) {

//            gridMoveTimer -= speed;
//            WormMovePositionList.Insert(0, gridPosition);

//            gridPosition += gridMoveDirection;



//            if (WormMovePositionList.Count  >= wormBodySize + 1) {
// 				WormMovePositionList.RemoveAt(WormMovePositionList.Count - 1);
// 			}
// 			//for (int i = 0; i < WormMovePositionList.Count; i++) {
// 				//Vector2Int wormMovePosition = WormMovePositionList[i];

//                //World_Sprite worldSprite = World_Sprite.Create(new Vector3(wormMovePosition.x, wormMovePosition.y), Vector3.one * 0.5f, Color.white);
// 				//FunctionTimer.Create(worldSprite.DestroySelf, speed);

//            //}



// 			transform.position = new Vector3(gridPosition.x,gridPosition.y);
// 			transform.eulerAngles = new Vector3(0, 0, RotateSprite(gridMoveDirection) - 90);

// 			for (int i=0;i < wormBodyTransformList.Count; i++) {
// 				Vector3 wormBodyPosition = new Vector3(WormMovePositionList[i].x, WormMovePositionList[i].y);
// 				wormBodyTransformList[i].transform.eulerAngles = new Vector3(0,0,RotateSprite(gridMoveDirection) - 90);
// 				wormBodyTransformList[i].position = wormBodyPosition;
// 			}

// 			//levelGrid.wormMoved(gridPosition);
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
// 		gridPositionList.AddRange(WormMovePositionList);
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
    private float speed;
    private LevelGrid levelGrid;
    private int WormBodySize;
    private List<Vector2Int> WormMovePositionList;
    private List<WormBodyPart> WormBodyPartList;

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    public void ChangeSpeed(string type)
    {
        if (type == "point")
        {
            if ((speed - 0.05) > 0.04)
            {
                speed -= 0.05f;

                if (WormBodySize <= 5)
                {
                    WormBodySize += 1;
                    CreateWormBodyPart();
                }
            }

        }
        else if (type == "slow")
        {
            speed -= 0.5f;
        }
    }

    private void Awake()
    {
        gridPosition = new Vector2Int(0,0);
        speed = 1f;
        gridMoveTimer = speed;
        gridMoveDirection = new Vector2Int(1, 0);

        WormMovePositionList = new List<Vector2Int>();
        WormBodySize = 0;

        WormBodyPartList = new List<WormBodyPart>();
    }

    private void Update()
    {
        HandleInput();
        HandleGridMovement();
    }

    public float GetSpeed()
    {
        return speed;
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
        if (gridMoveTimer >= speed)
        {
            gridMoveTimer -= speed;

            WormMovePositionList.Insert(0, gridPosition);

            gridPosition += gridMoveDirection;

            if (WormMovePositionList.Count >= WormBodySize + 1)
            {
                WormMovePositionList.RemoveAt(WormMovePositionList.Count - 1);
            }

            /*
            foreach (WormBodyPart WormBodyPart in WormBodyPartList) 
            {
                Vector2Int WormBodyPartGridPosition = WormBodyPart.GetGridPosition();
                if (gridPosition == WormBodyPartGridPosition)
                {
                    //game over!!
                    CMDebug.TextPopup("dead", transform.position); 
                }
            }
            */
            //this is needed later for when worm dies (but wormMovePosition currently doesn't exist, so uncomment later on when needed)

            /*for (int i = 0; i < WormMovePositionList.Count; i++) {
                Vector2Int wormMovePosition = WormMovePositionList[i];
                World_Sprite worldSprite = World_Sprite.Create(new Vector3(wormMovePosition.x, wormMovePosition.y), Vector3.one * .5f, Color.white);
                FunctionTimer.Create(worldSprite.DestroySelf, speed);
            }*/

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);

            UpdateWormBodyParts();
        }
    }

    private void CreateWormBodyPart()
    {
        WormBodyPart newBodyPart = new WormBodyPart(WormBodyPartList.Count);
        newBodyPart.SetGridPosition(gridPosition);
        // WormBodyPartList.Add(new WormBodyPart(WormBodyPartList.Count));
        WormBodyPartList.Add(newBodyPart);
    }

    private void UpdateWormBodyParts()
    {
        for (int i = 0; i < WormBodyPartList.Count; i++)
        {
            WormBodyPartList[i].SetGridPosition(WormMovePositionList[i]);
            // if (i < WormMovePositionList.Count - 1)
            // {
            //     Vector2Int direction = WormMovePositionList[i] - WormMovePositionList[i + 1];
            //     WormBodyPartList[i].SetRotation(GetAngleFromVector(direction));
            // }

            if (gridMoveDirection.x == 0 && gridMoveDirection.y == 1) {
                WormBodyPartList[i].SetRotate("UP");
            }
            else if (gridMoveDirection.x == 0 && gridMoveDirection.y == -1) {
                WormBodyPartList[i].SetRotate("DOWN");
            }
            else if (gridMoveDirection.x == -1 && gridMoveDirection.y == 0) {
                WormBodyPartList[i].SetRotate("LEFT");
            }
            else if (gridMoveDirection.x == 1 && gridMoveDirection.y == 0) {
                WormBodyPartList[i].SetRotate("RIGHT");
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

    // Return the full list of positions occupied by the worm: Head + Body
    public List<Vector2Int> GetFullWormGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        gridPositionList.AddRange(WormMovePositionList);
        return gridPositionList;
    }

    private class WormBodyPart
    {

        private Vector2Int gridPosition;
        private Transform transform;

        public WormBodyPart(int bodyIndex)
        {
            GameObject wormBodyGameObject = new GameObject("wormBody", typeof(SpriteRenderer));
            wormBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.wormBody;
            //wormBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -1 - bodyIndex;
            wormBodyGameObject.transform.localScale = new Vector3(2, 2, 2);
            transform = wormBodyGameObject.transform;
        }

        public void SetGridPosition(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
        /*
        public Vector2Int GetGridPosition()
        {
            return wormMovePosition.GetGridPosition();
        }
        */
        //this is needed later for when worm dies (but wormMovePosition currently doesn't exist, so uncomment later on when needed)

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

