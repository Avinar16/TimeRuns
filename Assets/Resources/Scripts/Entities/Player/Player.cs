using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public static Player instance { get; private set; }

    public System.Action OnDamageTaken;

    [SerializeField] private FloatingJoystick joystick;

    [Header("Damage window")]
    [SerializeField]
    [Range(0f, 2f)]
    public float invulnerabilityDuration;
    private bool _isInvulnerable;

    [SerializeField]
    Weapon[] weapons;

    [Header("Level")]
    [SerializeField]
    private int level = 0;

    void FixedUpdate()
    {
        Move();
        Detector.DetectNearestObject(100f, LayerMask.NameToLayer("Enemy"), transform.position, true);
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
        if (_isInvulnerable) { return; }
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
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 movementVector = new Vector3(horizontalInput, verticalInput, 0f);


        Vector2 targetPosition = transform.position + movementVector * speed * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);

        OnMove?.Invoke(movementVector);
    }
    public void LevelUp()
    {
        level++;
        weapons[level % 4].Upgrage();
    }
}
