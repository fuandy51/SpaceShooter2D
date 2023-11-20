using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScripts : MonoBehaviour
{
    public float speed = 5f;
    private Animator anim;
    public event System.Action destroyed;
    public GameObject enemyBulletPrefab;
    public float bulletSpeed = 5f;
    public float shootInterval = 2f;

    [SerializeField]
    private List<Transform> attackPoints = new List<Transform>();

    private bool isDestroyed = false;
    private bool isMovingRight = true; // Flag to determine the direction of horizontal movement

    [SerializeField]
    private int maxHealth = 1;

    // Adjust the maximum health as needed
    private int currentHealth;

    [SerializeField]
    private float leftBound = -5f; // Adjust as needed
    [SerializeField]
    private float rightBound = 5f; // Adjust as needed
    [SerializeField]
    private float movementDelay = 2f; // Adjust as needed
    [SerializeField]
    private float downwardDistance = 2f; // Adjust as needed

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDestroyed)
        {
            if (other.CompareTag("PlayerBullet") || other.CompareTag("Player"))
            {
                TakeDamage();
            }
            else if (other.CompareTag("Boundary"))
            {
                DestroyEnemy();
            }
        }
    }

    void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            PlayDestroyAnimationAndDestroy();
        }
    }

    void PlayDestroyAnimationAndDestroy()
    {
        // Disable the collider during the animation
        GetComponent<Collider2D>().enabled = false;

        anim.Play("Destroy1");
        Invoke("DestroyEnemy", 0.25f);
    }

    void DestroyEnemy()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;

            if (destroyed != null)
            {
                destroyed.Invoke();
            }

            Destroy(gameObject);
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        StartCoroutine(ShootRandomly());
        StartCoroutine(MoveRandomly());
        gameObject.SetActive(true);
    }

    IEnumerator ShootRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, shootInterval));
            Shoot();
        }
    }

    void Shoot()
    {
        foreach (Transform attackPoint in attackPoints)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, attackPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bullet.tag = "EnemyBullet";
            bulletRb.velocity = new Vector2(0f, -bulletSpeed);
        }
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            float moveDirection = isMovingRight ? 1f : -1f;
            float targetX = transform.position.x + moveDirection * Random.Range(leftBound, rightBound);

            while (Mathf.Abs(transform.position.x - targetX) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetX, transform.position.y), speed * Time.deltaTime);
                yield return null;
            }

            // Move downward
            float targetY = transform.position.y - downwardDistance;

            while (transform.position.y > targetY)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetY), speed * Time.deltaTime);
                yield return null;
            }

            isMovingRight = !isMovingRight;

            yield return new WaitForSeconds(movementDelay);
        }
    }
}
