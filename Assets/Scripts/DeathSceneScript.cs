using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathSceneScript : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        // Retrieve the final score from PlayerPrefs
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        // Display the final score on the screen
        finalScoreText.text = $"Final Score: {finalScore}";
    }
// Update is called once per frame
    void Update()
    {
        
    }
}
