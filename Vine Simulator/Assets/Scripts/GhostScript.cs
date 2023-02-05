using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public GameObject player;
    public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl cc = player.GetComponent<CameraControl>();

        if (cc.SelectedNode != null)
        {
            RaycastHit raycastHit;
            Camera mainCamera = Camera.main;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, 3))
            {
                transform.position = cc.SelectedNode.transform.position;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, (raycastHit.point - transform.position).normalized);
                
            }
        }
    }
}
