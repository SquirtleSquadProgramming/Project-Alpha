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
        float anglesX = player.transform.rotation.eulerAngles.y;
        float anglesY = playerCamera.transform.rotation.eulerAngles.x;
        player.transform.rotation = Quaternion.Euler(
            new Vector3(
                0f,
                anglesX + PlayerData.MouseSensitivity.x * Input.GetAxis("Mouse X"))
            );
        playerCamera.transform.rotation = Quaternion.Euler(
            new Vector3(
                anglesY + PlayerData.MouseSensitivity.y * Input.GetAxis("Mouse Y") * -1f,
                player.transform.rotation.eulerAngles.y
            )
        );
    }

    void MovePlayer()
    {
        Vector3 currentVelocity = rb.velocity;
        float theta = player.transform.rotation.eulerAngles.y;
        Vector3 rotatedVelocity = new Vector3(
            currentVelocity.x * (float)Math.Sin(theta) + currentVelocity.y * (float)Math.Cos(theta),
            currentVelocity.x * (float)Math.Cos(theta) + currentVelocity.y * (float)Math.Sin(theta),
            currentVelocity.z);
        Debug.Log(rotatedVelocity);
        rb.AddForce(speed * new Vector3(prevInputs.x * (float)Math.Sin(theta) + prevInputs.y * (float)Math.Cos(theta),
        prevInputs.x * (float)Math.Sin(theta) + prevInputs.y * (float)Math.Cos(theta)), ForceMode.Force);
        prevInputs = new Vector2(0f, 0f);
    }
    void FixedUpdate()
    {
        // MovePlayer();
    }
    void Update()
    {
        prevInputs += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime;
        UpdateCamera();
    }
}
