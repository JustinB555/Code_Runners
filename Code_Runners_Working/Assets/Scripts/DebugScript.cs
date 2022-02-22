/*
 * Editor: Wyatt Curtiss
 * Date of Creation: November 8, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    bool isToggled = false;
    bool mouseOnDebug = false;
    bool isImmortal = false;
    [SerializeField]
    bool showCollider = false;
    bool isPacketApplied = false;
    [SerializeField]
    bool isNavShowing = false;
    int plyrDmgValue = 15;
    int plyrHealValue = 15;
    int nmeMaxHealth = 0;
    //int lvlHealValue = 0;
    [SerializeField]
    GameObject debugMenu = null;
    [SerializeField]
    Text dmgPlyrInput = null;
    [SerializeField]
    Text healPlyrInput = null;
    [SerializeField]
    Text immortalToggleText = null;
    [SerializeField]
    Text colliderToggleText = null;
    [SerializeField]
    Text lvlInvinceToggleText = null;
    [SerializeField]
    Text showNavPointsToggleText = null;
    [SerializeField]
    Text healLvlInput = null;
    [SerializeField]
    Text dmgLvlInput = null;
    [SerializeField]
    Dropdown effectPicker = null;

    Scene_Manager scnMngr = null;
    [SerializeField]
    Player_Values plyrVl = null;
    OptionValueStore opVlSt = null;
    [SerializeField]
    JButler_AgentManager AgeMngr = null;
    [SerializeField]
    JButler_EnemySpawner NMESpawner = null;
    [SerializeField]
    Overlord overlord = null;
    [SerializeField]
    ShieldPickUp shield = null;
    [SerializeField]
    FireHazard fire = null;

    [SerializeField]
    GameObject player = null;

    [SerializeField]
    GameObject[] enemies = null;
    [SerializeField]
    GameObject[] checkpoints = null;

    [SerializeField]
    Dropdown ckpntPicker = null;
    public int selectedCkpt = 0;

    public int enemCount = 0;

    public int currentCheckpoint = 0;

    public int currentScene = 0;

    int savedDamage = 0;

    string on = "ON";
    string off = "OFF";
    private void Start()
    {
        debugMenu.SetActive(false);
        scnMngr = GameObject.Find("SceneManager").GetComponent<Scene_Manager>();
        opVlSt = GameObject.Find("OptionValueStore").GetComponent<OptionValueStore>();


        isImmortal = plyrVl.isImmortal;
        if (isImmortal)
            immortalToggleText.text = on;
        else
            immortalToggleText.text = off;

        ShowColliders();
        if (showCollider)
            colliderToggleText.text = off;
        else
            colliderToggleText.text = on;

        ShowNavPoints();
        if (isNavShowing)
            showNavPointsToggleText.text = off;
        else
            showNavPointsToggleText.text = on;

        overlord.isLevelInvince = false;
        if (overlord.isLevelInvince)
            lvlInvinceToggleText.text = on;
        else
            lvlInvinceToggleText.text = off;

        enemCount = enemies.Length;
        selectedCkpt = 0;

        currentScene = SceneManager.GetActiveScene().buildIndex;

        int maxHealth = plyrVl.maxHealth;

        if (opVlSt.isContinue && currentScene == opVlSt.sceneIndexNumber)
        {
            player.GetComponent<Transform>().position = checkpoints[opVlSt.checkpointValue].GetComponent<Transform>().position;
            savedDamage = maxHealth - opVlSt.playerHealth;
            plyrVl.TakeDamage(savedDamage);
        }
        else
            return;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) && Time.timeScale > 0)
        {
            mouseOnDebug = GameObject.Find("Player_EGO").GetComponent<Player_Control>().mouseOnDebug;

            if (!mouseOnDebug)
            {
                DebugToggle();
            }
            else
            {
                Debug.LogWarning("Take mouse off of the Debug Menu to close");
                return;
            }
        }
    }

    public void DebugToggle()
    {
        isToggled = !isToggled;

        if (isToggled)
        {
            debugMenu.SetActive(true);
        }
        else
        {
            debugMenu.SetActive(false);
        }
    }

    public void UpdateCheckpoint()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].GetComponent<Checkpoint>().playerIsInside == true)
            {
                currentCheckpoint = i;
                return;
            }
        }
    }

    public void ApplyEffect()
    {
        if (shield.isActive || fire.firA)
            isPacketApplied = true;
        else
            isPacketApplied = false;

        if (isPacketApplied)
        {
            if (effectPicker.value == 0)
            {
                fire.firA = false;
                shield.ShieldActivate();
                fire.Flames();
            }
            else if (effectPicker.value == 1)
            {
                shield.waitTime = 0.0f;
                fire.FireEffect();
            }
        }
        else
        {
            if (effectPicker.value == 0)
            {
                shield.ShieldActivate();
            }
            else if (effectPicker.value == 1)
            {
                shield.isActive = false;
                fire.FireEffect();
            }
        }
    }

    public void IMMORTAL()
    {
        isImmortal = !isImmortal;
        if (isImmortal)
        {
            plyrVl.isImmortal = true;
            immortalToggleText.text = on;
        }
        else
        {
            plyrVl.isImmortal = false;
            immortalToggleText.text = off;
        }
    }
    public void DMGPLAYER()
    {
        plyrDmgValue = int.Parse(dmgPlyrInput.text);
        plyrVl.TakeDamage(plyrDmgValue);
        //Debug.Log(plyrDmgValue);
    }
    public void DMGLEVEL()
    {
        overlord.levelCurrHealth -= float.Parse(dmgLvlInput.text);
    }
    public void HealPlayer()
    {
        plyrHealValue = int.Parse(healPlyrInput.text);
        plyrVl.TakeDamage(plyrHealValue * -1);
    }
    public void HEALLEVEL()
    {
        overlord.levelCurrHealth += float.Parse(healLvlInput.text);
    }

    public void KILLPLAYER()
    {
        plyrVl.TakeDamage(plyrVl.maxHealth);
    }
    public void KILLALLENEMIES()
    {
        nmeMaxHealth = enemies[0].GetComponent<Player_Values>().maxHealth;

        for (int i = 0; i < enemies.Length; i++)
        {
            //e.GetComponent<Player_Values>().TakeDamage(nmeMaxHealth);
            enemies[i].GetComponentInChildren<Enemy>().EnemyDamaged(nmeMaxHealth);
        }
    }
    public void CHCKPNTUpdate()
    {
        selectedCkpt = ckpntPicker.value;
        //Debug.Log(selectedCkpt);
    }
    public void TPCHECKPOINT()
    {

        player.GetComponent<Transform>().position = checkpoints[selectedCkpt].GetComponent<Transform>().position;


        //new Vector3(checkpoints[selectedCkpt].GetComponent<Transform>().position.x, checkpoints[selectedCkpt].GetComponent<Transform>().position.y, checkpoints[selectedCkpt].GetComponent<Transform>().position.z);
        //these ended up being the same, but the player was never moved.
        //Debug.Log("Player position: " + player.GetComponent<Transform>().position);
        //Debug.Log("Checkpoint " + selectedCkpt + " position: " + checkpoints[selectedCkpt].GetComponent<Transform>().position);


    }
    public void ShowColliders()
    {
        try
        {
            if (showCollider)
            {
                NMESpawner.SpawnCollider(showCollider);
                showCollider = !showCollider;
                colliderToggleText.text = on;
            }
            else
            {
                NMESpawner.SpawnCollider(showCollider);
                showCollider = !showCollider;
                colliderToggleText.text = off;
            }
        }
        catch (System.NullReferenceException e)
        {
            colliderToggleText.text = off;
            throw new System.Exception("There are no Enemy Spawners in this scene.\tIf you wanted a spawner, make sure to add one.\n" + e.Message);
        }
    }

    public void LevelImmortal()
    {
        if (overlord.isLevelInvince)
        {
            overlord.isLevelInvince = false;
            lvlInvinceToggleText.text = off;
        }
        else
        {
            overlord.isLevelInvince = true;
            lvlInvinceToggleText.text = on;
        }
    }

    public void ShowNavPoints()
    {
        if (isNavShowing)
        {
            AgeMngr.ShowNavPointsToggle(isNavShowing);
            isNavShowing = !isNavShowing;
            showNavPointsToggleText.text = on;
        }
        else
        {
            AgeMngr.ShowNavPointsToggle(isNavShowing);
            isNavShowing = !isNavShowing;
            showNavPointsToggleText.text = off;
        }
    }
    public void RESETLVL()
    {
        scnMngr.Replay();
    }
}
