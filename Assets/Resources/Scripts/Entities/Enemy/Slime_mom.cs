using System.Collections;
using UnityEngine;

public class Slime_mom : Enemy
{
    [SerializeField]
    private float distanceToRunAway;

    [SerializeField]
    GameObject Slime;
    [SerializeField]
    float timeToSpawn;
    [SerializeField]
    int slimesToSpawn;
    [SerializeField]
    int maxSpawns = 10;
    int currentSpawns;

    protected override void Move()
    {
        Vector3 PlayerPosition = Player.instance.transform.position;
        if (Vector3.Distance(PlayerPosition, transform.position) <= distanceToRunAway)
        {
            Vector2 movementVector = (transform.position - PlayerPosition).normalized;
            if (Vector3.Distance(PlayerPosition, transform.position) > MaxFollowDistanse)
            {
                rb.linearVelocity = movementVector * speed;
            }
        }
        else
        {
            base.Move();
        }
    }

    private IEnumerator SpawnSlimes()
    {
        if (currentSpawns <= maxSpawns)
        for (int i = 0; i < slimesToSpawn; i++)
        {
            Instantiate(Slime, transform.position, Quaternion.identity);
            currentSpawns++;
        }
        yield return new WaitForSeconds(timeToSpawn);
        StartCoroutine(SpawnSlimes());
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(SpawnSlimes());
    }
}
