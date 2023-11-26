using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybulletscript : MonoBehaviour
{
    public float speed = 5f;
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
        // Move towards the negative x-axis (to the left)
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}

