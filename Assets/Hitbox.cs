using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public float knockbackStrength = 10f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player player = col.GetComponent<Player>();
            if (player != null)
            {
                Vector2 dir = (col.transform.position - transform.position).normalized;
                player.TakeKnockBack(dir, knockbackStrength);
            }
        }
    }
}