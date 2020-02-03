using System.Collections;
using UnityEngine;

public class GoalKeeperPlayerControl : MonoBehaviour
{
    public int i;
    public bool check;

    public GameObject strikerAI;
    public bool freeze;
    public GameObject goalLine;

    public GameObject ball;
    public GameObject body;
    public bool blocking;
    public bool lob;
    public bool lob_right_left;

    private bool check_exultation;

    private void Start()
    {
        i = 0;
        check = true;
        freeze = false;
        check_exultation = false;
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") == 1 & !Input.GetKey(KeyCode.Joystick1Button2) & i < 2 & !freeze & check & Input.GetAxis("VerticalRight") == 0 & Input.GetAxis("HorizontalRight") == 0)
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Left");
            i = i + 1;
            StartCoroutine(TimePause());
        }

        if (Input.GetAxis("Horizontal") == -1 & !Input.GetKey(KeyCode.Joystick1Button2) & i > -2 & !freeze & check & Input.GetAxis("VerticalRight") == 0 & Input.GetAxis("HorizontalRight") == 0)
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Right");
            i = i - 1;
            StartCoroutine(TimePause());
        }

        // Automatic movement
        if (check & freeze & strikerAI.GetComponent<StrikerAIControl>().getRight() & goalLine.GetComponent<GoalLine>().getGoal())
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Stay_Left");
        }
        else if (check & freeze & strikerAI.GetComponent<StrikerAIControl>().getLeft() & goalLine.GetComponent<GoalLine>().getGoal()){
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Stay_Right");
        }
        else if (check & freeze & strikerAI.GetComponent<StrikerAIControl>().getCentral() & goalLine.GetComponent<GoalLine>().getGoal() && i < 0)
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Stay_Left");
        }
        else if (check & freeze & strikerAI.GetComponent<StrikerAIControl>().getCentral() & goalLine.GetComponent<GoalLine>().getGoal() && i > 0)
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Stay_Right");
        }
        else if (check & blocking & strikerAI.GetComponent<StrikerAIControl>().getCentral()){
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Central_Central");

            if (strikerAI.GetComponent<StrikerAIControl>().getLob())
            {
                fixLobBlocking();
                StartCoroutine(unfixBlocking());
            }
            else
            {
                fixBlocking();
                StartCoroutine(unfixBlocking());
            }
        }

        // 1
        if (Input.GetAxis("VerticalRight") <= -0.25 & Input.GetAxis("HorizontalRight") <= -0.25 & !freeze & check & strikerAI.GetComponent<StrikerAIControl>().checkShooting())
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Right_Down_2");
        }

        // 2  ====> MIGLIORARE ANIMAZIONE!
        if (Input.GetAxis("VerticalRight") <= -0.25 & Input.GetAxis("HorizontalRight") > -0.25 & Input.GetAxis("HorizontalRight") < 0.25 & !freeze & check & strikerAI.GetComponent<StrikerAIControl>().checkShooting())
        {
            check = false;

            System.Random r = new System.Random();
            int rInt = r.Next(0, 2);

            switch (rInt)
            {
                case 0:
                    gameObject.GetComponent<Animator>().SetTrigger("Central_Down");
                    break;
                case 1:
                    gameObject.GetComponent<Animator>().SetTrigger("Central_Down_Mirror");
                    break;
                default:
                    // TODO
                    break;
            }
        }

        // 3
        if (Input.GetAxis("VerticalRight") <= -0.25 & Input.GetAxis("HorizontalRight") >= 0.25 & !freeze & check & strikerAI.GetComponent<StrikerAIControl>().checkShooting())
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Left_Down_2");
        }

        // 4
        if (Input.GetAxis("VerticalRight") > -0.25 & Input.GetAxis("VerticalRight") < 0.25 & Input.GetAxis("HorizontalRight") <= -0.25 & !freeze & check & strikerAI.GetComponent<StrikerAIControl>().checkShooting())
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Right_Up");
        }

        // 6
        if (Input.GetAxis("VerticalRight") > -0.25 & Input.GetAxis("VerticalRight") < 0.25 & Input.GetAxis("HorizontalRight") >= 0.25 & !freeze & check & strikerAI.GetComponent<StrikerAIControl>().checkShooting())
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Left_Up");
        }

        // 7
        if (Input.GetAxis("VerticalRight") >= 0.25 & Input.GetAxis("HorizontalRight") <= -0.25 & !freeze & check & strikerAI.GetComponent<StrikerAIControl>().checkShooting())
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Right_Up_Up");
        }

        // 8
        if (Input.GetAxis("VerticalRight") >= 0.25 & Input.GetAxis("HorizontalRight") > -0.25 & Input.GetAxis("HorizontalRight") < 0.25 & !freeze & check & strikerAI.GetComponent<StrikerAIControl>().checkShooting())
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Central_Up");
        }

        // 9
        if (Input.GetAxis("VerticalRight") >= 0.25 & Input.GetAxis("HorizontalRight") >= 0.25 & !freeze & check & strikerAI.GetComponent<StrikerAIControl>().checkShooting())
        {
            check = false;
            gameObject.GetComponent<Animator>().SetTrigger("Left_Up_Up");
        }
    }

    IEnumerator TimePause()
    {
        yield return new WaitForSeconds(0.5f);
        check = true;
    }

    public bool getFreeze()
    {
        return freeze;
    }

    public void setFreeze(bool b)
    {
        freeze = b;
    }

    public bool getBlocking()
    {
        return blocking;
    }

    public void setBlocking(bool b)
    {
        blocking = b;
    }

    private void fixBlocking()
    {
        ball.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        ball.gameObject.transform.parent = body.transform;
        ball.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        ball.gameObject.GetComponent<Rigidbody>().useGravity = false;
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        ball.gameObject.GetComponent<Rigidbody>().detectCollisions = false;

        var pos = ball.gameObject.transform.localPosition;
        pos.x = -0.003f;
        pos.y = 0.309f;
        pos.z = 0.282f;
        ball.gameObject.transform.localPosition = pos;
    }

    private void fixLobBlocking()
    {
        ball.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        ball.gameObject.transform.parent = body.transform;
        ball.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        ball.gameObject.GetComponent<Rigidbody>().useGravity = false;
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        ball.gameObject.GetComponent<Rigidbody>().detectCollisions = false;

        var pos = ball.gameObject.transform.localPosition;
        pos.x = -0.003f;
        pos.y = 0.309f;
        pos.z = 0.282f;
        ball.gameObject.transform.localPosition = pos;
    }

    IEnumerator unfixBlocking()
    {
        yield return new WaitForSeconds(1.2f); // or 1.1f
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        ball.gameObject.GetComponent<Rigidbody>().useGravity = true;
        ball.gameObject.transform.parent = null;
        ball.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        ball.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void Exultation()
    {
        if (!check_exultation)
        {
            check_exultation = true;
            gameObject.GetComponent<Animator>().SetTrigger("Happy");
        }
    }

    public void Despair()
    {
        if (!check_exultation)
        {
            check_exultation = true;
            gameObject.GetComponent<Animator>().SetTrigger("Sad");
        }
    }

    public void resetExultation(bool b)
    {
        check_exultation = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball" & strikerAI.GetComponent<StrikerAIControl>().getLob())
        {
            setBlocking(true);
        }
    }
}
