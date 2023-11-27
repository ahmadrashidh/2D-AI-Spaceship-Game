using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5;

    [SerializeField]
    private GameObject EnemyBullet;

    [SerializeField]
    private Transform EnemyAttackPoint;

    private float attackTimer = 0f;
    public float timeBetweenAttacks = 2f;
    public string target;
    public float deathzone = -19;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        Attack();
        movePlayer();
        CheckDeathZone();
    }
    void movePlayer()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
    }


    void Attack()
    {
        attackTimer += Time.deltaTime;

        // Check if the attackTimer exceeds the timeBetweenAttacks
        if (attackTimer >= timeBetweenAttacks)
        {
            attackTimer = 0f; // Reset the attack timer
            EnemyBullet.SetActive(true);
            Instantiate(EnemyBullet, EnemyAttackPoint.position, Quaternion.identity);
        }
    }
    void CheckDeathZone()
    {
        if (transform.position.x < deathzone)
        {
            gameObject.SetActive(false); // Destroy this specific enemy when it crosses the deathzone
        }
    }
    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == collision.name)
        {
            if (target == "Spaceship")
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.health -= 25;
                player.healthText.text = player.health.ToString() + "%";
            }

        }
    }
}