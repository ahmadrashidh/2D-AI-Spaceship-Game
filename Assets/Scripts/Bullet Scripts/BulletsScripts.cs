using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsScripts : MonoBehaviour
{
    public float speed = 5f;
    public float deactivateTimer = 8f;
    public string target;
    public string target1;
    private string ENEMY_LABEL = "Enemy";
    private string ENEMY_BULLET_LABEL = "Enemy Bullet";
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
        Debug.Log("Collision:" + collision.gameObject.name);
        if (ENEMY_LABEL.Equals(collision.gameObject.name))
        {
            collision.gameObject.GetComponent<enemyscript>().Death();

        } else if (ENEMY_BULLET_LABEL.Equals(collision.gameObject.name))
        {
            Death();
            collision.gameObject.GetComponent<enemybulletscript>().Death();
        }
    }

}
