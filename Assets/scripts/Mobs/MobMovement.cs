using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement
{
    static public Transform enemyCastle, ownCastle;
    static bool started = false;
    //Esta hecho en estático ya que de este modo, podemos implementarlo y añadirlo a cualquier
    //mob usando la función Move(), un ejemplo está en el script "AntSoldier"
    public static void Move(MobStats stats)
    {
        //Permite identificar los castillos aliados y enemigos, son como parámetros de inicio
        if (!started)
        {
            enemyCastle = GameObject.Find("EnemyCastle").transform;
            ownCastle = GameObject.Find("OwnCastle").transform;
            started = true;
        }

        //Selecciona un objetivo, esto se utiliza para saber hasta donde va a caminar el bicho dependiendo
        //de su rango de ataque
        MobTargetSelection.DetectTarget(stats);

        //Determina la dirección con la que la hormiga se moverá
        float direction = (stats.IsAlly()) ? 1.0f : -1.0f;
        float castlePos = (stats.IsAlly()) ? enemyCastle.position.x : ownCastle.position.x;

        //El mob ALIADO se moverá hasta toparse con el castillo enemigo
        if ((stats.transform.position.x < castlePos && stats.IsAlly()) || (stats.transform.position.x > castlePos && !stats.IsAlly()))
        {
            //Si el mob aliado alcanza algún objetivo (sea castillo u otro mob) se dispara el evento "reachedTarget"
            //y sale de la función "movimiento", es decir, se detiene el mob
            if ((stats.transform.position.x > stats.target.transform.position.x - Range(stats) && stats.IsAlly()) || (stats.transform.position.x < stats.target.transform.position.x + Range(stats) && !stats.IsAlly()))
            {
                stats.mobEvents.endAttackFrame = false;
                stats.mobEvents.reachedTarget = true;
                stats.mobEvents.moving = false;
                return;
            }
            //Si no ha alcanzado el mob a su objetivo, el evento se pondrá en falso
            if (stats.mobEvents.endAttackFrame && !stats.mobEvents.freezed)
            {
                stats.mobEvents.moving = true;
                stats.mobEvents.reachedTarget = false;
                //Movimiento del mob tomando en cuenta la velocidad del mismo desde el script "stats"
                stats.transform.position = new Vector2(stats.transform.position.x + stats.GetSpeed() * Time.fixedDeltaTime * direction, stats.transform.position.y);
            }

        }
    }

    //Rango de ataque
    private static float Range(MobStats stats)
    {
        //Variable temporal para definir la distancia
        float result = 0.0f;
        switch (stats.GetMode())
        {
            //En el caso de que sea Melee
            case MobStats.Mode.Melee:
                {
                    //Definirá el rango de ataque
                    switch (stats.GetRange())
                    {
                        case MobStats.Range.Short:
                            result = 1.5f;
                            break;
                        case MobStats.Range.Medium:
                            result = 1.6f;
                            break;
                        case MobStats.Range.Long:
                            result = 1.7f;
                            break;
                        case MobStats.Range.VeryLong:
                            result = 1.8f;
                            break;
                        default:
                            break;
                    }
                    break;
                }
                //En el caso de que sea a distancia
            case MobStats.Mode.Range:
                {
                    //Se definirá otro rango
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
