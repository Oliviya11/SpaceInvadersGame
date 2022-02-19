using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launch : MonoBehaviour
{
    public void StartGame()
    {
        LoadScene("SpaceInvadersGame");
    }
    
    public void LoadMenuGame()
    {
        LoadScene("Launch");
    }

    void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
