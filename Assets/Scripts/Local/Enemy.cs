using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float Health = 25;
    private Animator animator;
    private int score;
    private int enemyKilled;
    private GameController gameController;
    private GameObject gameControllerObject;
    void Start()
    {
        animator = GetComponent<Animator>();
        gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        gameController.updateEnemyKilled(enemyKilled);
    }
    public void LoadGUIData()
    {
        gameController.updateEnemyKilled(PlayerPrefs.GetInt("Asesinados"));
        gameController.updateScore(PlayerPrefs.GetInt("Puntaje"));
    }
    public void LoadData()
    {
        enemyKilled = PlayerPrefs.GetInt("Asesinados");
        score = PlayerPrefs.GetInt("Puntaje");
    }
    void Update()
    {
        LoadData();
        LoadGUIData();
        if (Health <= 0f)
        {
            score += 50;
            enemyKilled ++;

            PlayerPrefs.SetInt("Puntaje", score);
            PlayerPrefs.SetInt("Asesinados", enemyKilled);
            LoadGUIData();
            LoadData();
            Enemy enemy = this;
            Destroy(enemy.gameObject);
            //gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Health -= 1f;
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("target"))
        {
            animator.SetBool("isStopped", true);
        }
    }
}

