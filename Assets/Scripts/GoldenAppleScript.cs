using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenAppleScript : MonoBehaviour
{

    public GameObject Worm;
    public GameObject Score;
    public GameObject Inventory;

    private bool onMap;

    GoldenApple goldenApple = new GoldenApple();

    public bool checkOnMap()
    {
        if (onMap == true)
        {
            return true;
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {

        goldenApple.AppleInit();
        goldenApple.transform.localScale = new Vector3Int(3, 3, 3);
        goldenApple.transform.position = new Vector3Int(-40, 0);
        // this.GetComponent<SpriteRenderer>().sortingOrder = 1;
        // transform.localScale = new Vector3Int(3, 3, 3);

        onMap = false;
    }

    public void ChanceSpawn()
    {
        if (Score.GetComponent<ScoreListener>().GetScore() >= 5)
        {

            int n = Random.Range(1, 6);

            if (n == 1)
            {
                goldenApple.changePosition();
                // transform.position = new Vector3Int((Random.Range(-15, 19)), (Random.Range(-9, 9)));
                onMap = true;
                
            }
        
        }
    }

    void SlowSpeed()
    {
        Debug.Log("Slowing Speed");
        //Worm.GetComponent<WormMovement>().ChangeSpeed("slow");
        Inventory.GetComponent<InventoryScript>().Add();

    }


    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (Worm.transform.position.x == goldenApple.transform.position.x && Worm.transform.position.y == goldenApple.transform.position.y)
        {

            onMap = false;
            SlowSpeed();
            goldenApple.transform.position = new Vector3Int(-40, 0);
            if (Inventory != null)
            {
                int goldenCount = Inventory.GetComponent<InventoryScript>().GetCount();
                if (Score != null && Score.GetComponent<ScoreScript>() != null)
                {
                    Score.GetComponent<ScoreScript>().UpdateGoldenAppleCount(goldenCount);
                }
            }
            //if (Score != null && Score.GetComponent<ScoreScript>() != null)
            //{
            //Score.GetComponent<ScoreScript>().UpdateGoldenAppleCount(Score.GetComponent<ScoreScript>().golden + 1);
            //}
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Inventory.GetComponent<InventoryScript>().Use();

        }


    }
}
