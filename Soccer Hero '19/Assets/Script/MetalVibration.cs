using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class MetalVibration : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            StartCoroutine(TimeVibration());
        }
    }

    IEnumerator TimeVibration()
    {
        GamePad.SetVibration(0, 0.6f, 0.6f);
        yield return new WaitForSeconds(0.3f);
        GamePad.SetVibration(0, 0, 0);
    }
}
