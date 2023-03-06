using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Premio : MonoBehaviour
{
    [SerializeField]
    private Transform _targetRespawn;
    private float x, y;
    public float rotationSpeed = 200.0f;
    public float movementSpeed = 5.0f;
    private void Start()
    {
        transform.position = _targetRespawn.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mlagent"))
        {
            Invoke("MoverPosicionInicial", 4);
            //Invoke("MoverPosicionInicial", 0);
        }
    }
    private void MoverPosicionInicial()
    {
        bool PosicionEncontrada = false;
        int intentos = 100;
        Vector3 posicionPotencial = Vector3.zero;
        while (!PosicionEncontrada || intentos >= 0)
        {
            intentos--;
            posicionPotencial = new Vector3(_targetRespawn.position.x+UnityEngine.Random.Range(-10f,10f), 0.555f, _targetRespawn.position.z+UnityEngine.Random.Range(-4f,4f));
            Collider[] colliders = Physics.OverlapSphere(posicionPotencial, 0.05f);
            if (colliders.Length == 0)
            {
                transform.position = posicionPotencial;
                PosicionEncontrada=true;
            }
        }
    }
}
