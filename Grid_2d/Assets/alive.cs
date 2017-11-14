using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alive : MonoBehaviour {

    public bool _alive = false;


    void Update()
    {
        if (_alive)
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
        else if (!_alive)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
