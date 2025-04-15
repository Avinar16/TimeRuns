using UnityEngine;

public abstract class Timer
{
    private readonly float totalTime;
    private float currentTime;
    private bool isRunning;

    public float CurrentTime => currentTime;
    public bool IsRunning => isRunning;

    protected Timer(float totalTime)
    {
        this.totalTime = Mathf.Max(0, totalTime);
        currentTime = this.totalTime;
        isRunning = true;
    }

    public virtual void Tick(float deltaTime)
    {
        if (!isRunning) return;

        currentTime = Mathf.Max(0, currentTime - deltaTime);

        if (currentTime <= 0)
        {
            isRunning = false;
            OnTimerEnd();
        }
    }

    public void Reset() => currentTime = totalTime;
    public void SetPaused(bool paused) => isRunning = !paused;

    protected abstract void OnTimerEnd();
}