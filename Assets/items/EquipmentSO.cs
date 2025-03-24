using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSO", menuName = "Scriptable Objects/EquipmentSO")]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int strength, def;

    [SerializeField]
    ItemType itemType;
    
    [SerializeField]
    private Sprite itemSprite;


    [SerializeField]
    private AnimationClip w1, w2, wAir;

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
        GameObject.Find("AnimationOverrideManager").GetComponent<AnimatorOverrider>().UnEquipWeaponAnim(itemType);
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
 