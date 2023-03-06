using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIBehaviour : MonoBehaviour
{

    public static GUIBehaviour Instance;

    public GameObject start;
    public GameObject serve;
    public GameObject[] play;
    public GameObject pause;
    public GameObject gameover;
    public GameObject checkpoint;
    [SerializeField] Text pressP;


    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        foreach(GameObject gui in play)
        {
            gui.SetActive(false);
        }
        serve.SetActive(false);
        gameover.SetActive(false);
        pause.SetActive(false);
        checkpoint.SetActive(false);
        
    }

    private void Start()
    {
        pressP = play[4].GetComponent<Text>();
    }

    public void StartToServe()
    {
        start.SetActive(false);
        gameover.SetActive(false);
        serve.SetActive(true);
        foreach (GameObject gui in play)
        {
            gui.SetActive(true);
        }
    }

    public void ServeToPlay()
    {
        serve.SetActive(false);
        checkpoint.SetActive(false);
    }


    public void PlayToServe()
    {
        serve.SetActive(true);
    }

    public void Pause()
    {
        pause.SetActive(true);
        PauseUpdate("continue");
    }

    public void unPause()
    {
        pause.SetActive(false);
        PauseUpdate("pause");
    }

    public void GameOver()
    {
        foreach (GameObject gui in play)
        {
            gui.SetActive(false);
        }
        serve.SetActive(false);
        gameover.SetActive(true);
    }

    public void CheckPoint()
    {
        checkpoint.SetActive(true);
    }

    void PauseUpdate(string t)
    {
        //pressP.text = "press P to " + t;
    }

}


