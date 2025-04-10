using UnityEngine;
using UnityEngine.UI;

public class TimeManipulator : MonoBehaviour
{
    [Header("Настройки ускорения")]
    public float initialTimeScale = 1f;    // Начальная скорость (1 = нормальная)
    public float increaseInterval = 3f;    // Ускорение каждые 3 секунды
    public float increaseAmount = 0.5f;    // ускорение на 10%
    public float maxTimeScale = 3f;        // Максимальное ускорение (3x)

    [Header("Текст скорости")]
    public Text speedText;

    private float lastIncreaseTime;

    private void Start()
    {
        Time.timeScale = initialTimeScale;
        lastIncreaseTime = Time.time;
        UpdateSpeedUI();
    }

    private void Update()
    {
        if (Time.time - lastIncreaseTime >= increaseInterval)
        {
            IncreaseSpeed();
            lastIncreaseTime = Time.time;
        }
    }

    private void IncreaseSpeed()
    {
        Time.timeScale += increaseAmount;
        Time.timeScale = Mathf.Min(Time.timeScale, maxTimeScale); // ограничение 
        UpdateSpeedUI();
    }

    private void UpdateSpeedUI()
    {
        if (speedText != null)
        {
            speedText.text = $"Скорость: {Time.timeScale:F1}x";
        }
    }
    public void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0f : initialTimeScale;
        UpdateSpeedUI();
    }
}