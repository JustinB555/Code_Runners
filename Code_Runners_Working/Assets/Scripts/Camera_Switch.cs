/*
 * Editor: Wyatt Curtiss
 * Date of Creation: October 30, 2020
*/

using UnityEngine;

public class Camera_Switch : MonoBehaviour
{
    [SerializeField]
    GameObject PrimaryCamera = null;
    [SerializeField]
    GameObject SecondaryCamera = null;

    public bool PrimaryActive = false;

    // Start is called before the first frame update
    void Start()
    {
        PrimaryActive = true;
        PrimaryCamera.SetActive(true);
        SecondaryCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                PrimaryActive = !PrimaryActive;
                Switch();
            }
        }
    }

    public void Switch()
    {
        if (PrimaryActive)
        {
            PrimaryCamera.SetActive(true);
            SecondaryCamera.SetActive(false);
        }
        else
        {
            PrimaryCamera.SetActive(false);
            SecondaryCamera.SetActive(true);
        }
    }
}
