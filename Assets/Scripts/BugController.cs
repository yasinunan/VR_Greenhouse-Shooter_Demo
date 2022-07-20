using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{

    Rigidbody rb;

    private float movementSpeed;
    private float rotationSpeed;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isRotatingDown = false;
    private bool isRotatingUp = false;

    private MeshRenderer meshRenderer;
    private Color mainColor;

    
    
    


    //private bool isWalking = false;


    private BugData bugData;
    private bool isShot = false;

    
    
    private void Update()
    {
        if (bugData.movementType == MovementType.Fly)
        {
            if (!isWandering)
            {
                StartCoroutine(Wander());

            }
            if (isRotatingRight)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
                transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
            }
            if (isRotatingLeft)
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
                transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
            }
            if (isRotatingDown)
            {
                transform.Rotate(transform.right * Time.deltaTime * -rotationSpeed);
                transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
            }
            if (isRotatingUp)
            {
                transform.Rotate(transform.right * Time.deltaTime * -rotationSpeed);
                transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
            }
        }
        
        

        //if (isWalking )
        //{
        //    transform.Translate(transform.forward * movementSpeed*Time.deltaTime);
        //    transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);

        //}
    }

    

    IEnumerator Wander()
    {

        float rotationTime = Random.Range(0.3f, 0.5f);
        float rotateWait = Random.Range(0.3f, 0.5f);
        int rotateDirection = Random.Range(1, 4);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 3);

        isWandering = true;

        //yield return new WaitForSeconds(walkTime);

        //isWalking = true;

        //yield return new WaitForSeconds(walkTime);

        //isWalking = false;

        //yield return new WaitForSeconds(rotateWait);

        if (rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }
        if (rotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }
        if (rotateDirection == 3)
        {
            isRotatingDown = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingDown = false;
        }
        if (rotateDirection == 3)
        {
            isRotatingUp = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingUp = false;
        }

        isWandering = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    private void Jump(bool isRight)
    {
        if (bugData.movementType == MovementType.Jump)
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.freezeRotation = false;
            if (isRight)
            {
                rb.AddForce(new Vector3(-200, 300, 0));
            }
            else
            {
                rb.AddForce(new Vector3(200, 300, 0));
            }
            
        }
    }
    public void Initialize(BugData _bugData , bool isRight)
    {
        bugData = _bugData;
        GameObject newBug = Instantiate(bugData.prefab,transform.position,Quaternion.identity, transform);
        meshRenderer = newBug.GetComponent<MeshRenderer>();
        rotationSpeed = bugData.rotationSpeed;
        movementSpeed = bugData.movementSpeed;
        mainColor = meshRenderer.material.color;
        Jump(isRight);

    }
    public bool GetIsShot()
    {
        return isShot;
    }

    public BugData GetBugData()
    {
        return bugData;
    }

    public void HitColor()
    {
        if (bugData.isHarmful)
        {
            StartCoroutine(HitColorChange(mainColor, Color.green));
        }
        else
        {
            StartCoroutine(HitColorChange(mainColor, Color.red));
        }
    }

    IEnumerator HitColorChange(Color mainColor, Color color)
    {
        isShot = true;
        if (gameObject != null)
        {
            if (bugData.isHarmful)
            {
                GameObject killedEffect= Instantiate(GameManager.instance.killedEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            meshRenderer.material.color = color;
            yield return new WaitForSeconds(.5f);
            isShot = false;
            meshRenderer.material.color = mainColor;

            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bugData.movementType == MovementType.Jump)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }

        }
    }


}
