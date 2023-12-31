using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsScripts : MonoBehaviour
{
    public float speed = 5f;
    public float deactivateTimer = 8f;
    public string target;
    public string target1;
    
    public LogicManager logic;
    public AudioSource audioSource;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivateTimer);
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicManager>();
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



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Enemy(Clone)" == collision.gameObject.name)
        {
            Debug.Log("Collision");
            audioSource.PlayOneShot(clip);
            collision.gameObject.GetComponent<enemyscript>().Death();

        }
    }

}
