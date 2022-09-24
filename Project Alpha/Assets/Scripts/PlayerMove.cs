using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject player;
    Camera playerCamera;
    GameObject body;
    public (float,float) sensitivity = (1f,1f); //X,Y
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        foreach (Transform child in player.transform)
        {
            if (child.gameObject.GetComponent<Camera>() != null)
            {
                playerCamera = child.gameObject.GetComponent<Camera>();
            }
            if (child.gameObject.tag == "playerBody")
            {
                playerCamera = child.gameObject.GetComponent<Camera>();
            }
        }
    }

    // Update is called once per frame
    void UpdateCamera()
    {
        Vector3 angles = playerCamera.transform.rotation.eulerAngles;
        Debug.Log(Input.GetAxis("Mouse X"));
        player.transform.rotation = Quaternion.Euler(angles + new Vector3(sensitivity.Item2 * Input.GetAxis("Mouse Y") * -1f, sensitivity.Item1 * Input.GetAxis("Mouse X"),0f));
    }
    void MovePlayer()
    {
        Vector3 currentVelocity = body.GetComponent<Rigidbody>().velocity;
    }
    void Update()
    {
        UpdateCamera();
    }
}
