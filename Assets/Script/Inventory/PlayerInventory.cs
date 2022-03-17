using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Player Inventory")]
[System.Serializable]
public class PlayerInventory : ScriptableObject
{
    public List<Inventory> myInventory = new List<Inventory>();

}
