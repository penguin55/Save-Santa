using System.Collections;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public static SFXManager _instance;

    // 0 Santa ho ho
    [SerializeField] private AudioClip[] _audioClip;

    [SerializeField] private AudioSource playerSource;
    [SerializeField] private AudioSource enemySource;
    [SerializeField] private AudioSource UISource;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    public void PlaySFX(string nameSource, int clip)
    {
        GetSource(nameSource).PlayOneShot(GetClip(clip));
    }
    
    public void PlaySFX(string nameSource, int clip, float delay)
    {
        StartCoroutine(DelayPlay(nameSource, clip, delay));
    }

    IEnumerator DelayPlay(string nameSource, int clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        GetSource(nameSource).PlayOneShot(GetClip(clip));
    }

    private AudioSource GetSource(string nameSource)
    {
        switch (nameSource.ToLower())
        {
            case "player":
                return playerSource;
            case "zombie":
                return enemySource;
            case "ui":
                return UISource;
            default:
                return null;
        }
    }

    private AudioClip GetClip(int clip)
    {
        return _audioClip[clip];
    }
}
