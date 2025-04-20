using UnityEngine;

public class TimeScale_Item : Item
{
    [Header("Settings")]
    [SerializeField, Range(0.1f, 1f)]
    private float minTimeScale = 0.5f;

    [SerializeField, Range(1.1f, 3f)]
    private float slowdownFactor = 3f;

    protected override void UseItem()
    {
        float newTimeScale = TimeManipulator.instance.currentTimeScale / slowdownFactor;


        newTimeScale = Mathf.Max(minTimeScale, newTimeScale);


        TimeManipulator.instance.currentTimeScale = newTimeScale;
        Time.timeScale = newTimeScale;


        TimeManipulator.instance.UpdateUI();

        TimeManipulator.instance.lastAccelerationTime = Time.unscaledTime;

    }
}