using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float deactive_Timer;
    public float Radius = 1f;

    [SerializeField]
    private GameObject enemy_Prefab;

    [SerializeField]
    private float minimumSpawnTime;

    [SerializeField]
    private float maximumSpawnTime;
    private float timeUntilSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            SpawnObjectRandom();
            SetTimeUntilSpawn();
        }
    }

    void SpawnObjectRandom()
    {
        Vector3 randomPos = Random.insideUnitCircle * Radius;
        Instantiate(enemy_Prefab, transform.position + randomPos, Quaternion.identity);
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        Invoke("DeactivateGameObject", deactive_Timer);
    }
}
