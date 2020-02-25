using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    private void Start()
    {
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            Debug.Log(gyro.attitude);
        }
    }
}
