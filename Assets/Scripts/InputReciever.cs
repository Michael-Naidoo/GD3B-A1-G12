using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputReciever : MonoBehaviour
{
    public Text text;
    private int i;
    public void Shoot()
    {
        i = 0;
    }

    public void Left()
    {
        i = 1;
    }

    public void Right()
    {
        i = 2;
    }

    public void Up()
    {
        i = 3;
    }

    public void Down()
    {
        i = 4;
    }

    private void Update()
    {
        text.text = i.ToString();
    }
}
