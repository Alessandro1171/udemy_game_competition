using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int score;
    public Text txt;

    void Start()
    {
        score = 50; // player starts with 50 points
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "Score: " + score;
        //txt.text = "testttttt";
    }
}
