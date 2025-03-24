using UnityEngine;

public class Slime : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        EnemyS enemy = hitInfo.GetComponent<EnemyS>();
        Debug.Log(enemy);
        if (enemy != null)
        {
            enemy.TakeDam(damage);
            Destroy(gameObject);
        }



    }
}
