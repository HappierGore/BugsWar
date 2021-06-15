using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMelee : MonoBehaviour
{
   public IEnumerator Attack(MobStats stats, MobEvents mobEvents, MobAttack mobAttack)
    {
        if(!mobEvents.alreadyAtacking && mobEvents.reachedTarget)
        {   
            MobStats targetStats = stats.target.GetComponent<MobStats>();
            mobEvents.alreadyAtacking = true;
            StartCoroutine(targetStats.TakeDamage(mobAttack.damage));
            yield return new WaitForSecondsRealtime(mobAttack.attackSpeed);
            mobEvents.alreadyAtacking = false;
        }
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator AttackCastle(MobStats stats, MobEvents mobEvents, MobAttack mobAttack)
    {
        if (!mobEvents.alreadyAtacking && mobEvents.reachedTarget)
        {
            CastleStats targetStats = stats.target.GetComponent<CastleStats>();
            mobEvents.alreadyAtacking = true;
            if(targetStats.GetHealth() > 0)
                StartCoroutine(targetStats.TakeDamage(mobAttack.damage));
            yield return new WaitForSecondsRealtime(mobAttack.attackSpeed);
            mobEvents.alreadyAtacking = false;
        }
        yield return new WaitForEndOfFrame();
    }
}
