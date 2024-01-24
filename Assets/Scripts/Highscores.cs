using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteKey("highscoreTable");
            Debug.Log("Leaderboard cleared!");
            Debug.Log(PlayerPrefs.GetString("highscoreTable"));
            PlayerPrefs.Save();
        }
    }
    
    public void Awake()
    {
        entryContainer = transform.Find("HighscoreInputContainer");
        entryTemplate = entryContainer.Find("HighscoreInputTemplate");

        if (entryContainer == null || entryTemplate == null)
        {
            Debug.LogError("Entry container or template not found!");
            return;
        }

        entryTemplate.gameObject.SetActive(false);



        // Add test entries
        //AddHighscoreEntry(100, "phil");
        //AddHighscoreEntry(200, "phil");
        //AddHighscoreEntry(300, "phil");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores();
            highscores.highscoreEntryList = new List<HighscoreEntry>();
        }

        // Sorting algorithm
        if (highscores.highscoreEntryList != null)
        {
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score ||
                        (highscores.highscoreEntryList[j].score == highscores.highscoreEntryList[i].score &&
                         string.Compare(highscores.highscoreEntryList[j].name, highscores.highscoreEntryList[i].name) < 0))
                    {
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }
        }

        // Trimming to top 10 entries
        if (highscores.highscoreEntryList.Count > 10)
        {
            highscores.highscoreEntryList = highscores.highscoreEntryList.Take(10).ToList();
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
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
        }

        entryTransform.Find("RankText").GetComponent<TMP_Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<TMP_Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<TMP_Text>().text = name;

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores();
            highscores.highscoreEntryList = new List<HighscoreEntry>();
        }

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        Debug.Log($"Adding Highscore Entry: Score - {score}, Name - {name}");
        Debug.Log($"Current Highscore Table: {json}");
    }

    [System.Serializable]
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
