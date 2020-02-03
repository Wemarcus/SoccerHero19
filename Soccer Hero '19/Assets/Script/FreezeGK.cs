using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGK : MonoBehaviour
{
    public GameObject GoalkeeperPlayer;
    public GameObject GoalkeeperAI;
    public GameObject StrikerPlayer;
    public GameObject StrikerAI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            GoalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().setFreeze(true);

            if (StrikerPlayer.activeSelf)
            {
                StrikerPlayer.GetComponent<StrikerPlayerControl>().Despair();
                GoalkeeperAI.GetComponent<GoalKeeper>().Exultation();
            } else if (StrikerAI.activeSelf)
            {
                StrikerAI.GetComponent<StrikerAIControl>().Despair();
                GoalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().Exultation();
            }
        }
    }
}
