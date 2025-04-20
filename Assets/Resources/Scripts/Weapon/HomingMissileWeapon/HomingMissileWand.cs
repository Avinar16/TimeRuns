using System.Collections;
using UnityEngine;

public class HomingMissileWand : Weapon
{
    [Header("Special weapon settings")]
    [SerializeField]
    private float orbitRadius;

    private GameObject target;

    protected override void Shoot()
    {
        if (!isReadyToShoot || target == null) return;

        for (int i = 0; i < bulletsToFire; i++)
        {
            // Spawn
            GameObject SpawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
            // Set damage and knockback
            Bullet bulletScript = SpawnedBullet.GetComponent<Bullet>();
            bulletScript.Initialize(this.damage, this.knockbackForce, this.isHostile);
            // Move the bullet
            Rigidbody2D bulletRb2d = SpawnedBullet.GetComponent<Rigidbody2D>();
            Vector2 direction = (target.transform.position - gameObject.transform.position).normalized;
            bulletRb2d.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }
        //reload
        isReadyToShoot = false;
        StartCoroutine(Reload(reloadTime));
    }
    void Update()
    {
        Navigate();
        Shoot();
    }

    protected override void Navigate()
    {
        target = Detector.nearestEnemyRelativeleToPlayer;
        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.parent.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x);
            float xPos = Mathf.Cos(angle);
            float yPos = Mathf.Sin(angle);

            Vector2 newPosition = new Vector2(xPos, yPos) * orbitRadius;
            //Debug.Log($"{newPosition}");
            transform.localPosition = Vector2.Lerp(transform.localPosition, newPosition, Time.deltaTime * 10f);
        }
        else
        {
            transform.localPosition = Vector3.right * orbitRadius;
        }
    }
}
