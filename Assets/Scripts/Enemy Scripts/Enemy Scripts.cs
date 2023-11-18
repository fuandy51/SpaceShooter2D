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
    private List<Transform> attackPoints = new List<Transform>(); // Using a list to store attack points

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet")|| 
            other.CompareTag("Player"))
            
        {
            PlayDestroyAnimationAndDestroy();
        }
        else if (other.CompareTag("Boundary"))
        {
            DestroyEnemy();
        }
    }

    void PlayDestroyAnimationAndDestroy()
    {
        anim.Play("Destroy1");
        Invoke("DestroyEnemy", 0.25f);
    }

    void DestroyEnemy()
    {
        if (destroyed != null)
        {
            destroyed.Invoke();
        }

        Destroy(gameObject);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ShootRandomly());
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

    void Update()
    {
        MoveEnemy();
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

    void MoveEnemy()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
