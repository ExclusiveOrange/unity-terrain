using System;
using UnityEngine;

namespace Player
{
  public class Movement : MonoBehaviour
  {
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    private void OnGUI()
    {
      GUI.Label(new Rect(10, 40, 300, 30), $"velocity.y {velocity.y}");
    }

    private void Update()
    {
      isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

      if (isGrounded && velocity.y < 0)
        velocity.y = 0f;

      float x = Input.GetAxis("Horizontal");
      float z = Input.GetAxis("Vertical");

      Transform currentTransform = transform;
      Vector3 move = currentTransform.right * x + currentTransform.forward * z;
      controller.Move(move * speed * Time.deltaTime);

      if (Input.GetButtonDown("Jump") && isGrounded)
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

      velocity.y += gravity * Time.deltaTime;
      controller.Move(velocity * Time.deltaTime);
    }
  }
}