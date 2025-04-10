using UnityEngine;

public class PlayerAnimation: EntityAnimator
{
    protected override void animateMovement(Vector3 direction)
    {
        animator.SetFloat("Velocity", direction.magnitude);
    }
    protected override void animateDeath()
    {

    }
}
