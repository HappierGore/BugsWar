using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStats : MonoBehaviour
{
    public enum Mode { Melee, Range }
    public enum Range { Short, Medium, Long, VeryLong }

    [SerializeField] float health = 0.0f, speed = 0.0f;
    [SerializeField] bool isAlly = false;
    [SerializeField] int foodCost = 0;

    [SerializeField] Mode mode = Mode.Melee;
    [SerializeField] Range range = Range.Short;

    public Transform target;

    public MobEvents mobEvents = new MobEvents();

    //Getters
    public int GetFoodBaseCost()
    {
        return foodCost;
    }
    public float GetHealth()
    {
        return health;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public bool IsAlly()
    {
        return isAlly;
    }
    public Mode GetMode()
    {
        return mode;
    }
    public Range GetRange()
    {
        return range;
    }
    //Modifiers

    public void IsEnemy()
    {
        isAlly = false;
    }

    //Función en corutina para evitar múltiples ejecuciones de este evento
    public IEnumerator TakeDamage(float damageTaken)
    {
        //Reducción de vida del mob en cuestión
        health -= damageTaken;
        //Dispara evento "damaged"
        mobEvents.damaged = true;
        yield return new WaitForEndOfFrame();
        //Regresa el evento "damaged" a nulo
        mobEvents.damaged = false;
        //Si la vida es menor a cero
        if(health <= 0)
        {
            //Dispara evento "died"
            mobEvents.died = true;
            yield return new WaitForEndOfFrame();
            //Regresa el evento "died" a nulo
            mobEvents.died = false;
        }
    }
}
