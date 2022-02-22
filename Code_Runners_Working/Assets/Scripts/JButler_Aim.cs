//////////////////////////////////////////////////
// Credits
// Base Credits: Trigonometry... seriously math
// Creator: Justin Butler
// Created: October 30, 2020
// Description:
// Used to track where the player is pointing.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JButler_Aim : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    [Header("DON'T TOUCH")]
    [Tooltip("If it ain't broke, don't fix it.\nReally this deals with how the cursor is seen on the world plane.")]
    [SerializeField] private float mZPos = 18.0f;

    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<Player_Control>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.timeScale > 0)
            RotatePlayer();
        //Debugging();
    }

    //////////////////////////////////////////////////
    // Mouse Methods
    //////////////////////////////////////////////////

    private Vector3 GetMousePos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZPos;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void RotatePlayer()
    {
        Vector3 pointA = this.gameObject.transform.position;
        Vector3 pointB = GetMousePos();
        Vector3 aim = (pointB - pointA).normalized;

        #region // Tried using Cos instead of Tan, but it didn't work.
        // Don't be dumb and set one of the points to (0,0), will come back as 0.
        // Could use Vector3.Dot instead (there are two methods to find the dot product).
        //float dot = Vector3.Dot(pointA, pointB);
        //float mag = (pointA.magnitude * pointB.magnitude);
        #endregion

        float rotate = Mathf.Atan2(aim.x, aim.z) * Mathf.Rad2Deg;

        this.gameObject.transform.eulerAngles = new Vector3(0f, rotate, 0f);

        #region // Debugging
        //Debug.Log("<color=lime>PointB position</color>: " + pointB);
        //Debug.Log("<color=blue>Dot Product</color> = " + dot);
        //Debug.Log("<color=cyan>PointA position</color>: " + pointA);
        //Debug.Log("<color=green>Magnitude</color> = " + mag);
        //Debug.Log("<color=red>Angle to rotate</color> = " + rotate);
        #endregion
    }

    //////////////////////////////////////////////////
    // Debugging
    //////////////////////////////////////////////////

    void Debugging()
    {
        Debug.Log(GetMousePos());
    }
}
