using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemyscript : MonoBehaviour
{
    enum states { New, Patrol, Attack}
    public enum moveDir { Up, Down}

    [SerializeField]
    private GameObject EnemyBullet;

    [SerializeField]
    private Transform EnemyAttackPoint;

    // Movement
    public float deathzone = -19;
    public float moveSpeed = 5;
    public moveDir dir { get; set; }
    public float TOP = 4.54f;
    public float BOTTOM = -3.95f;

    // Attack fields
    private float attackTimer = 0f;
    public float timeBetweenAttacks = 4f;
    private float forwardAttackTimer = 0f;
    private float forwardAttackTimeBoundary = 8f;

    // Animator, Audio Source
    private Animator anim;
    public AudioSource audioSource;
    public AudioClip clip;

    // Interacting Object
    private GameObject[] teamOnGround;
    
    // FSM
    private states state;

    void Start()
    {
        this.state = states.New;
        anim = GetComponent<Animator>();
        teamOnGround = GameObject.FindGameObjectsWithTag("enemy");
        setRandomDir();
    }

    void setRandomDir()
    {
        Array values = Enum.GetValues(typeof(moveDir));
        this.dir = (moveDir)values.GetValue(new System.Random().Next(values.Length));
    }

    // Update is called once per frame
    void Update()
    {
        teamOnGround = GameObject.FindGameObjectsWithTag("enemy");
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
                Patrol();
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


    void Patrol()
    {

        if (atFiringRange())
        {
            this.state = states.Attack;

        }
        else //move
        {
            move();
        }
    }

    void move()
    {
        if(transform.position.y >= TOP)
        {
            moveDown();
            dir = moveDir.Down;

        } else if(transform.position.y <= BOTTOM)
        {
            moveUp();
            dir = moveDir.Up;
        } else
        {
            if(dir == moveDir.Up)
            {
                moveUp();
            } else
            {
                moveDown();
            }
        }
    }

    void moveUp()
    {
        Vector3 newPosition = transform.position + (Vector3.up * moveSpeed) * Time.deltaTime;
        transform.position = newPosition;
    }

    void moveDown()
    {
        Vector3 newPosition = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;
        transform.position = newPosition;
    }


    bool isOverlapEnemy(Vector3 pos)
    {
        BoxCollider2D boxCollider = this.GetComponent<BoxCollider2D>();

        if(boxCollider != null)
        {

            Vector2 center = boxCollider.bounds.center;
            Vector2 size = boxCollider.bounds.size;

            center.y = pos.y;
            Collider2D collider = Physics2D.OverlapBox(center, size, 0f, LayerMask.GetMask("Ignore Raycast"));

            if(collider == null)
            {
                return false;
            } else
            {
                Debug.Log("OverlapCollider:" + collider.gameObject.name);
                return true;
            }
        }

        Debug.Log("EnemyNotBoxCollider");
        return false;

    }

    void shoot()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= timeBetweenAttacks)
        {
            attackTimer = 0f;
            Instantiate(EnemyBullet, EnemyAttackPoint.position, Quaternion.identity);
        }
    }


    void Attack()
    {

        forwardAttackTimer += Time.deltaTime;

        if(forwardAttackTimer >= forwardAttackTimeBoundary)
        {
            forwardAttack();
        } else
        {
            shoot();
        }


        if (!atFiringRange())
        {
            Debug.Log("PlayerEvaded");
            this.state = states.Patrol;
        }
    }

    void forwardAttack()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        shoot();
    }


    bool atFiringRange()
    {

        Vector3 directionToPlayer = Vector3.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.left, LayerMask.GetMask("PlayerAcc"));

        Debug.DrawRay(transform.position, Vector3.left);

        return (hit.collider != null && hit.collider.CompareTag("spaceship"));

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

        if("Enemy(Clone)" == collision.gameObject.name)
        {
            enemyscript collider = collision.gameObject.GetComponent<enemyscript>();

            if (this.dir == moveDir.Up)
            {
                this.dir = moveDir.Down;
                collider.dir = moveDir.Up;
            } else
            {
                this.dir = moveDir.Up;
                collider.dir = moveDir.Down;

            }



            Debug.Log("GO_Direction::" + this.dir + "Collider_GO_Direction::" + collider.dir);

        }

    }

    public void Death()
    {
        anim.Play("destruction");
        Destroy(this.gameObject,0.15f);
    }
}