﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5);

    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

