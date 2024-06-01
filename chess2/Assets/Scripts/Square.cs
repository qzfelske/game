using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{

    private bool hovering = false;
    private bool selected = false;

    private Renderer rend;

    private int fresnelPowerID;
    private int emissivePowerID;

    [SerializeField] private float floatHeight = 0.2f;
    private Vector3 startingPos;
    private Vector3 floatingPos;
    private Vector3 vel;

    private Vector2 startTouchPos;
    private Vector2 touchLength;

    private void Start()
    {
        rend = GetComponent<Renderer>();

        fresnelPowerID = Shader.PropertyToID("_Fresnel_power");
        emissivePowerID = Shader.PropertyToID("_Emission_power");

        startingPos = transform.position;
        floatingPos = transform.position + (transform.up * floatHeight);
    }

    void Update()
    {

            //click starts
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Input.mousePosition;
        } //click ends
        else if (Input.GetMouseButtonUp(0))
        { //TODO sometimes this breaks cause you swipe to where you started
            touchLength = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startTouchPos;
        }

        if (Input.GetMouseButtonUp(0) && hovering && touchLength.magnitude < Screen.width * .05)
        {
            if (!selected) { selected = true; }
            else { selected = false; }
        } else if (Input.GetMouseButtonUp(0) && !hovering && touchLength.magnitude < Screen.width * .05)
        {
            selected = false;
        }

        if (selected)
        {
            transform.position = Vector3.SmoothDamp(transform.position, floatingPos, ref vel, 0.1f);
            rend.material.SetFloat(emissivePowerID, 1);
        } 
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, startingPos, ref vel, 0.1f);
            rend.material.SetFloat(emissivePowerID, 0);
        }
    }

    private void OnMouseEnter()
    {
        hovering = true;
        rend.material.SetFloat(fresnelPowerID, 2); //TODO change fresnel to something else
    }

    private void OnMouseExit()
    {
        hovering = false;
        rend.material.SetFloat(fresnelPowerID, 1000);
    }

//    private void OnMouseUpAsButton()
//    {
//        if (Input.touchSupported && touchLength.magnitude < Screen.width * .05)
//        {
//            if (!selected) { selected = true; }
//            else { selected = false; }
//        }
//    }
}
