using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public Servidor servidor;

    public InputField InUsuario;
    public InputField InPass;
    public GameObject ImLoading;
    public DBUsuario usuario;

    public void RegistrarUsuario()
    {
        StartCoroutine(Registrar());
    }
    IEnumerator Registrar()
    {
        ImLoading.SetActive(true);
        string[] datos = new string[2];
        datos[0] = InUsuario.text;
        datos[1] = InPass.text;
        StartCoroutine(servidor.ConsumirServicio("Register", datos, PosCargar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        ImLoading.SetActive(false);

    }

    private void SavePlayerData(DBUsuario usuario)
    {
        PlayerPrefs.SetString("Usuario", usuario.Usuario);
        PlayerPrefs.SetString("Pass", usuario.Pass);
        PlayerPrefs.SetFloat("Posx", usuario.Posx);
        PlayerPrefs.SetFloat("Posy", usuario.Posy);
        PlayerPrefs.SetFloat("Posz", usuario.Posz);
        PlayerPrefs.SetInt("Puntaje", usuario.Puntaje);
        PlayerPrefs.SetFloat("Vida", usuario.Vida);
        PlayerPrefs.SetInt("Asesinados", usuario.Asesinados);
    }

    public void Login()
    {
        SceneManager.LoadScene("Login");
    }

    void PosCargar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 201: //Creado Correctamente
                SceneManager.LoadScene("Main Menu");
                usuario = JsonUtility.FromJson<DBUsuario>(servidor.respuesta.respuesta);
                SavePlayerData(usuario);
                break;
            case 401: //Error intentando crear ususario
                print(servidor.respuesta.mensaje);
                break;
            case 402: //Faltan datos para ejecutar la ccion solicitada
                print(servidor.respuesta.mensaje);
                break;
            case 403: //El usuario existe
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
