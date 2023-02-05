using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineScript : MonoBehaviour
{
    public float Energy = 10.0f;
    public float EnergyProduction = 1.0f;
    public float Nutrients = 10.0f;
    public float NutrientProduction = 1.0f;

    // Must contain parent object and children
    List<GameObject> Stems;
    List<GameObject> Roots;
    List<GameObject> Leaves;
    List<GameObject> Flowers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Process user input for adding plant parts

        //1. Check if the user has entered Build Mode for a specific part
        //2. Wait for the user to click on one of your end nodes.
        //3. When the user clicks on an end node, place a ghost of the model to be added.
        //4. When the user clicks again, add the part after checking if it's possible.

        //Fuck it leaves and flowers go anywhere
    }

    private void FixedUpdate()
    {
        EnergyProduction = 0;
        NutrientProduction = 0;

        foreach(GameObject stem in Stems)
        {
            EnergyProduction += stem.GetComponent<StemScript>().EnergyProduction;
            NutrientProduction += stem.GetComponent<StemScript>().NutrientProduction;
        }

        Energy += Time.deltaTime * EnergyProduction;
        Nutrients += Time.deltaTime * NutrientProduction;
    }

    void ProcessBrokenStem(GameObject brokenStem)
    {
        void AlignTree(GameObject currentBase, GameObject previousBase, GameObject originalBase)
        {
            //Check if the current object is a Root or a Stem.
            //Compile a list of all attachd objects. Remove the object that we just came from.
            //If the list is empty, orient this object such that it's parent is previousBase. Return.
            //If the list is not empty, call this function on each member.
            //Check if previousBase is null. If it isn't, orient this member such that its parent is previousBase and all other attached objects are children.

            List<GameObject> attached = new List<GameObject>();

            attached.AddRange(currentBase.GetComponent<StemScript>().Children);
            attached.Add(currentBase.GetComponent<StemScript>().Parent);

            attached.Remove(previousBase);

            foreach (GameObject otherObject in attached)
            {
                AlignTree(otherObject, currentBase, originalBase);
            }

            currentBase.GetComponent<StemScript>().Parent = previousBase;

            currentBase.GetComponent<StemScript>().Children = attached;

            currentBase.GetComponent<StemScript>().BaseRoot = originalBase;

            return;
        }

        // 1. Delete the child stem, and set any references to it as a parent in the Stems and Roots arrays to Null.
        for (int i = 0; i < Stems.Count; i++)
        {
            Stems[i].GetComponent<StemScript>().BaseRoot = null;

            if (Stems[i].GetComponent<StemScript>().Parent == brokenStem)
            {
                Stems[i].GetComponent<StemScript>().Parent = null;
            }
        }

        Stems.Remove(brokenStem);
        GameObject.Destroy(brokenStem);

        List<GameObject> baseRoots = new List<GameObject>();

        for (int i = 0; i < Roots.Count; i++)
        {
            Roots[i].GetComponent<RootScript>().BaseRoot = null;
        }

        for (int i = 0; i < Roots.Count; i++)
        {
            if (Roots[i].GetComponent<RootScript>().BaseRoot == null)
            {
                AlignTree(Roots[i], null, Roots[i]);
                baseRoots.Add(Roots[i]);
            }
            
        }

        if (baseRoots.Count > 1)
        {
            List<GameObject> DeleteObjs = new List<GameObject>();
            for (int i = 0; i < Stems.Count; i++)
            {
                if (Stems[i].GetComponent<StemScript>().BaseRoot != baseRoots[i])
                {
                    DeleteObjs.Add(Stems[i]);
                }
            }
        }

        for (int i = 0; i < Stems.Count; i++)
        {
            //Disable all stems with no BaseRoot
        }
    }
}
