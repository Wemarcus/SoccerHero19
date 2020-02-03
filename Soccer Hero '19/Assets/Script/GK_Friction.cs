using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GK_Friction : MonoBehaviour
{
    public float value = 0.6f;
    public float value_2 = 0.8f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {

            collision.gameObject.GetComponent<Rigidbody>().velocity *= Random.Range(value, value_2);
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity *= Random.Range(value, value_2);
        }
    }
}
