using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    //SLOT APPearance
    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private TMP_Text slotName;

    //DATA
    [SerializeField]
    private ItemType type = new ItemType();

    private Sprite itemSprite;
    private string itemName;
    private string itemDesc;

    //Other vars
    public bool slotInUse;
    [SerializeField]
    public GameObject selectedShader;

    [SerializeField]
    public bool isSelected;

    [SerializeField]
    private Sprite emptySprite;

    private InventoryS InventoryManager;

    private EquipmentSOLibrary equipmentSOLibrary;

    private void Start()
    {
        InventoryManager = GameObject.Find("Inventory").GetComponent<InventoryS>();
        equipmentSOLibrary = GameObject.Find("Inventory").GetComponent<EquipmentSOLibrary>();
    }

    public void OnPointerClick(PointerEventData eventData)
    { 
        //On left click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }
        //right click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
    }

    void LeftClick()
    {
        if (isSelected && slotInUse)
        {
            UnEquipGear();
        }
        else
        {
            InventoryManager.DeselectAllSlots();
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

    void RightClick()
    {
        UnEquipGear();
    }



    public void EquipGear(Sprite itemSprite, string itemName, string itemDesc)
    {
        //if smt is eq-p, unEq it
        if (slotInUse)
            UnEquipGear();
        //update img for slot
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;

        //update data
        this.itemName = itemName;
        this.itemDesc = itemDesc;

        //update playerStats
        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].EquipItem();
            }
        }


        slotInUse = true;
        Debug.Log(type);


    }

    public void UnEquipGear()
    {
        slotInUse = false;
        InventoryManager.DeselectAllSlots();

        InventoryManager.AddItem(itemName, 1, itemSprite, itemDesc, type);

        this.itemSprite = emptySprite;
        slotImage.sprite = itemSprite;
        slotName.enabled = true;

        //update playerStats
        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].UnEquipItem();
            }
        }

        GameObject.Find("StatManager").GetComponent<PlayerStats>().TurnOffPreviewStats();
    }

}
