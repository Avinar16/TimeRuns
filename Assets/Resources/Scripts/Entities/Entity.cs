using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public abstract class Entity : MonoBehaviour
{
    [Header("Stats")]
    [Range(0, 25)]
    [SerializeField]
    protected int speed;
    [Range(0, 100)]
    [SerializeField]
    public int MaxHealth;
    [Range(0, 100)]
    [SerializeField]
    public int health;

    protected Rigidbody2D rb;

    public System.Action<Vector3> OnMove;
    public System.Action OnDeath;


    

    

    protected virtual void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Die()
    {
        Destroy(this.gameObject);
        OnDeath?.Invoke();
    }
    public virtual void TakeDamage(int damage)
    {
        this.health -= damage;
        if (health <= 0 && gameObject != null)
        {
            Die();
        }
    }
    public virtual void TakeKnockBack(Vector2 direction, float distance)
    {
        //Vector2 targetPos = new Vector2(transform.position.x, transform.position.y) + direction * distance;
        //transform.position = Vector2.MoveTowards(transform.position, targetPos, distance);
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

        transform.position = targetPos; // Финализируем позицию
    }
    abstract protected void Move();
}
