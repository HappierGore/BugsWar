using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] float chancePercent = 50.0f;
    private bool alreadyAttacking = false;
    public enum TargetType {NEUTRAL,FIRE,WATER,ROCK,AIR,THUNDER,LIGHT,DARK}
    [SerializeField] TargetType targetType = TargetType.NEUTRAL;
    MobEvents mobEvents;
    MobStats mobStats;

    private void Start()
    {
        mobStats = GetComponent<MobStats>();
        mobEvents = mobStats.mobEvents;
    }

    public void FreezeHability()
    {
        if (mobEvents.attackedTarget)
        {
            float chance = Random.Range(0.0f, 100.0f);
            if (chance <= chancePercent)
            {
                if(mobStats.targets.Length > 0)
                {
                    for (int i = 0; i < mobStats.targets.Length; i++)
                    {
                        if(mobStats.targets[i].tag != "castle")
                        {
                            mobStats.targets[i].GetComponent<HabilitiesEffects>().hitSignal = mobEvents.attackedTarget;
                            mobStats.targets[i].GetComponent<HabilitiesEffects>().FreezeOn();
                        }
                    }
                }
                else if(mobStats.target.tag != "castle")
                {
                    mobStats.target.GetComponent<HabilitiesEffects>().hitSignal = mobEvents.attackedTarget;
                    mobStats.target.GetComponent<HabilitiesEffects>().FreezeOn();
                }
            }
        }
    }
}
