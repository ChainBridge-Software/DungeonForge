using System.Xml.Serialization;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSO", menuName = "Scriptable Objects/EquipmentSO")]
public class EquipmentSO : ScriptableObject
{
    [Header("Identification")]
    public string itemID; // Unique identifier e.g. "iron_sword_01"
    
    [Header("Visuals")]
    public string itemName;
    public Sprite itemSprite;
    public string leiras;
    public ItemType itemType;

    
    [Header("Stats")]
    public int strength, def, agility, dashHossz;
    public bool invincibility;

    [SerializeField]
    private AnimationClip w1, w2, wAir;

    public void PreviewEquipment()
    {
        GameObject.Find("StatManager").GetComponent<PlayerStats>().
            PreviewEquipmentStats(strength, def, agility, itemSprite, leiras);
    }

    public void EquipItem()
    {
        PlayerStats playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerStats.strength += strength;
        playerStats.def += def;
        playerStats.agility += agility;
        playerStats.dashHossz += dashHossz;
        if (!playerStats.invincibleDash)
            playerStats.invincibleDash = invincibility;
        playerStats.UpdateEquipmentStats();
    }

    public void UnEquipItem(string slot)
    {
        PlayerStats playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerStats.strength -= strength;
        playerStats.def -= def;
        playerStats.agility -= agility;
        playerStats.dashHossz -= dashHossz;
        playerStats.UpdateEquipmentStats();

        if (slot == "Armor")
            playerStats.invincibleDash = false;
        if (slot == "Weapon")
            GameObject.Find("AnimationOverrideManager").GetComponent<AnimatorOverrider>().UnEquipWeaponAnim(itemType);
        else if (slot == "ab1")
            GameObject.Find("AnimationOverrideManager").GetComponent<AnimatorOverrider>().UnEquipAbility1Anim();
        else if (slot == "ab2")
            GameObject.Find("AnimationOverrideManager").GetComponent<AnimatorOverrider>().UnEquipAbility2Anim();
        else if (slot == "ab3")
            GameObject.Find("AnimationOverrideManager").GetComponent<AnimatorOverrider>().UnEquipAbility3Anim();
    }
    public void WeaponAnim()
    {
        GameObject.Find("AnimationOverrideManager").GetComponent<AnimatorOverrider>().EquipWeaponAnim(itemType, w1, w2, wAir);
    }
    public void AbilityAnim(string ab)
    {
        if (ab == "ab1")
            GameObject.Find("AnimationOverrideManager").GetComponent<AnimatorOverrider>().EquipAbility1Anim(itemType, w1);
        else if (ab == "ab2")
            GameObject.Find("AnimationOverrideManager").GetComponent<AnimatorOverrider>().EquipAbility2Anim(itemType, w1);
        else
            GameObject.Find("AnimationOverrideManager").GetComponent<AnimatorOverrider>().EquipAbility3Anim(itemType, w1);
    }

    
}
 