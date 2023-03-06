using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgenteML : Agent
{
    [SerializeField]
    private float _fuerzaMovimiento = 200;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _enemyRespawPoint;
    public bool _training = true;
    private Rigidbody _rb;
    public float _velocidadMovimiento = 9;
    public override void Initialize()
    {
        _target = GameObject.FindGameObjectWithTag("target").transform;
        _rb = GetComponent<Rigidbody>();
        if (!_training) MaxStep = 0;
    }
    public override void OnEpisodeBegin()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        //MoverPosicionInicial();
    }
    /// <summary>
    /// El vectorAction nos sirve para construir un vector de desplzamiento
    /// [0]: X.
    /// [1]: Z.
    /// </summary>
    /// <param name="="actions"></param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        //Construir el vector con el vector recibido
        Vector3 movimiento = new Vector3(actions.ContinuousActions[0], 0f, actions.ContinuousActions[1]);
        //Debug.Log(movimiento.x+","+movimiento.y + "," + movimiento.z);
        //Sumamos el evctor construido al rigidbody como fuerza
        transform.LookAt(_target);
        //_rb.AddForce(movimiento*_fuerzaMovimiento*Time.deltaTime);
        transform.localPosition += movimiento * Time.deltaTime * _velocidadMovimiento;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //Calcular cuanto nos queda hasta el objetivo
        Vector3 alObjetivo = _target.position - transform.position;
        //Un vector ocupa 3 observaciones
        sensor.AddObservation(alObjetivo.normalized);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = movement.x;
        continuousActionsOut[1] = movement.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("target"))
        {
            if (_training)
            {
                AddReward(1f);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("target"))
        {
            if (_training)
            {
                AddReward(0.5f);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Border") || collision.gameObject.CompareTag("mlagent"))
        {
            if (_training)
            {
                AddReward(-0.1f);
            }
        }
    }
    private void MoverPosicionInicial()
    {
        bool PosicionEncontrada = false;
        int intentos = 800;
        Vector3 posicionPotencial = Vector3.zero;
        while (!PosicionEncontrada || intentos >= 0)
        {
            intentos--;
            posicionPotencial = new Vector3(_enemyRespawPoint.position.x + UnityEngine.Random.Range(-4f, 4f), 0.555f, _enemyRespawPoint.position.z + UnityEngine.Random.Range(-4f, 4f));
            Collider[] colliders = Physics.OverlapSphere(posicionPotencial, 0.05f);
            if (colliders.Length == 0)
            {
                transform.position = posicionPotencial;
                PosicionEncontrada = true;
            }
        }
    }
}
