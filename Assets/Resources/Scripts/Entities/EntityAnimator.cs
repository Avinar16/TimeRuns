using UnityEngine;

public abstract class EntityAnimator : MonoBehaviour
{
    protected Animator animator;
    protected Entity entity;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        entity = gameObject.GetComponent<Entity>();

        entity.OnMove += animateMovement;
        entity.OnDeath += animateDeath;
    }
    protected abstract void animateMovement(Vector3 direction);

    protected abstract void animateDeath();
    


}
