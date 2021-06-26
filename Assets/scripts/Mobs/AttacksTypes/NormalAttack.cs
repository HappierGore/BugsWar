using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    private bool alreadyAtacking = false;

    //Corutina para atacar, es na corutina ya que de lo contrario, lo ejecutaría múltiples veces
   public IEnumerator Attack(MobStats stats, MobEvents mobEvents, MobAttack mobAttack)
    {
        //Si no esta ya atacando y el mob en cuestión ha alcanzado a su objetivo (Mob)
        if(!alreadyAtacking && mobEvents.reachedTarget)
        {   
            //Activamos esta bandera para evitar que la corutina se ejecute múltiples veces
            alreadyAtacking = true;

            //Activamos evento "Attack" para que la animación de ataque inicie (Condicionado en los frames de la animación)
            mobEvents.attack = (!mobEvents.knockedback) ? true : false;

            //Momento del frame en donde se ejecutará el daño (Condicionado en los frames de la animación)
            if (mobEvents.dealDamage)
            {
                mobEvents.endAttackFrame = false;

                //Iniciamos la corutina que esta dentro del script stats pare hacer que reciba daño (Mirar MobStats y CastleStats)
                ChooseRoutineDamage(stats, mobAttack);

                //Evento para mostrar cuando el ataque está siendo ejecutado
                mobEvents.attackedTarget = true;

                //Revisar las habilidades del mob en cuestión y entonces, ejecutar sus habilidades
                CheckHabilitiesManager();

                //Regresamos el evento a su valor original
                mobEvents.dealDamage = false;

                for (int i = 0; i < 10; i++)
                {
                    if (mobEvents.knockedback) break;
                    yield return new WaitForSecondsRealtime(GetComponent<MobAnimManager>().attackTime / 10);
                }


                mobEvents.endAttackFrame = true;
                //Esperar el tiempo que tiene el mob como velocidad de ataque (en segundos)
                yield return new WaitForSecondsRealtime(mobAttack.attackSpeed);

                //Esperar a que el último frame de ataque haya sido alcanzado (Desde la animación)
                mobEvents.attack = false;
                mobEvents.attackedTarget = false;

            }
            //Una vez hecho todo lo del daño, desactivamos la bandera para que pueda volver a atacar el mob
            alreadyAtacking = false;
            
        }
        yield return new WaitForEndOfFrame();
    }

    //Permite encontrar el HabilityManager, si lo tiene, es por que el Mob en cuestión tiene habilidades únicas
    private void CheckHabilitiesManager()
    {
        if(TryGetComponent(out HabilityManager habilityManager))
        {
            StartCoroutine(habilityManager.ExecuteHabilities());
        }
    }

    //Permite escoger el tipo de objetivo: Mob o castillo
    private void ChooseRoutineDamage(MobStats stats,MobAttack mobAttack)
    {
        //Si el objetivo NO es un castillo
        if (stats.target.tag != "castle")
        {
            //Obtenemos los stats de ese target
            MobStats targetStats = stats.target.GetComponent<MobStats>();

            //Procedemos al ataque hacia el mob
            StartCoroutine(targetStats.TakeDamage(mobAttack.damage));
        }
        else
        {
            //Obtenemos los stats de ese target
            CastleStats targetStats = stats.target.GetComponent<CastleStats>();

            //Procedemos al ataque hacia el castillo
            StartCoroutine(targetStats.TakeDamage(mobAttack.damage));
        }
    }
}
