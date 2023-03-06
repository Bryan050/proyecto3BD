using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[CreateAssetMenu(fileName ="Servidor", menuName ="Morion/Servidor", order =1)]
public class Servidor : ScriptableObject
{
    public string servidor;
    public Servicio[] servicios;

    public bool ocupado=false;
    public Respuesta respuesta;

    public IEnumerator ConsumirServicio(string nombre, string[] datos, UnityAction unityAction)
    {
        ocupado = true;
        WWWForm formulario = new WWWForm();
        //Debug.Log(nombre);
        //Debug.Log(servicios[0].nombre);
        Servicio s = new Servicio();
        for (int i = 0; i < servicios.Length; i++)
        {
            if (servicios[i].nombre.Equals(nombre))
            {
                s = servicios[i];
                Debug.Log(nombre);

            }
        }
        for (int i = 0; i < s.parametros.Length; i++)
        {
            //Debug.Log(s.parametros[i] +" "+ datos[i].ToString());
            formulario.AddField(s.parametros[i], datos[i]);
        }
        UnityWebRequest www = UnityWebRequest.Post(servidor + "/" + s.URL, formulario);
        Debug.Log(servidor + "/" + s.URL);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            respuesta = new Respuesta();
        }
        else {
            Debug.Log(www.downloadHandler.text);
            respuesta = JsonUtility.FromJson<Respuesta>(www.downloadHandler.text);
            respuesta.LimpiarRespuesta();
            Debug.Log(respuesta.respuesta);
        }

        ocupado = false;
        unityAction.Invoke();

    }
}

[System.Serializable]
public class Servicio {
    public string nombre;
    public string URL;
    public string[] parametros;
}

[System.Serializable]
public class Respuesta
{
    public int codigo;
    public string mensaje;
    public string respuesta;

    public void LimpiarRespuesta()
    {
        respuesta = respuesta.Replace('#','"');
    }

        public Respuesta() {
            codigo = 404;
            mensaje = "Error";
        }
    
}

[System.Serializable]
public class DBUsuario
{
    public int id;
    public string usuario;
    public string pass;
    public float posx = 0f;
    public float posy = 0f;
    public float posz = 0f;
    public int puntaje = 0;
    public float vida = 100;
    public byte nivel = 1;
    public int asesinados = 0;

    public string Usuario
    {
        set { usuario = value; }
        get { return usuario; }
    }

    public string Pass
    {
        set { pass = value; }
        get { return pass; }
    }

    public float Posx
    {
        set { posx = value; }
        get { return posx; }
    }
    public float Posy
    {
        set { posy = value; }
        get { return posy; }
    }
    public float Posz
    {
        set { posz = value; }
        get { return posz; }
    }
    public int Puntaje
    {
        set { puntaje = value; }
        get { return puntaje;  }
    }
    public float Vida
    {
        set { vida = value; }
        get{ return vida; }
    }
    public byte Nivel
    {
        set { nivel = value; }
        get { return nivel; }
    }
    public int Asesinados
    {
        set { asesinados = value; }
        get { return asesinados; }
    }

}