using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(60, 300)] private float gameDuration = 180f;
    [SerializeField, Tooltip("Text component for timer display")]
    private Text timerText;
    [SerializeField] private Color warningColor = Color.red;

    private Timer timer;
    private Color defaultColor;

    private void Start()
    {
        timer = CreateTimer(gameDuration);
        defaultColor = timerText.color;
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
        UpdateUI();
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(timer.CurrentTime / 60);
        int seconds = Mathf.FloorToInt(timer.CurrentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";

        if (timer.CurrentTime <= 30f)
        {
            float lerpValue = Mathf.PingPong(Time.unscaledTime * 2, 1);
            timerText.color = Color.Lerp(defaultColor, warningColor, lerpValue);
        }
    }

    private Timer CreateTimer(float duration)
    {
        return new GameTimerImplementation(duration);
    }

    private class GameTimerImplementation : Timer
    {
        public GameTimerImplementation(float duration) : base(duration) { }

        protected override void OnTimerEnd()
        {
            Debug.Log("Win ili che");
        }
    }
}