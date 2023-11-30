using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_green : MonoBehaviour

{
    public float speed = 5f;
    public float range;
    int direction;
    float startingY;
    private float attackTimer = 0f;
    public float timeBetweenAttacks = 10f;
    [SerializeField]
    private GameObject EnemyBullet;

    [SerializeField]
    private Transform EnemyAttackPoint;
    public float minY, maxY;




    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
        direction = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        patrolling();
        
    }

   

    void patrolling()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime * direction);
        if (transform.position.y > maxY || transform.position.y < minY)
        {
            direction *= -1;
        }
    }

    void move()
    {
        transform.position = transform.position + (Vector3.left * speed) * Time.deltaTime;
    }
    void moveback()
    {
        if (transform.position.x > 8)
            transform.position = transform.position + (Vector3.right * speed) * Time.deltaTime;
    }

    void Attack()
    {
        attackTimer += Time.deltaTime;

        // Check if the attackTimer exceeds the timeBetweenAttacks
        if (attackTimer >= timeBetweenAttacks)
        {
            Debug.Log("AttackTimeTick");
            attackTimer = 0f; // Reset the attack timer
            Instantiate(EnemyBullet, EnemyAttackPoint.position, Quaternion.identity);
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

   
}

