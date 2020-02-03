using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPuller : MonoBehaviour
{
    public GameObject attractedTo;
    public float strengthOfAttraction = 5.0f;

    void Start()
    {
    }

    void FixedUpdate()
    {
        Vector3 direction = attractedTo.transform.position - transform.position;
        gameObject.GetComponent<Rigidbody>().AddForce(strengthOfAttraction * direction);

    }
}