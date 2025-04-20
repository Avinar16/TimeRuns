using UnityEngine;

public abstract class EntityAnimator : MonoBehaviour
{
    protected Animator animator;
    protected Entity entity;
    protected SpriteRenderer spriteRenderer;
    public bool isFlipped { get; private set; }
    protected virtual void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        entity = gameObject.GetComponentInParent<Entity>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        entity.OnMove += animateMovement;
        entity.OnDeath += animateDeath;
    }
    protected virtual void animateMovement(Vector3 direction)
    {
        Flip(direction);
    }

    protected abstract void animateDeath();
    protected virtual void OnDestroy()
    {
        entity.OnMove -= animateMovement;
        entity.OnDeath -= animateDeath;
    }
    protected void Flip(Vector2 direction)
    {
        if (direction.x > 0 && !isFlipped)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            isFlipped = true;
        }
        else if (direction.x < 0 && isFlipped)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            isFlipped = false;
        }
    }


}
