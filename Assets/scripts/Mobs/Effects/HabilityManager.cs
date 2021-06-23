using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityManager : MonoBehaviour
{
    private bool executingHabilities = false;
    Freeze freeze;
    KnockBack knockBack;

    private void Start()
    {
        //Si el mob en cuestión tiene el componente / habilidad Freeze, entonces guarda
        if (TryGetComponent(out Freeze freeze))
            this.freeze = freeze;
        if (TryGetComponent(out KnockBack knockBack))
            this.knockBack = knockBack;
    }

    //Evalua todas las habilidades que tenga el mob y las ejecuta
    public IEnumerator ExecuteHabilities()
    {
        if (!executingHabilities)
        {
            executingHabilities = true;
            FreezeExecute();
            KnockBackExecute();
        }
        yield return new WaitForEndOfFrame();
        executingHabilities = false;
    }

    //Habilidad "Freeze"
   private void FreezeExecute()
   {
        if (freeze != null)
            freeze.FreezeHability();
   }
    //Habilidad "Freeze"
    private void KnockBackExecute()
    {
        if (knockBack != null)
            knockBack.KnockBackHability();
    }
}
