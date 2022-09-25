using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public Camera playerCamera;
    public GameObject jumpCollider;
    public float speed = 5000.0f;
    public float velocityScaling = 1f; //coef1
    public float maxVel = 0.3f; //coef2
    Vector2 prevInputs; //make movement feel not garbage
    public bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData.MouseSensitivity = new Vector2(100f, 100f);
        player = gameObject;
        rb = player.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UpdateCamera()
    {
        player.transform.Rotate(
            new Vector3(
                0,
                PlayerData.MouseSensitivity.x / 50 * Input.GetAxis("Mouse X")
            )
        );
        playerCamera.transform.Rotate(
            new Vector3(
                -PlayerData.MouseSensitivity.y / 50 * Input.GetAxis("Mouse Y"),
                0
            )
        );
    }
    float VelocityScale(float directional)
    {
        return Math.Max(0, Math.Min(1, maxVel - Math.Abs(directional) * velocityScaling));
        //\max\left(0,\min\left(1,b-\left|cx\right|\right)\right)
        // return 1-(Math.Min((1-1/(1+Math.Abs(directional*velocityScaling))+maxVel),1)-maxVel)*(1/(1-maxVel)); //https://www.desmos.com/calculator/srjtyzbiiv
    }

    void MovePlayer()
    {
        if (grounded)
        {
            Debug.Log("somet");
        }
        Vector3 currentVelocity = rb.velocity;
        float theta = player.transform.rotation.eulerAngles.y * (float)Math.PI / 180f;
        // represents the player relative left-right forward-backwards velocity multipliers
        Vector3 rotatedVelocity = Quaternion.Euler(0f, -theta * 180 / (float)Math.PI, 0f) * currentVelocity;
        Vector2 velocityMultipliers = new Vector2(
            VelocityScale(rotatedVelocity.x),
            VelocityScale(rotatedVelocity.z)
        );
        //let the player stop
        if (Math.Sign(rotatedVelocity.x) != Math.Sign(prevInputs.x))
        {
            velocityMultipliers.x = 1;
        }

        if (Math.Sign(rotatedVelocity.z) != Math.Sign(prevInputs.y))
        {
            velocityMultipliers.y = 1;
        }

        Vector3 move = speed * new Vector3(
            prevInputs.x * (float)Math.Cos(-theta) * velocityMultipliers.x + prevInputs.y * (float)Math.Sin(theta) * velocityMultipliers.y,
            0f,
            prevInputs.x * (float)Math.Sin(-theta) * velocityMultipliers.x + prevInputs.y * (float)Math.Cos(theta) * velocityMultipliers.y
        );

        rb.AddForce(move, ForceMode.Force);
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
