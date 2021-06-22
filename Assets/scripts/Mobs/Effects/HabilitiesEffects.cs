using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilitiesEffects : MonoBehaviour
{
    private MobStats mobStats;
    private MobEvents mobEvents;

    private IEnumerator freeze;

    private bool alreadyAttacking = false;
    public bool hitSignal = false;

    private void Start()
    {
        mobStats = GetComponent<MobStats>();
        mobEvents = mobStats.mobEvents;
        freeze = KnockBackEffect();
    }

    public void FreezeOn()
    {
        if(mobEvents.freezed)
        {
            StopCoroutine(freeze);
            alreadyAttacking = false;
            freeze = KnockBackEffect();
            StartCoroutine(freeze);
            return;
        }
        mobEvents.freezed = true;
        freeze = KnockBackEffect();
        StartCoroutine(freeze);
    }

    public IEnumerator KnockBackEffect()
    {
        if (!alreadyAttacking)
        {
            print("freezed");
            alreadyAttacking = true;
            mobEvents.moving = false;

            yield return new WaitForSecondsRealtime(5.0f);

            mobEvents.freezed = false;
            alreadyAttacking = false;

            yield return new WaitForEndOfFrame();
        }
    }
}
