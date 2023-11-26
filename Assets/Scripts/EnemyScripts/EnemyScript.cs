using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5;
    public float deathzone = -19;
    private float attackTimer = 0f;
    public float timeBetweenAttacks = 2f; // Time between each attack
    

    [SerializeField]
    private GameObject enemy_Bullet;

    [SerializeField]
    private Transform enemy_attackPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        Attack();
        CheckDeathZone();
        
    }
    void Attack()
    {
        attackTimer += Time.deltaTime;

        // Check if the attackTimer exceeds the timeBetweenAttacks
        if (attackTimer >= timeBetweenAttacks)
        {
            attackTimer = 0f; // Reset the attack timer
            Instantiate(enemy_Bullet, enemy_attackPoint.position, Quaternion.identity);
        }
    }
    void CheckDeathZone()
    {
        if (transform.position.x < deathzone)
        {
            Destroy(gameObject); // Destroy this specific enemy when it crosses the deathzone
        }
    }
}
