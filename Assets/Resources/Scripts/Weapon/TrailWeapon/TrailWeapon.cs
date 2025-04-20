using UnityEngine;

public class TrailWeapon : Weapon
{
    [Header("Special weapon settings")]

    [Range(0f, 1f)]
    [SerializeField]
    private float bulletSpread;
    protected override void Navigate()
    {
        throw new System.NotImplementedException();
    }
    protected override void Shoot()
    {
        if (!isReadyToShoot) return;
        for (int i = 0; i < bulletsToFire; i++)
        {
            //spawn
            GameObject SpawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
            Bullet bulletScript = SpawnedBullet.GetComponent<Bullet>();
            bulletScript.Initialize(this.damage, this.knockbackForce, this.isHostile);
            Vector3 fireDirection = new Vector3(Random.Range(-bulletSpread, bulletSpread), -1, 0).normalized;
            //Move bullet
            Rigidbody2D bulletRb2d = SpawnedBullet.GetComponent<Rigidbody2D>();
            bulletRb2d.AddForce(fireDirection * bulletForce, ForceMode2D.Impulse);
        }
        //reload
        isReadyToShoot = false;
        StartCoroutine(Reload(reloadTime));
    }
    private void Update()
    {
        Shoot();
    }
}
