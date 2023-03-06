using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Arnold : MonoBehaviour
{
    public float rotationSpeed = 200.0f;
    public float movementSpeed = 5.0f;
    public float x, y;
    public Animator animator;
    private float health;
    private int score;
    private GameController gameController;
    private GameObject gameControllerObject;
    private WaveSpawner waveSpawner;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        animator = GetComponent<Animator>();
        gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        waveSpawner = GameObject.Find("Controller").GetComponent<WaveSpawner>();
        LoadGUIData();
    }
    public void LoadData()
    {
        Debug.Log(PlayerPrefs.GetFloat("Posx"));
        if (PlayerPrefs.GetFloat("Posx") != 0)
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("Posx"), PlayerPrefs.GetFloat("Posy"), PlayerPrefs.GetFloat("Posz"));
        }
        else
        {
            transform.position = new Vector3(225.46f, 0, 208.422f);
        }
        health = PlayerPrefs.GetFloat("Vida");
    }
    public void LoadGUIData()
    {
        gameController.updateHealth((int)PlayerPrefs.GetFloat("Vida"));
        gameController.updateEnemyKilled(PlayerPrefs.GetInt("Asesinados"));
        gameController.updateScore(PlayerPrefs.GetInt("Puntaje"));
    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        GetPosition();

        transform.Rotate(0,x*Time.deltaTime*rotationSpeed,0);
        transform.Translate(0,0,y*Time.deltaTime*movementSpeed);

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = 15.0f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = 5.0f;
        }
    }

    public void GetPosition()
    {
        PlayerPrefs.SetFloat("Posx", transform.position.x);
        PlayerPrefs.SetFloat("Posy", transform.position.y);
        PlayerPrefs.SetFloat("Posz", transform.position.z);
    }
    public void RestartValues()
    {
        PlayerPrefs.SetFloat("Posx", 225.46f);
        PlayerPrefs.SetFloat("Posy", 0);
        PlayerPrefs.SetFloat("Posz", 208.422f);
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.SetFloat("Vida", 100f);
        PlayerPrefs.SetInt("Asesinados", 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandAtack"))
        {
            health -= 0.5f;
            PlayerPrefs.SetFloat("Vida", health);
            LoadGUIData();
            if (health <= 0)
            {
                RestartValues();
                gameController.SaveGameDataBDD();
                SceneManager.LoadScene("Main Menu");
                //Destroy(gameObject);
            }
        }
        if (other.CompareTag("Meta"))
        {
            RestartValues();
            PlayerPrefs.SetInt("Puntaje", score);
            PlayerPrefs.SetInt("Nivel", PlayerPrefs.GetInt("Nivel")+1);
            gameController.SaveGameDataBDD();
            SceneManager.LoadScene("Main Menu");
            //Destroy(gameObject);
        }
        
        switch (other.gameObject.tag)
        {
            case "ActivationWall1":
                waveSpawner.SpawnWave(1);
                break;
            case "ActivationWall2":
                waveSpawner.SpawnWave(2);
                break;
            case "ActivationWall3":
                waveSpawner.SpawnWave(3);
                break;
            case "ActivationWall4":
                waveSpawner.SpawnWave(4);
                break;
        }
    }
    
}
