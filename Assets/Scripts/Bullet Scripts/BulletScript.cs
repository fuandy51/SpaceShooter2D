using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 8f;
    public event System.Action Destroyed;

    private void Start()
    {
        
    }

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        Vector3 temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            DestroyBullet();
        }
        else if (other.CompareTag("Enemy1"))
        {
            // Use .GetComponent<ScoreScript>() to get the ScoreScript component
            ScoreScript.scoreValue += 50;
            DestroyBullet();
        }
        else if (other.CompareTag("Enemy2"))
        {
            ScoreScript.scoreValue += 80;
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        if (Destroyed != null)
        {
            Destroyed.Invoke();
        }

        Destroy(gameObject);
        
    }
}
