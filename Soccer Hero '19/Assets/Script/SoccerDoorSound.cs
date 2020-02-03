using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerDoorSound : MonoBehaviour
{
    public AudioSource audio_source;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            audio_source.Play();
        }
    }
}
