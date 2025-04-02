// ItemDatabase.cs
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    [SerializeField] private ItemSO[] items;
    [SerializeField] private EquipmentSO[] equipment;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ItemSO GetItemByID(string itemID)
    {
        foreach (var item in items)
        {
            if (item.itemID == itemID) return item;
        }
        return null;
    }

    public EquipmentSO GetEquipmentByID(string itemID)
    {
        foreach (var equip in equipment)
        {
            if (equip.itemID == itemID) return equip;
        }
        return null;
    }
}
