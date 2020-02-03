using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressure : MonoBehaviour
{
    private float pressTime;
    private bool check_pressure;

    void Start()
    {
        pressTime = 0;
        check_pressure = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && !check_pressure)
        {
            pressTime = Time.time;
            //Debug.Log("Down: " + pressTime);
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button2) && !check_pressure)
        {
            check_pressure = true;
            pressTime = Time.time - pressTime;
            //Debug.Log("Up: " + pressTime);
        }
    }

    public float getTime()
    {
        if (check_pressure)
        {
            return pressTime;
        }
        else
        {
            return pressTime = Time.time - pressTime;
        }
    }

    public void resetCheck()
    {
        check_pressure = false;
    }
}
