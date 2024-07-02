using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{

    public void Play2p()
    {
        SceneManager.LoadScene(1);
    }

    public void Play3p()
    {
        SceneManager.LoadScene(2);
    }

    public void Play4p()
    {
        SceneManager.LoadScene(3);
    }

    public void Play2v2()
    {
        SceneManager.LoadScene(4);
    }
}
