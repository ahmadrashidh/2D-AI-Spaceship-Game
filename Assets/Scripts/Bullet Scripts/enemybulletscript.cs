using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybulletscript : MonoBehaviour
{
    public float speed = 5f;
    public string target;
    public string target1;
    public float deactivateTimer = 8f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;

    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
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
                collision.gameObject.GetComponent<PlayerController>().addHealth(-10);
               
            }
            if (target1 == "Player Bullet.prefab")
            {
                Death();
                
            }
        }
    }



}


