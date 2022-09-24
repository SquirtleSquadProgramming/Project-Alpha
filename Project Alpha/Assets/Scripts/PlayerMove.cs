using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject player;
    Camera playerCamera;
    public (float,float) sensitivity = (1.0f,1.0f); //X,Y
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
        }
    }

    // Update is called once per frame
    void UpdateCamera()
    {
        Vector3 angles = playerCamera.transform.rotation.eulerAngles;
        player.transform.rotation = Quaternion.Euler(angles + new Vector3(sensitivity.Item2 * Input.GetAxis("Mouse Y"), sensitivity.Item1 * Input.GetAxis("Mouse X"),0.0f));
    }
    void Update()
    {
        
    }
}
