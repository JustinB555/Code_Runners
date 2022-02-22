using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{


    public Scene currScene;
    public int sceneIndex;
    OptionValueStore opVlSt = null;



    public void Start()
    {
        currScene = SceneManager.GetActiveScene();
        sceneIndex = currScene.buildIndex;
        //Debug.Log(sceneIndex);
        opVlSt = GameObject.Find("OptionValueStore").GetComponent<OptionValueStore>();

    }

    //SCENE MANAGEMENT

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Add more scenes below

    public void Play()
    {
        StartCoroutine(PlayBuffer());
    }

    //public void Credits()
    //{
    //    SceneManager.LoadScene("Credits");
    //}

    public void Continue()
    {
        SceneManager.LoadScene(opVlSt.sceneIndexNumber);
    }

    public void Replay()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void NextLevel()
    {
        if (currScene.buildIndex >= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }

    public void PriorLevel()
    {
        SceneManager.LoadScene(sceneIndex - 1);
    }

    public void Quit()
    {
        opVlSt.Save();
        StartCoroutine(ExitBuffer());
    }


    IEnumerator PlayBuffer()
    {
        yield return new WaitForSeconds(.4f);
        SceneManager.LoadScene(sceneIndex + 1);
    }

    IEnumerator ExitBuffer()
    {
        yield return new WaitForSeconds(.4f);
        //save game data here if we ever decide to do that
        //Replace with just Application.Quit() when making a build
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
