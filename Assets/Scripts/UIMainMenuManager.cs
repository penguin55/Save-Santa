using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuManager : MonoBehaviour
{

    [SerializeField] private Animator exitAnimator;
    [SerializeField] private Animator playAnimator;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject playPanel;
    
    [SerializeField] private GameObject transitionInPanel;
    
    public void Start()
    {
        transitionInPanel.SetActive(true);
        StartCoroutine(TransitionIn());
    }
    
    IEnumerator TransitionIn()
    {
        yield return new WaitForSeconds(1.2f);
        transitionInPanel.SetActive(false);
    }
    
    public void Exit()
    {
        exitPanel.SetActive(true);
        exitAnimator.SetTrigger("Exit");
    }
    
    public void Back()
    {
        exitAnimator.SetTrigger("Back");
        StartCoroutine(DelayBack());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator DelayBack()
    {
        yield return new WaitForSeconds(0.6f);
        exitPanel.SetActive(false);
    }

    public void Play()
    {
        playPanel.SetActive(true);
        StartCoroutine(DelayStart());
    }
    
    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("SaveSanta_GAME_Tutorial");
    }
}
