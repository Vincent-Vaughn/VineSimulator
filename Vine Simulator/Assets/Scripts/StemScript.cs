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
    public static float BaseMass = 0.25f; // Per meter
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

    public GameObject Parent = null; // Stem or root object that is supporting this stem
    public List<GameObject> Children = new List<GameObject>(); // All other things branching off of this.
    private CharacterJoint Joint; // Joint connecting this to the parent

    public GameObject BaseRoot = null;
    public GameObject node = null;
    public GameObject PlayerCamera = null;
    public GameObject Vine = null;

    public Material StemMaterial;
    public Material RootMaterial;

    public bool isRoot = false;

    private Vector3 tensor = Vector3.zero;

    public float BaseWidth = 0.125f;
    // If you are a root, enable nutrient production, disable collision detection with terrain, fix position in space.

    //Stem creation process:
    //1. Stem instantiated; interaction system holds pointer to object.
    //2. Interaction system sets up new 
    
    void Start()
    {
        PlayerCamera = Camera.main.gameObject;
        Joint = GetComponent<CharacterJoint>();
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
        Parent.GetComponent<StemScript>().NotifyJointBreak(gameObject);

        foreach (GameObject ch in Children)
        {
            ch.GetComponent<StemScript>().NotifyJointBreak(gameObject);
        }
    }

    private void NotifyJointBreak(GameObject notifier)
    {
        if (isRoot)
        {
            
        }
        else
        {
            //If I am not a root, and the joint is not between me and a root, then delete it.
        }
    }

    public void Attach(GameObject parentObj, Vector3 span, bool root)
    {
        //
        //transform.localScale = new Vector3(1,span.magnitude,1);
        //Debug.Log(transform.localScale);

        transform.GetChild(0).localScale = new Vector3(BaseWidth, span.magnitude / 2, BaseWidth);
        transform.GetChild(0).localPosition = new Vector3(0, span.magnitude / 2, 0);
        transform.GetChild(1).localPosition = new Vector3(0,span.magnitude,0);

        parentObj.GetComponent<StemScript>().Children.Add(gameObject);
        Parent = parentObj;

        Joint = gameObject.GetComponent<CharacterJoint>();
        Joint.connectedBody = parentObj.GetComponent<Rigidbody>();
        Joint.connectedAnchor = parentObj.GetComponentInChildren<EndNode>().transform.localPosition;
        Joint.anchor = Vector3.zero;
        Vine = parentObj.GetComponent<StemScript>().Vine;
        Vine.GetComponent<VineScript>().Stems.Add(gameObject);
        transform.SetParent(Vine.transform);
        
        if (root)
        {
            NutrientProduction = BaseEnergyProduction;
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = RootMaterial;
        }
        else
        {
            NutrientProduction = 0;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = StemMaterial;
        }

        GetComponent<Rigidbody>().mass = span.magnitude * BaseMass;
        Vector3 currentTensor = GetComponent<Rigidbody>().inertiaTensor;
        GetComponent<Rigidbody>().inertiaTensor = currentTensor;
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, span.magnitude / 2, 0);
    }
}


//UI
//Toggle between free panning mode and building mode.
//