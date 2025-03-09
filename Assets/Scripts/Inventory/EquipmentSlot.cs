using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{

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
    private Image itemImage;

    //EQUIPPED SLOT
    [SerializeField]
    private EquippedSlot armor, W1, W2, Ab1, Ab2, Ab3;

    public GameObject selectedShader;
    public bool isSelected;

    private InventoryS inventoryManager;

    private EquipmentSOLibrary equipmentSOLibrary;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryS>();
        equipmentSOLibrary = GameObject.Find("Inventory").GetComponent<EquipmentSOLibrary>();
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
        this.quantity = 1;
        this.isFull = true;
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
        if (isFull)
        {
            if (isSelected)
            {
                EquipGear();


            }
            else
            {
                inventoryManager.DeselectAllSlots();
                selectedShader.SetActive(true);
                isSelected = true;
                for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
                {
                    if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                    {
                        equipmentSOLibrary.equipmentSO[i].PreviewEquipment();
                    }
                }
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            isSelected = true;
            GameObject.Find("StatManager").GetComponent<PlayerStats>().TurnOffPreviewStats();
        }

        


    }

    private void EquipGear()
    {
        if (type== ItemType.armor)
        {
            armor.EquipGear(itemSprite, itemName, itemDesc);
        } else if (type == ItemType.weapon)
        {
            if(!W1.GetComponent<EquippedSlot>().slotInUse)
                W1.EquipGear(itemSprite, itemName, itemDesc);
            else
                W2.EquipGear(itemSprite, itemName, itemDesc);

        } else if (type == ItemType.ability)
        {
            if (!Ab1.GetComponent<EquippedSlot>().slotInUse)
                Ab1.EquipGear(itemSprite, itemName, itemDesc);
            else if (!Ab2.GetComponent<EquippedSlot>().slotInUse)
                Ab2.EquipGear(itemSprite, itemName, itemDesc);
            else
                Ab2.EquipGear(itemSprite, itemName, itemDesc);
        }

        EmptySlot();
    }

    private void EmptySlot()
    {
        itemImage.sprite = emptySprite;
        isFull = false;

    }

    public void OnRightClick()
    {

    }
}
