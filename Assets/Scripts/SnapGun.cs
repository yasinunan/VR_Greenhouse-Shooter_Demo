using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapGun : MonoBehaviour
{

    TubeController tubeController;
    
    public bool isAttachedtoGun = false;

    //private Transform tube = null;

    private Transform tube = null;


    private void Start()
    {
        tubeController = FindObjectOfType<TubeController>();

        tubeController.TubePositionOnStart(transform);
        isAttachedtoGun = true;

    }

    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "TubeTrigger")      //when the trigger on the gun collides with the tube
        {
            
            tube = other.transform;
            tubeController.isAttachedtoHand = false;     // detached from hand
            tubeController.isAvailable = false;

            //Snap Animasyonu eklenecek!!!
            if (!isAttachedtoGun)
            {
                tube.parent = transform;                   //tube's new parent is gun
                tube.localPosition = Vector3.zero;
                tube.localEulerAngles = new Vector3(90, 90, 0);
                isAttachedtoGun = true;
            }            
        }
    }

   
    

}
