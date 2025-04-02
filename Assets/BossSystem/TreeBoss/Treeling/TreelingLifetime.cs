using UnityEngine;

public class TreelingLifetime : MonoBehaviour
{

    public EnemyS health;
    public float minusHpPerSecond = 1;

    void Start()
    {
        health = GetComponent<EnemyS>();
    }

    void Update()
    {
        health.health -= minusHpPerSecond * Time.deltaTime;
    }
}
