using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScene : MonoBehaviour
{
    public GameObject ball;
    public GameObject strikerPlayer;
    public GameObject goalkeeperPlayer;
    public GameObject strikerAI;
    public GameObject goalkeeperAI;
    public GameObject referee;
    public float sound_delay;

    public bool penalty_check;
    public int blue_team;
    public int red_team;

    public int diff;
    public int chances;
    public int diff_2;
    public int chances_2;
    public bool playoff;
    public GameObject background;
    public GameObject winner;
    public GameObject[] scores;
    public GameObject[] scores_p;
    public GameObject team_A;
    public GameObject team_B;

    private Quaternion striker_rotation;
    private Quaternion goalkeeperPlayer_rotation;
    private Quaternion goalkeeperAI_rotation;
    private Quaternion referee_rotation;
    private Animator anim_ref;
    private int goal_blue_team;
    private int goal_red_team;
    private bool finish;

    private void Start()
    {
        striker_rotation = strikerPlayer.transform.localRotation;
        goalkeeperPlayer_rotation = goalkeeperPlayer.transform.localRotation;
        goalkeeperAI_rotation = goalkeeperAI.transform.localRotation;
        referee_rotation = referee.transform.localRotation;
        penalty_check = true;
        blue_team = 1;
        red_team = 0;
        anim_ref = referee.GetComponent<Animator>();
        anim_ref.SetTrigger("Whistle");
        referee.GetComponent<AudioSource>().PlayDelayed(sound_delay);
        playoff = false;
        finish = false;
    }

    private void Update()
    {
        if (finish & Input.GetKey(KeyCode.Joystick1Button0))
        {
            penalty_check = true;
            blue_team = 1;
            red_team = 0;
            anim_ref.SetTrigger("Whistle");
            referee.GetComponent<AudioSource>().PlayDelayed(sound_delay);
            playoff = false;
            finish = false;
            goal_blue_team = 0;
            goal_red_team = 0;

            goalkeeperPlayer.SetActive(false);
            goalkeeperAI.SetActive(true);
            goalkeeperAI.GetComponent<GoalKeeper>().setStayRight(false);
            goalkeeperAI.GetComponent<GoalKeeper>().setStayLeft(false);
            goalkeeperAI.GetComponent<GoalKeeper>().resetExultation(false);
            goalkeeperAI.transform.position = new Vector3(9.54f, 0, 40.1225f);
            goalkeeperAI.transform.localRotation = goalkeeperAI_rotation;

            strikerAI.SetActive(false);
            strikerPlayer.SetActive(true);
            strikerPlayer.transform.position = new Vector3(22.28f, 0, 40.004f);
            strikerPlayer.transform.localRotation = striker_rotation;
            strikerPlayer.GetComponent<StrikerPlayerControl>().resetExultation(false);
            strikerPlayer.GetComponent<KeyPressure>().resetCheck();

            ball.transform.position = new Vector3(20.433f, 0.138f, 40.119f);
            ball.transform.rotation = new Quaternion(0, 0, 0, 0);
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ball.GetComponent<Obi.ObiCollider>().Thickness = 0;

            strikerPlayer.GetComponent<StrikerPlayerControl>().setScores();
            scores[0].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
            scores[1].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
            scores[2].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
            scores[3].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
            scores[4].GetComponent<Image>().color = new Color32(255, 255, 255, 60);

            strikerAI.GetComponent<StrikerAIControl>().setScores();
            scores_p[0].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
            scores_p[1].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
            scores_p[2].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
            scores_p[3].GetComponent<Image>().color = new Color32(255, 255, 255, 60);
            scores_p[4].GetComponent<Image>().color = new Color32(255, 255, 255, 60);

            background.SetActive(false);
            team_A.GetComponent<Text>().text = "0";
            team_B.GetComponent<Text>().text = "0";
        }

        if (finish & Input.GetKey(KeyCode.Joystick1Button1))
        {
            Application.Quit();
        }
    }

    // 0 = blu ; 1 = red
    public bool checkEnd(int i)
    {
        if (!playoff)
        {
            if (goal_blue_team >= goal_red_team)
            {
                diff = goal_blue_team - goal_red_team;
            }
            else
            {
                diff = goal_red_team - goal_blue_team;
            }

            if (goal_blue_team >= goal_red_team & (5 - red_team >= diff))
            {
                Debug.Log("bisogna proseguire..");
                return false;
            }
            else if (goal_red_team >= goal_blue_team & (5 - blue_team >= diff))
            {
                Debug.Log("bisogna proseguire..");
                return false;
            }
            else
            {
                if (goal_blue_team > goal_red_team)
                {
                    Debug.Log("TEAM BLU VICTORY");
                    background.SetActive(true);
                    winner.GetComponent<Text>().text = "TEAM BLU VICTORY";
                    winner.GetComponent<Text>().color = new Color32(6, 119, 152, 255);
                    finish = true;
                    return true;
                }
                else
                {
                    Debug.Log("TEAM RED VICTORY");
                    background.SetActive(true);
                    winner.GetComponent<Text>().text = "TEAM RED VICTORY";
                    winner.GetComponent<Text>().color = new Color32(164, 49, 49, 255);
                    finish = true;
                    return true;
                }
            }
        }
        else
        {
            diff = goal_blue_team - goal_red_team;

            if (diff != 0 & i == 1)
            {
                if (goal_blue_team > goal_red_team)
                {
                    Debug.Log("TEAM BLU VICTORY");
                    background.SetActive(true);
                    winner.GetComponent<Text>().text = "TEAM BLU VICTORY";
                    winner.GetComponent<Text>().color = new Color32(6, 119, 152, 255);
                    finish = true;
                    return true;
                }
                else
                {
                    Debug.Log("TEAM RED VICTORY");
                    background.SetActive(true);
                    winner.GetComponent<Text>().text = "TEAM RED VICTORY";
                    winner.GetComponent<Text>().color = new Color32(164, 49, 49, 255);
                    finish = true;
                    return true;
                }
            }
            else
            {
                Debug.Log("si va ad oltranza..");
                return false;
            }
        }
    }

    public void nextKick()
    {
        setPenaltyCheck(true);
        referee.transform.localRotation = referee_rotation;
        anim_ref.SetTrigger("Whistle");
        referee.GetComponent<AudioSource>().PlayDelayed(sound_delay);

        if (blue_team > red_team)
        {
            red_team = red_team + 1;

            goalkeeperAI.SetActive(false);
            goalkeeperPlayer.SetActive(true);
            goalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().check = true;
            goalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().setFreeze(false);
            goalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().setBlocking(false);
            goalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().resetExultation(false);
            goalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().i = 0;
            goalkeeperPlayer.transform.position = new Vector3(9.54f, 0, 40.1225f);
            goalkeeperPlayer.transform.localRotation = goalkeeperPlayer_rotation;

            strikerPlayer.SetActive(false);
            strikerAI.SetActive(true);
            strikerAI.GetComponent<StrikerAIControl>().setCentral(false);
            strikerAI.GetComponent<StrikerAIControl>().setLeft(false);
            strikerAI.GetComponent<StrikerAIControl>().setRight(false);
            strikerAI.GetComponent<StrikerAIControl>().setLob(false);
            strikerAI.transform.position = new Vector3(22.28f, 0, 40.004f);
            strikerAI.transform.localRotation = striker_rotation;
            strikerAI.GetComponent<StrikerAIControl>().resetExultation(false);
        }
        else if (blue_team == red_team)
        {
            blue_team = blue_team + 1;

            goalkeeperPlayer.SetActive(false);
            goalkeeperAI.SetActive(true);
            goalkeeperAI.GetComponent<GoalKeeper>().setStayRight(false);
            goalkeeperAI.GetComponent<GoalKeeper>().setStayLeft(false);
            goalkeeperAI.GetComponent<GoalKeeper>().resetExultation(false);
            goalkeeperAI.transform.position = new Vector3(9.54f, 0, 40.1225f);
            goalkeeperAI.transform.localRotation = goalkeeperAI_rotation;

            strikerAI.SetActive(false);
            strikerPlayer.SetActive(true);
            strikerPlayer.transform.position = new Vector3(22.28f, 0, 40.004f);
            strikerPlayer.transform.localRotation = striker_rotation;
            strikerPlayer.GetComponent<StrikerPlayerControl>().resetExultation(false);
            strikerPlayer.GetComponent<KeyPressure>().resetCheck();
        }

        ball.transform.position = new Vector3(20.433f, 0.138f, 40.119f);
        ball.transform.rotation = new Quaternion(0, 0, 0, 0);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.GetComponent<Obi.ObiCollider>().Thickness = 0;
       
    }

    public bool getPenaltyCheck()
    {
        return penalty_check;
    }

    public void setPenaltyCheck(bool b)
    {
        penalty_check = b;
    }

    public void setGolBlueTeam()
    {
        goal_blue_team = goal_blue_team + 1;
    }

    public void setGolRedTeam()
    {
        goal_red_team = goal_red_team + 1;
    }

    public void setPlayOff(bool b)
    {
        playoff = b;
    }
}
