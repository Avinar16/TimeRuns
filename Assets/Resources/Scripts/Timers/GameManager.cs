using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isGameOver;

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
        Player.instance.OnDeath += GameOver;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("FAILURE");
    }
}