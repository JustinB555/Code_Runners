/* 
   Editor: Jacob Arnold, Justin Butler
   Date of Creation: 10/29/2020
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public bool playEnd = false;
    Rigidbody rb;
    public CharacterController cc;
    [SerializeField]
    float moveSpeed = 0.1f;
    bool nodeEntrance = false;
    bool isMultiTrack = true;
    public Animation anim;
    int selectedNode = 0;
    Main_UI MUI;
    private JButler_Melee melee = null;
    private JButler_Shooting shooting = null;
    private JButler_Aim aim = null;
    public Player_Values pv = null;

    public bool isMelee = false;

    Vector2 dir;

    SphereCollider sCollider;

    SoundManager sndmngr = null;

    Pause pause = null;

    public bool mouseOnDebug = false;

    Overlord overlord = null;

    float horizontal;
    float vertical;

    bool selectionActive = false;
    public bool currentSelection = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        MUI = GameObject.Find("Main_UI").GetComponent<Main_UI>();
        melee = FindObjectOfType<JButler_Melee>();
        shooting = GameObject.Find("Player_EGO").GetComponent<JButler_Shooting>();
        pv = GameObject.Find("Player_EGO").GetComponent<Player_Values>();
        sCollider = GetComponent<SphereCollider>();
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        pause = GameObject.Find("Pause Canvas").GetComponent<Pause>();
        // Hot fix for the aim controls, still currently unsure what happened.
        aim = FindObjectOfType<JButler_Aim>();
        aim.enabled = false;
        aim.enabled = true;
        isMelee = false;
        overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
        anim = GetComponent<Animation>();
    }

    public void MeleeToggle()
    {
        isMelee = !isMelee;
    }

    public void PlayNoise()
    {
        if (overlord.isWin)
        {
            sndmngr.Play("Win");
            pause.PauseGame();
        }
        else
        {
            sndmngr.Play("Lose");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            anim.PlayQueued("Death", QueueMode.CompleteOthers);
        }

        playEnd = !playEnd;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal * moveSpeed, 0, vertical * moveSpeed);
    }

    void Update()
    {

        if(!playEnd)
        {
            if(MUI.isEnd)
                PlayNoise();
        }

        //Base Movement
        if(Time.timeScale > 0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            
        }

        //Button Controls
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Shooting attack
            if(Time.timeScale > 0 && anim.isPlaying == false)
            {
                if (!mouseOnDebug)
                {
                sndmngr.Play("Shoot");
                shooting.ShootProjectile();
                }
                else
                {
                    return;
                }
            }
        }


        // Melee Attack
        if (Input.GetKeyDown(KeyCode.Mouse1) && isMelee == false)
        {
            if(Time.timeScale > 0 && anim.isPlaying == false)
            {

                // Melee animation? //yes

                anim.PlayQueued("Melee", QueueMode.CompleteOthers);

                if (melee.NearEnemy())
                {
                    sndmngr.Play("MeleeHit");
                    melee.MeleeDamage();
                }
                else
                {
                    sndmngr.Play("Melee");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(anim.isPlaying == false)
            {
                if (Time.timeScale > 0)
                {
                    //interact
                    if (nodeEntrance)
                    {
                        sndmngr.Play("Node");
                        if (selectedNode == 1)
                        {
                            selectionActive = true;
                            anim.PlayQueued("Metrics01");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("Metrics01A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("Metrics01B", QueueMode.CompleteOthers);
                            }


                        }
                        else if (selectedNode == 2)
                        {
                            selectionActive = true;
                            anim.PlayQueued("Metrics02");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("Metrics02A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("Metrics02B", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 3)
                        {
                            selectionActive = true;
                            anim.PlayQueued("Metrics03");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("Metrics03A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("Metrics03B", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 4)
                        {
                            anim.PlayQueued("NAB01");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("NAB01A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("NAB01B", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 5)
                        {
                            anim.PlayQueued("NAB03");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("NAB01B", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("NAB02A", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 6)
                        {
                            anim.PlayQueued("NAB02");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("NAB02A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("NAB01A", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 7)
                        {
                            anim.PlayQueued("NAB04");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("NAB04A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("NAB04B", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 8)
                        {
                            anim.PlayQueued("NAB06");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("NAB05A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("NAB04A", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 9)
                        {
                            anim.PlayQueued("NAB05");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("NAB05A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("NAB04B", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 10)
                        {
                            anim.PlayQueued("NAB07");
                        }
                        else if (selectedNode == 11)
                        {
                            anim.PlayQueued("NAB08");
                        }
                        else if (selectedNode == 12)
                        {
                            anim.PlayQueued("LHAB01");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("LHAB01A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("LHAB03B", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 13)
                        {
                            anim.PlayQueued("LHAB02");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("LHAB01A", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("LHAB03A", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 14)
                        {
                            anim.PlayQueued("LHAB03");
                            if (currentSelection == false)
                            {
                                anim.PlayQueued("LHAB03B", QueueMode.CompleteOthers);
                            }
                            else if (currentSelection == true)
                            {
                                anim.PlayQueued("LHAB03A", QueueMode.CompleteOthers);
                            }
                        }
                        else if (selectedNode == 15)
                        {
                            anim.PlayQueued("LHAB04");
                        }
                        else if (selectedNode == 16)
                        {
                            anim.PlayQueued("LHAB05");
                        }
                        //Add more paths as necessary
                    }

                }
            }
        }

        if (nodeEntrance && isMultiTrack == true)
        {
            MUI.Selection.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                sndmngr.Play("ButtonSelect");
                currentSelection = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                sndmngr.Play("ButtonSelect");
                currentSelection = false;
            }
        }
        else
        {
            MUI.Selection.SetActive(false);
        }

        if (currentSelection)
        {
            MUI.rSelect.color = Color.green;
            MUI.lSelect.color = Color.grey;
        }
        else
        {
            MUI.rSelect.color = Color.gray;
            MUI.lSelect.color = Color.green;
        }
    }
    
    public void PlayerLost()
    {
        sndmngr.Play("enemyshatter");
        anim.PlayQueued("Death_idle", QueueMode.CompleteOthers);
        pause.PauseGame();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Node"))
        {
            selectedNode = other.GetComponent<Node_Indicator>().pathNumber;
            nodeEntrance = true;
            MUI.interactText.text = "Press 'E' to interact";
            if (other.GetComponent<Node_Indicator>().multiPath)
            {
                isMultiTrack = true;
            }
            else
            {
                isMultiTrack = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Node"))
        {
            nodeEntrance = false;
            selectedNode = 0;
            MUI.interactText.text = "";
        }
    }

    public void colliderDisable()
    {
        sCollider.enabled = false;
    }

    public void colliderEnable()
    {
        sCollider.enabled = true;
    }

    public void ResetSelections()
    {
        selectionActive = false;
        currentSelection = false;
    }

    /*public void NextNode()
    {
        if(selectedNode == 1 && currentSelection == false)
        {
            //anim.PlayQueued("Metrics01A", QueueMode.PlayNow);
            //anim.Play("Metrics01A");
        }
        else if (selectedNode == 1 && currentSelection == true)
        {
            anim.Play("Metrics01B");
        }
    }*/

    public void MouseOnDebugToggle()
    {
        mouseOnDebug = !mouseOnDebug;
    }
}
