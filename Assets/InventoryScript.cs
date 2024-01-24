using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    private int inventoryCount;
    public GameObject Worm;

    [SerializeField] public ParticleSystem powerUpAnimation;


    // Start is called before the first frame update
    void Start()
    {
        inventoryCount = 0;
    }

    public void Add()
    {
        inventoryCount += 1;
        Debug.Log($"you have: {inventoryCount} golden apples");
    }

    public void Use()
    {
        if (inventoryCount - 1 >= 0)
        {
            Worm.GetComponent<WormMovement>().ChangeSpeed("slow");
            powerUpAnimation.transform.position = Worm.transform.position;
            powerUpAnimation.Play();
            inventoryCount -= 1;
        }
        else
        {
            Debug.Log("Inventory Empty!");
        }
    }

    public int GetCount()
    {
        return inventoryCount;
    }

}
