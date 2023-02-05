using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public GameObject player;
    public float ScrollSensitivity = 1.0f;
    private float vertShift = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = (transform.localScale / 10) * StemScript.MaxLength;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        CameraControl cc = player.GetComponent<CameraControl>();

        if (cc.SelectedNode != null)
        {
            Vector3 nodePos = cc.SelectedNode.transform.position;

            vertShift += Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;
            vertShift = Mathf.Min(vertShift, StemScript.MaxLength);
            vertShift = Mathf.Max(vertShift, -StemScript.MaxLength);

            transform.position = nodePos + vertShift * Vector3.up;
        }
        //Scroll wheel
        //Get maximum displacement and node position
        //
    }
}
