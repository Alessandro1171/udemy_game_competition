using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOVer : MonoBehaviour
{
    public void Restat()
    {
        SceneManager.LoadScene(0);
    }
}
