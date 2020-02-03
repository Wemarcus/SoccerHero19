using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppingBall : MonoBehaviour
{
    // Variabili
    bool ball_owned = false;
    public float strengthOfAttraction = 5.0f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (ball_owned)
        {
            Vector3 direction = player.gameObject.transform.position - transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(strengthOfAttraction * direction);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ball_owned = true;
            Debug.Log("Palla in possesso di " + other.gameObject.name);
            player = other.gameObject;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ball_owned = false;
            Debug.Log("Palla di nessuno!");
        }
    }

}
