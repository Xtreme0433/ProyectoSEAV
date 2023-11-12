using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 10f;
    public float xInput, zInput;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }
    void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        xInput = Input.GetAxis("Horizontal") * moveSpeed;
        zInput = Input.GetAxis("Vertical") * moveSpeed;
    }

    private void Move() {
        rb.AddForce(new Vector3(xInput, 0f, zInput));
    }
}
