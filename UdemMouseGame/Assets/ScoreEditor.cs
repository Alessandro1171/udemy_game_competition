using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEditor : MonoBehaviour
{
    public int score;
    public Text txt;

    void Start()
    {
        score = 50; // player starts with 50 points 
    }

    void Update()
    {
        txt.text = "testttttt";
        //txt.text = "Score: " + score;
    }
}
