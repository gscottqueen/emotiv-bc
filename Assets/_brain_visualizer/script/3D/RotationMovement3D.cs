using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMovement3D : MonoBehaviour
{
  // Quaternion values
  public float qW;
  public float qX;
  public float qY;
  public float qZ;

  // Acceleration values
  public float accelerationX;
  public float accelerationY;
  public float accelerationZ;

  private void Update()
  {
    // Update the rotation based on quaternion values
    Quaternion rotation = new Quaternion(qX, qY, qZ, qW);
    transform.rotation = rotation;

    // Apply rotation based on acceleration values
    Vector3 acceleration = new Vector3(accelerationX, accelerationY, accelerationZ);
    transform.Rotate(acceleration * Time.deltaTime);
  }
}