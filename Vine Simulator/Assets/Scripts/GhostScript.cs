using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public GameObject player;
    public GameObject plane;

    public bool makeRoot = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        CameraControl cc = player.GetComponent<CameraControl>();
        //ransform.localScale = transform.localScale * StemScript.MaxLength;
        Debug.Log(cc.SelectedNode.transform.position);
        transform.position = cc.SelectedNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl cc = player.GetComponent<CameraControl>();
        transform.position = cc.SelectedNode.transform.position;
        
        RaycastHit raycastHit;
        Camera mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, LayerMask.GetMask("Plane")))
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, (raycastHit.point - transform.position).normalized);
            transform.localScale = new Vector3(1,Mathf.Min((raycastHit.point - cc.SelectedNode.transform.position).magnitude, StemScript.MaxLength),1);

            ray = new Ray(transform.position, transform.rotation * Vector3.up);

            if (Physics.SphereCast(ray, 0.15f, out raycastHit, transform.localScale.y, LayerMask.GetMask("Default")))
            {
                //Debug.DrawLine(transform.position, transform.position + transform.rotation * (Vector3.up * transform.localScale.y), Color.red, 0.1f);
                //Debug.Log("Root");
                
                makeRoot = true;
            }
            else
            {
                makeRoot = false;
            }
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public Vector3 GetEndpointPos()
    {
        return transform.GetChild(0).position;
        //return transform.position + transform.rotation * (Vector3.up * transform.localScale.y);
    }
    
}
