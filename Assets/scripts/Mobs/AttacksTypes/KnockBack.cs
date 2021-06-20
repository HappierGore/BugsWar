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
    MobStats stats;

    private void Start()
    {
        stats = GetComponent<MobStats>();
    }

    private void FixedUpdate()
    {
        if (!alreadyAttacking)
        {
            StartCoroutine(KnockBackHability());
        }
    }

    public IEnumerator KnockBackHability()
    {
        float chance = Random.Range(0.0f, 100.0f);
        if (stats.mobEvents.endAttackFrame)
            print(chance);
        if (stats.mobEvents.endAttackFrame && chance <= chancePercent)
        {
            alreadyAttacking = true;
            print("KnockingBack");
        }
        yield return new WaitForSecondsRealtime(stats.gameObject.GetComponent<MobAttack>().attackSpeed);
        alreadyAttacking = false;
    }
}
