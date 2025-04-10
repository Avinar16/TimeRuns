using UnityEngine;
using UnityEngine.UI;

public class TimeManipulator : MonoBehaviour
{
    [Header("Time Acceleration Settings")]
    public float initialTimeScale = 1f;    // Normal speed = 1.0
    public float increaseInterval = 3f;    // Every 3 seconds
    public float increaseAmount = 0.1f;    // +10% speed
    public float maxTimeScale = 3f;        // Max 3x speed

    [Header("Speed UI")]
    public Text speedText;

    private float lastIncreaseTime;
    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = initialTimeScale;
        lastIncreaseTime = Time.unscaledTime;
        UpdateSpeedUI();
    }

    private void Update()
    {
        if (isPaused) return;

        if (Time.unscaledTime - lastIncreaseTime >= increaseInterval)
        {
            IncreaseSpeed();
            lastIncreaseTime = Time.unscaledTime;
        }
    }

    private void IncreaseSpeed()
    {
        Time.timeScale = Mathf.Min(Time.timeScale + increaseAmount, maxTimeScale);
        UpdateSpeedUI();
    }

    private void UpdateSpeedUI()
    {
        if (speedText != null)
        {
            speedText.text = $"Speed: {Time.timeScale:F1}x";
        }
    }

    public void SetPause(bool pause)
    {
        isPaused = pause;
        Time.timeScale = pause ? 0f : initialTimeScale;
        UpdateSpeedUI();
    }
}