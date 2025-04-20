using UnityEngine;

public class LevelUp_item : Item
{
    protected override void UseItem()
    {
        Player.instance.LevelUp();
    }

}
