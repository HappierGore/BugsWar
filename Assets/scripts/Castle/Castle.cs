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

    // Update is called once per frame
    void Update()
    {
        if (stats.castleEvents.destroyed)
        {
            this.gameObject.SetActive(false);
        }
    }
}
