using Assets.Scripts.UnityComponents;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text youWin;
    public Text youLose;
    public Image[] health;
    public Button loseButton;
    public Button winButton;
    public Button mainMenuButton;
    public Text goal;

    void Start()
    {
        Static.ui = this;
        youLose.gameObject.SetActive(false);
        youWin.gameObject.SetActive(false);
        loseButton.gameObject.SetActive(false);
        winButton.gameObject.SetActive(false);

        loseButton.onClick.AddListener(reload);
        winButton.onClick.AddListener(mainMenu);
        mainMenuButton.onClick.AddListener(mainMenu);

        goal.text = Static.CurrentLvl.AsteroidsDestroyed.ToString() + "/" + Static.CurrentLvl.AsteroidsGoal.ToString();
    }

    public void SetHealth(int curHealth)
    {   
        if(curHealth == 3)
        {
            health[2].gameObject.SetActive(true);
            health[1].gameObject.SetActive(true);
            health[0].gameObject.SetActive(true);
        }
        if(curHealth == 2)
        {
            health[2].gameObject.SetActive(false);
            health[1].gameObject.SetActive(true);
            health[0].gameObject.SetActive(true);
        }
        if (curHealth == 1)
        {
            health[2].gameObject.SetActive(false);
            health[1].gameObject.SetActive(false);
            health[0].gameObject.SetActive(true);
        }
        if (curHealth == 0)
        {
            health[2].gameObject.SetActive(false);
            health[1].gameObject.SetActive(false);
            health[0].gameObject.SetActive(false);
        }
    }

    public void WinScreen()
    {
        youWin.gameObject.SetActive(true);
        winButton.gameObject.SetActive(true);
        Static.CurrentLvl.Restart();
        Service<EcsWorld>.Get().Destroy();
    }

    public void LoseScreen()
    {
        youLose.gameObject.SetActive(true);
        loseButton.gameObject.SetActive(true);
        Static.CurrentLvl.Restart();
        Service<EcsWorld>.Get().Destroy();
    }

    void reload()
    {
        Pool.asteroids.Clear();
        Pool.bullets.Clear();
        Static.CurrentLvl.Restart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Pool.asteroids.Clear();
        Pool.bullets.Clear();
    }
}
