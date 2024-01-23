using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{

    public GameObject Worm;

    // Update is called once per frame
    void Update()
    {

        if (Worm.transform.position.x < -15 || Worm.transform.position.x > 19 || Worm.transform.position.y < -9 || Worm.transform.position.y > 9)
        {
            Debug.Log("Out of Range");
            
        }
        
    }
}
