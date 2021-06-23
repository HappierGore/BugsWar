using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEvents
{
    //Mob Flags
    public bool moving = false;
    public bool attackedTarget = false;


    //Targets events
    public bool reachedTarget = false;
    public bool killTarget = false;

    //Health Events
    public bool damaged = false;
    public bool died = false;

    //Anim events
    public bool endAttackFrame = true;
    public bool attack = false;
    public bool dealDamage = false;

    //Mob Effects (Taken)
    public bool freezed = false;
    public bool knockedback = false;

}
