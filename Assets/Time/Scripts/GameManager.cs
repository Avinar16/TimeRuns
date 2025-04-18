using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool isGameOver;

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

    public void GameOver(bool isVictory)
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Debug.Log(isVictory ? "VICTORY!" : "DEFEAT!");
    }
}