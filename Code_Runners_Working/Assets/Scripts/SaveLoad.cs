using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public bool canContinue = false;

    OptionValueStore opVlSt = null;

    private void Awake()
    {

        opVlSt = GameObject.Find("OptionValueStore").GetComponent<OptionValueStore>();
        opVlSt.Load();
    }

    private void Start()
    {
        opVlSt.isContinue = false;

        if (opVlSt.sceneIndexNumber != 0)        
            canContinue = true;
        else
            canContinue = false;

        if (canContinue)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void ContinueTrue()
    {
        opVlSt.ContinueTrue();
    }
    public void ContinueOff()
    {
        opVlSt.ContinueOff();
    }
}
