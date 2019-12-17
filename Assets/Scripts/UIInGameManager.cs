using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInGameManager : MonoBehaviour
{

    [SerializeField] private GameObject transitionInPanel;
    [SerializeField] private GameObject transitionOutPanel;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Animator pauseAnimator;

    public void Start()
    {
        GameManagement._instance.PauseGame = false;
        transitionOutPanel.SetActive(false);
        transitionInPanel.SetActive(true);
        StartCoroutine(TransitionIn());
    }
    
    IEnumerator TransitionIn()
    {
        yield return new WaitForSeconds(1.2f);
        GameManagement._instance.StartTheGame();
        transitionInPanel.SetActive(false);
    }

    public void Home()
    {
        transitionOutPanel.SetActive(true);
        GameManagement._instance.PauseGame = true;
        StartCoroutine(TransitionOut("SaveSanta_MENU"));
    }

    IEnumerator TransitionOut(string scene)
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(scene);
    }

    public void GoToScene(string scene)
    {
        transitionOutPanel.SetActive(true);
        GameManagement._instance.PauseGame = true;
        StartCoroutine(TransitionOut(scene));
    }

    public void Pause()
    {
        GameManagement._instance.PauseGame = !GameManagement._instance.PauseGame;
        if (GameManagement._instance.PauseGame)
        {
            pausePanel.SetActive(true);
            pauseAnimator.SetTrigger("Pause");
        }
        else
        {
            pauseAnimator.SetTrigger("Unpause");
            StartCoroutine(DelayUnPause());
        }
    }

    IEnumerator DelayUnPause()
    {
        yield return new WaitForSeconds(0.6f);
        pausePanel.SetActive(false);
    }

    public void ClickButton()
    {
        SFXManager._instance.PlaySFX("UI", 2);
    }
}
