using UnityEngine;

public class InventoryS : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;

    public ItemSlot[] itemSlot;
    public EquipmentSlot[] equipmentSlot;

    public InputManager inputManager;

    public ItemSO[] itemSOs;

    public EquippedSlot[] equippedSlot;

    private bool inventoryOpen;
    private bool equipmentOpen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventoryMenu.SetActive(false);
        EquipmentMenu.SetActive(false);
            
        
    }


    

    // Update is called once per frame
    void Update()
    {

        inventoryOpen = inputManager.GetBooleanTriggered("InventoryMenu");
        equipmentOpen = inputManager.GetBooleanTriggered("EquipmentMenu");

        if (inventoryOpen)
        {
            Inventory();
        }
            

        if (equipmentOpen)
        {
            Equipment();
        }
        
        
    }

    void Equipment()
    {
        if (EquipmentMenu.activeSelf)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(true);
        }
    }

    void Inventory()
    {
        if (InventoryMenu.activeSelf)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            EquipmentMenu.SetActive(false);
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDesc, ItemType type)
    {
        if (type == ItemType.consumable)
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
                {
                    int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDesc, type);
                    if (leftOverItems > 0)
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDesc, type);
                    return leftOverItems;
                }
            }
            return quantity;
        } else
        {
            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                if (equipmentSlot[i].isFull == false)
                {
                    int leftOverItems = equipmentSlot[i].AddItem(itemName, quantity, itemSprite, itemDesc, type);
                    if (leftOverItems > 0)
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDesc, type);
                    return leftOverItems;
                }
            }
            return quantity;
        }

        
    }

    public bool UseItem(string itemName)
    {
        for(int i = 0;i < itemSOs.Length;i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }
        return false;
    }

    public void DeselectAllSlots()
    {
        for(int i = 0;i < itemSlot.Length;i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].isSelected = false;
        }
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            equipmentSlot[i].selectedShader.SetActive(false);
            equipmentSlot[i].isSelected = false;
        }
        for (int i = 0; i < equippedSlot.Length; i++)
        {
            equippedSlot[i].selectedShader.SetActive(false);
            equippedSlot[i].isSelected = false;
        }

    }
}

public enum ItemType
{
    consumable,
    weapon,
    armor,
    ability,
    none,
};
