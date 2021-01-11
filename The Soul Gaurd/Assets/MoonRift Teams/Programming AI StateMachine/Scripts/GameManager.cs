using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private LayerMask layerMask;

    public MusicManager musicManager;

    //Jeremiah's Code
    //private Animal currentTarget;
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
        //Debug.Log(LayerMask.GetMask("Clickable"));
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

    //private void ClickTarget()
    //{
    //    if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
    //    {
    //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            Debug.Log("Hit");
    //            //player.MyTarget = hit.transform;//7
    //            player.MyTarget = hit.transform.GetChild(0);
    //            //if (currentTarget != null)
    //            //{
    //            //    currentTarget.Deselect();
    //            //}

    //            //currentTarget = hit.collider.GetComponent<Animal>();
    //            //player.MyTarget = currentTarget.Select();
    //        }




            //else
            //{
            //    if (currentTarget != null)
            //    {
            //        currentTarget.Deselect();
            //    }

            //    currentTarget = null;
            //    player.MyTarget = null;
            //}
            //RaycastHit hit = Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero, Mathf.Infinity, 512);

            //if (hit.collider != null)
            //{
            //    Debug.Log("ClickTarget");
            //    player.MyTarget = hit.transform;
            //}
            //else
            //{
            //    player.MyTarget = null;
            //}
        //}
    //}
}
