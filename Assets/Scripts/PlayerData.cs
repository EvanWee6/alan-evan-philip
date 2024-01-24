using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static PlayerData instance;

    public string playerName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}