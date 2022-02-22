/*
 * Editor: Wyatt Curtiss
 * Date of Creation: 11/7/2020
 * Notes: Bruh
*/

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{

    [SerializeField]
    GameObject optionsCanvas = null;
    [SerializeField]
    GameObject mainCanavas = null;
    [SerializeField]
    GameObject pauseCanvas = null;

    //public static OptionsScript instance;

    public bool optionsActive = false;
    private string currScene = null;

    [SerializeField]
    Slider musicSlide = null;
    [SerializeField]
    Slider sfxSlide = null;

    SoundManager soundManager;
    OptionValueStore optionValue;

    public float musicValue = 1f;
    public float sfxValue = 1f;

    private void Awake()
    {

        //    if (instance == null)
        //        instance = this;
        //    else
        //    {
        //        Destroy(gameObject);
        //        return;
        //    }

        //    DontDestroyOnLoad(gameObject);


    }


    // Start is called before the first frame update
    void Start()
    {
        optionValue = GameObject.Find("OptionValueStore").GetComponent<OptionValueStore>();



        musicValue = optionValue.musicStore;
        sfxValue = optionValue.sfxStore;
        musicSlide.value = optionValue.musicStore;
        sfxSlide.value = optionValue.sfxStore;


        soundManager = GameObject.Find("AudioManager").GetComponent<SoundManager>();

        optionsActive = false;

        SetOptions();

    }

    void SetOptions()
    {
        for (int i = 0; i < soundManager.audioSources.Length; i++)
        {
            if (i == 0 || i == 1)
                soundManager.audioSources[i].volume = optionValue.musicStore;
            else
                soundManager.audioSources[i].volume = optionValue.sfxStore;

        }
    }

    public void OptionsToggle()
    {
        optionsActive = !optionsActive;
        currScene = SceneManager.GetActiveScene().name;

        if (optionsActive && currScene == "MainMenu")
        {
            mainCanavas.SetActive(false);
            optionsCanvas.SetActive(true);
        }
        else if (!optionsActive && currScene == "MainMenu")
        {
            optionsCanvas.SetActive(false);
            mainCanavas.SetActive(true);
        }

        else if (optionsActive && currScene != "MainMenu")
        {
            pauseCanvas.SetActive(false);
            optionsCanvas.SetActive(true);
        }
        else if (!optionsActive && currScene != "MainMenu")
        {
            optionsCanvas.SetActive(false);
            pauseCanvas.SetActive(true);
        }
    }

    public void MusicChange()
    {

        musicValue = musicSlide.value;
        optionValue.musicStore = musicValue;

        soundManager.audioSources[0].volume = musicSlide.value;
        soundManager.audioSources[1].volume = musicSlide.value;

        //musicSlide.value;
    }

    public void SFXChange()
    {

        sfxValue = sfxSlide.value;
        optionValue.sfxStore = sfxValue;

        for (int i = 2; i < soundManager.audioSources.Length; i++)
        {
            soundManager.audioSources[i].volume = sfxSlide.value;
        }
    }
}
