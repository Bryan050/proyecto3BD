using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text txtHealth;
    public Text txtEnemyKilled;
    public Text txtScore;
    private EditarUsuario editarUsuario;
    private Login login;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(player, Vector3.zero, Quaternion.identity) as GameObject;
        editarUsuario = GameObject.Find("Controller").GetComponent<EditarUsuario>();
        login = GameObject.Find("Controller").GetComponent<Login>();

        txtHealth = txtHealth.GetComponent<Text>();
        txtEnemyKilled = txtEnemyKilled.GetComponent<Text>();
    }

    public void updateHealth(int health)
    {
        txtHealth.text = health.ToString();
    }
    public void updateEnemyKilled(int enemyKilled)
    {
        txtEnemyKilled.text = enemyKilled.ToString();
    }
    public void updateScore(int score)
    {
        txtScore.text = score.ToString();
    }
    public void SaveGameDataBDD()
    {
        editarUsuario.ActualizarUsuario();
    }
    public void LoadGameDataBDD()
    {
        login.IniciarSesion2();
    }
}
