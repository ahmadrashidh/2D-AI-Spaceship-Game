using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemyscript : MonoBehaviour
{
    enum states { New, Patrol, Attack}
    enum moveDir { Up, Down}

    [SerializeField]
    private GameObject EnemyBullet;

    [SerializeField]
    private Transform EnemyAttackPoint;

    // Move fields
    public float deathzone = -19;
    public float moveSpeed = 5;

    // Attack fields
    private float attackTimer = 0f;
    public float timeBetweenAttacks = 10f;

    // Animator, Audio Source
    private Animator anim;
    public AudioSource audioSource;
    public AudioClip clip;

    // Interacting Object
    private GameObject player;
    private GameObject scene;
    private Vector2 screenPosition;


    // FSM
    private states state;

    void Start()
    {
        this.state = states.New;

        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("spaceship").gameObject;
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        play();
        CheckDeathZone();
    }

    void play()
    {
        Debug.Log("State:" + this.state);


        switch (this.state)
        {
            case states.New:
                assemble();
                break;

            case states.Patrol:
                findPlayer();
                break;

            case states.Attack:
                Attack();
                break;

        }

    }

    void assemble()
    {
        if (transform.position.x > 9)
        {
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        }
        else
        {
            this.state = states.Patrol;
        }
    }



    void findPlayer()
    {

        if (atFiringRange())
        {
            Debug.Log("Player Detected");
            this.state = states.Attack;

        } else
        {
            move();
        }
    }

    void move()
    {
        Array values = Enum.GetValues(typeof(moveDir));
        System.Random rnd = new System.Random();
        moveDir dir = (moveDir) values.GetValue(rnd.Next(values.Length));

        Debug.Log("Move Direction");
        if(dir == moveDir.Up)
        {
            // check boundaries
            // what if other enemy on the way

        } else // moveDir.Down
        {
            // check boundaries and other enemy
            // what if other enemy on the way
        }



    }




    void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= timeBetweenAttacks)
        {
            attackTimer = 0f; 
            Instantiate(EnemyBullet, EnemyAttackPoint.position, Quaternion.identity);
        }

        if (!atFiringRange())
        {
            Debug.Log("Enemy Moved");
            this.state = states.Patrol;

        }
    }

    bool atFiringRange()
    {
        double playerY = Math.Abs(player.transform.position.y);
        double enemyY = Math.Abs(transform.position.y);

        double enemyTopVision = enemyY + 0.5;
        double enemyBottomVision = enemyY - 0.5;

        Debug.Log("SearchPosition::" + playerY + " < " + enemyTopVision + "&&" + playerY + ">" + enemyBottomVision);

        return (playerY < enemyTopVision && playerY > enemyBottomVision);

    }

    void CheckDeathZone()
    {
        if (transform.position.x < deathzone)
        {
            Destroy(this.gameObject); 
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