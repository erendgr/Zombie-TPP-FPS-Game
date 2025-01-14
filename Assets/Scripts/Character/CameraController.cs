using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 targetDistance;
    private float mouseX, mouseY;
    [SerializeField] private float sensitivity;
    private Vector3 objRotation;
    public Transform charBody;
    private PlayerController _playerController;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerController = GameObject.Find("Swat").GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        if (_playerController.isDead == false)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + targetDistance, 0.5f);
        
            mouseX += Input.GetAxis("Mouse X") * sensitivity;
            mouseY += Input.GetAxis("Mouse Y") * sensitivity;
            if (mouseY >= 25)
            {
                mouseY = 25;
            }
            if (mouseY <= -40)
            {
                mouseY = -40;
            }
            transform.eulerAngles = new Vector3(-mouseY, mouseX-90, 0);
            target.transform.eulerAngles = new Vector3(0, mouseX-90, 0);
        
            // Vector3 temp = transform.localEulerAngles;
            // temp.z = 0;
            // temp.y = transform.localEulerAngles.y;
            // temp.x = transform.localEulerAngles.x + 10;
            // objRotation = temp;
            // charBody.transform.eulerAngles = objRotation;
        }
        

    }
}