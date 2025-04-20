using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    protected float MaxFollowDistanse;
    [SerializeField]
    [Range(0, 10)]
    int collisionDamage;
    [SerializeField]
    GameObject ItemToDrop;
    [SerializeField]
    float chanceToDrop;

    protected virtual void FixedUpdate()
    {
        Move();
    }
    protected virtual void Start()
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
        OnMove?.Invoke(movementVector);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.instance.TakeDamage(collisionDamage);
        }
    }
    protected override void Die()
    {
        if (ItemToDrop != null)
        {
            float random = Random.Range(0, 1f);
            if (random < chanceToDrop)
            {
                Instantiate(ItemToDrop, transform.position, transform.rotation);
            }
        }
        base.Die();
    }
}