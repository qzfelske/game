using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{

    private bool isTopView = false;

    private Vector3 origin = new Vector3(0, 0, 0);
    private Vector3 topRotation = new Vector3(90, 0, 0);

    private Vector3 lastRot;
    private Vector3 targetRot;

    public float rotationSensitivity;
   
    void Start()
    {
        
    }

    void Update()
    {

        if (!isTopView)
        {

            //for computer
            if (!Input.touchSupported && Input.GetMouseButton(0))
            {
                targetRot += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * rotationSensitivity;
            }

            //for mobile       REMOVE COMMENTS FOR BUILD
            if (/*Input.touchSupported &&*/ Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                targetRot += new Vector3(-Input.GetTouch(0).deltaPosition.y, Input.GetTouch(0).deltaPosition.x) * rotationSensitivity;
            }

            if (targetRot.x < 0)
            {
                targetRot.x += 0 - targetRot.x;
            } 
            else if (targetRot.x > 90)
            {
                targetRot.x += 90 - targetRot.x;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), 0.03f);
            
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(topRotation), 0.03f);
        }
        
        transform.position = origin - (5 * transform.forward);
    }

    public void toggleView()
    {
        if (!isTopView)
        {
            isTopView = true;
        } 
        else
        { 
            isTopView = false;
        }
    }
}
