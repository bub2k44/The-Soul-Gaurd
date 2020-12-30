using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public MusicManager musicManager;

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
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                player.MyTarget = hit.transform;//7
                player.MyTarget = hit.transform.GetChild(7);
                
            }
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
        }
    }
}
