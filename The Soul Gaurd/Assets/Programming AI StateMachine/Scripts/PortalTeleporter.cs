using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;

    private void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                float rotaionDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotaionDiff += 180;
                player.Rotate(Vector3.up, rotaionDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotaionDiff, 0f) * portalToPlayer;

                player.GetComponent<PlayerInputHandler>().enabled = false;

                player.position = reciever.position + positionOffset;

                player.GetComponent<PlayerInputHandler>().enabled = true;

                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}
