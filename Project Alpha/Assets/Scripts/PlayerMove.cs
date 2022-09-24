using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public Camera playerCamera;
    public float speed = 5000.0f;
    public float velocityScaling = -0.8f; //coef1
    public float maxVel = 0.3f; //coef2
    Vector2 prevInputs; //make movement feel not garbage

    // Start is called before the first frame update
    void Start()
    {
        PlayerData.MouseSensitivity = new Vector2(100f,100f);
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
    float VelocityScale(float directional)
    {
        return 1-(Math.Min((1-1/(1+Math.Abs(directional*velocityScaling))+maxVel),1)-maxVel)*(1/(1-maxVel)); //https://www.desmos.com/calculator/srjtyzbiiv
    }

    void MovePlayer()
    {
        Vector3 currentVelocity = rb.velocity;
        float theta = player.transform.rotation.eulerAngles.y * (float)Math.PI / 180f;
        /*
        represents the player relative left-right forward-backwards velocity multipliers
        they are calculated as velocity by (min((1-1/(1+abs(velocityInDirection*coef1))+coef2),1)-coef2)*(1/(1-coef2))
        */
        // Debug.Log(Time.time*10);
        // Debug.Log(Quaternion.Euler(new Vector3(0f,Time.time*10,0f)) * new Vector3(1f,0f,0f));
        Vector3 rotatedVelocity = Quaternion.Euler(0f,-theta * 180 / (float)Math.PI,0f) * currentVelocity;
        Vector2 velocityMultipliers = new Vector2(
            VelocityScale(rotatedVelocity.x),
            VelocityScale(rotatedVelocity.z)
        );

        Vector3 move = speed * new Vector3(
            prevInputs.x * (float)Math.Cos(-theta) * velocityMultipliers.x + prevInputs.y * (float)Math.Sin(theta) * velocityMultipliers.y,
            0f,
            prevInputs.x * (float)Math.Sin(-theta) * velocityMultipliers.x + prevInputs.y * (float)Math.Cos(theta) * velocityMultipliers.y
        );
        // Debug.Log(rotatedVelocity);
        // Debug.Log("Normal:  " + currentVelocity.ToString());
        // Debug.Log("Rotated: " + rotatedVelocity.ToString());
        Debug.Log("Mul: " + velocityMultipliers.ToString());
        Debug.Log("In:  " +  prevInputs.ToString());
        rb.AddForce(
            move
            , ForceMode.Force);
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
