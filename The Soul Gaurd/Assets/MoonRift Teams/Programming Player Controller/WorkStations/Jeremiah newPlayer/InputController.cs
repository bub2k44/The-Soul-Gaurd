using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject camera1st;
    public GameObject camera3rd;

    public GameObject person1st;
    public GameObject person3rd;

    public bool is1stPerson = false;
    public bool is3rdPerson = false;

    private void Start()
    {
        DontDestroyOnLoad(this);

        camera1st.SetActive(false);
        camera3rd.SetActive(true);

        is3rdPerson = true;
        is1stPerson = false;
    }

    private void Update()
    {
        //Get ThirdPerson
        if (Input.GetKeyDown(KeyCode.V) && !is1stPerson)
        {
            GetThirdPerson();
            camera1st.SetActive(true);
            camera3rd.SetActive(false);
        }
        //Get FirstPerson
        if (Input.GetKeyDown(KeyCode.B) && !is3rdPerson)
        {
            GetFirstPerson();
            camera1st.SetActive(false);
            camera3rd.SetActive(true);
        }
    }

    private void GetThirdPerson()
    {
        is3rdPerson = false;
        is1stPerson = true;

        Vector3 tempPosition = person1st.transform.position;

        Quaternion tempRotation = person1st.transform.rotation;

        person1st.transform.position = person3rd.transform.position;

        person1st.transform.rotation = person3rd.transform.rotation;

        person3rd.transform.position = tempPosition;

        person3rd.transform.rotation = tempRotation;
    }

    private void GetFirstPerson()
    {
        is3rdPerson = true;
        is1stPerson = false;

        Vector3 tempPosition = person3rd.transform.position;

        Quaternion tempRotation = camera3rd.transform.rotation;

        person3rd.transform.position = person1st.transform.position;

        person3rd.transform.rotation = person1st.transform.rotation;

        person1st.transform.position = tempPosition;

        person1st.transform.rotation = tempRotation;
    }
}
