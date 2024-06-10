using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector2 turnVector = Vector2.zero;

    [SerializeField]
    private float sensitivity = 1.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetCameraOrientation()
    {
        turnVector.x += Input.GetAxis("Mouse X") * sensitivity;
        turnVector.y += Input.GetAxis("Mouse Y") * sensitivity;

        turnVector.y = Mathf.Clamp(turnVector.y, -20f, 60f);

        transform.localRotation = Quaternion.Euler(turnVector.y, turnVector.x, 0);
    }
}
