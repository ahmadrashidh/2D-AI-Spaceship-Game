using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpawnerScript : MonoBehaviour
{
    public GameObject booster;
    public float spawnRate = 2;
    public float timer = 0;
    public float HeightOffset = 2;
    public float deathzone = -19;
    // Start is called before the first frame update
    void Start()
    {

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
            SpawnBooster();
            timer = 0;
        }
    }

    void SpawnBooster()
    {
        float lowestpoint = transform.position.y - HeightOffset;
        float Highestpoint = transform.position.y + HeightOffset;
        Instantiate(booster, new Vector3(transform.position.x, Random.Range(lowestpoint, Highestpoint), -10), transform.rotation);
    }
}
