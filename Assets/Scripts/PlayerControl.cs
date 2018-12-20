using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl: MonoBehaviour {

    public GameObject groundcheck;
    private Rigidbody rb;
    public static bool grounded;
    private float speed = 5;
    private float jump = 3;
    private float rotationSpeed = 3;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        //Moviemiento con el joystick izquierdo con la direccion
        float inputZ = Input.GetAxis("Horizontal");
        float inputX = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(inputZ * speed, rb.velocity.y, inputX * speed);

        Vector3 lookDirection = new Vector3(inputZ, 0, inputX);
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        float step = rotationSpeed * Time.deltaTime;
        if (inputZ != 0 || inputX != 0) transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);

        // GROUNDCHECK + SALTO

        grounded = Physics.Linecast(transform.position, groundcheck.transform.position, 1 << LayerMask.NameToLayer("Grounded"));


        if (Input.GetAxis("Jump") != 0 && grounded)
        {
            rb.velocity = new Vector3(0, jump, 0);
        }

        
        

    }
}