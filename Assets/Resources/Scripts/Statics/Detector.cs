using System;
using System.Numerics;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class Detector
{
    public static GameObject nearestEnemyRelativeleToPlayer { get; private set; }
    public static GameObject DetectNearestObject(float radius, LayerMask layer, UnityEngine.Vector3 startpos, bool fromPlayer=false)
    {
        Collider2D[] hitObject = Physics2D.OverlapCircleAll(
            startpos,
            radius,
            1 << layer
        );

        GameObject nearestObject = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D obj in hitObject)
        {
            float distance = UnityEngine.Vector3.Distance(startpos, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObject = obj.gameObject;
                if(layer.value == LayerMask.NameToLayer("Enemy") && fromPlayer)
                {
                    nearestEnemyRelativeleToPlayer = nearestObject;
                }
            }
        }
        return nearestObject;
    }
}
