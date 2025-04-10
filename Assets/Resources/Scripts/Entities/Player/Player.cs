using UnityEngine;

public class Player : Entity
{
    void Update()
    {
        if (health <= 0 && gameObject != null)
        {
            Die();
        }

    }
    void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
         float xAxis = Input.GetAxisRaw("Horizontal");
         float yAxis = Input.GetAxisRaw("Vertical");
        Vector3 movementVector = new Vector3(xAxis, yAxis, 0);
        // Move
        rb.linearVelocity = movementVector * speed;
        // Send message to all subscribers
        OnMove?.Invoke(movementVector);

        // Flips the character if going left/right
        Flip(movementVector);
    }
    protected override void Die()
    {
        Destroy(this.gameObject);
        OnDeath?.Invoke();
    }
    public override void TakeDamage(int damage)
    {
        this.health -= damage;
    }

}
