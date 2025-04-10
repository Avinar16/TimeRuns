using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool _isGameRunning = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver(bool playerWon)
    {
        _isGameRunning = false;
        Debug.Log(playerWon ? "Победа!" : "Поражение!");
    }
}