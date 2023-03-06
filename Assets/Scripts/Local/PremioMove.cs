using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremioMove : MonoBehaviour
{
    private float x, y;
    public float rotationSpeed = 200.0f;
    public float movementSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * movementSpeed);
    }
}
