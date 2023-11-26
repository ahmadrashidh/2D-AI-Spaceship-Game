using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float minY, maxY;

    [SerializeField]
    private GameObject playerBullet;

    [SerializeField]
    private Transform attackPoint;

    public float attackTimer = 1f;
    private float crntAttackTimer;
    private bool canAttack;
    public string target;

    // Start is called before the first frame update
    void Start()
    {
        crntAttackTimer = attackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        Attack();
    }

    void movePlayer()
    {
        if(Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > maxY)
                temp.y = maxY;

            transform.position = temp;

        } else if(Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < minY)
                temp.y = minY;

            transform.position = temp;

        }
    }

    void Attack()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer > crntAttackTimer)
        {
            canAttack = true;
        }



        if (Input.GetKeyDown(KeyCode.K))
        {
            if (canAttack)
            {
                canAttack = false;
                attackTimer = 0f;
                Instantiate(playerBullet, attackPoint.position, Quaternion.identity);
            }
            
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (target == "Enemy")
        {
            Death();
            collision.gameObject.GetComponent<enemyscript>().Death();
        }
    }
}
