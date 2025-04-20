using System.Collections;
using UnityEngine;

public class HomingMissileWand : Weapon
{
    [Header("Special weapon settings")]
    [SerializeField]
    private float orbitRadius;
    [SerializeField]
    private float timeBetweenBullets;

    private GameObject target;

    protected override void Shoot()
    {
        
        if (!isReadyToShoot || target == null) { return; }
        isReadyToShoot = false;
        
        StartCoroutine(ShootRoutine());
    }
    private IEnumerator ShootRoutine()
    {
       // Debug.Log($"{target}");
        for (int i = 0; i < bulletsToFire; i++)
        {
            //Spawn
            GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
            Bullet bulletScript = spawnedBullet.GetComponent<Bullet>();
            bulletScript.Initialize(damage, knockbackForce, isHostile);

            // Move the bullet
            Rigidbody2D bulletRb2d = spawnedBullet.GetComponent<Rigidbody2D>();
            Vector2 direction = (target.transform.position - transform.position).normalized;
            bulletRb2d.AddForce(direction * bulletForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(timeBetweenBullets);
        }

        StartCoroutine(Reload(reloadTime));
    }
    void Update()
    {
        Navigate();
        Shoot();
    }

    protected override void Navigate()
    {
        if (!isHostile)
        {
            target = Detector.DetectNearestObject(shootingRange, LayerMask.NameToLayer("Enemy"), transform.position, true);
        }
        else
        {
            target = Player.instance.gameObject;
        }
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
    protected override void Start()
    {
        base.Start();
        Upgrage();
    }
    public override void Upgrage()
    {
        base.Upgrage();
        if (level > 1)
        {
            bulletsToFire++;
        }
    }
}
