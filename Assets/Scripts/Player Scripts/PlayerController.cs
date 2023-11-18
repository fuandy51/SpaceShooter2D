using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float min_X, max_X;
    public event System.Action destroyed;

    private Animator anim;

    [SerializeField]
    private GameObject player_Bullet;

    [SerializeField]
    private Transform attack_Point;

    public float attack_Timer = 0.35f;
    private float current_Attack_Timer;
    private bool canAttack;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        anim = GetComponent<Animator>();
        gameObject.SetActive(true);
        current_Attack_Timer = attack_Timer;
    }

    void Die()
    {
        PlayDestroyAnimationAndRespawn();
    }

    void PlayDestroyAnimationAndRespawn()
    {
        anim.Play("PlayerExplosion");
        StartCoroutine(SwitchToIdleAfterDelay(0.5f));
    }

    IEnumerator SwitchToIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.Play("PlayerIdle");
        Respawn();
    }

    void Respawn()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 temp = transform.position;
        temp.x += horizontalInput * speed * Time.deltaTime;

        if (temp.x > max_X)
            temp.x = max_X;
        else if (temp.x < min_X)
            temp.x = min_X;

        transform.position = temp;
    }

    void Attack()
    {
        Instantiate(player_Bullet, attack_Point.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet")|| 
            other.CompareTag("Enemy1")|| 
            other.CompareTag("Enemy2"))
        {
            PlayDestroyAnimationAndRespawn();
            Destroy(other.gameObject);
        }
    }
}
