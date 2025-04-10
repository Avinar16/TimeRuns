using UnityEngine;
using UnityEngine.UI;

public abstract class Timer : MonoBehaviour
{
    [Header("Base Timer Settings")]
    public float TotalTime = 180f;
    public float CurrentTime;
    public Text TimerText;

    protected bool IsRunning = true;

    protected virtual void Start()
    {
        CurrentTime = TotalTime;
    }

    protected virtual void Update()
    {
        if (!IsRunning) return;

        CurrentTime -= Time.deltaTime;
        UpdateTimerDisplay();

        if (CurrentTime <= 0)
        {
            CurrentTime = 0;
            OnTimerEnd();
        }
    }

    protected void UpdateTimerDisplay()
    {
        if (TimerText != null)
        {
            int minutes = Mathf.FloorToInt(CurrentTime / 60f);
            int seconds = Mathf.FloorToInt(CurrentTime % 60f);
            TimerText.text = $"{minutes:00}:{seconds:00}";

            if (CurrentTime <= 30f)
            {
                TimerText.color = new Color(1, 0, 0, Mathf.PingPong(Time.unscaledTime, 1));
            }
        }
    }

    public abstract void OnTimerEnd();
}