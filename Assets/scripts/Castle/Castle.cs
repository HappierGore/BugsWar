using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    CastleStats stats;
    void Start()
    {
        stats = GetComponent<CastleStats>();
    }

    //Manager del castillo
    void Update()
    {
        //Si el castillo es destruido, desactivamos el objetos
        if (stats.castleEvents.destroyed)
        {
            this.gameObject.SetActive(false);
        }
    }
}
