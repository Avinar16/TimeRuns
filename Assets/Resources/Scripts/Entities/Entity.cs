using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour
{
    [Header("Stats")]
    [Range(0, 25)]
    [SerializeField]
    protected int speed;

    protected Rigidbody2D rb;

    public System.Action<Vector3> OnMove;
    public System.Action OnDeath;

    [Range(0, 100)]
    [SerializeField]
    protected int health;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && gameObject != null)
        {
            Die();
        }
    }

    public virtual void TakeKnockBack(Vector2 direction, float distance)
    {
        StartCoroutine(KnockBackRoutine(direction, distance));
    }

    private IEnumerator KnockBackRoutine(Vector2 direction, float distance, float duration = 0.25f)
    {
        Vector2 startPos = transform.position;
        Vector2 targetPos = startPos + direction * distance;

        float elapsed = 0;

        while (elapsed < duration)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }

    abstract protected void Move();
}