using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int Damage;
    protected int knockbackDistance;
    [SerializeField]
    protected float liveTime;
    [SerializeField]
    protected bool isHostile;
    protected void Start()
    {
        if (isHostile)
        {
            gameObject.layer = LayerMask.NameToLayer("Hostile_projectile");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Player_projectile");
        }
        StartCoroutine(DestroyByTime());
    }
    public void Initialize(int damage, int knockback, bool isHostile)
    {
        this.Damage = damage;
        this.knockbackDistance = knockback;
        this.isHostile = isHostile;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && isHostile)
        {
            Vector2 knockBackDirection = (Player.instance.transform.position - transform.position).normalized;
            Player.instance.TakeKnockBack(knockBackDirection, knockbackDistance);
            Player.instance.TakeDamage(Damage);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !isHostile)
        {
            Entity target = collision.gameObject.GetComponent<Entity>();
            target.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
    protected IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(gameObject);
    }
}
