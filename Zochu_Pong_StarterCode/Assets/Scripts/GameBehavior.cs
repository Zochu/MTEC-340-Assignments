using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    public Player[] Players = new Player[2];

    //private BallBehavior bh;
    public State CurrentState;
    public Button start;
    public float VelocityIncrement = 0.1f;
    public float InitVelocity = 5.0f;
    [SerializeField] AudioClip sstart;
    [SerializeField] AudioClip gameov;
    [SerializeField] AudioClip pauseUIFX;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip gamebgm;

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

        CurrentState = State.SStart;
    }

    private void Start()
    {
        Button btn = start.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        CurrentState = State.serve;
        GUIBehaviour.Instance.StartToServe();
        AudioBehavior.Instance.PlaySound(sstart);
        BGM.Instance.bgm.clip = gamebgm;
        BGM.Instance.bgm.Play(0);
    }


    private void Update()
    {
        if (CurrentState == State.serve && Input.GetKeyDown(KeyCode.Space))
        {
            CurrentState = State.Play;
            GUIBehaviour.Instance.ServeToPlay();
            AudioBehavior.Instance.PlaySound(shoot);
        }

        if (CurrentState == State.Play && Input.GetKeyUp(KeyCode.P))
        {
            CurrentState = State.Pause;
            GUIBehaviour.Instance.Pause();
            AudioBehavior.Instance.PlaySound(pauseUIFX);
        }

        else if (CurrentState == State.Pause && Input.GetKeyUp(KeyCode.P))
        {
            CurrentState = State.Play;
            GUIBehaviour.Instance.unPause();
            AudioBehavior.Instance.PlaySound(pauseUIFX);
        }

        //if (CurrentState != State.serve && Input.GetKeyDown(KeyCode.P))
        //CurrentState = CurrentState == State.Play ? State.Pause : State.Play;

        if (CurrentState == State.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            //CurrentState = State.serve;
            //GameBehavior.Instance.CurrentState = State.serve;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateScore(int player)
    {
        Players[player].Score++;
        foreach (Player p in Players)
        {
            if(p.Score > 5)
            {
                CurrentState = State.GameOver;
                GUIBehaviour.Instance.GameOver();
                AudioBehavior.Instance.PlaySound(gameov);
                BGM.Instance.bgm.Pause();
                break;
            }
            else if(p.Score == 5)
            {
                CurrentState = State.serve;
                GUIBehaviour.Instance.CheckPoint();
            }
            else
            {
                CurrentState = State.serve;
                GUIBehaviour.Instance.PlayToServe();
            }
        }
    }

}
