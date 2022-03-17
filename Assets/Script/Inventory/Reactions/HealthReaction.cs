using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : Reaction
{
    public FloatVaule playerHealth;
    public MySignal healSignal;
    
    public void Use(int healAmount)
    {
        playerHealth.RuntimeValue += healAmount;
        if (playerHealth.RuntimeValue >= 0 && playerHealth.RuntimeValue <= playerHealth.MaxValue)
        {
            healSignal.Raise();
        }
        else if (playerHealth.RuntimeValue > playerHealth.MaxValue)
        {
            playerHealth.RuntimeValue = playerHealth.MaxValue;
            healSignal.Raise();
        }
    }
}
