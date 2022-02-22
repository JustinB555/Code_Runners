/* 
   Editor: Jacob Arnold
   Date of Creation: 11/2/2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlord : MonoBehaviour
{

    public bool isWin = false;
    public bool isLevelInvince = false;

    public int maxEnemy;
    public float enemyCountF;
    public float enemyCountI;

    //public int maxObj;
    //public int currObj = 0;    -Remove with Objective Feature

    public int levelMaxHealth = 500;
    public float levelCurrHealth;
    Main_UI MUI = null;

    public bool playerAlive = true;

    [SerializeField]
    public float damageModifier = 0.10f;

    public int totalScore = 0;
    [SerializeField]
    int baseScore = 3;

    Pause pauseMenu = null;
    Scene_Manager scnmngr = null;

    void Start()
    {
        maxEnemy = FindObjectsOfType<Enemy>().Length;

        levelCurrHealth = levelMaxHealth;

        MUI = GameObject.Find("Main_UI").GetComponent<Main_UI>();

        MUI.SetLevelMaxHealth(levelMaxHealth);

        pauseMenu = GameObject.Find("Pause Canvas").GetComponent<Pause>();

        scnmngr = GameObject.Find("SceneManager").GetComponent<Scene_Manager>();

        isLevelInvince = false;

    }

    void Update()
    {
        enemyCountI = FindObjectsOfType<Enemy>().Length;

        enemyCountF = enemyCountI;

        if (enemyCountI == 0 && SceneManager.GetActiveScene().name != "01LevelHealthActionBlock")
        {
            Winning();
        }

        if (levelCurrHealth <= 0 || playerAlive == false)
        {
            if (SceneManager.GetActiveScene().name != "00NodeTransportActionBlock")
            {
                Losing();
            }
        }

        if (levelCurrHealth > levelMaxHealth)
        {
            levelCurrHealth = levelMaxHealth;
        }

        //Score Updates

        totalScore = baseScore * Mathf.RoundToInt(levelCurrHealth);
        if (SceneManager.GetActiveScene().name != "00NodeTransportActionBlock")
        {
            MUI.scoreText.text = totalScore.ToString();
        }
        else
        {
            MUI.scoreText.text = "";
        }

        /*if (Input.GetKeyDown(KeyCode.L))   Testing ONLY
        {
            ScoreEvent(2);
        }*/

    }

    //Level DOT
    private void FixedUpdate()
    {
        if (!isLevelInvince)
        {
            if (levelCurrHealth >= 0 && Time.timeScale > 0)
            {
                levelCurrHealth = levelCurrHealth -= (enemyCountF * damageModifier);

                MUI.SetLevelHealth(levelCurrHealth);
            }
        }
    }

    public void Winning()
    {
        isWin = true;
        MUI.OpenEndGame("You Win!", Color.green);
    }
    public void Losing()
    {
        isWin = false;
        MUI.OpenEndGame("You Lose!", Color.red);
    }


    /*public void CompleteObjective()
    {
        currObj++;                      -Remove with Objective Feature
    }
    */

    public void LevelDamage(int damage)
    {
        if (!isLevelInvince)
        {
            levelCurrHealth -= damage;

            if (levelCurrHealth <= 0)
                levelCurrHealth = 0;
        }
    }

    public void ScoreEvent(int score)
    {
        baseScore += score;
    }
}
