using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobSave : MonoBehaviour
{
    public GameObject gk;
    public GameObject hand;

    public float x;
    public float y;
    public float z;

    private void OnCollisionEnter(Collision collision)
    {
        if (gk.GetComponent<GoalKeeper>() != null)
        {

            if (collision.gameObject.tag == "Ball" & gk.GetComponent<GoalKeeper>().lob_right_left)
            {
                gk.GetComponent<GoalKeeper>().lob_right_left = false;

                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                collision.gameObject.transform.parent = hand.transform;
                collision.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
                collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                collision.gameObject.GetComponent<Rigidbody>().detectCollisions = false;

                var pos = collision.gameObject.transform.localPosition;
                pos.x = x;
                pos.y = y;
                pos.z = z;
                collision.gameObject.transform.localPosition = pos;

            }

        } else if (gk.GetComponent<GoalKeeperPlayerControl>() != null)
        {

            if (collision.gameObject.tag == "Ball" & gk.GetComponent<GoalKeeperPlayerControl>().lob_right_left)
            {
                gk.GetComponent<GoalKeeper>().lob_right_left = false;

                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                collision.gameObject.transform.parent = hand.transform;
                collision.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
                collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                collision.gameObject.GetComponent<Rigidbody>().detectCollisions = false;

                var pos = collision.gameObject.transform.localPosition;
                pos.x = x;
                pos.y = y;
                pos.z = z;
                collision.gameObject.transform.localPosition = pos;

            }

        }
    }

}
