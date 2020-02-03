using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSound : MonoBehaviour
{
    public AudioClip sound_tmp;
    public AudioSource audio_source;
    private AudioClip sound;
    private bool check;

    void Start()
    {
        sound = MakeSubclip(sound_tmp, 3f, 5.5f);
        audio_source.clip = sound;
        check = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball" & check)
        {
            audio_source.Play();
            check = false;
            StartCoroutine(Time_Check());
        }
    }

    IEnumerator Time_Check()
    {
        yield return new WaitForSeconds(3);
        check = true;
    }

    private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    {
        /* Create a new audio clip */
        int frequency = clip.frequency;
        float timeLength = stop - start;
        int samplesLength = (int)(frequency * timeLength);
        AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);
        /* Create a temporary buffer for the samples */
        float[] data = new float[samplesLength];
        /* Get the data from the original clip */
        clip.GetData(data, (int)(frequency * start));
        /* Transfer the data to the new clip */
        newClip.SetData(data, 0);
        /* Return the sub clip */
        return newClip;
    }
}
