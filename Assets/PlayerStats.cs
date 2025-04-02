using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int strength, def, agility, dashHossz;
    public bool invincibleDash;

    [SerializeField]
    private TMP_Text strengthText, defText, agilityText;

    [SerializeField]
    private TMP_Text attackPreText, defPreText, agilityPreText;

    [SerializeField]
    private TMP_Text leiras;

    [SerializeField]
    private Image previewImage;

    [SerializeField]
    private GameObject selectedItemStats;
    [SerializeField]
    private GameObject selectedItemImage;

    void Start()
    {
        UpdateEquipmentStats();
    }


    public void UpdateEquipmentStats()
    {
        strengthText.text = strength.ToString();
        defText.text = def.ToString();
        agilityText.text = agility.ToString();
    }

    
    public void PreviewEquipmentStats(int strength, int def, int agi, Sprite itemSprite, string leir)
    {
        attackPreText.text = strength.ToString();
        defPreText.text = def.ToString();
        agilityPreText.text = agi.ToString() ;
        leiras.text = leir.ToString();

        //img
        previewImage.sprite = itemSprite;

        selectedItemImage.SetActive(true);
        selectedItemStats.SetActive(true);
    }

    public void TurnOffPreviewStats()
    {
        selectedItemImage.SetActive(false);
        selectedItemStats.SetActive(false);
    }
    
}
