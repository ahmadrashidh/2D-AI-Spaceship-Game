using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsScripts : MonoBehaviour
{
    public float speed = 5f;
    public float deactivateTimer = 8f;
    public string target;
    public string target1;
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
        temp.x += speed * Time.deltaTime;
        transform.position = temp;

    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == collision.name)
        {
            Debug.Log("collision.detected");
            if (target == "Enemy")
            {
                collision.gameObject.GetComponent<enemyscript>().Death();
            }
            if (target1 == "Enemy Bullet")
            {
                Death();
                collision.gameObject.GetComponent<enemybulletscript>().Death();
            }
        }
    }

}
