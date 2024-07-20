using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : MonoBehaviour
{
    public InputReciever iR;

    private void Awake()
    {
        iR = GameObject.FindWithTag("InputRecieverTag").gameObject.GetComponent<InputReciever>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            iR.Shoot();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            iR.Left();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            iR.Right();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            iR.Up();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            iR.Down();
        }
    }
}
