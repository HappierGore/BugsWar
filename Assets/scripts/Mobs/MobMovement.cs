using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement
{
    static public Transform enemyCastle, ownCastle;
    static bool started = false;

    public static void Move(MobStats stats)
    {
        if (!started)
        {
            enemyCastle = GameObject.Find("EnemyCastle").transform;
            ownCastle = GameObject.Find("OwnCastle").transform;
            started = true;
        }

        MobTargetSelection.DetectTarget(stats);

        float direction = (stats.IsAlly()) ?  1.0f : -1.0f; //Define la dirección de movimiento del mob
        if (stats.transform.position.x < enemyCastle.position.x && stats.IsAlly())
        {
            if (stats.transform.position.x > stats.target.transform.position.x - Range(stats))
            {
                stats.mobEvents.reachedTarget = true;
                return;
            }//Si se topa con un enemigo, retorna
            stats.mobEvents.reachedTarget = false;
            stats.transform.position = new Vector2(stats.transform.position.x + stats.GetSpeed() * Time.fixedDeltaTime * direction, stats.transform.position.y);
        }//Si la distancia en X del mob es menor a la base enemiga, muévete
        else if (stats.transform.position.x > ownCastle.position.x && !stats.IsAlly())
        {
            if (stats.transform.position.x < stats.target.transform.position.x + Range(stats))
            {
                stats.mobEvents.reachedTarget = true;
                return;
            }//Si se topa con un enemigo, retorna
            stats.mobEvents.reachedTarget = false;
            stats.transform.position = new Vector2(stats.transform.position.x + stats.GetSpeed() * Time.fixedDeltaTime * direction, stats.transform.position.y);
        }//Si la distancia en X del mob es mayor a la base aliada, se mueve
    }


    private static float Range(MobStats stats)
    {
        float result = 0.0f;
        switch (stats.GetMode())
        {
            case MobStats.Mode.Melee:
                {
                    switch (stats.GetRange())
                    {
                        case MobStats.Range.Short:
                            result = 0.5f;
                            break;
                        case MobStats.Range.Medium:
                            result = 1.0f;
                            break;
                        case MobStats.Range.Long:
                            result = 1.5f;
                            break;
                        case MobStats.Range.VeryLong:
                            result = 2.0f;
                            break;
                        default:
                            break;
                    }
                    break;
                }
            case MobStats.Mode.Range:
                {
                    switch (stats.GetRange())
                    {
                        case MobStats.Range.Short:
                            result = 1.5f;
                            break;
                        case MobStats.Range.Medium:
                            result = 2.5f;
                            break;
                        case MobStats.Range.Long:
                            result = 3.5f;
                            break;
                        case MobStats.Range.VeryLong:
                            result = 4.5f;
                            break;
                        default:
                            break;
                    }
                    break;
                }
        }
        return result;
    }
}
