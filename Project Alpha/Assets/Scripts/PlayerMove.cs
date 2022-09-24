using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public Camera playerCamera;
    public float speed = 100.0f;
    Vector2 prevInputs; //make movement feel not garbage

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        rb = player.GetComponent<Rigidbody>();
    }

    void UpdateCamera()
    {
        player.transform.Rotate(
            new Vector3(
                0,
                PlayerData.MouseSensitivity.x / 100 * Input.GetAxis("Mouse X")
            )
        );
        playerCamera.transform.Rotate(
            new Vector3(
                -PlayerData.MouseSensitivity.y / 100 * Input.GetAxis("Mouse Y"),
                0
            )
        );
    }

    void MovePlayer()
    {
        Vector3 currentVelocity = rb.velocity;
        float theta = player.transform.rotation.eulerAngles.y * (float)Math.PI / 180f;
        Vector3 rotatedVelocity = new Vector3(
            currentVelocity.x * (float)Math.Sin(theta) + currentVelocity.y * (float)Math.Cos(theta),
            currentVelocity.x * (float)Math.Cos(theta) + currentVelocity.y * (float)Math.Sin(theta),
            currentVelocity.z);
        
        rb.AddForce(
            speed * new Vector3(
                prevInputs.x * (float)Math.Cos(-theta) + prevInputs.y * (float)Math.Sin(theta),
                0f,
                prevInputs.x * (float)Math.Sin(-theta) + prevInputs.y * (float)Math.Cos(theta)), ForceMode.Force);
        prevInputs = new Vector2(0f, 0f);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        prevInputs += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime;
        UpdateCamera();
    }
}
