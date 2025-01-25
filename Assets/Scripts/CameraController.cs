using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float sensX = 2.0f;
    public float sensY = 2.0f;
    public float angleLimit = 90.0f;

    private float rotX = 0.0f;
    private float rotY = 0.0f;

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        rotX += Input.GetAxis("Mouse X") * sensX;
        rotY -= Input.GetAxis("Mouse Y") * sensY;

        rotY = Mathf.Clamp(rotY, -angleLimit, angleLimit);

        transform.rotation = Quaternion.Euler(rotY, rotX, 0);

    }
}
