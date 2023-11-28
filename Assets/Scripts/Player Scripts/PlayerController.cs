using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    public string BOOSTER_LABEL = "Booster(Clone)";
    public string ENEMY_LABEL = "Enemy";
    public float health;
    public Image progressFill;
    private Vector3 progressScale;

    public LogicManager logic;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        crntAttackTimer = attackTimer;
        health = 100;
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        Attack();
        checkHealth();

    }

    private void checkHealth()
    {

        progressScale = progressFill.rectTransform.localScale;

        progressScale.x = (health / 100);
        progressFill.rectTransform.localScale = progressScale;
      

        if (health < 0f)
        {
            gameObject.SetActive(false);
            logic.GameOver();
        }

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

    public void addHealth(int score)
    {
        health = (health + score) > 100 ? 100 : (health + score);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        collision.gameObject.GetComponent<BoosterMoveScript>().Die();

        if (ENEMY_LABEL == collision.gameObject.name)
        {
            addHealth(-25);
            collision.gameObject.GetComponent<enemyscript>().Death();
        } else
        {
            addHealth(25);
        }
    }
}
