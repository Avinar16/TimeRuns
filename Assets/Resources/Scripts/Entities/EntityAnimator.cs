using UnityEngine;

public abstract class EntityAnimator : MonoBehaviour
{
    protected Animator animator;
    protected Entity entity;
    protected virtual void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        entity = gameObject.GetComponent<Entity>();

        entity.OnMove += animateMovement;
        entity.OnDeath += animateDeath;
    }
    protected abstract void animateMovement(Vector3 direction);

    protected abstract void animateDeath();
    protected virtual void OnDestroy()
    {
        entity.OnMove -= animateMovement;
        entity.OnDeath -= animateDeath;
    }


}
