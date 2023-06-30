using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour
{
    public void StartB()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitB()
    {
        Application.Quit();
    }
}
