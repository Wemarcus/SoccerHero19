using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StrikerAIControl : MonoBehaviour
{
    // VARIABILI PUBBLICHE
    public GameObject soccer_door;
    public Rigidbody ball;
    public float thrust = 2000;
    //public GameObject e;
    public GameObject rules;
    public float shoot_delay;
    public float ai_delay;
    public AudioClip sound_tmp;
    public GameObject[] scores;
    public GameObject[] scores_p;
    public bool check_exultation;
    public GameObject goalkeeperPlayer;

    // VARIABILI PRIVATE
    private bool shooting;
    private Animator anim;
    private AudioSource audio_source;
    private AudioClip sound;
    private int i_scores;
    private bool right;
    private bool left;
    private bool central;
    private bool lob;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audio_source = gameObject.GetComponent<AudioSource>();
        sound = MakeSubclip(sound_tmp, 6.0f, 9.5f);
        audio_source.clip = sound;
        shooting = false;
        central = false;
        right = false;
        left = false;
        lob = false;
    }

    void Update()
    {
        if (rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            rules.GetComponent<ResetScene>().setPenaltyCheck(false);

            System.Random r = new System.Random();
            int rInt = r.Next(0, 8);

            switch (rInt)
            {
                case 0:
                    int newInt = r.Next(0, 3);

                    switch (newInt)
                    {
                        case 0:
                            StartCoroutine(Shoot_Central_Up());
                            break;
                        case 1:
                            setCentral(true);
                            StartCoroutine(Shoot_Central_Central());
                            break;
                        case 2:
                            StartCoroutine(Shoot_Central_Down());
                            break;
                        default:
                            // TODO
                            break;
                    }
                    break;
                case 1:
                    int newInteger = r.Next(0, 3);

                    switch (newInteger)
                    {
                        case 0:
                            setCentral(true);
                            setLob(true);
                            StartCoroutine(Shoot_Lob_Central());
                            break;
                        case 1:
                            setRight(true);
                            StartCoroutine(Shoot_Lob_Right());
                            break;
                        case 2:
                            setLeft(true);
                            StartCoroutine(Shoot_Lob_Left());
                            break;
                        default:
                            // TODO
                            break;
                    }
                    break;
                case 2:
                    setLeft(true);
                    StartCoroutine(Shoot_Left_Down());
                    break;
                case 3:
                    setRight(true);
                    StartCoroutine(Shoot_Right_Down());
                    break;
                case 4:
                    setLeft(true);
                    StartCoroutine(Shoot_Left_Central());
                    break;
                case 5:
                    setRight(true);
                    StartCoroutine(Shoot_Right_Central());
                    break;
                case 6:
                    setLeft(true);
                    StartCoroutine(Shoot_Left_Up());
                    break;
                case 7:
                    setRight(true);
                    StartCoroutine(Shoot_Right_Up());
                    break;
                default:
                    // TODO
                    break;
            }
        }
    }

    public bool checkShooting()
    {
        return shooting;
    }

    IEnumerator Shooting_Time()
    {
        yield return new WaitForSeconds(2); // OLD 3

        if (!soccer_door.GetComponent<GoalLine>().getGoal())
        {
            Debug.Log("RED TEAM: NO GOL!");
            Despair();
            goalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().Exultation();
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

        yield return new WaitForSeconds(2); // OLD 1

        if (i_scores > 4)
        {
            rules.GetComponent<ResetScene>().setPlayOff(true);
        }

        if (!rules.GetComponent<ResetScene>().checkEnd(1))
        {
            if (i_scores > 4)
            {
                i_scores = 0;
                scores[i_scores].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                scores[i_scores + 1].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                scores[i_scores + 2].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                scores[i_scores + 3].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                scores[i_scores + 4].GetComponent<Image>().color = new Color32(255, 255, 255, 60);

                scores_p[i_scores].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                scores_p[i_scores + 1].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                scores_p[i_scores + 2].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                scores_p[i_scores + 3].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                scores_p[i_scores + 4].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
            }

            rules.GetComponent<ResetScene>().nextKick();
        }
    }

    IEnumerator Shoot_Central_Up()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * thrust);
        ball.AddForce(transform.up * (thrust / 3.13f) * UnityEngine.Random.Range(0.9f, 1.25f)); // ADD .13f
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Central_Central()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * thrust);
        ball.AddForce(transform.up * (thrust / 4));
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Central_Down()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * (thrust * 1.1f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 8));
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Lob_Central()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        audio_source.Play();
        //Debug.Log("Ho calciato la palla!");
        ball.AddForce(transform.forward * (thrust / 1.8f)); //OLD 2.2 // 2.1
        ball.AddForce(transform.up * (thrust / 3)); //OLD 3.1 // 2.9
        //ball.AddForce(transform.right * (thrust / 50));
        ball.AddTorque(0, 6000, 0);
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Lob_Right()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * (thrust / 1.8f)); //OLD 2.2 // 2.1
        ball.AddForce(transform.up * (thrust / 3)); //OLD 3.1 // 2.9
        ball.AddForce(transform.right * (thrust / 9.5f));
        ball.AddTorque(0, 6000, 0);
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Lob_Left()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * (thrust / 1.8f)); //OLD 2.2 // 2.1
        ball.AddForce(transform.up * (thrust / 3)); //OLD 3.1 // 2.9
        ball.AddForce(-transform.right * (thrust / 9.5f));
        ball.AddTorque(0, 6000, 0);
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Left_Down()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 9));
        ball.AddForce(-transform.right * (thrust / 3f) * UnityEngine.Random.Range(0.6f, 1.45f)); // OLD 3.6
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Right_Down()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 9));
        ball.AddForce(transform.right * (thrust / 3f) * UnityEngine.Random.Range(0.6f, 1.45f)); // OLD 3.6
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Left_Up()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 3.1f) * UnityEngine.Random.Range(0.8f, 1.25f)); // ADD .1f
        ball.AddForce(-transform.right * (thrust / 3.5f) * UnityEngine.Random.Range(0.6f, 1.45f)); // OLD 4
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Right_Up()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 3.1f) * UnityEngine.Random.Range(0.8f, 1.25f)); // ADD .1f
        ball.AddForce(transform.right * (thrust / 3.5f) * UnityEngine.Random.Range(0.6f, 1.45f)); // OLD 4
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Left_Central()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * (thrust * 1.1f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 4));
        ball.AddForce(-transform.right * (thrust / 3.5f) * UnityEngine.Random.Range(0.6f, 1.45f));
        StartCoroutine(Shooting_Time());
    }

    IEnumerator Shoot_Right_Central()
    {
        yield return new WaitForSeconds(ai_delay);
        anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(shoot_delay);
        //Debug.Log("Ho calciato la palla!");
        audio_source.Play();
        ball.AddForce(transform.forward * (thrust * 1.1f)); // ADD 1.1f
        ball.AddForce(transform.up * (thrust / 4));
        ball.AddForce(transform.right * (thrust / 3.5f) * UnityEngine.Random.Range(0.6f, 1.45f));
        StartCoroutine(Shooting_Time());
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

    public bool getCentral()
    {
        return central;
    }

    public void setCentral(bool b)
    {
        central = b;
    }

    public bool getRight()
    {
        return right;
    }

    public void setRight(bool b)
    {
        right = b;
    }

    public bool getLeft()
    {
        return left;
    }

    public void setLeft(bool b)
    {
        left = b;
    }

    public bool getLob()
    {
        return lob;
    }

    public void setLob(bool b)
    {
        lob = b;
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
}
