using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayUI : UIFunction
{
    public static GameplayUI instance;
    public Text m_lapCounter, m_placeText;
    [SerializeField]
    AudioClip lapDone;
    int laps = 0;

    public override void CallFunction(int _index, UiScroller _ref)
    {
        switch (_index)
        {
            case 0:
                GameStateManager.instance.ChangeStategames(GameStateManager.GameSate.Gameplay);
                break;
            case 2:
                Application.Quit();
                break;
            case 1:
                SceneManager.LoadScene(1);
                break;
        }
    }

    public void NextLap()
    {
        laps++;
        SoundManager.instance.playSound(lapDone, 1, 1);
        m_lapCounter.text = laps + "/3";
        StartCoroutine(flash());
        if (laps == 3)
            GameStateManager.instance.ChangeStategames(GameStateManager.GameSate.GameOver);
    }

    IEnumerator flash()
    {
        m_lapCounter.GetComponent<flash>().enabled = true;
        yield return new  WaitForSeconds(1.5f);
        m_lapCounter.GetComponent<flash>().enabled = false;
        m_lapCounter.enabled = true;
    }

    void Start()
    {
        instance = this;
    }

    void Update()
    {
 
    }
}