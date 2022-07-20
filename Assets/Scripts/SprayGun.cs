using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class SprayGun : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public GameObject trigger;
    public GameObject tubeTrigger;

    public InputActionReference triggerAction = null;

    private float scaleFactor = 0.05f;
    private Transform poison;


    [HideInInspector]
    public bool isFired = false;
    private bool triggerState = false;
    

    SnapGun snapGun;

    //public AudioSource audioSource;
    //public AudioClip audioClip;


    private void Start()
    {
        snapGun = FindObjectOfType<SnapGun>();
    }
    private void Update()
    {
        if (snapGun.isAttachedtoGun)
        {
            poison = FindObjectOfType<PoisonTube>().transform;
        }

        float triggerButtonValue = triggerAction.action.ReadValue<float>();
        
        ChangeTriggerState(triggerButtonValue == 1f);
    }

    private void ChangeTriggerState(bool state)
    {
        if (triggerState != state)
        {
            triggerState = state;

            if (triggerState)
            {
                if (GameManager.instance.isPlaying)
                {
                    if (snapGun.isAttachedtoGun)
                    {
                        Fire();
                        ScalePoisonTube();
                        isFired = true;
                    }
                }

                TriggerActiveAnimation();
            }
            else
            {
                TriggerDeactiveAnimation();
                isFired = false;
            }
        }
    }
    public void Fire()
    {
        if (poison !=null && poison.transform.localScale.z>0)
        {
            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;

            Destroy(spawnedBullet, 1f);

            RaycastHit hit;
            if (Physics.SphereCast(barrel.position, .08f , barrel.forward, out hit , 1000))
            {
                GameObject shotBug = hit.transform.gameObject;

                BugController bugController = shotBug.GetComponent<BugController>();

                if (bugController != null)
                {
                    
                    if (bugController.GetBugData().isHarmful == false || !bugController.GetIsShot())
                    {
                        GameManager.instance.SetScore(bugController.GetBugData().score, bugController.GetBugData().isHarmful);
                        GameManager.instance.ScoreUI(shotBug, bugController.GetBugData().isHarmful, bugController.GetBugData().score);
                        bugController.HitColor();
                    }
                    
                }
            }
        }
        //audioSource.PlayOneShot(audioClip);

    }

    

    public void TriggerActiveAnimation()   //Pulls the trigger.
    {
        
        Quaternion onTriggedRotation = Quaternion.Euler(30, 0, 0);
        trigger.transform.localRotation = Quaternion.Slerp(trigger.transform.rotation, onTriggedRotation,5);       
    }
    public void TriggerDeactiveAnimation()  //Releases the trigger.
    {
        
        Quaternion onTriggedRotation = Quaternion.Euler(0, 0, 0);
        trigger.transform.localRotation = Quaternion.Slerp(trigger.transform.rotation, onTriggedRotation, 5);        
    }

    public void ScalePoisonTube()
    {
        if (poison != null)
        {
            Vector3 scaleVector = poison.transform.localScale;

            if (snapGun.isAttachedtoGun)
            {
                if (scaleVector.z > 0)
                {
                    scaleVector -= new Vector3(0, 0, scaleFactor);
                    poison.transform.localScale = scaleVector;

                }
            }
        }
    }
}
