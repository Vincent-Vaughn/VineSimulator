using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootScript : MonoBehaviour
{
    public float NutrientProduction = 1.0f;

    // Start is called before the first frame update
    public static float BaseStressStrength = 10.0f;
    public static float BaseTorqueStrength = 10.0f;

    // 
    public static float StressStrengthIncrease = 5.0f;
    public static float TorqueStrengthIncrease = 5.0f;

    // Mass = (Base Mass + Mass Strength Increase * Strength Level) * Length
    public static float BaseMass = 1.0f; // Per meter
    public static float MassStrengthIncrease = 0.1f; // Per meter

    // Energy Cost = Growth Energy Cost * Length
    public static float GrowthEnergyCost = 1.0f; // Per meter
    public static float GrowthNutrientCost = 1.0f; // Per meter

    // 
    public static float MaxLength = 1.0f;
    public static float MinLength = 0.25f;

    public int StrengthLevel = 1;

    // Strengthen Energy Cost = StrengthenEnergyCost * Length
    // SImilar for nutrient cost.
    public static float StrengthenEnergyCost = 1.0f; // Per meter
    public static float StrengthenNutrientCost = 1.0f; // Per meter

    public GameObject Parent; // Stem or root object that is supporting this stem
    public List<GameObject> Children = new List<GameObject>(); // All other things branching off of this.
    public HingeJoint Joint; // Joint connecting this to the parent

    public List<GameObject> Flowers = new List<GameObject>();
    public List<GameObject> Leaves = new List<GameObject>();

    public GameObject BaseRoot = null;

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
        SendMessageUpwards("ProcessBrokenStem", gameObject);
        //Tell the parent that the joint just broke
        //Set status to broken
        //
    }

    public void AttachJoint()
    {
        //Find the end of the parent stem/root; this is the back end of this stem.
        //You are passed a vector that is the axis of the stem ontop of the parent in world coordinates.
    }
}


//UI
//Toggle between free panning mode and building mode.
//