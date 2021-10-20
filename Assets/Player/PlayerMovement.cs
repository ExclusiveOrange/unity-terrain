using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

  // Update is called once per frame
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