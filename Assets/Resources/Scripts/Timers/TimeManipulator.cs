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
    [SerializeField] private Color highlightColor = Color.yellow;

    public float currentTimeScale;
    public float lastAccelerationTime;
    private Color originalColor;
    private bool isPaused;

    public static TimeManipulator instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        currentTimeScale = initialTimeScale;
        Time.timeScale = currentTimeScale;
        originalColor = speedIndicator.color;
        UpdateUI();
    }

    private void Update()
    {
        if (isPaused) return;

        if (Time.unscaledTime - lastAccelerationTime >= accelerationInterval)
        {
            AccelerateTime();
            lastAccelerationTime = Time.unscaledTime;
        }

        if (speedIndicator.color != originalColor)
        {
            speedIndicator.color = Color.Lerp(speedIndicator.color, originalColor, Time.unscaledDeltaTime * 5f);
        }
    }

    public void AccelerateTime()
    {
        currentTimeScale = Mathf.Min(currentTimeScale + accelerationStep, maxTimeScale);
        Time.timeScale = currentTimeScale;
        speedIndicator.color = highlightColor;
        UpdateUI();
    }

    public void UpdateUI()
    {
        speedIndicator.text = $"Speed: {Time.timeScale:0.0}x";
    }

    public void SetPause(bool pause)
    {
        isPaused = pause;
        Time.timeScale = pause ? 0f : currentTimeScale;
    }
}