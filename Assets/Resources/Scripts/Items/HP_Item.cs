using UnityEngine;

public class HP_Item : Item
{
    [SerializeField]
    private int health;

    protected override void UseItem()
    {
        if(Player.instance.health < Player.instance.MaxHealth)
        {
            Player.instance.health += health;
            if(Player.instance.health > Player.instance.MaxHealth)
            {
                Player.instance.health = Player.instance.MaxHealth;
            }
        }
    }
}
