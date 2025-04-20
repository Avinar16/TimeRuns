using System.Collections;
using UnityEngine;

public class Vertical_gravity_weapon : Weapon
{
    [Header("Special weapon settings")]

    [Range(0f, 1f)]
    [SerializeField]
    private float bulletSpread;
    protected override void Shoot()
    {
        if (!isReadyToShoot) return;
        for (int i = 0; i < bulletsToFire; i++)
        {
            //spawn
            GameObject SpawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
            //Init bullet
            Bullet bulletScript = SpawnedBullet.GetComponent<Bullet>();
            bulletScript.Initialize(this.damage, this.knockbackForce, this.isHostile);
            //Move bullet
            Rigidbody2D bulletRb2d = SpawnedBullet.GetComponent<Rigidbody2D>();
            float randX = Random.Range(-bulletSpread, bulletSpread);
            Vector2 direction = new Vector2(randX, 1);
            bulletRb2d.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }
        //reload
        isReadyToShoot = false;
        StartCoroutine(Reload(reloadTime));
    }
    protected override void Navigate()
    {
        throw new System.NotImplementedException();
    }
    private void Update()
    {
        Shoot();
    }
    public override void Upgrage()
    {
        base.Upgrage();
        if (level > 1) { 
        bulletsToFire++;
        }
    }
}
