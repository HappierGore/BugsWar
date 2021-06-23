using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilitiesEffects : MonoBehaviour
{
    private MobStats mobStats;
    private MobEvents mobEvents;

    private IEnumerator freeze,knockBack;

    private bool freezed = false,knockbacked = false;

    private void Start()
    {
        mobStats = GetComponent<MobStats>();
        mobEvents = mobStats.mobEvents;
        freeze = FreezeEffect(1);
        knockBack = KnockBackEffect(1);
    }

    //Freeze

    public void FreezeOn(int level,MobStats.TargetType targetType)
    {
        if(targetType == mobStats.mobType)
        {
            if(mobEvents.freezed)
            {
                StopCoroutine(freeze);
                freezed = false;
                freeze = FreezeEffect(level);
                StartCoroutine(freeze);
                return;
            }
            mobEvents.freezed = true;
            freeze = FreezeEffect(level);
            StartCoroutine(freeze);
        }
    }

    private IEnumerator FreezeEffect(int level)
    {
        if (!freezed)
        {
            freezed = true;
            mobEvents.moving = false;

            yield return new WaitForSecondsRealtime(level);

            mobEvents.freezed = false;
            freezed = false;

            yield return new WaitForEndOfFrame();
        }
    }

    //KnockBack

    public void KnockedBackOn(int level,MobStats.TargetType targetType)
    {
        if(targetType == mobStats.mobType)
        {
            if (mobEvents.knockedback)
            {
                StopCoroutine(knockBack);
                knockbacked = false;
                knockBack = KnockBackEffect(level);
                StartCoroutine(knockBack);
                return;
            }
            mobEvents.knockedback = true;
            knockBack = KnockBackEffect(level);
            StartCoroutine(knockBack);
        }
    }

    private IEnumerator KnockBackEffect(int level)
    {
        if (!knockbacked)
        {
            knockbacked = true;
            mobEvents.moving = false;

            for (int i = 0; i < level*20; i++)
            {
                if (!mobStats.IsAlly() && transform.position.x < GameObject.Find("EnemyCastle").transform.position.x)
                    transform.position = new Vector2(transform.position.x + level * 0.1f, transform.position.y);
                else if(mobStats.IsAlly() && transform.position.x > GameObject.Find("OwnCastle").transform.position.x)
                    transform.position = new Vector2(transform.position.x - level * 0.1f, transform.position.y);
                yield return new WaitForEndOfFrame();
            }

            mobEvents.knockedback = false;
            knockbacked = false;

            yield return new WaitForEndOfFrame();
        }
    }
}
