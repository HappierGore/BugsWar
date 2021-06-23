using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] float chancePercent = 50.0f;
    [SerializeField] MobStats.TargetType targetType = MobStats.TargetType.NEUTRAL;
    MobEvents mobEvents;
    MobStats mobStats;

    private void Start()
    {
        mobStats = GetComponent<MobStats>();
        mobEvents = mobStats.mobEvents;
    }

    public void KnockBackHability()
    {
        if (mobEvents.attackedTarget)
        {
            float chance = Random.Range(0.0f, 100.0f);
            if (chance <= chancePercent)
            {
                if (mobStats.targets.Length > 0)
                {
                    for (int i = 0; i < mobStats.targets.Length; i++)
                    {
                        if (mobStats.targets[i].tag != "castle")
                        {
                            mobStats.targets[i].GetComponent<HabilitiesEffects>().KnockedBackOn(level,targetType);
                        }
                    }
                }
                else if (mobStats.target.tag != "castle")
                {
                    mobStats.target.GetComponent<HabilitiesEffects>().FreezeOn(level,targetType);
                }
            }
        }
    }
}
