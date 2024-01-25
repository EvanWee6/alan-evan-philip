using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class WormMovement : MonoBehaviour
{
    [SerializeField] private Highscore highscoreTable;
    [SerializeField] private UI_InputWindow inputWindow;
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    private enum State
    {
        Alive,
        Dead
    }

    private State state;
    private Direction gridMoveDirection;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float speed;
    private LevelGrid levelGrid;
    private int WormBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> WormBodyPartList;

    public GameObject Score;
    public GameObject Timer;
    public ScoreScript scorescript;

    private string playerName;
    private int finalScore;
    
    // [SerializeField] public GameObject Inventory;
    // [SerializeField] private InventoryScript Inventory;

    public GameObject Inventory;

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
        scorescript = GameObject.FindObjectOfType<ScoreScript>();
    }

    //Mine 

    // void Start() {
    //     Inventory = new InventoryScript();
    // }
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

    //public static string GetSkin()
    //{
    //    return Inventory.GetComponent<InventoryScript>().ReadSkins();
    //}

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    private IEnumerator ResetSpeedAfterDelay(float originalSpeed, float delay)
    {
        yield return new WaitForSeconds(delay);
        speed = originalSpeed;
        Debug.Log($"Back to original speed: {originalSpeed}");
    }

    public string GetSkin()
    {
        return Inventory.GetComponent<InventoryScript>().ReadSkins();
    }

    public void onLose()
    {
        state = State.Dead;

        Timer.GetComponent<TimerScript>().EndTimer();
        float time = Timer.GetComponent<TimerScript>().GetTime();

        int finalScore = Score.GetComponent<ScoreListener>().GetScore();

        Debug.ClearDeveloperConsole();
        Debug.Log($"Final Stats\n score:{finalScore}\ntime: {time}");

        // Pass the playerName to the Highscore script
        UI_InputWindow uI_InputWindow = FindObjectOfType<UI_InputWindow>();
        if (uI_InputWindow != null)
        {
            string playerName = uI_InputWindow.GetPlayerName();
            highscoreTable.AddHighscoreEntry(finalScore, playerName);
        }

        SceneManager.LoadScene("DeathScene");
    }


    public void SelectSkins() {
        // if (Inventory.GetComponent<InventoryScript>().GetSkins()[0] == "Blue") {
        //     Debug.Log("Chose Blue SKin!");
        // }
        // else {
        //     Debug.Log("Can't find any skins");
        // }

        //Debug.Log(Inventory.GetComponent<InventoryScript>().ReadSkins());
        if (Inventory.GetComponent<InventoryScript>().ReadSkins() == "Blue")
        {
            this.GetComponent<SpriteRenderer>().sprite = GameAssets.i.BwormHead;
        } 
        else if (Inventory.GetComponent<InventoryScript>().ReadSkins() == "green") {
            this.GetComponent<SpriteRenderer>().sprite = GameAssets.i.GwormHead;
        }
        else if (Inventory.GetComponent<InventoryScript>().ReadSkins() == "white") {
            this.GetComponent<SpriteRenderer>().sprite = GameAssets.i.WwormHead;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = GameAssets.i.wormHead;
        }

    }

    public float GetSpeed()
    {
        return speed;
    }
    //Mine ends

    private void UpdateSpeed()
    {
        int currentSpeed = (int)(1 / speed);  // Assuming speed is a float representing time between moves
        scorescript.UpdateSpeedText(currentSpeed);
    }
    //I followed a tutorial for the movement
    private void Awake()
    {

        SelectSkins();

        // Inventory = new InventoryScript();
        
        // Inventory.GetComponent<InventoryScript>().GetSkins();
       
        Debug.Log($"Player Name: {playerName}");
        /*
        UI_InputWindow inputWindow = FindObjectOfType<UI_InputWindow>();
        if (inputWindow != null)
        {
            playerName = inputWindow.GetPlayerName();
        }
        else
        {
            playerName = "NULL";
        }
        */

        gridPosition = new Vector2Int(0, 0);
        speed = .5f;
        gridMoveTimer = speed;
        gridMoveDirection = Direction.Right;

        transform.localScale = new Vector3Int(3, 3, 3);

        snakeMovePositionList = new List<SnakeMovePosition>();
        WormBodySize = 0;

        WormBodyPartList = new List<SnakeBodyPart>();

        state = State.Alive;

    }

    private void Update()
    {
        switch (state)
        {
            case State.Alive:
                HandleInput();
                HandleGridMovement();
                UpdateSpeed();
                break;
            case State.Dead:
                break;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gridMoveDirection != Direction.Down)
            {
                gridMoveDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gridMoveDirection != Direction.Up)
            {
                gridMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (gridMoveDirection != Direction.Right)
            {
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (gridMoveDirection != Direction.Left)
            {
                gridMoveDirection = Direction.Right;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= speed)
        {
            gridMoveTimer -= speed;

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }

            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Direction.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
                case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                case Direction.Up: gridMoveDirectionVector = new Vector2Int(0, +1); break;
                case Direction.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;
            }

            gridPosition += gridMoveDirectionVector;

            if (snakeMovePositionList.Count >= WormBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            UpdateSnakeBodyParts();

            foreach (SnakeBodyPart snakeBodyPart in WormBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (gridPosition == snakeBodyPartGridPosition)
                {
                    // Game Over!
                    CMDebug.TextPopup("DEAD!", transform.position);
                    state = State.Dead;
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);
        }
    }

    private void CreateWormBodyPart()
    {
        WormBodyPartList.Add(new SnakeBodyPart(WormBodyPartList.Count, Inventory.GetComponent<InventoryScript>()));
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < WormBodyPartList.Count; i++)
        {
            WormBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
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
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }




    /*
     * Handles a Single Snake Body Part
     * */
    private class SnakeBodyPart
    {

        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        private InventoryScript inventory;

        public SnakeBodyPart(int bodyIndex, InventoryScript inventory)
        {
            this.inventory = inventory;

            if (inventory.ReadSkins() == "Blue") {
                GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
                snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.BwormBody;
                snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                snakeBodyGameObject.GetComponent<SpriteRenderer>().transform.localScale = new Vector3Int(3, 3, 3);
                transform = snakeBodyGameObject.transform;
            }
            else if (inventory.ReadSkins() == "green") {
                GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
                snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.GwormBody;
                snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                snakeBodyGameObject.GetComponent<SpriteRenderer>().transform.localScale = new Vector3Int(3, 3, 3);
                transform = snakeBodyGameObject.transform;
            }
            else if (inventory.ReadSkins() == "white") {
                GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
                snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.WwormBody;
                snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                snakeBodyGameObject.GetComponent<SpriteRenderer>().transform.localScale = new Vector3Int(3, 3, 3);
                transform = snakeBodyGameObject.transform; 
            }
            else {
                GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
                snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.wormBody;
                snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                snakeBodyGameObject.GetComponent<SpriteRenderer>().transform.localScale = new Vector3Int(3, 3, 3);
                transform = snakeBodyGameObject.transform; 
            }
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;

            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Up: // Currently going Up
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0;
                            break;
                        case Direction.Left: // Previously was going Left
                            angle = 0 + 45;
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Right: // Previously was going Right
                            angle = 0 - 45;
                            transform.position += new Vector3(-.2f, .2f);
                            break;
                    }
                    break;
                case Direction.Down: // Currently going Down
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180;
                            break;
                        case Direction.Left: // Previously was going Left
                            angle = 180 - 45;
                            transform.position += new Vector3(.2f, -.2f);
                            break;
                        case Direction.Right: // Previously was going Right
                            angle = 180 + 45;
                            transform.position += new Vector3(-.2f, -.2f);
                            break;
                    }
                    break;
                case Direction.Left: // Currently going to the Left
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = +90;
                            break;
                        case Direction.Down: // Previously was going Down
                            angle = 180 - 45;
                            transform.position += new Vector3(-.2f, .2f);
                            break;
                        case Direction.Up: // Previously was going Up
                            angle = 45;
                            transform.position += new Vector3(-.2f, -.2f);
                            break;
                    }
                    break;
                case Direction.Right: // Currently going to the Right
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90;
                            break;
                        case Direction.Down: // Previously was going Down
                            angle = 180 + 45;
                            transform.position += new Vector3(.2f, .2f);
                            break;
                        case Direction.Up: // Previously was going Up
                            angle = -45;
                            transform.position += new Vector3(.2f, -.2f);
                            break;
                    }
                    break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }
    }



    /*
     * Handles one Move Position from the Snake
     * */
    private class SnakeMovePosition
    {

        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
        }

    }

}
