using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon stats")]
    [SerializeField]
    protected float reloadTime;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int knockbackForce;
    [SerializeField]
    protected float shootingRange;
    [Header("Bullet")]
    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected float bulletForce;
    [SerializeField]
    protected bool isHostile;
    [SerializeField]
    protected int bulletsToFire;
    [Header("Level")]
    [SerializeField]protected int level = 0;
    [SerializeField] protected int maxLevel = 5;

    protected bool isReadyToShoot;
    protected abstract void Navigate();
    protected abstract void Shoot();
    protected virtual IEnumerator Reload(float time) {
        yield return new WaitForSeconds(time);
        isReadyToShoot = true;
    }
    protected virtual void Start()
    {
        isReadyToShoot = true;
    }
    public virtual void Upgrage()
    {
        if (level >= maxLevel) return;
        level++;
        if(level == 1)
        {
            gameObject.SetActive(true);
            return;
        }
    }

}
