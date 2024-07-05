using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player; // Ana karakter
    public Transform spawnPoint; // Düşmanların spawn noktası
    public GameObject enemyPrefab; // Düşman Prefabı
    public float spawnInterwal = 2f; //Düşmanların spawn aralığı
    public HealthBar healthBar; // Can barı scripti
    public Timer timer; //Timer scripti

    public float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterwal;
    }

    // Update is called once per frame
    void Update()
    {
        //düşman üretme sürecini kontrol etme
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnInterwal;
        }
    }

    void SpawnEnemy()
    {
        //rastgele pozisyonda düşman üretme
        Vector3 spawnPosition = new Vector3(Random.Range(-9f, 31f), spawnPoint.position.y, 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

}
