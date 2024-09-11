using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint; 
    public float spawnInterval = 2f; 

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval); // Spawn fonksiyonunu belirli aralıklarla çağır
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Yeni bir düşman oluştur
    }
}
