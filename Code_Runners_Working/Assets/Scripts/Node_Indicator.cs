/* 
   Editor: Jacob Arnold
   Date of Creation: 10/31/2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Indicator : MonoBehaviour
{
    public int pathNumber = 0;

    bool isactive = false;
    bool peak = false;
    float change = 0;
    [SerializeField]
    float speed = 0.02f;

    public bool multiPath = true;

    Color selfColor;

    [SerializeField]
    GameObject[] pathways;
    [SerializeField]
    GameObject[] pathA;
    [SerializeField]
    GameObject[] pathB;

    Player_Control pc;
    void Start()
    {
        selfColor = GetComponent<Renderer>().material.color;
        pc = GameObject.Find("Player_EGO").GetComponent<Player_Control>();
    }

    void FixedUpdate()
    {
        if (peak == false && isactive == true)
        {
            change += speed;
            selfColor = Color.Lerp(Color.yellow, Color.grey, change);
            //GetComponent<Renderer>().material.color = selfColor;

            for (int i = 0; i <= pathways.Length - 1; i++)
            {
                pathways[i].GetComponent<Renderer>().material.color = selfColor;
                if (change >= 1)
                {
                    peak = true;
                }
            }

            if (pc.currentSelection)
            {
                for (int i = 0; i <= pathA.Length - 1; i++)
                {
                    pathA[i].GetComponent<Renderer>().material.color = selfColor;
                    if (change >= 1)
                    {
                        peak = true;
                    }
                }

                for (int t = 0; t <= pathB.Length - 1; t++)
                {
                    pathB[t].GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
            else
            {
                for (int i = 0; i <= pathB.Length - 1; i++)
                {
                    pathB[i].GetComponent<Renderer>().material.color = selfColor;
                    if (change >= 1)
                    {
                        peak = true;
                    }
                }

                for (int t = 0; t <= pathA.Length - 1; t++)
                {
                    pathA[t].GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
        }
        else if (peak = true && isactive == true)
        {
            change -= speed;
            selfColor = Color.Lerp(Color.yellow, Color.grey, change);
            //GetComponent<Renderer>().material.color = selfColor;

            for (int i = 0; i <= pathways.Length -1; i++)
            {
                pathways[i].GetComponent<Renderer>().material.color = selfColor;
                if (change <= 0)
                {
                    peak = false;
                }
            }

            if (pc.currentSelection)
            {
                for (int i = 0; i <= pathA.Length - 1; i++)
                {
                    pathA[i].GetComponent<Renderer>().material.color = selfColor;
                    if (change >= 1)
                    {
                        peak = true;
                    }
                }

                for (int t = 0; t <= pathB.Length - 1; t++)
                {
                    pathB[t].GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
            else
            {
                for (int i = 0; i <= pathB.Length - 1; i++)
                {
                    pathB[i].GetComponent<Renderer>().material.color = selfColor;
                    if (change >= 1)
                    {
                        peak = true;
                    }
                }

                for (int t = 0; t <= pathA.Length - 1; t++)
                {
                    pathA[t].GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player_Control>())
        {
            isactive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player_Control>())
        {
            //Debug.Log("Left trigger");
            isactive = false;
            ResetAll();
        }
    }

    private void ResetAll()
    {
        for (int i = 0; i <= pathways.Length - 1; i++)
        {
            pathways[i].GetComponent<Renderer>().material.color = Color.yellow;
            change = 0;
            peak = false;
        }

        for (int i = 0; i <= pathA.Length - 1; i++)
        {
            pathA[i].GetComponent<Renderer>().material.color = Color.yellow;
            change = 0;
            peak = false;
        }

        for (int i = 0; i <= pathB.Length - 1; i++)
        {
            pathB[i].GetComponent<Renderer>().material.color = Color.yellow;
            change = 0;
            peak = false;
        }
    }
}
