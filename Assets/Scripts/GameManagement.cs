using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{

    public static GameManagement _instance;

    [SerializeField] private GameObject gameOverPanel;

    [HideInInspector] public bool StartGame;
    [HideInInspector] public bool PauseGame;
    [HideInInspector] public bool HitAble;
    
    [SerializeField] private GameObject[] zombie;

    [SerializeField] private Santa santa;

    private int zombieCount;

    //To determine where santa start the pos and end the position when the game is over
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform finalPos;

    public void StartTheGame()
    {
        StartGame = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        HitAble = false;
        _instance = this;
        zombieCount = zombie.Length;
        gameOverPanel.SetActive(false);
        santa.Initialize(2,startPos);
        StartCoroutine(GameStart());
    }

    public void DecreaseZombieCount()
    {
        zombieCount--;
        if (zombieCount < 0) zombieCount = 0;

        if (zombieCount == 0)
        {
            SFXManager._instance.PlaySFX("Player",0, 1);
            StartCoroutine(GameOver());
        }
    }
    
    IEnumerator GameStart()
    {
        yield return new WaitUntil(SantaOnStart);
        if (TutorialManager._instance == null ) HitAble = true;
        else TutorialManager._instance.StartTutorial();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.3f);
        santa.WalkToDirection(finalPos);
        yield return new WaitUntil(SantaOnFinish);
        PauseGame = true;
        gameOverPanel.SetActive(true);
    }

    //Check is Santa in Finish Position or no
    private bool SantaOnFinish()
    {
        return santa.GetDirection() == null;
    }
    
    //Check is Santa in Start Position or no
    private bool SantaOnStart()
    {
        return santa.GetDirection() == null;
    }
}
