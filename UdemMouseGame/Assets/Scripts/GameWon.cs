using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
}
