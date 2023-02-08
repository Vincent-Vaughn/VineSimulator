using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed = 0.5f;
    public float LookSpeed = 15.0f;
    public bool BuildMode = false;
    public GameObject SelectedNode = null;
    public GameObject plane;
    public GameObject ghost;
    public GameObject stem;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            BuildMode = !BuildMode;
        }

        if (BuildMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (SelectedNode == null)
            {
            //wHEN THE PLAYER CLICKS in build mode, check if they clicked on a node.
                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log('f');
                    RaycastHit raycastHit;
                    Camera mainCamera = Camera.main;
                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, LayerMask.GetMask("EndNode")))
                    {
                        if (raycastHit.collider.transform.gameObject.GetComponent<EndNode>() != null)
                        {
                            Debug.Log("Hit EndNode");
                            SelectedNode = raycastHit.collider.transform.gameObject;
                            plane.SetActive(true);
                            ghost.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Hit " + raycastHit.collider.transform.gameObject.name);
                            SelectedNode = null;
                            plane.SetActive(false);
                            ghost.SetActive(false);
                        }
                    }
                    else
                    {
                        Debug.Log("Hit nothing");
                        SelectedNode = null;
                        plane.SetActive(false);
                        ghost.SetActive(false);
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    RaycastHit raycastHit;
                    Camera mainCamera = Camera.main;
                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                    
                    if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, LayerMask.GetMask("Plane")))
                    {
                        Debug.Log("Adding stem");
                        //Create new stem and deselect this node
                        GameObject newStem = Instantiate(stem, SelectedNode.transform.position, ghost.transform.rotation);

                        newStem.GetComponent<StemScript>().Attach(SelectedNode.transform.parent.gameObject, ghost.GetComponent<GhostScript>().GetEndpointPos() - SelectedNode.transform.position, ghost.GetComponent<GhostScript>().makeRoot);
                    }

                    SelectedNode = null;
                    plane.SetActive(false);
                    ghost.SetActive(false);
                }
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            plane.SetActive(false);
            ghost.SetActive(false);

            SelectedNode = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!BuildMode)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * LookSpeed, transform.localEulerAngles.y + Input.GetAxis("Mouse X") * LookSpeed, 0.0f);
            transform.position = (transform.position + transform.rotation * (Input.GetAxis("Horizontal") * Vector3.right * Speed) + transform.rotation * (Input.GetAxis("Vertical") * Vector3.forward * Speed));
            transform.position = transform.position + (Input.GetAxis("Jump") - Input.GetAxis("Shift")) * Speed * Vector3.up;
        }

    }
}