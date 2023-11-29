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

    public float deathzone = -19;
    private float attackTimer = 0f;
    public float timeBetweenAttacks = 10f;
    public string target;
    public string target1;
    private Animator anim;
    public AudioSource audioSource;
    public AudioClip clip;


    void Start()
    {
        anim = GetComponent<Animator>();
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
            Debug.Log("AttackTimeTick");
            attackTimer = 0f; // Reset the attack timer
            Instantiate(EnemyBullet, EnemyAttackPoint.position, Quaternion.identity);
        }
    }
    void CheckDeathZone()
    {
        if (transform.position.x < deathzone)
        {
            Destroy(this.gameObject); // Destroy this specific enemy when it crosses the deathzone
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy Collision:" + collision.gameObject.name);

        if ("Spaceship" == collision.gameObject.name)
        {
            audioSource.PlayOneShot(clip);
            collision.gameObject.GetComponent<PlayerController>().addHealth(-10);
            Death();
        }

        if ("Player Bullet" == collision.gameObject.name)
        {
            audioSource.PlayOneShot(clip);
            Death();
        }
    }
    public void Death()
    {
        anim.Play("destruction");
        Destroy(this.gameObject,0.15f);
    }
}