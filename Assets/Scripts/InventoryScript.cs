using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    private int inventoryCount;
    public GameObject Worm;

	private string[] SkinInventory;

    [SerializeField] public ParticleSystem powerUpAnimation;


    // Start is called before the first frame update
    void Start()
    {
        inventoryCount = 0;
		string[] SkinInventory = new string[0];
    }

    public void Add()
    {
        inventoryCount += 1;
        Debug.Log($"you have: {inventoryCount} golden apples");
    }

	public void AddSkin(string skin) {
		string[] tmpInventory = new string[this.SkinInventory.Length +1];

		tmpInventory[tmpInventory.Length -1] = skin;

		this.SkinInventory = tmpInventory;

		foreach(string i in this.SkinInventory) {
			Debug.Log(i);
		}

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
