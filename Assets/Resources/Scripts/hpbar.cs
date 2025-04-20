using UnityEngine;
using UnityEngine.UI;
public class hpbar : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        text.text = $"HP: {Player.instance.health}/{Player.instance.MaxHealth}";
    }

}
