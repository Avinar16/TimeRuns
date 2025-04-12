using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Entity : MonoBehaviour
{
    [Header("Stats")]
    [Range(0, 25)]
    [SerializeField]
    protected int speed;

    private bool isFlipped;

    protected Rigidbody2D rb;

    public System.Action<Vector3> OnMove;
    public System.Action OnDeath;

    public SpriteRenderer spriteRenderer { get; private set; }

    [Range(0, 100)]
    [SerializeField]
    protected int health;


    protected virtual void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    protected void Flip(Vector2 direction)
    {
        if(direction.x > 0 && !isFlipped)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            isFlipped = true;
        }
        else if(direction.x < 0 && isFlipped) {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            isFlipped = false;
        }
    }
    protected void Die()
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
        Vector2 targetPos = new Vector2(transform.position.x, transform.position.y) + direction * distance;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, distance);
    }
    abstract protected void Move();
}
