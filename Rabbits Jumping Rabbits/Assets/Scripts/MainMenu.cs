using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static int numberOfPlayers;
    void Start()
    {
        
    }

    public void Play2p()
    {
        SceneManager.LoadScene(1);
        numberOfPlayers = 2;
    }

    public void Play3p()
    {
        SceneManager.LoadScene(2);
        numberOfPlayers = 3;
    }

    public void Play4p()
    {
        SceneManager.LoadScene(3);
        numberOfPlayers = 4;
    }

    public void Play2v2()
    {
        SceneManager.LoadScene(4);
    }

    public void ReturnToMenu()
    {

    }


}
