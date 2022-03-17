using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    public PlayerInventory inventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;

    public Collider2D boundary;
    private Transform target; //reference to the player
    public bool isShop = false;

    void MakeInventorySlots()
    {
        if(playerInventory)
        {
            for(int i = 0; i <playerInventory.myInventory.Count; i++)
            {
                if (playerInventory.myInventory[i].stack > 0)
                {
                    GameObject temp =
                    Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        newSlot.Setup(playerInventory.myInventory[i], this, isShop);
                    }
                } 
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        
    }

    private void OnEnable()
    {
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }

        MakeInventorySlots();
    }

    public bool CheckIfCanSell()
    {
        bool canSell;
        if (target)
        {
            canSell = boundary.bounds.Contains(target.transform.position);
        }
        else
        {
            canSell = false;
        }

        return canSell;
    }
}
