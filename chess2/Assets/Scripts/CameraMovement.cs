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
            if (Input.GetMouseButton(0))
            {

                targetRot += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * rotationSensitivity;
                //targetRot += new Vector3(-Input.GetTouch(0).deltaPosition.y , Input.GetTouch(0).deltaPosition.x) * rotationSensitivity;

            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), 0.03f);
            
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(topRotation), 0.03f);
        }

        if (transform.rotation.x < 0)
        {
            //TODO: clamp so x always < 0
            //transform.rotation = Quaternion.Euler(new Vector3(0.1f, transform.rotation.eulerAngles.y));
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
