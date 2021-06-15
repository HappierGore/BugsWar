using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStats : MonoBehaviour
{
    //Vida del castillo
    [SerializeField] float health = 0.0f;
    //Definir si el castillo es aliado o enemigo
    [SerializeField] bool isAlly = false;
    //Eventos del castillo
    public CastleEvents castleEvents = new CastleEvents();
    //Getters
    public float GetHealth()
    {
        return health;
    }
    public bool IsAlly()
    {
        return isAlly;
    }


    //Recibe daño el castillo
    public IEnumerator TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        castleEvents.damaged = true;
        yield return new WaitForEndOfFrame();
        castleEvents.damaged = false;
        if (health <= 0)
        {
            castleEvents.destroyed = true;
            yield return new WaitForEndOfFrame();
            castleEvents.destroyed = false;
        }
    }

}
