/*using System.Collections;
using System.Collections.Generic;
using EmotivUnityPlugin;
using UnityEngine;

namespace dirox.emotiv.controller
{
  public class RotationMovement3D : MonoBehaviour
  {
    [SerializeField] private DataSubscriber subscription;
    *//*[SerializeField] private Object data;*//*

    [SerializeField] private double qW;
    [SerializeField] private double qX;
    [SerializeField] private double qY;
    [SerializeField] private double qZ;

    // Acceleration values
    [SerializeField] private double accelerationX;
    [SerializeField] private double accelerationY;
    [SerializeField] private double accelerationZ;

    private void Start()
    {
      GameObject motionGameObject = gameObject;
    }


    private void Update()
    {
      if (subscription.data != null & subscription.data.Length > 0)
      {
        Debug.Log(subscription.chanStr);
        Debug.Log(subscription.data.Length);
        Debug.Log(subscription.data[0]);
        Debug.Log(subscription.data[1]);
        Debug.Log(subscription.data[2]);
        Debug.Log(subscription.data[3]);
        // Quaternion values
        if (subscription.chanStr == "Q0") qW = subscription.data[0];
        if (subscription.chanStr == "Q1") qW = subscription.data[0];
        if (subscription.chanStr == "Q2") qX = subscription.data[0];
        if (subscription.chanStr == "Q3") qY = subscription.data[0];
        if (subscription.chanStr == "Q0") qZ = subscription.data[0];
        if (subscription.chanStr == "ACCX") accelerationX = subscription.data[0];
        if (subscription.chanStr == "ACCY") accelerationY = subscription.data[0];
        if (subscription.chanStr == "ACCZ") accelerationZ = subscription.data[0];
      }
      *//*Debug.Log(qW);*//*
      // Update the rotation based on quaternion values
      Quaternion rotation = new Quaternion((float)qX, (float)qY, (float)qZ, (float)qW);
      transform.rotation = rotation;

      // Apply rotation based on acceleration values
      Vector3 acceleration = new Vector3((float)accelerationX, (float)accelerationY, (float)accelerationZ);
      transform.Rotate(acceleration * Time.deltaTime);
    }
  }
}*/