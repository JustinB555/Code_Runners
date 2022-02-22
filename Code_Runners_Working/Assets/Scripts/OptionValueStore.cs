/*
 * Editor: Wyatt Curtiss
 * Date of Creation: October 7, 2020
 * Notes: To store the valeus for the options and sound volume
 *        Save Data serialization is from this Unity Livestream: https://www.youtube.com/watch?v=J6FfcJpbPXE
*/

using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class OptionValueStore : MonoBehaviour
{
    public int sceneIndexNumber = 0;
    public int playerHealth = 0;
    public int checkpointValue = 0;

    public bool isContinue = false;

    OptionsScript options;
    [SerializeField]
    public float musicStore = 1f;
    [SerializeField]
    public float sfxStore = 1f;

    Player_Values plVl = null;
    DebugScript debug = null;
    private static OptionValueStore instance;

    public int buildIndexNumber = 0;
    private void Awake()
    {
        //Load();

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ContinueTrue()
    {
        isContinue = true;
        //Debug.Log("isContinue is true");
    }

    public void ContinueOff()
    {
        isContinue = false;
        //Debug.Log("isCOntinue is false");
    }

    private void Start()
    {
        options = GameObject.Find("OPTIONS CANVAS").GetComponent<OptionsScript>();
        //Debug.Log("current scene: " + scnmngr.sceneIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("player health is: " + playerHealth);
            Debug.Log("current checkpoint value is: " + checkpointValue);
            Debug.Log("Current scene index number is: " + sceneIndexNumber);
        }
    }

    public void SwapValues()
    {
        musicStore = options.musicValue;
        sfxStore = options.sfxValue;
    }

    public void CheckpointData()
    {
        plVl = GameObject.Find("Player_EGO").GetComponent<Player_Values>();
        debug = GameObject.Find("DEBUG").GetComponent<DebugScript>();

        sceneIndexNumber = SceneManager.GetActiveScene().buildIndex;
        playerHealth = plVl.currHealth;
        checkpointValue = debug.currentCheckpoint;
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //creates a file in C:// user roaming data. Opens the files to save stuff.
        FileStream file = File.Create(Application.persistentDataPath + "/FS202011_Undesirables.dat");
        Debug.Log("Save data folder created in: " + Application.persistentDataPath + System.Environment.NewLine + "The folder is called: /FS202011_Undesirables.dat");

        PlayerData data = new PlayerData();
        data.playerHealth = playerHealth;
        data.sceneIndexNumber = sceneIndexNumber;
        data.checkpointValue = checkpointValue;
        data.musicStore = musicStore;
        data.sfxStore = sfxStore;

        //Debug.Log(playerHealth);
        //Debug.Log(sceneIndexNumber);
        //Debug.Log(checkpointValue);

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/FS202011_Undesirables.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/FS202011_Undesirables.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerHealth = data.playerHealth;
            sceneIndexNumber = data.sceneIndexNumber;
            checkpointValue = data.checkpointValue;
            musicStore = data.musicStore;
            sfxStore = data.sfxStore;
        }
    }
}

[Serializable]
class PlayerData
{
    public int playerHealth;
    public int sceneIndexNumber;
    public int checkpointValue;
    public float musicStore;
    public float sfxStore;
}
