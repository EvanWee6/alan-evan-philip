using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyingScript : MonoBehaviour
{

    public GameObject Inventory;
    [SerializeField] private TextMeshProUGUI skin;
    public void Buy()
    {
        Inventory.GetComponent<InventoryScript>().AddSkin(skin.text);
    }
}
