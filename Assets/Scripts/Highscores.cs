using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransfromList;

    public void Awake()
    {


        entryContainer = transform.Find("HighscoreInputContainer");
        entryTemplate = entryContainer.Find("HighscoreInputTemplate");

        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{score = 113, name = "xojoballs"},
            new HighscoreEntry{score = 113, name = "cojoballs"},
            new HighscoreEntry{score = 320, name = "jojoballs"},
            new HighscoreEntry{score = 413, name = "gojoballs"},
            new HighscoreEntry{score = 510, name = "hojoballs"},
            new HighscoreEntry{score = 630, name = "gojoballs"},
            new HighscoreEntry{score = 720, name = "gojoballs"},
            new HighscoreEntry{score = 810, name = "ojoballs"},
            new HighscoreEntry{score = 910, name = "Lojoballs"},
            new HighscoreEntry{score = 160, name = "gojoballs"},
        };

        //sorting algorithm, chatgpt fixed error
        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].score > highscoreEntryList[i].score || (highscoreEntryList[j].score == highscoreEntryList[i].score &&
                     string.Compare(highscoreEntryList[j].name, highscoreEntryList[i].name) < 0))
                {
                    HighscoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransfromList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransfrom(highscoreEntry, entryContainer, highscoreEntryTransfromList);
        }

        //Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscoreEntryList);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));


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

    /*
    private class Highscore
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    */

    [System.Serializable]
    //this is for each individual highscore entry
    private class HighscoreEntry
    {
        public int score;
        public string name;

    }

}
