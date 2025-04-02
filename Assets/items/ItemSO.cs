using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
   [Header("Identification")]
    public string itemID; // Unique identifier e.g. "health_potion_01"
    
    [Header("Properties")]
    public string itemName;
    
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;


    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            PlayerHealth playerhealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            if (playerhealth.health == playerhealth.maxHealth)
            {
                return false;
            }
            else
            {
                playerhealth.Heal(amountToChangeStat);
                return true;
            }
        }
        return false;
    }


    public enum StatToChange
    {
        none,
        health,
        mana,
    };

    public enum AttributeToChange
    {
        none,
        strength,
        agility,
        healthBar,
    }
}
