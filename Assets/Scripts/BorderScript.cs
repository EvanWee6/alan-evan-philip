using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{

    public GameObject Worm;
	public GameObject Coins;

    private bool lost = false;

    void Lose() {
        Worm.GetComponent<WormMovement>().onLose();
		Coins.GetComponent<CoinListener>().WriteCoins();
    }

    // Update is called once per frame
    void Update()
    {
        if (lost == false) {

            if (Worm.transform.position.x < -15 || Worm.transform.position.x > 19 || Worm.transform.position.y < -9 || Worm.transform.position.y > 9)
                {
                    Debug.Log("Out of Range");
                    // Worm.transform.position = new Vector3(0,0,0);
                    Lose();         
                    lost = true;
                }
        }
        
        
    }
}
