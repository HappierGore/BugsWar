using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStats : MonoBehaviour
{

    [SerializeField] float health = 0.0f;
    [SerializeField] bool isAlly = false;

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
    //Modifiers

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
