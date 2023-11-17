using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScripts : MonoBehaviour
{
    public float speed = 5f;
    public float rotate_Speed = 50f;
    private Animator anim;

    public GameObject enemyBulletPrefab;
    public float bulletSpeed = 5f;
    public float shootInterval = 2f;
    public float moveSpeed = 2f;
    public float min_Y, max_Y;

    [SerializeField]
    private Transform attack_Point;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.Play("Destroy1");
        Destroy(gameObject, 0.5f);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ShootRandomly());
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
        GameObject bullet = Instantiate(enemyBulletPrefab, attack_Point.position, Quaternion.Euler(0f, 0f, 90f));
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's initial velocity along the y-axis
        bulletRb.velocity = new Vector2(0f, -bulletSpeed);
    }

    void MoveEnemy()
    {
        // Move the enemy downward
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
