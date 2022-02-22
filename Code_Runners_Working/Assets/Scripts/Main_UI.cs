/* 
   Editor: Jacob Arnold
   Date of Creation: 11/2/2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_UI : MonoBehaviour
{
    public bool isEnd = false;
    [SerializeField]
    bool nabLevel = false;

    public Slider healthSlider;
    public Slider levelSlider;

    //public Text objText;  -Remove with Objective Feature
    public Text enemyText;
    public Text scoreText;
    public Text interactText;
    public Text endText;
    public Text lHealthText;
    public Text timerText;
    public Text objText;
    public Text Score;

    public GameObject Endgame;
    public GameObject MainMenu;
    public GameObject CPReplay;
    public GameObject PriorLevel;
    public GameObject NextLevel;
    public GameObject Selection;
    public GameObject lHealthBar;

    public Image lSelect;
    public Image rSelect;
    public Image hImage;

    Overlord Overlord;
    SoundManager sndmngr = null;
    OptionValueStore opVlSt = null;
    Pause pause = null;
    GameObject pauseCanvas = null;
    private void Start()
    {
        levelSlider.maxValue = 100;
        levelSlider.value = 100;

        Overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        opVlSt = GameObject.Find("OptionValueStore").GetComponent<OptionValueStore>();
        pause = GameObject.Find("Pause Canvas").GetComponent<Pause>();
        pauseCanvas = GameObject.Find("Pause Canvas");

        Endgame.SetActive(false);
        MainMenu.SetActive(false);
        CPReplay.SetActive(false);
        PriorLevel.SetActive(false);
        NextLevel.SetActive(false);

        enemyText.text = Overlord.enemyCountI + "/" + Overlord.maxEnemy;
        // objText.text = Overlord.currObj + "/" + Overlord.maxObj;     -Remove with Objective Feature

        if(nabLevel)
        {
            lHealthBar.SetActive(false);
            timerText.text = "Time: " + Time.timeSinceLevelLoad;
            objText.text = "Objectives remaining: " + FindObjectsOfType<NABObjective>().LongLength;
            Score.text = "";
        }
        else
        {
            lHealthBar.SetActive(true);
            timerText.text = "";
            objText.text = "";
        }
    }

    private void Update()
    {
        enemyText.text = Overlord.enemyCountI + "/" + Overlord.maxEnemy;
        //objText.text = Overlord.currObj + "/" + Overlord.maxObj;                           -Removed with Objective Feature
        if(!nabLevel)
        {
            lHealthText.text = "Level Health: " + Mathf.RoundToInt(Overlord.levelCurrHealth);
        }
        else
        {
            timerText.text = "Time: " + Mathf.Round(Time.timeSinceLevelLoad);
            objText.text = "Objectives remaining: " + FindObjectsOfType<NABObjective>().LongLength;

            if (FindObjectsOfType<NABObjective>().LongLength == 0)
            {
                Overlord.Winning();
            }
        }
    }
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetLevelMaxHealth(int health)
    {
        if (!nabLevel)
        {
            levelSlider.maxValue = health;
        }
    }

    public void SetLevelHealth(float health)
    {
        if(!nabLevel)
        {
            levelSlider.value = health;
        }
    }

    public void OpenEndGame(string text, Color textColor)
    {
        isEnd = true;
        pauseCanvas.SetActive(false);
        endText.text = text;
        endText.color = textColor;


        Endgame.SetActive(true);

        MainMenu.SetActive(true);

        CPReplay.SetActive(true);

        PriorLevel.SetActive(true);

        NextLevel.SetActive(true);
    }


    public void Save()
    {
        opVlSt.Save();
    }

    public void ContinueTrue()
    {
        opVlSt.isContinue = true;
    }
    public void ContinueFalse()
    {
        opVlSt.isContinue = false;
    }

}
