using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
  public class MouseLook : MonoBehaviour
  {
    // See Project Settings / Input Manager to change the mouse sensitivity for each axis directly.
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;
    private bool mouseLocked = false; // start with mouse unlocked

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      mouseLocked = IsMouseLocked();

      if (!mouseLocked)
      {
        if (Input.GetMouseButtonDown(0))
          LockMouse(); // fall-through to ignore this mouse click by returning

        return;
      }

      float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
      float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

      xRotation -= mouseY;
      xRotation = Mathf.Clamp(xRotation, -90f, 90f);

      transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
      playerBody.Rotate(Vector3.up * mouseX);
    }

    private static bool IsMouseLocked()
    {
      return
        Cursor.lockState == CursorLockMode.Locked
        && !Cursor.visible;
    }

    private static void LockMouse()
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
  }
}