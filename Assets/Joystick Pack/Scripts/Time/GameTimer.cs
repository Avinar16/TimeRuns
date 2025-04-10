using UnityEngine;

public class GameTimer : Timer
{
    public override void OnTimerEnd()
    {
        IsRunning = false;
        GameManager.Instance.GameOver(true);
    }
}