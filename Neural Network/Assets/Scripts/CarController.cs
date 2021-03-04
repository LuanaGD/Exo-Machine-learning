using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSteerAngle = 42;
    public float motorForce = 1000;

    public WheelCollider wheelFrontLeftCollider, wheelFrontRightCollider, wheelRearLeftCollider, wheelRearRightCollider;
    public Transform wheelFrontLeft, wheelFrontRight, wheelRearLeft, wheelRearRight;

    public Rigidbody rb;
    public Transform centerOfMass;

    public float horizontalInput;
    public float verticalInput;
    float steeringAngle;

    void Start()
    {
        rb.centerOfMass = centerOfMass.localPosition;

    }


    void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    void Steer()
    {
        steeringAngle = horizontalInput * maxSteerAngle;

        wheelFrontLeftCollider.steerAngle = steeringAngle;
        wheelFrontRightCollider.steerAngle = steeringAngle;

    }

    void Accelerate()
    {
        wheelFrontLeftCollider.motorTorque = verticalInput * motorForce;
        wheelFrontRightCollider.motorTorque = verticalInput * motorForce;

        //wheelRearLeftCollider.motorTorque = verticalInput * motorForce;
        //wheelRearRightCollider.motorTorque = verticalInput * motorForce;
    }

    void UpdateWheelPoses()
    {
        UpdateWheelPose(wheelFrontLeftCollider, wheelFrontLeft);
        UpdateWheelPose(wheelFrontRightCollider, wheelFrontRight);
        UpdateWheelPose(wheelRearLeftCollider, wheelRearLeft);
        UpdateWheelPose(wheelRearRightCollider, wheelRearRight);
    }

    Vector3 pos;
    Quaternion quat;
    void UpdateWheelPose(WheelCollider col, Transform tr)
    {
        pos = tr.position;
        quat = tr.rotation;

        col.GetWorldPose(out pos, out quat);

        tr.position = pos;
        tr.rotation = quat;
    }

    public void Reset()
    {
        horizontalInput = 0;
        verticalInput = 0;
    }
}

