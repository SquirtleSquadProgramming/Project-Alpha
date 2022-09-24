using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject player;
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
    }

    void UpdateCamera()
    {
        Vector3 angles = playerCamera.transform.rotation.eulerAngles;
        Debug.Log(Input.GetAxis("Mouse X"));
        player.transform.rotation = Quaternion.Euler(
            angles + new Vector3(
                PlayerData.MouseSensitivity.y * Input.GetAxis("Mouse Y") * -1f,
                PlayerData.MouseSensitivity.x * Input.GetAxis("Mouse X"),
                0f)
            );
    }

    void MovePlayer()
    {
        Vector3 currentVelocity = player.GetComponent<Rigidbody>().velocity;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }
}
