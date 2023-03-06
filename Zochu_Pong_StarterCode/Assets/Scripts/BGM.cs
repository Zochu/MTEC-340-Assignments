using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{

    public static BGM Instance;

    [SerializeField] AudioClip start;
    [SerializeField] AudioClip play;

    public AudioSource bgm;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance);
        else
            Instance = this;

        bgm = GetComponent<AudioSource>();

        bgm.clip = start;
        bgm.Play(0);
    }

    // Update is called once per frame
    //void Start()
    //{
        
    //}
}
