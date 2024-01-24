using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI_InputWindow : MonoBehaviour
{
    private const string playerNameKey = "PlayerName";

    [SerializeField] private Highscore highscoreTable;
    [SerializeField] private WormMovement wormMovement;

    private TMP_InputField inputField;

    private void Start()
    {
        inputField = FindObjectOfType<TMP_InputField>();
        LoadPlayerName(); // Load playerName from PlayerData
    }

    private void LoadPlayerName()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (playerData != null)
        {
            if (!string.IsNullOrEmpty(playerData.playerName))
            {
                Debug.Log($"Loaded Player Name: {playerData.playerName}");
            }
        }
    }

    public void Back()
    {
        SavePlayerName(); // Save playerName to PlayerData before changing scene
        SceneManager.LoadScene("mainmenu");
    }

    public void Okay()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (playerData != null)
        {
            playerData.playerName = inputField.text;
            SavePlayerName(); // Save playerName to PlayerData before changing scene
            SceneManager.LoadScene("MainScene");
        }
    }

    private void SavePlayerName()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (playerData != null)
        {
            Debug.Log($"Player Name Saved: {playerData.playerName}");
        }
    }

    public string GetPlayerName()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        return playerData != null ? playerData.playerName : "";
    }
}