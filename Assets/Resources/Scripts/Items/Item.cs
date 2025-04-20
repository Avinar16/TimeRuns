using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected abstract void UseItem();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UseItem();
            Destroy(gameObject);
        }
        
    }

}
