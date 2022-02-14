using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemy;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5.5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    private int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if(countdown <= 0f){
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave(){
        // Debug.Log("Wave Incoming!");
        for(int i = 0; i < waveNumber; i++){
            SpawnEnemy();
            yield return new WaitForSeconds(.1f);
        }
        waveNumber++;
    }
    void SpawnEnemy(){
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
