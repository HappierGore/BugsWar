using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleUpgrades : MonoBehaviour
{
    private TroopShop troopShop;
    //Food Multiplier
    public float foodMultiplierSpeed = 0.5f;
    public float foodMultiplierCost;
    private float foodMultiplierCostMultiplierSpeed = 1.0f, foodCostMultiplierSpeed = 10.0f;
    public int foodMultiplicerTimesUpgraded = 0;
    //
    private void Start()
    {
        troopShop = GetComponent<TroopShop>();
    }
    private void Update()
    {
        foodMultiplierCost = Mathf.RoundToInt(foodCostMultiplierSpeed * foodMultiplierCostMultiplierSpeed);
        if (UltimateButton.GetButtonDown("UpgradeFoodSpeed") && troopShop.GetFood() >= (foodCostMultiplierSpeed * foodMultiplierCostMultiplierSpeed))
        {
            troopShop.TakeFood(foodMultiplierCost);
            if (foodMultiplierSpeed <= 0.2f)
                foodMultiplierSpeed -= 0.01f;
            else
                foodMultiplierSpeed -= 0.1f;
            foodMultiplierCostMultiplierSpeed *= 1.5f;
            foodMultiplicerTimesUpgraded++;
        }
    }


}
