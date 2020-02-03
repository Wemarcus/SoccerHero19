using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixNet : MonoBehaviour
{
    public float fix_value = 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            other.gameObject.GetComponent<Obi.ObiCollider>().Thickness = fix_value;
        }
    }
}
