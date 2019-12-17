using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    
    private float startLoop = 4.020570f;
    private float endLoop = 36.02f;

    private float startTime = 0;

    private void Awake()
    {
        // Singleton to make 1 instance
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        source.clip = clip;
        source.loop = true;
        source.time = startTime;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        OnLoop();
    }

    public void OnLoop()
    {
        if (startTime != startLoop && source.time >= startLoop)
        {
            startTime = startLoop;
        }

        if (startTime != 0 && source.time < startLoop)
        {
            source.time = startTime;
        }
    }
}
