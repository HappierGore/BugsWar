using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] float chancePercent = 50.0f;
    private bool alreadyAttacking = false;
    public enum TargetType {NEUTRAL,FIRE,WATER,ROCK,AIR,THUNDER,LIGHT,DARK}
    [SerializeField] TargetType targetType = TargetType.NEUTRAL;
    MobEvents mobEvents;

    private void Start()
    {
        mobEvents = GetComponent<MobStats>().mobEvents;
    }

    public IEnumerator KnockBackHability()
    {
        print("Ejecutando Corutine");
        if (mobEvents.attackedTarget)
        {
            print("Atacó");
            if (!alreadyAttacking)
            {
                alreadyAttacking = true;
                float chance = Random.Range(0.0f, 100.0f);
                if (chance <= chancePercent)
                {
                    alreadyAttacking = true;
                    print("KnockingBack");
                }
                yield return new WaitForEndOfFrame();
                alreadyAttacking = false;
            }
        }
    }
}
