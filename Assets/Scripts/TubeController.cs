using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TubeController : MonoBehaviour
{
    public GameObject tubePrefab;
    public InputActionReference triggerAction = null;
    

    public bool triggerState = false;
    public bool isAttachedtoHand = false;
    public bool isAvailable = true;

    private Transform poison;


    private void Start()
    {
        
        //isAvailable = true;
        SetTubeVisibility(true);
    }

    private void Update()
    {      
        float triggerButtonValue = triggerAction.action.ReadValue<float>();

        ChangeTriggerState(triggerButtonValue == 1f);
    }

    public void SetTubeVisibility(bool isVisible)
    {
        tubePrefab.SetActive(isVisible);
    }

    public void DropTube()
    {
        tubePrefab.GetComponent<Collider>().enabled = false;
        tubePrefab.transform.parent = null;
        tubePrefab.GetComponent<Rigidbody>().isKinematic = false;
        tubePrefab.GetComponent<Rigidbody>().useGravity = true;
    }

    private void ChangeTriggerState(bool state)
    {
        if (triggerState != state)
        {
            triggerState = state;

            if (triggerState)     //when the trigger is pressed
            {
                if (GameManager.instance.isPlaying)
                {

                    if (!isAttachedtoHand && isAvailable)          //if there is no other(tube) in hand
                    { 
                        TubeAttachedtoHand();
                    }               
                }
            } 
        }
    }

    public void TubeAttachedtoHand()
    {
       
        tubePrefab.GetComponent<Rigidbody>().isKinematic = true;
        tubePrefab.GetComponent<Rigidbody>().useGravity = false;
        tubePrefab.transform.parent = transform;
        

        tubePrefab.transform.localPosition = Vector3.zero;
        tubePrefab.transform.localEulerAngles = Vector3.zero;

        SetTubeVisibility(true);
        tubePrefab.GetComponent<Collider>().enabled = true;
        poison = FindObjectOfType<PoisonTube>().transform;
        poison.localScale = Vector3.one;

        isAttachedtoHand = true;
    }

    public void TubePositionOnStart(Transform transform)
    {
        isAttachedtoHand = false;
        isAvailable = false;

        tubePrefab.transform.parent = transform;
        tubePrefab.transform.localPosition = Vector3.zero;
        tubePrefab.transform.localEulerAngles = new Vector3(90, 90, 0);
        
    }





}
