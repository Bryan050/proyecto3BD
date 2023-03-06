using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Servidor servidor;

    public InputField InUsuario;
    public InputField InPass;
    public GameObject ImLoading;
    public DBUsuario usuario;

    public void IniciarSesion()
    {
        StartCoroutine(Iniciar());
    }
    IEnumerator Iniciar()
    {
        ImLoading.SetActive(true);
        string[] datos = new string[2];
        datos[0] = InUsuario.text;
        datos[1] = InPass.text;
        StartCoroutine(servidor.ConsumirServicio("Login", datos, PosCargar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        ImLoading.SetActive(false);
    }
    public void IniciarSesion2()
    {
        StartCoroutine(Iniciar2());
    }
    IEnumerator Iniciar2()
    {
        string[] datos = new string[2];
        datos[0] = PlayerPrefs.GetString("Usuario");
        datos[1] = PlayerPrefs.GetString("Pass");
        StartCoroutine(servidor.ConsumirServicio("Login", datos, PosCargar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
    }
    private void SavePlayerData(DBUsuario usuario)
    {
        PlayerPrefs.SetString("Usuario", usuario.Usuario);
        PlayerPrefs.SetString("Pass", usuario.Pass);
        PlayerPrefs.SetFloat("Posx", usuario.Posx);
        PlayerPrefs.SetFloat("Posy", usuario.Posy);
        PlayerPrefs.SetFloat("Posz", usuario.Posz);
        PlayerPrefs.SetInt("Puntaje", usuario.Puntaje);
        PlayerPrefs.SetInt("Nivel", usuario.Nivel);
        PlayerPrefs.SetFloat("Vida", usuario.Vida);
        PlayerPrefs.SetInt("Asesinados", usuario.Asesinados);
    }
    public void Register()
    {
        SceneManager.LoadScene("Register");
    }
    void PosCargar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 204: //El usuario o la contraseña son incorrectos
                print(servidor.respuesta.mensaje);
                break;
            case 205: //inicio de sesion correcto
                SceneManager.LoadScene("Main Menu");
                usuario = JsonUtility.FromJson<DBUsuario>(servidor.respuesta.respuesta);
                SavePlayerData(usuario);
                break;
            case 402: //Faltan datos para ejecutar la ccion solicitada
                print(servidor.respuesta.mensaje);
                break;
            case 404: //Errror
                print("Error, no se puede conectar con el servidor");
                break;
            default:
                break;
        }
    }
}
