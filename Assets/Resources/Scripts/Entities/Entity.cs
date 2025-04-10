using UnityEngine;

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

    [Range(0, 100)]
    [SerializeField]
    protected int health;
    void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();    
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
    abstract public void TakeDamage(int damage);


    abstract protected void Move();


    abstract protected void Die();

}
