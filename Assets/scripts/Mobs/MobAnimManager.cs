using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimManager : MonoBehaviour
{
    private Animator animator;
    private MobEvents mobEvents;
    void Start()
    {
        animator = GetComponent<Animator>();
        mobEvents = GetComponent<MobStats>().mobEvents;
    }

    // Update is called once per frame
    void Update()
    {
        //Si se está moviendo el mob, activa
        if (mobEvents.moving)
            animator.SetBool("Moving", true);
        else
            //Si no, desactiva
            animator.SetBool("Moving", false);

        //Si el mob esta atacando y no se está moviendo, entonces activa la animación de ataque
        if (mobEvents.attack && !mobEvents.moving)
            animator.SetBool("Attack", true);
        //Si no, desactiva
        else
            animator.SetBool("Attack", false);
    }

    //Funciones para utilizar en los eventos de los frames de la animación en cuestión
    public void SetAttackState(string value)
    {
        mobEvents.attack = (value.ToLower() == "false") ? false : true;
    }

    public void SetDealDamage(string value)
    {
        mobEvents.dealDamage = (value.ToLower() == "false") ? false : true;
    }
}
