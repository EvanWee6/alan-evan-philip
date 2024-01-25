using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class BuyingScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skin;
    public void Buy()
    {

		string path = "skins.txt";
    Debug.Log($"Buying {skin.text}");
		File.WriteAllText(path, skin.text);
	
    }
}
