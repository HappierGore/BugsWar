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
        if(stats.TryGetComponent(out AreaAttack areaAttack))
        {
            stats.targets = GetTargetsInRangeAndPos(stats, areaAttack.radius);
        }
    }
    
    //Obtendrá los objetivos cercanos en un radio definido tomando como base u origen el target original del mob
    public static Transform[] GetTargetsInRangeAndPos(MobStats stats, float radius)
    {
        //String temporal para definir si es aliado o enemigo
        string targetTemp = (stats.IsAlly()) ? "enemies" : "allies";

        //Objeto en arreglo para encontrar todos los posibles enemigos con el tag previemente definido
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTemp);
        GameObject castle = (stats.IsAlly()) ? GameObject.Find("EnemyCastle") : GameObject.Find("OwnCastle");

        //Transform del resultado (Es decir, los objetivos seleccionados)
        Transform[] result = new Transform[targets.Length + 1];
        //Si hay objetivos

        float originPos = stats.target.transform.position.x;
        if (targets.Length > 0)
        {
            //Iniciar ciclo for donde se ejecutará la cantidad de veces de el tamaño del arreglo "targets"
            for (int i = 0; i < targets.Length; i++)
            {
                float tempPos = DistanceP1P2(targets[i].transform.position.x, stats.target.position.x);
                if(tempPos < radius)
                {
                    result[i] = targets[i].transform;
                }
            }
            //Después, al final se agregará el castillo, en caso de que este esté dentro del rango de alcance
            for (int i = 0; i < result.Length; i++)
            {
                float tempPos = DistanceP1P2(castle.transform.position.x, stats.target.position.x);
                if (result[i] == null && tempPos < radius)
                    result[i] = castle.transform;
            }
        }
        result = ResizeArray(result);

        return result;
    }

    //Función que nos permite saber distancias entre dos puntos
    private static float DistanceP1P2(float P1, float P2)
    {
        float distance = Mathf.Abs(P1 - P2);
        return distance;
    }

    //Permitirá eliminar todos los objetos "nulos" dentro de un arreglo
    public static Transform[] ResizeArray(Transform[] ArrayToResize)
    {
        int size = 0;
        for (int i = 0; i < ArrayToResize.Length; i++)
        {
            if (ArrayToResize[i] != null)
            {
                size += 1;
            }
        }
        Transform[] temp = new Transform[size];
        for (int i = 0; i < ArrayToResize.Length; i++)
        {
            if (ArrayToResize[i] != null)
            {
                //print("Primer condicional en " + i);
                for (int j = 0; j < temp.Length; j++)
                {
                    if (temp[j] == null)
                    {
                        // print("Segunda condicional i = " + i + " j = " + j);
                        temp[j] = ArrayToResize[i];
                        break;
                    }
                }
            }
        }

        return temp;
    }
}
