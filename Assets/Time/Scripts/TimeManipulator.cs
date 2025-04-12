using UnityEngine;
using UnityEngine.UI;

public class TimeManipulator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(1f, 5f)] private float maxTimeScale = 3f;
    [SerializeField, Range(0.1f, 1f)] private float initialTimeScale = 1f;
    [SerializeField, Range(1f, 10f)] private float accelerationInterval = 3f;
    [SerializeField, Range(0.05f, 0.3f)] private float accelerationStep = 0.1f;

    [Header("UI")]
    [SerializeField] private Text speedIndicator;

    private float currentTimeScale;
    private float lastAccelerationTime;

    private void Start()
    {
        currentTimeScale = initialTimeScale;
        Time.timeScale = currentTimeScale;
        UpdateUI();
    }

    private void Update()
    {
        if (Time.unscaledTime - lastAccelerationTime >= accelerationInterval)
        {
            AccelerateTime();
            lastAccelerationTime = Time.unscaledTime;
        }
    }

    private void AccelerateTime()
    {
        currentTimeScale = Mathf.Min(
            currentTimeScale + accelerationStep,
            maxTimeScale
        );
        Time.timeScale = currentTimeScale;
        UpdateUI();
    }

    private void UpdateUI()
    {
        speedIndicator.text = $"Speed: {Time.timeScale:0.0}x";
    }
}