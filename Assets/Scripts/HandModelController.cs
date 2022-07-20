using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandModelController : MonoBehaviour
{
    private Transform defaultParentObject;
    private Transform myTransform;

    void Start () {
        myTransform = transform;
        defaultParentObject = myTransform.parent;
    }

    public void SetParentObject (Transform parentObject) {
        myTransform.SetParent(parentObject);
    }

    public void SetDefaultParentObject () {
        myTransform.SetParent(defaultParentObject);
    }
}
