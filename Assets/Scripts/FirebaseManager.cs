using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
using TMPro;
using System;

public class FirebaseManager : MonoBehaviour
{
    public DatabaseReference usersRef;
   
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public Text username;
    public Text phone;
    public Text TC;
    public Text userType;
    public Text score;
    public Text _try;
    public Text userID;

    private float scoreToInt;

    private static FirebaseManager _instance;
    public static FirebaseManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FirebaseManager>();

            }
            return _instance;
        }
    }

    void Start()
    {
        StartCoroutine(Initilization());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator Initilization()
    {
        var task = FirebaseApp.CheckAndFixDependenciesAsync();
        while (!task.IsCompleted)
        {
            yield return null;
        }

        if(task.IsCanceled || task.IsFaulted)
        {
            Debug.Log("Database Error:" + task.Exception);

        }

        var dependencyStatus = task.Result;

        if (dependencyStatus == DependencyStatus.Available)
        {
            usersRef = FirebaseDatabase.DefaultInstance.GetReference("Users");
            Debug.Log("Init completed");
        }
        else
        {
            Debug.Log("Database Error:");

        }
    }

    public void SaveUser()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        Dictionary<string, object> user = new Dictionary<string, object>();
        user["username"] = username;
        user["password"] = password;

        //string key = usersRef.Push().Key;

        string key = username;
        usersRef.Child(key).UpdateChildrenAsync(user);


    }

    public void GetData()
    {
        StartCoroutine(GetUserData());
        
    }

    public void PrintUserData()
    {
        StartCoroutine(PrintData());

    }

    public IEnumerator GetUserData()
    {
        string name = usernameInput.text;
        var task = usersRef.Child(name).GetValueAsync();

        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.Log("Database Error:" + task.Exception);
            yield break;
        }
            DataSnapshot snapshot = task.Result;

            foreach (DataSnapshot user in snapshot.Children)
            {
                if (user.Key == "Score")
                {
                    //usersRef.Child(name).Child("Score").SetValueAsync(GameManager.instance.totalScore);
                    // Debug.Log(user.Value.ToString());

                    scoreToInt = Convert.ToInt32(user.Value);
                }
            

               if (user.Key == "Try" ) 
               {
                int i =  Convert.ToInt32(user.Value);

                if (i < 2)
                {
                    if(GameManager.instance.totalScore > scoreToInt)
                    {
                        usersRef.Child(name).Child("Score").SetValueAsync(GameManager.instance.totalScore);
                    }

                    i+=1; 
                    usersRef.Child(name).Child("Try").SetValueAsync(i);       
                }
                else
                {
                    usersRef.Child(name).Child("Score").SetValueAsync(GameManager.instance.totalScore);
                    i += 1;
                    usersRef.Child(name).Child("Try").SetValueAsync(i);
                }


               }
            }   
        }

    public IEnumerator PrintData()
    {
        string name1 = usernameInput.text;
        var task = usersRef.Child(name1).GetValueAsync();

        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.Log("Database Error:" + task.Exception);
            yield break;
        }
        DataSnapshot snapshot = task.Result;

        foreach (DataSnapshot user in snapshot.Children)
        {
            if (user.Key == "Username")
            {
                username.text = user.Value.ToString();
            }
            if (user.Key == "UserType")
            {
                userType.text = user.Value.ToString();
            }
            if (user.Key == "Phone")
            {
                phone.text = user.Value.ToString();
            }
            if (user.Key == "Score")
            {
                score.text = user.Value.ToString();
            }
            if (user.Key == "TC")
            {
                TC.text = user.Value.ToString();
            }
            if (user.Key == "Username")
            {
                username.text = user.Value.ToString();
            }
            if (user.Key == "Try")
            {
                _try.text = user.Value.ToString();
            }


        }

        userID.text = name1;
    }

}


