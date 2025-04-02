using UnityEngine;

public class AfterBoss : MonoBehaviour
{

    public BossHealth bossHealth;
    public Transform boss;
    public GameObject[] objectsToEnable;

    public GameObject[] objectsToDrop;

    private Vector2 bossPosition;

    public void Start()
    {
        // Disable the objects at the start
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(false);
        }

        bossPosition = boss.position;
        
    }

    // If the boss is defeated, enable the objects and drop the objects
    private void Update()
    {
        

        if (bossHealth.health <= 0)
        {
            foreach (GameObject obj in objectsToEnable)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in objectsToDrop)
            {
                // Instantiate the object at the boss's position
                Instantiate(obj, bossPosition, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
