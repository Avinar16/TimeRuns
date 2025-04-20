using UnityEngine;
using System.Collections.Generic;

public class ItemList: MonoBehaviour
{
    [SerializeField]
    public GameObject Hp;

    [SerializeField]
    public GameObject Level;

    [SerializeField]
    public GameObject Health;

    private List<GameObject> items;

    static public ItemList instance;
    public GameObject ChooseRandomItem()
    {
        int random = Random.Range(0, items.Count);
        return items[random];
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        items = new List<GameObject>() { Hp, Level, Health };
    }
}
