using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static float BaseStressStrength = 10.0f;
    public static float BaseTorqueStrength = 10.0f;
    public static float StressStrengthIncrease = 5.0f;
    public static float TorqueStrengthIncrease = 5.0f;

    public static float Base

    public static float BaseMass = 1.0f;
    public static float MassStrengthIncrease = 0.1f;

    public static float MaxLength = 1.0f;

    public int Strength = 1;

    GameObject ParentSupport; // Stem or root object that is supporting this stem

    HingeJoint joint;

    //Stem creation process:
    //1. Stem instantiated; interaction system holds pointer to object.
    //2. Interaction system sets up new 

    void Start()
    {
        // The player adds a new stem. You are given a pointer to the parent.
        // Place the new joint at the 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnJointBreak(float breakForce)
    {
        //Tell the parent that the joint just broke
        //Set status to broken
        //
    }
}


//UI
//Toggle between free panning mode and building mode.
//