using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public FloatVaule coin;
    public MySignal coinSignal;

    [Header("UI to change")]
    [SerializeField] private Text itemNumberText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image smallImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject sellButton;
    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private Text sellPrice;

    //Variables of the item
    private Inventory item;
    private InventoryManager manager;

    [Header("Information To Show")]
    public GameObject information;
    private bool canShow;

    public void Setup(Inventory newItem, InventoryManager newManager, bool isShop)
    {
        item = newItem;
        manager = newManager;
        if(item)
        {
            itemImage.sprite = item.itemImage;
            smallImage.sprite = item.itemImage;
            if(itemNumberText != null)
            {
            itemNumberText.text = "" + item.stack;
            }
            
            if (isShop)
            {
                sellPrice.text = "" + item.purchasePrice;
            }
            else
            {
                sellPrice.text = "" + item.sellPrice;
            }
            SetTextAndButton(item.itemName, item.itemDesription, item.isUsable);
        }

       
    }

    public void SetTextAndButton(string name, string desrciption, bool buttonActive)
    {
        nameText.text = name;
        descriptionText.text = desrciption;
        if (useButton != null)
        {
            useButton.SetActive(buttonActive);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        canShow = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Showing Information UI
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        GetComponent<RectTransform>().localScale = new Vector3(1.15f, 1.15f, 1);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sellButton != null && manager.CheckIfCanSell())
        {
            sellButton.SetActive(true);
        }

        if (canShow)
        {
            information.SetActive(true);
            canShow = false;
        }
        else
        {
            information.SetActive(false);
            canShow = true;
        }

    }

    public void UseButtonPressed()
    {

        if (item)
        {
            item.Consume();
            //item.DecreaseAmount();

            //clear all slots and refill all slots with new numbers
            if (item.stack <=0)
            {
                manager.UpdateInventory();
            }
            else
            {
                itemNumberText.text = "" + item.stack;
            }
        }
        
    }

    public void SellButtonPressed()
    {
        if (manager.CheckIfCanSell())
        {
            if (item && item.stack > 0)
            {
                item.DecreaseAmount();
                coin.RuntimeValue += item.sellPrice;
                coinSignal.Raise();

                //clear all slots and refill all slots with new numbers
                if (item.stack <= 0)
                {
                    manager.UpdateInventory();
                }
                else
                {
                    itemNumberText.text = "" + item.stack;
                }
            }
        }
        else
        {
            sellButton.SetActive(false);
        }
       
    }

    public void PurchaseButtonPressed()
    {
        coin.RuntimeValue -= item.purchasePrice;
        coinSignal.Raise();

        for (int i = 0; i < manager.inventory.myInventory.Count; i++)
        {
            if (manager.inventory.myInventory[i].itemName == nameText.text)
            {
                manager.inventory.myInventory[i].stack++;
                break;
            }
        }
        
    }
}
