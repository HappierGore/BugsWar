using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityManager : MonoBehaviour
{
    private bool executingHabilities = false;
    Freeze freeze;

    private void Start()
    {
        //Si el mob en cuestión tiene el componente / habilidad Freeze, entonces guarda
        if (TryGetComponent(out Freeze freeze))
            this.freeze = freeze;
    }

    //Evalua todas las habilidades que tenga el mob y las ejecuta
    public IEnumerator ExecuteHabilities()
    {
        if (!executingHabilities)
        {
            executingHabilities = true;
            FreezeExecute();
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
}
