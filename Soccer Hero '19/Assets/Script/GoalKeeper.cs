using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoalKeeper : MonoBehaviour
{
    //public GameObject striker;
    public GameObject ball;
    public GameObject body;
    public GameObject rules;
    public bool blocking;
    public bool lob;
    public bool lob_right_left;
    public GameObject goalLine;

    private bool stay_left;
    private bool stay_right;
    private bool check_exultation;

    private void Start()
    {
        stay_left = false;
        stay_right = false;
        check_exultation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stay_left && goalLine.GetComponent<GoalLine>().getGoal())
        {
            stay_left = false;
            gameObject.GetComponent<Animator>().SetTrigger("Stay_Left");
        }

        if (stay_right && goalLine.GetComponent<GoalLine>().getGoal())
        {
            stay_right = false;
            gameObject.GetComponent<Animator>().SetTrigger("Stay_Right");
        }

        //if (Input.GetKeyDown(KeyCode.Keypad0) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if (Input.GetAxis("Vertical") > 0 & Input.GetAxis("Horizontal") == 0 & Input.GetKey(KeyCode.Joystick1Button2) & Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 3);

            switch (rInt)
            {
                case 0:
                    StartCoroutine(Save_Central_Lob());
                    break;
                case 1:
                    StartCoroutine(Right_Down());
                    break;
                case 2:
                    StartCoroutine(Left_Down());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        if (Input.GetAxis("Vertical") > 0 & Input.GetAxis("Horizontal") <= -0.25 & Input.GetKey(KeyCode.Joystick1Button2) & Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 2);

            switch (rInt)
            {
                case 0:
                    StartCoroutine(Left_Down());
                    break;
                case 1:
                    StartCoroutine(Save_Right_Lob());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        if (Input.GetAxis("Vertical") > 0 & Input.GetAxis("Horizontal") >= 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 2);

            switch (rInt)
            {
                case 0:
                    StartCoroutine(Right_Down());
                    break;
                case 1:
                    StartCoroutine(Save_Left_Lob());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Keypad1) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if (Input.GetAxis("Vertical") <= -0.25 & Input.GetAxis("Horizontal") <= -0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 3);

            switch (rInt)
            {
                case 0:
                    stay_right = true;
                    //StartCoroutine(Stay_Right());
                    break;
                case 1:
                    StartCoroutine(Moving_Right());
                    break;
                case 2:
                    StartCoroutine(Left_Down());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Keypad3) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if (Input.GetAxis("Vertical") <= -0.25 & Input.GetAxis("Horizontal") >= 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 3);

            switch (rInt)
            {
                case 0:
                    stay_left = true;
                    //StartCoroutine(Stay_Left());
                    break;
                case 1:
                    StartCoroutine(Right_Down());
                    break;
                case 2:
                    StartCoroutine(Moving_Left());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Keypad2) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if (Input.GetAxis("Vertical") <= -0.25 & Input.GetAxis("Horizontal") > -0.25 & Input.GetAxis("Horizontal") < 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 6);

            switch (rInt)
            {
                case 0:
                    StartCoroutine(Save_Central_Down());
                    break;
                case 1:
                    StartCoroutine(Save_Central_Down_Mirror());
                    break;
                case 2:
                    StartCoroutine(Right_Down());
                    break;
                case 3:
                    StartCoroutine(Left_Down());
                    break;
                case 4:
                    StartCoroutine(Moving_Left());
                    break;
                case 5:
                    StartCoroutine(Moving_Right());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Keypad5) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if (Input.GetAxis("Vertical") == 0 & Input.GetAxis("Horizontal") == 0 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 3);

            switch (rInt)
            {
                case 0:
                    StartCoroutine(Save_Central_Central());
                    break;
                case 1:
                    StartCoroutine(Right_Down());
                    break;
                case 2:
                    StartCoroutine(Left_Down());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Keypad4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if (Input.GetAxis("Vertical") < 0.25 & Input.GetAxis("Vertical") > -0.25 & Input.GetAxis("Horizontal") <= -0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 4);

            switch (rInt)
            {
                case 0:
                    stay_right = true;
                    //StartCoroutine(Stay_Right());
                    break;
                case 1:
                    StartCoroutine(Right_Down());
                    break;
                case 2:
                    StartCoroutine(Left_Down());
                    break;
                case 3:
                    StartCoroutine(Right_Up());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Keypad7) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if (Input.GetAxis("Vertical") >= 0.25 & Input.GetAxis("Horizontal") <= -0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 4);

            switch (rInt)
            {
                case 0:
                    stay_right = true;
                    //StartCoroutine(Stay_Right());
                    break;
                case 1:
                    StartCoroutine(Right_Down());
                    break;
                case 2:
                    StartCoroutine(Left_Down());
                    break;
                case 3:
                    StartCoroutine(Right_Up_Up());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Keypad6) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if (Input.GetAxis("Vertical") < 0.25 & Input.GetAxis("Vertical") > -0.25 & Input.GetAxis("Horizontal") >= 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 4);

            switch (rInt)
            {
                case 0:
                    stay_left = true;
                    //StartCoroutine(Stay_Left());
                    break;
                case 1:
                    StartCoroutine(Right_Down());
                    break;
                case 2:
                    StartCoroutine(Left_Down());
                    break;
                case 3:
                    StartCoroutine(Left_Up());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Keypad9) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if (Input.GetAxis("Vertical") >= 0.25 & Input.GetAxis("Horizontal") >= 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 4);

            switch (rInt)
            {
                case 0:
                    stay_left = true;
                    //StartCoroutine(Stay_Left());
                    break;
                case 1:
                    StartCoroutine(Right_Down());
                    break;
                case 2:
                    StartCoroutine(Left_Down());
                    break;
                case 3:
                    StartCoroutine(Left_Up_Up());
                    break;
                default:
                    // cosa???
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Keypad8) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        if(Input.GetAxis("Vertical") >= 0.25 & Input.GetAxis("Horizontal") > -0.25 & Input.GetAxis("Horizontal") < 0.25 & Input.GetKey(KeyCode.Joystick1Button2) & !Input.GetKey(KeyCode.Joystick1Button4) & rules.GetComponent<ResetScene>().getPenaltyCheck())
        {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 3);

            switch (rInt)
            {
                case 0:
                    rInt = r.Next(0, 2);
                    if (rInt == 0)
                    {
                        StartCoroutine(Save_Central_Up());
                    }
                    else
                    {
                        StartCoroutine(Save_Central_Up_New());
                    }
                    break;
                case 1:
                    StartCoroutine(Right_Down());
                    break;
                case 2:
                    StartCoroutine(Left_Down());
                    break;
                default:
                    // cosa???
                    break;
            }
        }
    }

    IEnumerator Right_Down()
    {
        yield return new WaitForSeconds(0.23f);
        gameObject.GetComponent<Animator>().SetTrigger("Right_Down");
    }

    IEnumerator Left_Down()
    {
        yield return new WaitForSeconds(0.23f);
        gameObject.GetComponent<Animator>().SetTrigger("Left_Down");
    }

    IEnumerator Moving_Right()
    {
        yield return new WaitForSeconds(0.23f);
        gameObject.GetComponent<Animator>().SetTrigger("Moving_Right");
    }

    IEnumerator Moving_Left()
    {
        yield return new WaitForSeconds(0.23f);
        gameObject.GetComponent<Animator>().SetTrigger("Moving_Left");
    }

    IEnumerator Right_Up()
    {
        yield return new WaitForSeconds(0.23f);
        gameObject.GetComponent<Animator>().SetTrigger("Right_Up");
    }

    IEnumerator Left_Up()
    {
        yield return new WaitForSeconds(0.23f);
        gameObject.GetComponent<Animator>().SetTrigger("Left_Up");
    }

    IEnumerator Left_Up_Up()
    {
        yield return new WaitForSeconds(0.13f);
        gameObject.GetComponent<Animator>().SetTrigger("Left_Up_Up");
    }

    IEnumerator Right_Up_Up()
    {
        yield return new WaitForSeconds(0.13f);
        gameObject.GetComponent<Animator>().SetTrigger("Right_Up_Up");
    }

    IEnumerator Stay_Left()
    {
        yield return new WaitForSeconds(0.58f); //OLD 0.58f
        gameObject.GetComponent<Animator>().SetTrigger("Stay_Left");
    }

    IEnumerator Stay_Right()
    {
        yield return new WaitForSeconds(0.58f); //OLD 0.58f
        gameObject.GetComponent<Animator>().SetTrigger("Stay_Right");
    }

    IEnumerator Save_Central_Up()
    {
        yield return new WaitForSeconds(0.78f); // OLD 0.78
        gameObject.GetComponent<Animator>().SetTrigger("Central_Up");
    }

    IEnumerator Save_Central_Up_New()
    {
        yield return new WaitForSeconds(1.025f); // OLD 0.78
        gameObject.GetComponent<Animator>().SetTrigger("Central_Up_New");
    }

    IEnumerator Save_Central_Down()
    {
        yield return new WaitForSeconds(0.40f); // OLD 0.15f
        gameObject.GetComponent<Animator>().SetTrigger("Central_Down");
    }

    IEnumerator Save_Central_Down_Mirror()
    {
        yield return new WaitForSeconds(0.40f); // OLD 0.15f
        gameObject.GetComponent<Animator>().SetTrigger("Central_Down_Mirror");
    }

    IEnumerator Save_Central_Central()
    {
        yield return new WaitForSeconds(0.88f); // OLD 0.65f
        gameObject.GetComponent<Animator>().SetTrigger("Central_Central");
        blocking = true;
        yield return new WaitForSeconds(1.8f);
        blocking = false;
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        ball.gameObject.GetComponent<Rigidbody>().useGravity = true;
        ball.gameObject.transform.parent = null;
        ball.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        ball.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    IEnumerator Save_Central_Lob()
    {
        yield return new WaitForSeconds(1.48f); // OLD 1.25f
        gameObject.GetComponent<Animator>().SetTrigger("Central_Central");
        lob = true;
        yield return new WaitForSeconds(1.8f);
        lob = false;
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        ball.gameObject.GetComponent<Rigidbody>().useGravity = true;
        ball.gameObject.transform.parent = null;
        ball.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        ball.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    IEnumerator Save_Right_Lob()
    {
        yield return new WaitForSeconds(0.93f);
        gameObject.GetComponent<Animator>().SetTrigger("Right_Up");
        lob_right_left = true;
        yield return new WaitForSeconds(3.1f);
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        ball.gameObject.GetComponent<Rigidbody>().useGravity = true;
        ball.gameObject.transform.parent = null;
        ball.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        ball.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    IEnumerator Save_Left_Lob()
    {
        yield return new WaitForSeconds(0.93f);
        gameObject.GetComponent<Animator>().SetTrigger("Left_Up");
        lob_right_left = true;
        yield return new WaitForSeconds(3.1f);
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        ball.gameObject.GetComponent<Rigidbody>().useGravity = true;
        ball.gameObject.transform.parent = null;
        ball.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        ball.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void setStayLeft(bool b)
    {
        stay_left = b;
    }

    public void setStayRight(bool b)
    {
        stay_right = b;
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
        if (other.tag == "Ball" & blocking)
        {
            //Debug.Log("blocco la palla!");

            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            other.gameObject.transform.parent = body.transform;
            ball.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().detectCollisions = false;

            var pos = other.gameObject.transform.localPosition;
            //pos.x = -0.025f;
            //pos.y = 0.169f;
            //pos.z = 0.375f;
            pos.x = -0.003f;
            pos.y = 0.309f;
            pos.z = 0.282f;
            other.gameObject.transform.localPosition = pos;
        }

        if (other.tag == "Ball" & lob)
        {
            //Debug.Log("blocco la palla!");

            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            other.gameObject.transform.parent = body.transform;
            ball.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().detectCollisions = false;

            var pos = other.gameObject.transform.localPosition;
            //pos.x = -0.017f;
            //pos.y = 0.136f;
            //pos.z = 0.321f;
            pos.x = -0.003f;
            pos.y = 0.309f;
            pos.z = 0.282f;
            other.gameObject.transform.localPosition = pos;
        }
    }
}
