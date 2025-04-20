using UnityEngine;

public class TimeScale_Item : Item
{
    [SerializeField]
    private float timeDelta;
    protected override void UseItem()
    {
        TimeManipulator.instance.currentTimeScale -= timeDelta;
        TimeManipulator.instance.AccelerateTime();
    }
}
