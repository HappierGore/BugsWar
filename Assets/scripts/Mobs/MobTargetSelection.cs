using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTargetSelection
{

    public static void DetectTarget(MobStats stats)
    {
        string targetTemp = (stats.IsAlly()) ? "enemies" : "allies" ;
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTemp);
        Transform[] result = new Transform[targets.Length];
        float distanceTemp = 0.0f, resultTemp = 9999999.0f;
        if(targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                distanceTemp = DistanceP1P2(stats.transform.position.x, targets[i].transform.position.x);
                if (distanceTemp < resultTemp)
                {
                    resultTemp = distanceTemp;
                    stats.target = targets[i].transform;
                }
            }
        }
        else
        {
            stats.target = (stats.IsAlly()) ? MobMovement.enemyCastle : MobMovement.ownCastle;
        }
    }

    private static float DistanceP1P2(float P1, float P2)
    {
        float distance = Mathf.Abs(P1 - P2);
        return distance;
    }
}
