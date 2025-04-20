using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class Shield : Weapon
{
    private float angle = 0f;
    [Header("Special settings")]
    [SerializeField]
    private float orbitRadius;
    [SerializeField]
    private float orbitSpeed;
    protected override void Shoot()
    {
        throw new System.NotImplementedException();
    }
    protected override void Navigate()
    {
        angle += Time.deltaTime * orbitSpeed;
        if (angle >= Mathf.PI * 2)
        {
            angle -= Mathf.PI * 2;
        }

        // Вычисляем позицию на орбите
        float posX = Mathf.Cos(angle);
        float posY = Mathf.Sin(angle);
        Vector2 newPos = new Vector2(posX, y: posY) * orbitRadius;

        // Если щит - дочерний объект игрока:
        transform.localPosition = newPos;

        // Вращение щита (переводим радианы в градусы)
        float degrees = angle * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, degrees);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy target = collision.gameObject.GetComponent<Enemy>();
            target.TakeDamage(damage);
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            target.TakeKnockBack(direction, knockbackForce);
            StartCoroutine(Reload(reloadTime));
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
        }
    }
    private void Update()
    {
        Navigate();
    }

}
