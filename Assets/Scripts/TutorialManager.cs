using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager _instance;
    
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject santaHint;
    [SerializeField] private GameObject zombieHint;
    [SerializeField] private GameObject clickHint;

    [SerializeField] private float delayHint;
    private float time;

    private bool nextHint;
    
    // Start is called before the first frame update
    void Start()
    {
        time = delayHint;
        _instance = this;
    }

    public void StartTutorial()
    {
        StartCoroutine(DelayNextHint());
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorialPanel.activeSelf) return;

        if (clickHint.activeSelf) return;
        
        if (time < 0)
        {
            time = delayHint;
            clickHint.SetActive(true);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    IEnumerator DelayNextHint()
    {
        tutorialPanel.SetActive(true);
        santaHint.SetActive(true);
        yield return new WaitUntil(NextHint);
        santaHint.SetActive(false);
        nextHint = false;
        zombieHint.SetActive(true);
        yield return new WaitUntil(NextHint);
        zombieHint.SetActive(false);
        tutorialPanel.SetActive(false);
        nextHint = false;
        GameManagement._instance.HitAble = true;
    }

    private bool NextHint()
    {
        return nextHint;
    }

    private void OnMouseDown()
    {
        clickHint.SetActive(false);
        nextHint = true;
    }
}
