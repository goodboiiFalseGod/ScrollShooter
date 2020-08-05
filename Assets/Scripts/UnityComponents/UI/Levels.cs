using Assets.Scripts.UnityComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelSettings[] LevelsList;
    public Image[] LevelsStates;
    public Button[] LevelStartButtons;
    public Sprite[] nums;
    public Sprite[] LevelsStatesSprites;

    private void Start()
    {
        for(int i = 0; i < LevelsList.Length; i++)
        {
            if(LevelsList[i].LevelState == LevelSettings.LevelStates.Locked)
            {
                LevelsStates[i].sprite = LevelsStatesSprites[0];
            }

            if (LevelsList[i].LevelState == LevelSettings.LevelStates.Unlocked)
            {
                LevelsStates[i].sprite = LevelsStatesSprites[1];
            }

            if (LevelsList[i].LevelState == LevelSettings.LevelStates.Completed)
            {
                LevelsStates[i].sprite = LevelsStatesSprites[2];
            }

            if(LevelsList[i].LevelState == LevelSettings.LevelStates.Completed)
            {   
                if(LevelsList[i + 1].LevelState != LevelSettings.LevelStates.Completed)
                {
                    LevelsList[i + 1].LevelState = LevelSettings.LevelStates.Unlocked;
                }
            }

            /*if(LevelsList[i - 1].LevelState == LevelSettings.LevelStates.Locked)
            {
                LevelsList[i].LevelState = LevelSettings.LevelStates.Completed;
            }*/

            LevelStartButtons[0].onClick.AddListener(delegate { Load(0); });
            LevelStartButtons[1].onClick.AddListener(delegate { Load(1); });
            LevelStartButtons[2].onClick.AddListener(delegate { Load(2); });
            LevelStartButtons[3].onClick.AddListener(delegate { Load(3); });
        }
    }

    private void Load(int No)
    {
        Debug.Log(No.ToString());
        if(LevelsList[No].LevelState != LevelSettings.LevelStates.Locked)
        {
            Static.CurrentLvl = LevelsList[No];
            SceneManager.LoadScene("Level1");
        }
    }
}
