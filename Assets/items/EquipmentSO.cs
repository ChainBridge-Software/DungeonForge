using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSO", menuName = "Scriptable Objects/EquipmentSO")]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int strength, def;

    [SerializeField]
    private Sprite itemSprite;

    public void PreviewEquipment()
    {
        GameObject.Find("StatManager").GetComponent<PlayerStats>().
            PreviewEquipmentStats(strength, def, itemSprite);
    }

    public void EquipItem()
    {
        PlayerStats playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerStats.strength += strength;
        playerStats.def += def;
        playerStats.UpdateEquipmentStats();
    }

    public void UnEquipItem()
    {
        PlayerStats playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerStats.strength -= strength;
        playerStats.def -= def;
        playerStats.UpdateEquipmentStats();
    }
}
 