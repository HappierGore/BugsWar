using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleUpgrades : MonoBehaviour
{
    private TroopShop troopShop;
    //Multiplicador de comida (Mejora)
    public float foodMultiplierSpeed = 0.8f;

    //Costo del multiplicador de comida
    public float foodMultiplierCost;

    //Variables iniciales
    private float foodMultiplierCostMultiplierSpeed = 1.0f, foodCostMultiplierSpeed = 10.0f;

    //Cantidad de veces mejoradas del castillo
    public int foodMultiplicerTimesUpgraded = 0;
    //
    private void Start()
    {
        troopShop = GetComponent<TroopShop>();
    }
    private void Update()
    {
        //Evitamos puntos decimales y creamos el costo del multiplicador de comida
        foodMultiplierCost = Mathf.RoundToInt(foodCostMultiplierSpeed * foodMultiplierCostMultiplierSpeed);

        //Si se presiona el botón de mejorar velocidad de generación de recursos, se tiene suficientes recursos
        if (UltimateButton.GetButtonDown("UpgradeFoodSpeed") && troopShop.GetFood() >= (foodCostMultiplierSpeed * foodMultiplierCostMultiplierSpeed))
        {
            //Tomar comida
            troopShop.TakeFood(foodMultiplierCost);

            //EXPERIMENTAL (Aún no tenemos una forma exacta en la que se generará dinero)
            if (foodMultiplierSpeed <= 0.2f)
                foodMultiplierSpeed -= 0.01f;
            else
                foodMultiplierSpeed -= 0.1f;
            foodMultiplierCostMultiplierSpeed *= 1.5f;
            foodMultiplicerTimesUpgraded++;
        }
    }


}
