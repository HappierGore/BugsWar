using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    private bool alreadyAtacking = false;
    //Corutina para atacar, es una corutina ya que de lo contrario, lo ejecutaría múltiples veces
   public IEnumerator Attack(MobStats stats, MobEvents mobEvents, MobAttack mobAttack)
    {
        //Si no esta ya atacando el jugador y el mob en cuestión ha alcanzado a su objetivo (Mob)
        if(!alreadyAtacking && mobEvents.reachedTarget)
        {   
            //Obtener stats del objetivo
            MobStats targetStats = stats.target.GetComponent<MobStats>();

            //Activamos esta bandera para evitar que la corutina se ejecute múltiples veces
            alreadyAtacking = true;
            //Activamos evento "Attack" para que la animación de ataque inicie (Condicionado en los frames de la animación)
            mobEvents.attack = true;
            
            //Momento del frame en donde se ejecutará el daño (Condicionado en los frames de la animación)
            if(mobEvents.dealDamage)
            {
                //Iniciamos la corutina que esta dentro del script stats pare hacer que reciba daño (Mirar MobStats)
                StartCoroutine(targetStats.TakeDamage(mobAttack.damage));

                //Esperar el tiempo que tiene el mob como velocidad de ataque (en segundos)
                yield return new WaitForSecondsRealtime(mobAttack.attackSpeed);

                //Regresamos los eventos al valor original
                mobEvents.attack = false;
                mobEvents.dealDamage = false;
            }
            //Una vez hecho todo lo del daño, desactivamos la bandera para que pueda volver a atacar el mob
            alreadyAtacking = false;
            
        }
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator AttackCastle(MobStats stats, MobEvents mobEvents, MobAttack mobAttack)
    {
        //Si no esta actualmente atacando y ya ha alcanzado su objetivo
        if (!alreadyAtacking && mobEvents.reachedTarget)
        {
            //Obtiene el componente CastleStats del castillo a atacar en cuestión
            CastleStats targetStats = stats.target.GetComponent<CastleStats>();

            //Activamos esta bandera para evitar que la corutina se ejecute múltiples veces
            alreadyAtacking = true;
            //Activamos evento "Attack" para que la animación de ataque inicie (Condicionado en los frames de la animación)
            mobEvents.attack = true;

            //Si el objetivo tiene más de 0 de vida, realizar el ataque, esto es necesario ya que el edificio no se destruye
            //sólo se deshabilita, por lo que si no se condiciona enviará errores.
            if (targetStats.GetHealth() > 0)
            {
                if (mobEvents.dealDamage)
                {
                    //Iniciamos la corutina que esta dentro del script stats pare hacer que reciba daño (Mirar MobStats)
                    StartCoroutine(targetStats.TakeDamage(mobAttack.damage));

                    //Esperar el tiempo que tiene el mob como velocidad de ataque (en segundos)
                    yield return new WaitForSecondsRealtime(mobAttack.attackSpeed);

                    //Regresamos los eventos al valor original
                    mobEvents.attack = false;
                    mobEvents.dealDamage = false;
                }
               
            }
            //Regresamos bandera a su valor original para permitirle al mob atacar nuevamente
            alreadyAtacking = false;
        }
        yield return new WaitForEndOfFrame();
    }
}
