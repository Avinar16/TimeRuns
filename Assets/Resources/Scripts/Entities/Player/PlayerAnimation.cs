using System.Collections;
using UnityEngine;

public class PlayerAnimation: EntityAnimator
{
    protected override void animateMovement(Vector3 direction)
    {
        base.animateMovement(direction);
        animator.SetFloat("Velocity", direction.magnitude);
    }
    protected override void animateDeath()
    {
        Debug.Log("ne poka ne hochu");
    }
    protected override void Start()
    {
        base.Start();

        Debug.Log($"{Player.instance.OnDamageTaken} {spriteRenderer}");
        Player.instance.OnDamageTaken += animateDamage;
    }
    void animateDamage()
    {
        StartCoroutine(Blink(new Color(1, 1, 1, 0.5f), Player.instance.invulnerabilityDuration - 0.2f, spriteRenderer));
    }
    private IEnumerator Blink(Color _damageColor, float blink_time, SpriteRenderer _sprite)
    {
        float endTime = Time.time + blink_time;
        float _blinkInterval = 0.1f;
        Color _originalColor = _sprite.color;

        while (Time.time < endTime)
        {
            _sprite.color = _damageColor;
            yield return new WaitForSeconds(_blinkInterval);

            _sprite.color = _originalColor;
            yield return new WaitForSeconds(_blinkInterval);
        }
        _sprite.color = _originalColor;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (Player.instance != null)
        {
            Player.instance.OnDamageTaken -= animateDamage;
        }
    }
}
