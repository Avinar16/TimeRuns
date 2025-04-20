using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(60, 300)] private float gameDuration = 180f;
    [SerializeField] private Text timerText;
    [SerializeField] private Color warningColor = Color.red;
    [SerializeField] private float pulseIntensity = 0.2f;
    [SerializeField] private float pulseSpeed = 2f;

    [Header("Gradient")]
    public Gradient timerGradient;

    private Timer timer;
    private Color defaultColor;
    private Vector3 originalScale;

    private void Start()
    {
        timer = new GameTimerImplementation(gameDuration);
        defaultColor = timerText.color;
        originalScale = timerText.transform.localScale;
    }

    private void Update()
    {
        if (!timer.IsRunning) return;

        timer.Tick(Time.deltaTime);
        UpdateUI();

        if (timer.CurrentTime <= 30f)
        {
            float scale = 1f + Mathf.PingPong(Time.unscaledTime * pulseSpeed, pulseIntensity);
            timerText.transform.localScale = originalScale * scale;
            timerText.color = Color.Lerp(defaultColor, warningColor, Mathf.PingPong(Time.unscaledTime * 2, 1));
        }
        else
        {
            timerText.transform.localScale = originalScale;
            timerText.color = defaultColor;
        }

        if (timer.CurrentTime <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(timer.CurrentTime / 60);
        int seconds = Mathf.FloorToInt(timer.CurrentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
        float progress = timer.CurrentTime / gameDuration;
        timerText.color = timerGradient.Evaluate(progress);
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