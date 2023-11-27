using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterMoveScript : MonoBehaviour
{
    public float moveSpeed = 2;
    public float deathzone = -19;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        DeactivateOnDeathZone();
    }

    void DeactivateOnDeathZone()
    {
        if (transform.position.x < deathzone)
        {

            gameObject.SetActive(false);
        }
    }

    public void Die()
    {
        Debug.Log("Deactivating Booster");
        gameObject.SetActive(false);
    }
}
