using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items")]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public string itemName;
    public string itemDesription;
    public string imagePath;
    public string reactionPath;
    public Sprite itemImage;
    public int stack;
    public float sellPrice;
    public float purchasePrice;
    public int itemLevel;
    public bool isUsable;
    public bool isUnique;
    public Reaction reaction;

    public void Consume()
    {
        if(stack > 0)
        {
            HealthReaction healthReaction = (HealthReaction)reaction;
            if (healthReaction != null)
            {
                healthReaction.Use(4);
            }
            
        }
        
    }

    public void DecreaseAmount()
    {
        stack--;
        if(stack < 0)
        {
            stack = 0;
        }
    }
}