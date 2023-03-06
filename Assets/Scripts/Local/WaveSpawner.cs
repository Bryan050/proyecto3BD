using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveSpawner : MonoBehaviour
{

    public int waveLength = 5;
    public GameObject enemy;

    public float timeToNextEnemy = 0.5f;
    public GameObject enemyRespawnPoint1;
    public GameObject enemyRespawnPoint2;
    public GameObject enemyRespawnPoint3;
    public GameObject enemyRespawnPoint4;
    private GameController gameController;
    private GameObject gameControllerObject;
    private GameObject enemyClone;

    private void Start()
    {
        enemyClone = Instantiate(enemy) as GameObject;

        enemyRespawnPoint1 = GameObject.FindWithTag("EnemyRespawnPoint1");
        enemyRespawnPoint2 = GameObject.FindWithTag("EnemyRespawnPoint2");
        enemyRespawnPoint3 = GameObject.FindWithTag("EnemyRespawnPoint3");
        enemyRespawnPoint4 = GameObject.FindWithTag("EnemyRespawnPoint4");
        gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    public void SpawnWave(int enemyRespawnPoint)
    {
        if (enemyClone==null)
        {
            enemyClone = Instantiate(enemy) as GameObject;
        }
        gameController.SaveGameDataBDD();
        switch (enemyRespawnPoint)
        {
            case 1:
                StartCoroutine(Spawn(enemyRespawnPoint1));
                break;
            case 2:
                StartCoroutine(Spawn(enemyRespawnPoint2));
                break;
            case 3:
                StartCoroutine(Spawn(enemyRespawnPoint3));
                break;
            case 4:
                StartCoroutine(Spawn(enemyRespawnPoint4));
                break;
        }
    }
    private IEnumerator Spawn(GameObject spawnPoint)
    {
        UnityEngine.Debug.Log(spawnPoint.transform.position.ToString());

        for (int i = 0; i <= waveLength; i++)
        {
            UnityEngine.Debug.Log("SPAWNING");

            Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeToNextEnemy);
        }
    }
}
