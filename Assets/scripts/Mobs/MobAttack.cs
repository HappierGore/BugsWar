using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public float attackSpeed, damage;
    public enum DamageType { Magic, Normal, Ballistic, Elemental};
    public enum ElementalType { Water, Fire, Wind, Dirt}
    public DamageType damageType;
    public ElementalType elementalType;
}



