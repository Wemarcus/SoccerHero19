using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GoalLine : MonoBehaviour
{
    public bool goal;
    public GameObject StrikerPlayer;
    public GameObject StrikerAI;
    public GameObject GoalkeeperPlayer;
    public GameObject GoalkeeperAI;
    //public GameObject g; // OLD ==> da cancellare
    public GameObject team_A;
    public GameObject team_B;
    public GameObject rules;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball" && !goal && StrikerPlayer.GetComponent<StrikerPlayerControl>().checkShooting())
        {
            Debug.Log("BLU TEAM: GOL!");
            goal = true;
            //g.GetComponent<Text>().text = (Int32.Parse(g.GetComponent<Text>().text) + 1).ToString(); // OLD
            team_A.GetComponent<Text>().text = (Int32.Parse(team_A.GetComponent<Text>().text) + 1).ToString();
            rules.GetComponent<ResetScene>().setGolBlueTeam();

            other.gameObject.GetComponent<Obi.ObiCollider>().Thickness = 0.1f;
            StrikerPlayer.GetComponent<StrikerPlayerControl>().Exultation();
            GoalkeeperAI.GetComponent<GoalKeeper>().Despair();
        }

        if (other.tag == "Ball" && !goal && StrikerAI.GetComponent<StrikerAIControl>().checkShooting())
        {
            GoalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().setFreeze(true);

            Debug.Log("RED TEAM: GOL!");
            goal = true;
            //g.GetComponent<Text>().text = (Int32.Parse(g.GetComponent<Text>().text) + 1).ToString(); // OLD
            team_B.GetComponent<Text>().text = (Int32.Parse(team_B.GetComponent<Text>().text) + 1).ToString();
            rules.GetComponent<ResetScene>().setGolRedTeam();

            other.gameObject.GetComponent<Obi.ObiCollider>().Thickness = 0.1f;
            StrikerAI.GetComponent<StrikerAIControl>().Exultation();
            GoalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().Despair();
        }
    }

    public bool getGoal()
    {
        return goal;
    }

    public void setGoal(bool b)
    {
        goal = b;
    }
}
