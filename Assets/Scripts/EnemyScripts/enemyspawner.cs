using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public float spawnRate = 2;
    public float timer = 0;
    public float HeightOffset = 10;
    private const int NO_ENEMIES = 1;
    private int ctr_enemies = 0;

    void Start()
    {
        spawnenemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnenemy();
            timer = 0;
        }

    }

    void spawnenemy()
    {
        float lowestpoint = transform.position.y - HeightOffset;
        float Highestpoint = transform.position.y + HeightOffset;

        Debug.Log("ctr_enemies:" + ctr_enemies);
        if(ctr_enemies < NO_ENEMIES)
        {
            Debug.Log("SpawningEnemy: " + ctr_enemies);
            Instantiate(enemy, new Vector3(transform.position.x, Random.Range(lowestpoint, Highestpoint), -10), transform.rotation);
            ctr_enemies++;
        }
            
    }

}
