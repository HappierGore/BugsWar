using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEvents
{
    //Mob Flags
    public bool moving = false;
    public bool attack = false;
    public bool dealDamage = false;

    //Targets events
    public bool reachedTarget = false;

    //Health Events
    public bool damaged = false;
    public bool died = false;

    //Anim events
    public bool endAttackFrame = false;

}
