using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int strength, def;

    [SerializeField]
    private TMP_Text strengthText, defText;

    [SerializeField]
    private TMP_Text attackPreText, defPreText;

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
    }

    public void PreviewEquipmentStats(int strength, int def, Sprite itemSprite)
    {
        attackPreText.text = strength.ToString();
        defPreText.text = def.ToString();

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
