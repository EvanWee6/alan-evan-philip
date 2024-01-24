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

	
	public GameObject Score;
    public GameObject Timer;

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
            speed += 0.07f;
            Debug.Log($"Slowing speed; speed now = {speed}");

            StartCoroutine(ResetSpeedAfterDelay((speed - 0.07f), 3f));
        }
    }

    private IEnumerator ResetSpeedAfterDelay(float originalSpeed, float delay) {
        yield return new WaitForSeconds(delay);
        speed = originalSpeed;
        Debug.Log($"Back to original speed: {originalSpeed}");
    }

    void Start() {
        Timer.GetComponent<TimerScript>().StartTimer();
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

    public void onLose() {
        Timer.GetComponent<TimerScript>().EndTimer();
        float time = Timer.GetComponent<TimerScript>().GetTime();

        gridMoveDirection = new Vector2Int(0,0);
        Debug.Log("You lose!");
        transform.position = new Vector3(0,0);
        for (int i=0;i<WormBodyPartList.Count;i++) {
            WormBodyPartList[i].SetGridPosition(new Vector2Int(0,0));
        }
		Debug.ClearDeveloperConsole();
		Debug.Log($"Final Stats\n score:{Score.GetComponent<ScoreListener>().GetScore()}\ntime: {time}");
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

    public void SetSpeed(int n) {
        speed = n;
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

            //if (gridMoveDirection.x == 0 && gridMoveDirection.y == 1) {
            //    WormBodyPartList[i].SetRotate("UP");
            //}
            //else if (gridMoveDirection.x == 0 && gridMoveDirection.y == -1) {
            //    WormBodyPartList[i].SetRotate("DOWN");
            //}
            //else if (gridMoveDirection.x == -1 && gridMoveDirection.y == 0) {
            //    WormBodyPartList[i].SetRotate("LEFT");
            //}
            //else if (gridMoveDirection.x == 1 && gridMoveDirection.y == 0) {
            //    WormBodyPartList[i].SetRotate("RIGHT");
            //}
            
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
            wormBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
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

