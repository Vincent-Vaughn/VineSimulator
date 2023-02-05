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
            if (SelectedNode != null)
            {
                plane.SetActive(true);
                ghost.SetActive(true);
            }
            else
            {
                //wHEN THE PLAYER CLICKS in build mode, check if they clicked on a node.
                if (Input.GetButtonDown("Fire1"))
                {
                    RaycastHit raycastHit;
                    Camera mainCamera = Camera.main;
                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out raycastHit))
                    {
                        if (raycastHit.transform.gameObject.GetComponent<StemScript>() != null)
                        {
                            
                        }
                    }
                }


                //Raycast when clicks happen. If they hit an EndNode, then set SelectedNode to this node.
            }
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