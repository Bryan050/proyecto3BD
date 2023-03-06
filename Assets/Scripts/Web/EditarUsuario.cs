using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditarUsuario : MonoBehaviour
{
    public Servidor servidor;
    public DBUsuario usuario;

    public void ActualizarUsuario()
    {
        StartCoroutine(Actualizar());
    }
    IEnumerator Actualizar()
    {
        //ImLoading.SetActive(true);
        string[] datos = new string[9];
        datos[0] = PlayerPrefs.GetString("Usuario");
        datos[1] = PlayerPrefs.GetString("Pass");
        datos[2] = PlayerPrefs.GetFloat("Posx").ToString();
        datos[3] = PlayerPrefs.GetFloat("Posy").ToString();
        datos[4] = PlayerPrefs.GetFloat("Posz").ToString();
        datos[5] = PlayerPrefs.GetInt("Puntaje").ToString();
        datos[6] = PlayerPrefs.GetFloat("Vida").ToString();
        datos[7] = PlayerPrefs.GetInt("Nivel").ToString();
        datos[8] = PlayerPrefs.GetInt("Asesinados").ToString();

        StartCoroutine(servidor.ConsumirServicio("Update", datos, PosCargar));
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        //ImLoading.SetActive(false);
    }

    void PosCargar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 201: //Creado Correctamente
                SceneManager.LoadScene("Entorno");
                usuario = JsonUtility.FromJson<DBUsuario>(servidor.respuesta.respuesta);
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
