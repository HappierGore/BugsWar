using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSoldier : MonoBehaviour
{
    MobStats stats;
    MobAttack mobAttack;
    NormalMelee normalMelee;
    void Start()
    {
        stats = GetComponent<MobStats>();
        mobAttack = GetComponent<MobAttack>();
        normalMelee = GetComponent<NormalMelee>();
    }

    void Update()
    {
        //Hacer que el mob se mueva, todo ya esta condicionado en el script MobMovement para que este decida hacia qué dirección andar
        MobMovement.Move(stats);

        //Si el mob ha alcanzado su objetivo
        if(stats.mobEvents.reachedTarget)
        {
            //Si el objetivo no es el castillo, entonces atacas a un mob
            if(stats.target.tag != "castle")
                StartCoroutine(normalMelee.Attack(stats, stats.mobEvents, mobAttack));
            //Si es un castillo, entonces ataca el castillo (Al ser diferentes scripts, es necesario separar)
            else
                StartCoroutine(normalMelee.AttackCastle(stats, stats.mobEvents, mobAttack));
        }
        //Si el mob muere, destruir objeto
        if(stats.mobEvents.died)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
