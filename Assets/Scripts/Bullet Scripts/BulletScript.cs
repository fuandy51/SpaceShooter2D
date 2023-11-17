using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5f;
    public float deactivate_Timer = 3f;
    public float maxY = 10f; // Replace with the desired maxY value
    public float minY = 10f;

    void Start()
    {
        Invoke("DeactivateGameObject", deactivate_Timer);
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        gameObject.SetActive(true);
    }

    void Update()
    {
        Move();

        // Check if bullet is out of maxY
        if (transform.position.y > maxY || transform.position.y < minY)
        {
            DeactivateGameObject();
        }
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }
}

