using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public Camera playerCamera;
    public float speed = 5000.0f;
    public float groundedSpeed = 5000.0f;
    public float velocityScaling = 1f; //coef1
    public float maxVel = 0.3f; //coef2
    Vector2 prevInputs; //make movement feel not garbage
    public bool grounded = false;
    bool groundedPrevious;
    int jumping = 0; //reverse coyote time
    public float jumpStrength = 8f;
    public float gravity = 8f;
    public float groundedDrag = 6f;
    public float counterStrafeMultiplier = 5.0f;
    //FIXME: non public variables for stuff when done
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
            velocityMultipliers.x = counterStrafeMultiplier;
        }

        if (Math.Sign(rotatedVelocity.z) != Math.Sign(prevInputs.y))
        {
            velocityMultipliers.y = counterStrafeMultiplier;
        }

        Vector3 move = (grounded && groundedPrevious ? groundedSpeed : speed) * new Vector3(
            prevInputs.x * (float)Math.Cos(-theta) * velocityMultipliers.x + prevInputs.y * (float)Math.Sin(theta) * velocityMultipliers.y,
            0f,
            prevInputs.x * (float)Math.Sin(-theta) * velocityMultipliers.x + prevInputs.y * (float)Math.Cos(theta) * velocityMultipliers.y
        );
        rb.AddForce(move, ForceMode.Force);

        if (jumping > 0 && grounded)
        {
            grounded = false;
            groundedPrevious = false;
            rb.drag = 0;
            Vector3 velocity = rb.velocity;
            velocity.y = jumpStrength;
            rb.velocity = velocity;
            jumping = 0;
        }

        if (grounded && groundedPrevious)
        {
            rb.drag = groundedDrag;
        }
        else
        {
            rb.drag = 0;
        }
        groundedPrevious = grounded; //bhop = 0 drag

        prevInputs = new Vector2(0f, 0f);
        jumping -= 1;
    }
    void Gravity()
    {
        rb.AddForce(0f, -gravity, 0f, ForceMode.Force);
    }

    void FixedUpdate()
    {
<<<<<<< HEAD
        if(player.transform.position.y < -25)
        {
            player.transform.position = new Vector3(0f,2f,0f);
        }
=======
        if (Time.timeScale == 0)
            return;
>>>>>>> b9dd670c6c43c5f2b7666a7eff3b842c4397983f
        MovePlayer();
        Gravity();
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;
        prevInputs += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = 5;
        }
        UpdateCamera();
    }
}
