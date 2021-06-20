using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityManager : MonoBehaviour
{
    KnockBack knockBack = null;
    private bool executingHabilities = false;


    //Evalua todas las habilidades que tenga el mob y las ejecuta
    public IEnumerator ExecuteHabilities()
    {
        if (!executingHabilities)
        {
            executingHabilities = true;
            KnockBackExecute();
        }
        yield return new WaitForEndOfFrame();
        executingHabilities = false;
    }

    //Habilidad "knockBack"
   private void KnockBackExecute()
   {
       if(TryGetComponent(out KnockBack knockBack))
       {
            StartCoroutine(knockBack.KnockBackHability());
       }
   }
}
