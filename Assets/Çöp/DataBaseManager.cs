using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class DataBaseManager : MonoBehaviour
{
    public DatabaseReference usersRef;

    private void Start()
    {
        StartCoroutine(Initilization());
    }

    IEnumerator Initilization()
    {
        var task = FirebaseApp.CheckAndFixDependenciesAsync();
        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError("Database Error: " + task.Exception);
        }

        var dependencyStatus = task.Result;

        if (dependencyStatus == DependencyStatus.Available)
        {
            usersRef = FirebaseDatabase.DefaultInstance.GetReference("Users");
            Debug.Log("init completed");
        }

        else
        {
            Debug.LogError("Database Error: " );
        }
    }  
}








