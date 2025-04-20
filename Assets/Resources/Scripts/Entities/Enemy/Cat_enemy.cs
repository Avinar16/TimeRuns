using UnityEngine;

public class Cat_enemy : Enemy
{
    [SerializeField]
    private float distanceToRunAway;
    [SerializeField]
    GameObject ItemToDrop;
    [SerializeField]
    float chanceToDrop;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void Move()
    {
        Vector3 PlayerPosition = Player.instance.transform.position;
        if (Vector3.Distance(PlayerPosition, transform.position) <= distanceToRunAway)
        {
            Vector2 movementVector = (transform.position - PlayerPosition).normalized;
            if (Vector3.Distance(PlayerPosition, transform.position) > MaxFollowDistanse)
            {
                rb.linearVelocity = movementVector * speed;
            }
        }
        else
        {
            base.Move();
        }
    }
    protected override void Die()
    {
        float random = Random.Range(0, 1f);
        if (random < chanceToDrop)
        {
            Instantiate(ItemToDrop, transform.position, transform.rotation);
        }
        base.Die();
    }
}
