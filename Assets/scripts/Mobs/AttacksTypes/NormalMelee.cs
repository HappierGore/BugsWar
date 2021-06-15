using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMelee : MonoBehaviour
{

    //Corutina para atacar, es una corutina ya que de lo contrario, lo ejecutaría múltiples veces
   public IEnumerator Attack(MobStats stats, MobEvents mobEvents, MobAttack mobAttack)
    {
        //Si no esta ya atacando el jugador y el mob en cuestión ha alcanzado a su objetivo (Mob)
        if(!mobEvents.alreadyAtacking && mobEvents.reachedTarget)
        {   
            //Obtener stats del objetivo
            MobStats targetStats = stats.target.GetComponent<MobStats>();

            //Activamos esta bandera para evitar que la corutina se ejecute múltiples veces
            mobEvents.alreadyAtacking = true;

            //Iniciamos la corutina que esta dentro del script stats pare hacer que reciba daño (Mirar MobStats)
            StartCoroutine(targetStats.TakeDamage(mobAttack.damage));

            //Esperar el tiempo que tiene el mob como velocidad de ataque (en segundos)
            yield return new WaitForSecondsRealtime(mobAttack.attackSpeed);

            //Una vez hecho todo lo del daño, desactivamos la bandera para que pueda volver a atacar el mob
            mobEvents.alreadyAtacking = false;
        }
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator AttackCastle(MobStats stats, MobEvents mobEvents, MobAttack mobAttack)
    {
        //Si no esta actualmente atacando y ya ha alcanzado su objetivo
        if (!mobEvents.alreadyAtacking && mobEvents.reachedTarget)
        {
            //Obtiene el componente CastleStats del castillo a atacar en cuestión
            CastleStats targetStats = stats.target.GetComponent<CastleStats>();

            //Activamos bandera para evitar duplicaciones de corutinas
            mobEvents.alreadyAtacking = true;

            //Si el objetivo tiene más de 0 de vida, realizar el ataque, esto es necesario ya que el edificio no se destruye
            //sólo se deshabilita, por lo que si no se condiciona enviará errores.
            if(targetStats.GetHealth() > 0)
                StartCoroutine(targetStats.TakeDamage(mobAttack.damage));

            //Esperar el tiempo de ataque del mob en cuestión
            yield return new WaitForSecondsRealtime(mobAttack.attackSpeed);

            //Regresamos bandera a su valor original para permitirle al mob atacar nuevamente
            mobEvents.alreadyAtacking = false;
        }
        yield return new WaitForEndOfFrame();
    }
}
