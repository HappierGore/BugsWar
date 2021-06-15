using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTargetSelection
{
    //Función para detectar el objetivo del mob considerando si es aliado o enemigo
    public static void DetectTarget(MobStats stats)
    {
        //String temporal para definir si es aliado o enemigo
        string targetTemp = (stats.IsAlly()) ? "enemies" : "allies" ;

        //Objeto en arreglo para encontrar todos los posibles enemigos con el tag previemente definido
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTemp);

        //Transform del resultado (Es decir, los objetivos seleccionados)
        Transform[] result = new Transform[targets.Length];

        //Variables para cálculo de distancias, resultTemp es el primer valor que tendrá la variable, como si fuera su valor máximo
        float distanceTemp = 0.0f, resultTemp = 9999999.0f;

        //Si hay objetivos
        if(targets.Length > 0)
        {
            //Iniciar ciclo for donde se ejecutará la cantidad de veces de el tamaño del arreglo "targets"
            for (int i = 0; i < targets.Length; i++)
            {
                //Variable temporal para definir la distancia entre el mob en cuestión y alguno de sus objetivos (Considerando TODOS, incluso el castillo)
                //de este modo logramos permitir que el mob que tome como objetivo al mob enemigo más cercano
                distanceTemp = DistanceP1P2(stats.transform.position.x, targets[i].transform.position.x);

                //Si la distancia anterior calculada es menor a un valor a un valor máximo previamente definido, entonces seleccionará
                //ese objetivo para asegurar que tome como objetivo el target más cercano al mob en cuestión
                if (distanceTemp < resultTemp)
                {
                    //Cambia la variable de la distancia máxima actual
                    resultTemp = distanceTemp;
                    //Selecciona ese objetivo
                    stats.target = targets[i].transform;
                }
            }
        }
        else
        {
            //Si no hay mobs físicos como targets(objetivos) entonces selecciona algún castillo
            stats.target = (stats.IsAlly()) ? MobMovement.enemyCastle : MobMovement.ownCastle;
        }
    }

    //Función que nos permite saber distancias entre dos puntos
    private static float DistanceP1P2(float P1, float P2)
    {
        float distance = Mathf.Abs(P1 - P2);
        return distance;
    }
}
