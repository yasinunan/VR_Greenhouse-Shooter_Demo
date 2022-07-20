using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PoisonTube : MonoBehaviour
{
    
    SnapGun snapGun;
    TubeController tubeController;

    

    public InputActionReference gripAction = null;
    public bool gripState = false;

    

    private void Start()
    {

        //snapGun = FindObjectOfType<SnapGun>();
        tubeController = FindObjectOfType<TubeController>();
        //Debug.Log(snapGun.gameObject.name);

    }

    
    
    private void Update()
    {
        float gripButtonValue = gripAction.action.ReadValue<float>();

        ChangeTriggerState(gripButtonValue == 1f);        
    }

    private void ChangeTriggerState(bool state)
    {
        if (gripState != state)
        {
            gripState = state;

            if (gripState)
            {
                if (GameManager.instance.isPlaying)
                {
                    snapGun = FindObjectOfType<SnapGun>();
                    DestroyEmptyTube();
                }
            }
        }
    }

    public void DestroyEmptyTube()
    {
        if (transform.localScale.z <= 0)
        {
            snapGun.isAttachedtoGun = false;
            tubeController.isAvailable = true;

            tubeController.DropTube();
        }
    }
}
