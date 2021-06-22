using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBehaviour : MonoBehaviour
{
    MobStats stats;
    MobAttack mobAttack;
    AreaAttack areaAttack;
    NormalAttack normalAttack;
    void Start()
    {
        stats = GetComponent<MobStats>();

        mobAttack = GetComponent<MobAttack>();

        if (TryGetComponent(out NormalAttack normalAttack))
            this.normalAttack = normalAttack;

        else if (TryGetComponent(out AreaAttack areaAttack))
            this.areaAttack = areaAttack;
    }

    void Update()
    {
        //Hacer que el mob se mueva, todo ya esta condicionado en el script MobMovement para que este decida hacia qué dirección andar
        MobMovement.Move(stats);

        //Si el mob ha alcanzado su objetivo
        if(stats.mobEvents.reachedTarget)
        {
            if (normalAttack != null)
                StartCoroutine(normalAttack.Attack(stats, stats.mobEvents, mobAttack));

            else if (areaAttack != null)
                StartCoroutine(areaAttack.Attack(stats, stats.mobEvents, mobAttack));
        }
        //Si el mob muere, destruir objeto
        if(stats.mobEvents.died)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
