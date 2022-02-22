/*
 * Editor: Wyatt Curtiss
 * Date of Creation: October 7, 2020
 * Notes: I did this originally for MIlestone 1 but for some reason thought it'd be easier to do it with SceneManager. I hate myself.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField]
    GameObject mainCanvas = null;
    [SerializeField]
    GameObject creditsCanvas = null;

    [SerializeField]
    Text playText = null;

    OptionValueStore opVlSt = null;

    public bool creditsActive = false;

    private void Start()
    {
        creditsActive = false;
        opVlSt = GameObject.Find("OptionValueStore").GetComponent<OptionValueStore>();

        if(opVlSt.sceneIndexNumber > 0)
        {
            playText.text = "NEW GAME";
        }
        else
        {
            playText.text = "PLAY";
        }
    }

    public void Creditstoggle()
    {
        creditsActive = !creditsActive;

        if (creditsActive)
        {
            mainCanvas.SetActive(false);
            creditsCanvas.SetActive(true);
        }
        else if (!creditsActive)
        {
            creditsCanvas.SetActive(false);
            mainCanvas.SetActive(true);
        }
    }

}
