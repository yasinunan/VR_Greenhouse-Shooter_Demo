using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantRotate : MonoBehaviour
{

    // Start is called before the first frame update

    [ExecuteInEditMode]
    private void Awake()
    {
        float randomDegree = Random.Range(0f, 360f);
        transform.Rotate(Vector3.up * randomDegree, Space.World);
    }

   


}
