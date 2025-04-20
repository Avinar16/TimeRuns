using UnityEngine;

public class Enemy: Entity
{
    [SerializeField]
    protected float MaxFollowDistanse;
    [SerializeField]
    [Range(0, 10)]
    int collisionDamage;

    [SerializeField]
    float KnockBackDistance;

    protected virtual void FixedUpdate()
    {
        Move();
    }
    private void Start()
    {
        OnDeath += () => AudioManager.Instance.PlaySFX("EnemyDeath");
    }
    protected override void Move()
    {
        Vector3 PlayerPosition = Player.instance.transform.position;
        Vector2 movementVector = new Vector2((PlayerPosition.x - transform.position.x), PlayerPosition.y - transform.position.y).normalized;
        if (Vector3.Distance(PlayerPosition, transform.position) > MaxFollowDistanse)
        {
            rb.linearVelocity = movementVector * speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 knockBackDirection = (Player.instance.transform.position - transform.position).normalized;
            Player.instance.TakeKnockBack(knockBackDirection, KnockBackDistance);
            Player.instance.TakeDamage(collisionDamage);
        }
    }
}
