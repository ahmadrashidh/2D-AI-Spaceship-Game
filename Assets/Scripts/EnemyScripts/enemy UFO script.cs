using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyUFOscript : MonoBehaviour
{

    public float speed = 5f;
    public float range;
    int direction;
    public float minY, maxY;
    float startingY;
    private float attackTimer = 0f;
    public float timeBetweenAttacks = 10f;
    [SerializeField]
    private GameObject EnemyBullet;

    [SerializeField]
    private Transform EnemyAttackPoint;
    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
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
    {   if (transform.position.x > 9.3)
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
