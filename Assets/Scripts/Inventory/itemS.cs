using UnityEngine;

public class itemS : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    public ItemType type;

    [TextArea]
    [SerializeField]
    private string itemDesc;

    private InventoryS inventoryManager;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryS>() ;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDesc, type);

            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            } else
                quantity = leftOverItems;
            
        }
    }
}
