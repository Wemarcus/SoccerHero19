using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StrikerPlayerControl : MonoBehaviour
{
    // VARIABILI PUBBLICHE
    public GameObject soccer_door;
    public Rigidbody ball;
    public float thrust = 2000;
    //public GameObject e; // OLD ===> da cancellare
    public GameObject rules;
    public float shoot_delay; // OLD 0.45f
    public AudioClip sound_tmp;
    public GameObject[] scores;
    private int i_scores;
    public bool check_exultation;
    public GameObject goalkeeperAI;

    // VARIABILI PRIVATE
    private bool shooting;
    private Animator anim;
    private AudioSource audio_source;
    private AudioClip sound;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audio_source = gameObject.GetComponent<AudioSource>();
        sound = MakeSubclip(sound_tmp, 6.0f, 9.5f);
        audio_source.clip = sound;
        i_scores = 0;
        check_exultation = false;
    }

    void Update()
    {
        // NUOVI COMANDI

        if (Input.GetAxis("Vertical") >= 0.25 & Input.GetAxis("Horizontal") > -0.25 & Input.GetAxis("Horizontal") < 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Central_Up());
        }

        if (Input.GetAxis("Vertical") == 0 & Input.GetAxis("Horizontal") == 0 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Central_Central());
        }

        if (Input.GetAxis("Vertical") <= -0.25 & Input.GetAxis("Horizontal") > -0.25 & Input.GetAxis("Horizontal") < 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Central_Down());
        }

        if (Input.GetAxis("Vertical") > 0 & Input.GetAxis("Horizontal") == 0 & Input.GetKey(KeyCode.Joystick1Button2) & Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Lob_Central());
        }

        if (Input.GetAxis("Vertical") > 0 & Input.GetAxis("Horizontal") >= 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Lob_Right());
        }

        if (Input.GetAxis("Vertical") > 0 & Input.GetAxis("Horizontal") <= -0.25 & Input.GetKey(KeyCode.Joystick1Button2) & Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Lob_Left());
        }

        if (Input.GetAxis("Vertical") <= -0.25 & Input.GetAxis("Horizontal") <= -0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Left_Down());
        }

        if (Input.GetAxis("Vertical") <= -0.25 & Input.GetAxis("Horizontal") >= 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Right_Down());
        }

        if (Input.GetAxis("Vertical") < 0.25 & Input.GetAxis("Vertical") > -0.25 & Input.GetAxis("Horizontal") <= -0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Left_Central());
        }

        if (Input.GetAxis("Vertical") < 0.25 & Input.GetAxis("Vertical") > -0.25 & Input.GetAxis("Horizontal") >= 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Right_Central());
        }

        if (Input.GetAxis("Vertical") >= 0.25 & Input.GetAxis("Horizontal") <= -0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Left_Up());
        }

        if (Input.GetAxis("Vertical") >= 0.25 & Input.GetAxis("Horizontal") >= 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Right_Up());
        }

        // VECCHI COMANDI

        /*if (Input.GetKeyDown(KeyCode.Keypad2) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Central_Down());
        }

        if (Input.GetKeyDown(KeyCode.Keypad8) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Central_Up());
        }

        if (Input.GetKeyDown(KeyCode.Keypad5) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Central_Central());
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Left_Down());
        }

        if (Input.GetKeyDown(KeyCode.Keypad3) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Right_Down());
        }

        if (Input.GetKeyDown(KeyCode.Keypad7) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Left_Up());
        }

        if (Input.GetKeyDown(KeyCode.Keypad9) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Right_Up());
        }

        if (Input.GetKeyDown(KeyCode.Keypad4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Left_Central());
        }

        if (Input.GetKeyDown(KeyCode.Keypad6) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Right_Central());
        }

        if (Input.GetKeyDown(KeyCode.Keypad0) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);
            StartCoroutine(Shoot_Lob_Central());
        }*/
    }

    public bool checkShooting()
    {
        return shooting;
    }

    IEnumerator Shoot_Lob_Central()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        audio_source.Play();
        //Debug.Log("Ho calciato la palla!");
        shooting = true;
        ball.AddForce(transform.forward * (thrust / 1.8f)); //OLD 2.2 // 2.1
        ball.AddForce(transform.up * (thrust / 3)); //OLD 3.1 // 2.9
        //ball.AddForce(transform.right * (thrust / 50));
        ball.AddTorque(0, 6000, 0);
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Lob_Right()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * (thrust / 1.8f)); //OLD 2.2 // 2.1
        ball.AddForce(transform.up * (thrust / 3)); //OLD 3.1 // 2.9
        ball.AddForce(transform.right * (thrust / 9.5f));
        ball.AddTorque(0, 6000, 0);
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Lob_Left()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * (thrust / 1.8f)); //OLD 2.2 // 2.1
        ball.AddForce(transform.up * (thrust / 3)); //OLD 3.1 // 2.9
        ball.AddForce(-transform.right * (thrust / 9.5f));
        ball.AddTorque(0, 6000, 0);
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Central_Down()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * (thrust * 1.1f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 8));
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Central_Up()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");

        float tmp = gameObject.GetComponent<KeyPressure>().getTime();
        //Debug.Log("Calcio la palla: " + tmp);

        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * thrust);
        ball.AddForce(transform.up * (thrust / 3.13f) * ((tmp/2)+0.9f)); // ADD .13f
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Central_Central()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * thrust);
        ball.AddForce(transform.up * (thrust / 4));
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Left_Down()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");

        float tmp = gameObject.GetComponent<KeyPressure>().getTime();
        //Debug.Log("Calcio la palla: " + tmp);

        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 9));
        ball.AddForce(-transform.right * (thrust / 3f) * (tmp + 0.7f)); // OLD 3.6
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Right_Down()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");

        float tmp = gameObject.GetComponent<KeyPressure>().getTime();
        //Debug.Log("Calcio la palla: " + tmp);

        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 9));
        ball.AddForce(transform.right * (thrust / 3f) * (tmp + 0.7f)); // OLD 3.6
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Left_Up()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");

        float tmp = gameObject.GetComponent<KeyPressure>().getTime();
        //Debug.Log("Calcio la palla: " + tmp);

        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 3.1f) * ((tmp / 2) + 0.85f)); // ADD .1f
        ball.AddForce(-transform.right * (thrust / 3.5f) * (tmp + 0.7f)); // OLD 4
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Right_Up()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");

        float tmp = gameObject.GetComponent<KeyPressure>().getTime();
        //Debug.Log("Calcio la palla: " + tmp);

        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 3.1f) * ((tmp / 2) + 0.85f)); // ADD .1f
        ball.AddForce(transform.right * (thrust / 3.5f) * (tmp + 0.7f)); // OLD 4
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Left_Central()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");

        float tmp = gameObject.GetComponent<KeyPressure>().getTime();
        //Debug.Log("Calcio la palla: " + tmp);

        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * (thrust * 1.1f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 4));
        ball.AddForce(-transform.right * (thrust / 3.5f) * (tmp + 0.7f));
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Right_Central()
    {
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");

        float tmp = gameObject.GetComponent<KeyPressure>().getTime();
        //Debug.Log("Calcio la palla: " + tmp);

        audio_source.Play();
        shooting = true;
        ball.AddForce(transform.forward * (thrust * 1.1f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 4));
        ball.AddForce(transform.right * (thrust / 3.5f) * (tmp + 0.7f));
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shooting_Time()
    {
        yield return new WaitForSeconds(2); // OLD 3

        if (i_scores > 4)
        {
            i_scores = 0;
        }

        if (!soccer_door.GetComponent<GoalLine>().getGoal())
        {
            Debug.Log("BLU TEAM: NO GOL!");
            Despair();
            goalkeeperAI.GetComponent<GoalKeeper>().Exultation();
            //e.GetComponent<Text>().text = (Int32.Parse(e.GetComponent<Text>().text) + 1).ToString();
            scores[i_scores].GetComponent<Image>().color = new Color32(188, 59, 63, 255);
        }
        else
        {
            scores[i_scores].GetComponent<Image>().color = new Color32(59, 188, 84, 255);
        }

        i_scores = i_scores + 1;

        shooting = false;
        soccer_door.GetComponent<GoalLine>().setGoal(false);

        yield return new WaitForSeconds(2);  // OLD 1

        if (!rules.GetComponent<ResetScene>().checkEnd(0))
        {
            rules.GetComponent<ResetScene>().nextKick();
        }
    }

    public void Exultation()
    {
        if (!check_exultation)
        {
            check_exultation = true;
            anim.SetTrigger("Happy");
        }
    }

    public void Despair()
    {
        if (!check_exultation)
        {
            check_exultation = true;
            anim.SetTrigger("Said");
        }
    }

    public void resetExultation(bool b)
    {
        check_exultation = false;
    }

    public void setScores()
    {
        i_scores = 0;
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
