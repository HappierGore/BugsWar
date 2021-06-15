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
        MobMovement.Move(stats);
        if(stats.mobEvents.reachedTarget)
        {
            if(stats.target.tag != "castle")
                StartCoroutine(normalMelee.Attack(stats, stats.mobEvents, mobAttack));
            else
                StartCoroutine(normalMelee.AttackCastle(stats, stats.mobEvents, mobAttack));
        }
        if(stats.mobEvents.died)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
