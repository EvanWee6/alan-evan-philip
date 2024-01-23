using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenAppleScript : MonoBehaviour
{

    public GameObject Worm;
    public GameObject Score;

    private bool onMap;

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
        transform.position = new Vector3Int(-40, 0);
        this.GetComponent<SpriteRenderer>().sortingOrder = 1;
        transform.localScale = new Vector3Int(3, 3, 3);

        onMap = false;
    }

    public void ChanceSpawn()
    {
        if (Score.GetComponent<ScoreListener>().GetScore() >= 5) {

            int n = Random.Range(1, 6);

            if (n == 1)
            {
                transform.position = new Vector3Int((Random.Range(-15, 19)), (Random.Range(-9, 9)));
                onMap = true;
            }
        }
    }

    void SlowSpeed()
    {
        Debug.Log("Slowing Speed");
        Worm.GetComponent<WormMovement>().ChangeSpeed("slow");
    }


    // Update is called once per frame
    void Update()
    {

        if (Worm.transform.position.x == transform.position.x && Worm.transform.position.y == transform.position.y) {
            
                onMap = false;
                SlowSpeed();
			    transform.position = new Vector3Int(-40,0);
            
        }
    }
}
