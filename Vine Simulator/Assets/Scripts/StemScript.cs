using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemScript : MonoBehaviour
{
    public float EnergyProduction = 0;
    public float NutrientProduction = 0;

    public float BaseNutrientProduction = 1.0f;
    public float BaseEnergyProduction = 1.0f;

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
    public float Length = 0.5f;
    
    public int StrengthLevel = 1;

    // Strengthen Energy Cost = StrengthenEnergyCost * Length
    // SImilar for nutrient cost.
    public float StrengthenEnergyCost = 1.0f; // Per meter
    public float StrengthenNutrientCost = 1.0f; // Per meter

    public GameObject Parent; // Stem or root object that is supporting this stem
    public List<GameObject> Children = new List<GameObject>(); // All other things branching off of this.
    public CharacterJoint Joint; // Joint connecting this to the parent

    public GameObject BaseRoot = null;
    public GameObject node;
    public GameObject PlayerCamera;

    public bool isRoot = false;
    // If you are a root, enable nutrient production, disable collision detection with terrain, fix position in space.

    //Stem creation process:
    //1. Stem instantiated; interaction system holds pointer to object.
    //2. Interaction system sets up new 
    
    void Start()
    {
        GetComponent<Rigidbody>().mass = BaseMass * Length;

    }

    // Update is called once per frame
    void Update()
    {
        CameraControl player = PlayerCamera.GetComponent<CameraControl>();

        if (player.BuildMode == true)
        {
            if ((player.SelectedNode == null) || (player.SelectedNode == node))
            {
                node.SetActive(true);
            }
            else
            {
                node.SetActive(false);
            }

        }
        else
        {
            node.SetActive(false);
        }
    }

    private void OnJointBreak(float breakForce)
    {
        SendMessageUpwards("ProcessBrokenStem", gameObject);
        //Tell the parent that the joint just broke
        //Set status to broken
        //
    }

    public void AttachJoint(GameObject parentObj, Vector3 span)
    {
        Joint = gameObject.GetComponent<CharacterJoint>();
        Parent = parentObj;
        Joint.connectedBody = parentObj.GetComponent<Rigidbody>();
        Joint.connectedAnchor = parentObj.GetComponentInChildren<EndNode>().transform.position;
        Joint.anchor = Vector3.zero;
        
        //gameObject.GetComponent<HingeJoint>().
        //Find the end of the parent stem/root; this is the back end of this stem.
        //You are passed a vector that is the axis of the stem ontop of the parent in world coordinates.
        //Assume that you are already positioned in the way that VineScript wants you.
    }

    public void SetRoot(bool root)
    {
        if (root)
        {
            NutrientProduction = BaseEnergyProduction;

        }
        else
        {
            NutrientProduction = 0;
        }
    }
}


//UI
//Toggle between free panning mode and building mode.
//