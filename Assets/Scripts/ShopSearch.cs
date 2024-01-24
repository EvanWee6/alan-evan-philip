using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ShopSearch : MonoBehaviour
{
    public TMP_InputField searchInputField;
    public List<GameObject> allShopItems;

    private void Start()
    {
        searchInputField.onValueChanged.AddListener(delegate { Search(); });
        UpdateDisplay(allShopItems); // Display all items initially
    }

    private void Search()
    {
        string searchText = searchInputField.text.ToLower(); // Convert to lowercase so its case-insensitive while searching

        List<GameObject> relevantItems = new List<GameObject>();

        // searching algo!!
        foreach (GameObject item in allShopItems)
        {
            TextMeshProUGUI itemNameTMP = item.GetComponentInChildren<TextMeshProUGUI>();

            if (itemNameTMP != null)
            {
                string itemName = itemNameTMP.text.ToLower();

                if (itemName.Contains(searchText))
                {
                    relevantItems.Add(item);
                }
            }
        }

        UpdateDisplay(relevantItems);
    }

    private void UpdateDisplay(List<GameObject> itemsToDisplay)
    {
        // Hide all items
        foreach (GameObject item in allShopItems)
        {
            item.SetActive(false);
        }

        // Show relevant items
        foreach (GameObject item in itemsToDisplay)
        {
            item.SetActive(true);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
