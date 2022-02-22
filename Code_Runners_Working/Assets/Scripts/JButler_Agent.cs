//////////////////////////////////////////////////
// Credits
// Base Credits: Shawn Kendall, Justin Murphy;
//               FSGDN Game Development C202009 00
// Creator: Justin Butler
// Created: November 9, 2020
// Description:
// Responsible for all AI enemy behaviours and interactions with the player. This State Machine is setup 
// in such a way that it can be used for more things.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class JButler_Agent : CodeRunner.StateMachine.JButler_MachineBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    [Header("Object References")]
    [Tooltip("Keeps track of every Nav Point for this Agent.\nIF THE ROAMER WANTS A SET RANDOM PATH, YOU ARE ABLE TOO!")]
    [SerializeField] NavPoint[] myNavPoints = null;
    [Tooltip("Part that we pivot (very necessary!)")]
    [SerializeField] RotateHere aimHere = null;
    [Tooltip("The collider that turns on before an explosion. KEEP OFF!!!")]
    [SerializeField] GameObject explosionRadius = null;

    [Header("Enemy Types (DON'T CHANGE IN DEDICATED PREFABS!!!)")]
    [Tooltip("Moves only, doesn't chase or shoot.")]
    [SerializeField] bool hazard = false;
    [Tooltip("Stationary, shoots fast but low damage.")]
    [SerializeField] bool turret = false;
    [Tooltip("Moves around in a set path, chases players when nearby.")]
    [SerializeField] bool patroler = false;
    [Tooltip("Moves around randomly, shoots at player from a distance. Keep myNavPoints at 0 for full randomness.")]
    [SerializeField] bool roamer = false;
    [Tooltip("Will forever chase the player! When near enough, it will explode.")]
    [SerializeField] bool exploder = false;

    [Header("TURN OFF IF YOU WANT THE AGENT TO START OFF!!!")]
    [Tooltip("In order for managers to get necessary data, use this instead of the enable bool if you want an agent to start off.")]
    [SerializeField] bool agentActive = true;

    [Header("Enemy Type Specific")]
    [Tooltip("Used by: Roamer and Turret")]
    [SerializeField] float fireRate = 1.0f;
    [Tooltip("Used by: Turret")]
    [SerializeField] float rotateSpeed = 0.5f;
    [Tooltip("Used by: Exploder")]
    [SerializeField] float explosionTrigger = 1.25f;
    [Tooltip("Used by: Exploder")]
    [SerializeField] float detonationTimer = 1.5f;

    private int navIndex = 0;
    private int lastIndex = 0;
    private int victimIndex = 0;
    private float timer = 0.0f;
    private bool isPursuing = false;
    private bool isFiring = false;
    private bool isStuck = false;
    private bool isExploding = false;
    private Dictionary<GameObject, int> explodedOn = new Dictionary<GameObject, int>();
    private Vector3 oldPosition = Vector3.zero;

    private SoundManager sndmngr = null;
    private Player_Control player = null;

    //////////////////////////////////////////////////
    // OVERRIDE METHODS
    //////////////////////////////////////////////////

    public override void Awake()
    {
        base.Awake();
        NoRotate();
    }

    public override void Start()
    {
        base.Start();

        player = FindObjectOfType<Player_Control>();
        if (roamer && myNavPoints.Length == 0)
            myNavPoints = FindObjectsOfType<NavPoint>();
        lastIndex = myNavPoints.Length + 1;
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        ActiveAgents();
    }

    public override void AddStates()
    {
        AddState<PatrolState>();
        AddState<IdleState>();
        AddState<PursueState>();
        AddState<ShootingState>();
        AddState<RoamingState>();
        AddState<SentryState>();
        AddState<ExplodingState>();

        FindInitialState();
    }

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;
        PositionFrom2Secs();
        IsStuck();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
            RestartState();
    }

    //////////////////////////////////////////////////
    // Helper Methods (Override)
    //////////////////////////////////////////////////

    public void FindInitialState()
    {
        if (hazard || patroler || exploder)
            SetInitialState<PatrolState>();
        else if (roamer)
            SetInitialState<RoamingState>();
        else if (turret)
            SetInitialState<SentryState>();
    }

    public void NoRotate()
    {
        GetComponent<NavMeshAgent>().updateRotation = false;
    }

    public void ActiveAgents()
    {
        if (!agentActive)
            this.gameObject.SetActive(false);
    }

    //////////////////////////////////////////////////
    // Helper Methods (Direction)
    //////////////////////////////////////////////////

    public void PickNextNavPoint()
    {
        ++navIndex;
        if (navIndex >= myNavPoints.Length)
            navIndex = 0;
    }

    public void PickDirection()
    {
        PickNextNavPoint();
    }

    public void FindDirection()
    {
        if (navIndex == lastIndex && roamer)
        {
            RandomDirection();
            FindDirection();
        }
        else if (navIndex == lastIndex && (hazard || patroler || exploder))
        {
            PickDirection();
            FindDirection();
        }
        else
        {
            try
            {
            GetComponent<NavMeshAgent>().SetDestination(myNavPoints[navIndex].transform.position);
            lastIndex = navIndex;
            }
            catch (System.IndexOutOfRangeException e)
            {
                if (navIndex == 0)
                    throw new System.Exception(name + " is trying to call SetDestination, but there is no index!\tDo you have the correct enemy type selected?\tDid you forget to add NavPoints the the array?\n" + e.Message);
            }
        }
    }

    public void StopDestination()
    {
        GetComponent<NavMeshAgent>().SetDestination(GetComponent<NavMeshAgent>().transform.position);
    }

    public void RandomDirection()
    {
        navIndex = Random.Range(0, myNavPoints.Length);
        //Debug.Log("<color=cyan>NavIndex</color> = " + navIndex);
    }

    public void LookAtPoint()
    {
        Vector3 pointA = aimHere.transform.position;
        Vector3 pointB = myNavPoints[navIndex].transform.position;
        Vector3 aim = (pointB - pointA).normalized;

        float rotate = Mathf.Atan2(aim.x, aim.z) * Mathf.Rad2Deg;

        aimHere.gameObject.transform.eulerAngles = new Vector3(0f, rotate, 0f);
    }

    public float RotateSpeed()
    {
        return rotateSpeed;
    }

    public void LookAround(float rotateAmount)
    {
        aimHere.transform.Rotate(new Vector3(0f, rotateAmount, 0f), Space.Self);
    }

    //////////////////////////////////////////////////
    // Helper Methods (Player related)
    //////////////////////////////////////////////////

    public void FindPlayer()
    {
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
    }

    public void PursuePlayer()
    {
        if (patroler || exploder)
        {
            isPursuing = true;
            ChangeState<PursueState>();
        }
    }

    public void StopPursuing()
    {
        if (patroler || exploder)
        {
            isPursuing = false;
            ChangeState<PatrolState>();
        }
        else if (roamer)
        {
            isFiring = false;
            ChangeState<RoamingState>();
        }
        else if (turret)
        {
            isFiring = false;
            ChangeState<SentryState>();
        }
    }

    public void DemolitionInRange()
    {
        //Debug.Log(GetComponent<NavMeshAgent>().remainingDistance);
        if (GetComponent<NavMeshAgent>().remainingDistance <= explosionTrigger)
            ChangeState<ExplodingState>();
    }

    public bool IsExploding()
    {
        return isExploding;
    }

    public float DetonationTimer()
    {
        return detonationTimer;
    }

    public void FuseSound()
    {
        sndmngr.Play("fuse");
    }

    public void Detonate()
    {
        sndmngr.Play("Boom");
        isExploding = true;
        this.explosionRadius.SetActive(true);
    }

    public void FireFromADistance()
    {
        if (roamer || turret)
        {
            isFiring = true;
            ChangeState<ShootingState>();
        }
    }

    public void WhoFired()
    {
        GetComponentInChildren<Enemy>().WeFire();
    }

    public float FireRate()
    {
        return fireRate;
    }

    public void PLayChargeUpSound()
    {
        sndmngr.Play("chargingshot");
    }

    public void Aim()
    {
        Vector3 pointA = aimHere.transform.position;
        Vector3 pointB = player.transform.position;
        Vector3 aim = (pointB - pointA).normalized;
        
        float rotate = Mathf.Atan2(aim.x, aim.z) * Mathf.Rad2Deg;
        
        aimHere.gameObject.transform.eulerAngles = new Vector3(0f, rotate, 0f);
    }

    //////////////////////////////////////////////////
    // Helper Methods (AI related)
    //////////////////////////////////////////////////

    public bool FindRoamer()
    {
        if (patroler || hazard || turret || exploder)
            return true;
        else
            return false;
    }

    public bool FindTurret()
    {
        if (patroler || hazard || roamer || exploder)
            return true;
        else
            return false;
    }

    public bool FindPursuers()
    {
        if (hazard || roamer || turret)
            return true;
        else
            return false;
    }

    public bool Exploder()
    {
        return exploder;
    }

    public void PositionFrom2Secs()
    {
        if (timer >= 2.0)
        {
            oldPosition = transform.position;
            timer = 0.0f;
        }
    }

    public bool IsStuck()
    {
        if (oldPosition == transform.position && timer >= 1.55f)
            return isStuck = true;
        else
            return isStuck = false;
    }

    public void RestartState()
    {
        if (FindRoamer() && isStuck && !isPursuing && !isFiring && !isExploding && FindTurret())
        {
            ChangeState<PatrolState>();
            //Debug.Log("You were stuck");
        }
        else if (!FindRoamer() && isStuck && !isPursuing && !isFiring && !isExploding && FindTurret())
        {
            ChangeState<RoamingState>();
            //Debug.Log("You were stuck");
        }
    }

    private void AddVictim(Collider other)
    {
        explodedOn.Add(other.gameObject, victimIndex++);
    }

    private bool CheckVictims(Collider other)
    {
        foreach (var name in explodedOn)
        {
            if (name.Key.name == other.gameObject.name)
                return true;
            else
                continue;
        }
        return false;
    }

    //////////////////////////////////////////////////
    // Collision Event
    //////////////////////////////////////////////////

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NavPoint>() && !isPursuing && !isFiring)
            ChangeState<IdleState>();

        if (other.gameObject.GetComponent<Player_Control>())
            GetComponentInChildren<Enemy>().TriggerCheckMelee(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player_Control>())
            GetComponentInChildren<Enemy>().TriggerCheckMelee(other);

        if ((other.gameObject.GetComponent<Player_Control>() || other.GetComponentInChildren<Enemy>() || other.gameObject.GetComponent<FloorReference>()) && exploder && isExploding && !CheckVictims(other))
        {
            GetComponentInChildren<Enemy>().TriggerCheckExplosion(other);
            AddVictim(other);
        }
    }
}

//////////////////////////////////////////////////
// STATES
//////////////////////////////////////////////////

public class NavAgentState : CodeRunner.StateMachine.JButler_State
{
    protected JButler_Agent GetAgent()
    {
        return ((JButler_Agent)machine);
    }
}

public class PatrolState : NavAgentState
{
    public override void Enter()
    {
        base.Enter();
        GetAgent().PickDirection();
        GetAgent().FindDirection();
        GetAgent().LookAtPoint();
    }
}

public class IdleState : NavAgentState
{
    float timer = 0;

    public override void Enter()
    {
        base.Enter();
        timer = 0;
    }

    public override void Execute()
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f && GetAgent().FindRoamer())
        {
            machine.ChangeState<PatrolState>();
        }
        else if (timer >= 1.5f && !GetAgent().FindRoamer())
        {
            machine.ChangeState<RoamingState>();
        }
    }
}

public class PursueState : NavAgentState
{
    public override void Enter()
    {
        base.Enter();
        GetAgent().FindPlayer();
    }

    public override void Execute()
    {
        base.Execute();
        GetAgent().FindPlayer();
        if (GetAgent().Exploder())
            GetAgent().DemolitionInRange();
    }

    public override void PhysicsExecute()
    {
        base.PhysicsExecute();
        GetAgent().Aim();
    }
}

public class ShootingState : NavAgentState
{
    float timer = 0.0f;

    public override void Enter()
    {
        base.Enter();
        GetAgent().StopDestination();
        timer = 0.0f;
    }

    public override void Execute()
    {
        base.Execute();
        if(timer == 0.0f)
        {
            GetAgent().PLayChargeUpSound();
        }

        timer += Time.deltaTime;

        if (timer >= GetAgent().FireRate())
        {
            GetAgent().WhoFired();
            timer = 0.0f;
        }
        
    }

    public override void PhysicsExecute()
    {
        base.PhysicsExecute();
        GetAgent().Aim();
    }

    public override void Exit()
    {
        timer = 0.1f;
        base.Exit();
    }
}

public class RoamingState : NavAgentState
{
    public override void Enter()
    {
        base.Enter();
        GetAgent().RandomDirection();
        GetAgent().FindDirection();
        GetAgent().LookAtPoint();
    }
}

public class SentryState : NavAgentState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsExecute()
    {
        base.PhysicsExecute();

        GetAgent().LookAround(GetAgent().RotateSpeed());
    }
}

public class ExplodingState : NavAgentState
{
    float timer = 0.0f;

    public override void Enter()
    {
        base.Enter();
        GetAgent().StopDestination();
        timer = 0.0f;
        GetAgent().FuseSound();
    }

    public override void Execute()
    {
        base.Execute();
        timer += Time.deltaTime;
        if (timer >= GetAgent().DetonationTimer())
            GetAgent().Detonate();
    }
}
