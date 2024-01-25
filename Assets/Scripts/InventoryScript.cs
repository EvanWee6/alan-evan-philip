using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    private int inventoryCount;
    public GameObject Worm;

	private string[] SkinInventory = new string[0];
    
	private static InventoryScript instance;

    [SerializeField] public ParticleSystem powerUpAnimation;


    // Start is called before the first frame update
    void Start()
    {
        inventoryCount = 0;
		// SkinInventory = new string[0];

        ReadSkins();
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

		//foreach(string i in this.SkinInventory) {
		//	Debug.Log(i);
		//}

	}

    public string ReadSkins()
    {
        string skins = System.IO.File.ReadAllText("skins.txt");

        // foreach (string skin in skins)
        // {
        //     Debug.Log(skin);
        //     AddSkin(skin);    
        // }

        AddSkin(skins);
        Debug.Log(SkinInventory[SkinInventory.Length - 1]);
        return SkinInventory[SkinInventory.Length-1];
    }

    public string[] GetSkins() {
       return this.SkinInventory;
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
