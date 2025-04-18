using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public static Player instance { get; private set; }

    public System.Action OnDamageTaken;

    [Header("Damage window")]
    [SerializeField]
    [Range(0f, 2f)]
    public float invulnerabilityDuration;
    private bool _isInvulnerable;

    void FixedUpdate()
    {
        Move();
    }
    protected override void Awake()
    {
        instance = this;
        base.Awake();

        OnDamageTaken += () => AudioManager.Instance.PlaySFX("PlayerDamage");
        OnDeath += () => AudioManager.Instance.PlaySFX("PlayerDeath");
        OnMove += (direction) =>
        {
            if (direction.magnitude > 0.1f)
                AudioManager.Instance.PlaySFX("Footstep");
        };
    }
    public override void TakeKnockBack(Vector2 direction, float distance)
    {
        if(_isInvulnerable) { return; }
        base.TakeKnockBack(direction, distance);
    }
    public override void TakeDamage(int damage)
    {
        if (_isInvulnerable) return;
        base.TakeDamage(damage);

        // Message to all subs
        OnDamageTaken?.Invoke();
        StartCoroutine(ProcessDamageWindow());
    }
    private IEnumerator ProcessDamageWindow()
    {
        _isInvulnerable = true;
        gameObject.layer = LayerMask.NameToLayer("PlayerInvulnerable");
        yield return new WaitForSeconds(invulnerabilityDuration);
        gameObject.layer = LayerMask.NameToLayer("Player");
        _isInvulnerable = false;
    }

    protected override void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        Vector3 movementVector = new Vector2(xAxis, yAxis);
        // Move
        Vector2 targetPosition = transform.position + movementVector;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        // Invoke event
        OnMove?.Invoke(movementVector);
        // Flip model
        Flip(movementVector);
    }
}
