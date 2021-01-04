using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformReset : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.position = gameObject.transform.parent.position;
    }
}
