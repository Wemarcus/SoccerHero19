using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_Friction : MonoBehaviour
{

    public float value = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity *= value;
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity *= value;
        }
    }
}
