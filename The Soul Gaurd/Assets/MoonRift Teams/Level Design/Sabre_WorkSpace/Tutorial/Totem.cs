using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public GameObject Text;
    public GameObject Player;
    private bool visible = true;
    public float distanceToAppear = 8;
    public Renderer objRenderer;
    public Renderer objRenderer1;
    Vector3 cameraPosition;

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        Text.transform.LookAt(Player.transform);
        disappearChecker();
        cameraPosition = Player.transform.position;
    }
    private void disappearChecker()
    {
        float distance = Vector3.Distance(cameraPosition, transform.position);

        // We have reached the distance to Enable Object
        if (distance < distanceToAppear)
        {
            if (!visible)
            {
                objRenderer.enabled = true;
                objRenderer1.enabled = true;// Show Object
                visible = true;
            }
        }
        else if (visible)
        {
            objRenderer.enabled = false;
            objRenderer1.enabled = false;// Hide Object
            visible = false;
        }
    }
}