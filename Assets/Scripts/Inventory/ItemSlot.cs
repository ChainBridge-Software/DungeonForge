using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int maxItems;

    //item data
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDesc;
    public Sprite emptySprite;

    public ItemType type;


    //slot
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    //desc
    public Image itemDescImg;
    public TMP_Text itemDescName;
    public TMP_Text itemDescText;



    public GameObject selectedShader;
    public bool isSelected;

    private InventoryS inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryS>();
    }


    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDesc, ItemType type)
    {
        //check for fullness
        if (isFull)
            return quantity;

        this.type = type;
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDesc = itemDesc;


        this.quantity += quantity;
        Debug.Log(quantity);
        if (this.quantity >= maxItems)
        {
            Debug.Log("full slot");
            quantityText.text = maxItems.ToString();
            quantityText.enabled = true;
            isFull = true;
        
            int extraItems = this.quantity - maxItems;
            this.quantity = maxItems;
            return extraItems;
        }
        //update quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        Debug.Log("clicked");
        Debug.Log(isSelected);
        if (isSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);

            if (usable)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                    EmptySlot();
            }

            
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            isSelected = true;
            itemDescName.text = itemName;
            itemDescText.text = itemDesc;
            itemDescImg.sprite = itemSprite;
            if (itemDescImg.sprite == null)
            {
                itemDescImg.sprite = emptySprite;
            }
        }

        
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;
        itemDescName.text = "";
        itemDescText.text = "";
        itemDescImg.sprite = emptySprite;

    }

    public void OnRightClick()
    {

    }
}
