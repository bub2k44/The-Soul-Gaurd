using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    AgentController player;

    [SerializeField]
    private LayerMask layerMask;

    public MusicManager musicManager;

    private NPC currentTarget;

    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    private void Start()
    {
        musicManager.ChangeTrackWithoutFade("Default");
    }

    private void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())//&& !EventSystem.current.IsPointerOverGameObject()
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask))//512
            {
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.blue);
                Debug.Log("Did hit");
                if (hit.collider != null)
                {
                    if (currentTarget != null)
                    {
                        currentTarget.Deselect();
                    }

                    currentTarget = hit.collider.GetComponent<NPC>();
                    player.MyTarget = currentTarget.Select();
                    UIManager.MyInstance.ShowTargetFrame(currentTarget);
                }
                else
                {
                    UIManager.MyInstance.HideTargetFrame();

                    if (currentTarget != null)
                    {
                        currentTarget.Deselect();
                    }

                    currentTarget = null;
                    player.MyTarget = null;
                }
            }
        }
    }
}
