using System;
using UnityEngine;
using UnityEngine.UI;
using Obi.CrossPlatformInput;
using System.Collections;

namespace Obi.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class StrikerUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        // aggiunte Personali
        public GameObject soccer_door;
        public Rigidbody ball;
        public float thrust = 2000;
        public GameObject e;
        public GameObject rules;
        public float shoot_delay; // OLD 0.45f
        //private bool ball_owned;
        private bool shooting;
        private Animator anim;

        private void Start()
        {
            anim = gameObject.GetComponent<Animator>();

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (Input.GetKeyDown(KeyCode.Keypad2) & rules.GetComponent<ResetScene>().getPenaltyCheck())
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
            }

        }

        public bool checkShooting()
        {
            return shooting;
        }

        IEnumerator Shoot_Lob_Central()
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shoot_delay);
            //Debug.Log("Ho calciato la palla!");
            shooting = true;
            ball.AddForce(transform.forward * (thrust / 1.8f)); //OLD 2.2 // 2.1
            ball.AddForce(transform.up * (thrust / 3)); //OLD 3.1 // 2.9
            //ball.AddForce(transform.right * (thrust / 50));
            ball.AddTorque(0, 6000, 0);
            StartCoroutine(Shooting_Time());
        }

        IEnumerator Shoot_Central_Down()
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shoot_delay);
            //Debug.Log("Ho calciato la palla!");
            shooting = true;
            ball.AddForce(transform.forward * (thrust*1.1f)); // ADD 1.1f
            ball.AddForce(transform.up * (thrust / 8));
            StartCoroutine(Shooting_Time());
        }

        IEnumerator Shoot_Central_Up()
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shoot_delay);
            //Debug.Log("Ho calciato la palla!");
            shooting = true;
            ball.AddForce(transform.forward * thrust);
            ball.AddForce(transform.up * (thrust / 3.13f)); // ADD .13f
            StartCoroutine(Shooting_Time());
        }

        IEnumerator Shoot_Central_Central()
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shoot_delay);
            //Debug.Log("Ho calciato la palla!");
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
            shooting = true;
            ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
            ball.AddForce(transform.up * (thrust / 9));
            ball.AddForce(-transform.right * (thrust / 3f)); // OLD 3.6
            StartCoroutine(Shooting_Time());
        }

        IEnumerator Shoot_Right_Down()
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shoot_delay);
            //Debug.Log("Ho calciato la palla!");
            shooting = true;
            ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
            ball.AddForce(transform.up * (thrust / 9));
            ball.AddForce(transform.right * (thrust / 3f)); // OLD 3.6
            StartCoroutine(Shooting_Time());
        }

        IEnumerator Shoot_Left_Up()
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shoot_delay);
            //Debug.Log("Ho calciato la palla!");
            shooting = true;
            ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
            ball.AddForce(transform.up * (thrust / 3.1f)); // ADD .1f
            ball.AddForce(-transform.right * (thrust / 3.5f)); // OLD 4
            StartCoroutine(Shooting_Time());
        }

        IEnumerator Shoot_Right_Up()
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shoot_delay);
            //Debug.Log("Ho calciato la palla!");
            shooting = true;
            ball.AddForce(transform.forward * (thrust * 1.2f)); // ADD 1.1f
            ball.AddForce(transform.up * (thrust / 3.1f)); // ADD .1f
            ball.AddForce(transform.right * (thrust / 3.5f)); // OLD 4
            StartCoroutine(Shooting_Time());
        }

        IEnumerator Shoot_Left_Central()
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shoot_delay);
            //Debug.Log("Ho calciato la palla!");
            shooting = true;
            ball.AddForce(transform.forward * (thrust * 1.1f)); // ADD 1.1f
            ball.AddForce(transform.up * (thrust / 4));
            ball.AddForce(-transform.right * (thrust / 3.5f));
            StartCoroutine(Shooting_Time());
        }

        IEnumerator Shoot_Right_Central()
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(shoot_delay);
            //Debug.Log("Ho calciato la palla!");
            shooting = true;
            ball.AddForce(transform.forward * (thrust * 1.1f)); // ADD 1.1f
            ball.AddForce(transform.up * (thrust / 4));
            ball.AddForce(transform.right * (thrust / 3.5f));
            StartCoroutine(Shooting_Time());
        }

        IEnumerator Shooting_Time()
        {
            yield return new WaitForSeconds(3);

            if (!soccer_door.GetComponent<GoalLine>().getGoal())
            {
                Debug.Log("NO GOL!");
                e.GetComponent<Text>().text = (Int32.Parse(e.GetComponent<Text>().text) + 1).ToString();
            }

            shooting = false;
            soccer_door.GetComponent<GoalLine>().setGoal(false);

            yield return new WaitForSeconds(1);

            rules.GetComponent<ResetScene>().nextKick();
        }

        /*private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Ball")
            {
                ball_owned = true;
                //Debug.Log("Ho la palla!");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag == "Ball")
            {
                ball_owned = false;
                //Debug.Log("NON ho piu' la palla!");
            }
        }*/

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif
	
            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
