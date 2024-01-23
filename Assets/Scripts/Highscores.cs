using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    //private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransfromList;

    public void Awake()
    {
        entryContainer = transform.Find("HighscoreInputContainer");
        entryTemplate = entryContainer.Find("HighscoreInputTemplate");

        entryTemplate.gameObject.SetActive(false);

        AddHighscoreEntry(1000, "gojo");
        AddHighscoreEntry(10000, "geto");
        AddHighscoreEntry(9999, "bob");
        AddHighscoreEntry(999999, "k");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores();
            highscores.highscoreEntryList = new List<HighscoreEntry>();
        }

        // Sorting algorithm
        //sorting algorithm, chatgpt fixed error
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score || (highscores.highscoreEntryList[j].score == highscores.highscoreEntryList[i].score &&
                     string.Compare(highscores.highscoreEntryList[j].name, highscores.highscoreEntryList[i].name) < 0))
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        // Trimming to top 10 entries
        if (highscores.highscoreEntryList.Count > 10)
        {
            highscores.highscoreEntryList = highscores.highscoreEntryList.Take(10).ToList();
        }

        highscoreEntryTransfromList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransfrom(highscoreEntry, entryContainer, highscoreEntryTransfromList);
        }
    }

    private void CreateHighscoreEntryTransfrom(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 23.4f;
    
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight - 30 * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        } //tutorial

        entryTransform.Find("RankText").GetComponent<TMP_Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("ScoreText").GetComponent<TMP_Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<TMP_Text>().text = name;

        transformList.Add(entryTransform);
        
    }

    private void AddHighscoreEntry(int score, string name)
    {
        //creates highscore entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        //loads the saved entries
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //adds the new entry to the highscores
        highscores.highscoreEntryList.Add(highscoreEntry);


        //saves the updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    

    //this is for each individual highscore entry
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;

    }

}
