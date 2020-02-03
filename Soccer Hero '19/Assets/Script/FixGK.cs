using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixGK : MonoBehaviour
{
    public GameObject goalkeeperPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            goalkeeperPlayer.GetComponent<GoalKeeperPlayerControl>().setBlocking(true);
        }
    }
}
