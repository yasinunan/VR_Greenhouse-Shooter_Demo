using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    public TMP_InputField usernameInput;



    
    void Update()
    {
        
    }

    public void Add_1()
    {
        usernameInput.text += "1";
    }
    public void Add_2()
    {
        usernameInput.text += "2";
    }
    public void Add_3()
    {
        usernameInput.text += "3";
    }
    public void Add_4()
    {
        usernameInput.text += "4";
    }
    public void Add_5()
    {
        usernameInput.text += "5";
    }
    public void Add_6()
    {
        usernameInput.text += "6";
    }
    public void Add_7()
    {
        usernameInput.text += "7";
    }
    public void Add_8()
    {
        usernameInput.text += "8";
    }
    public void Add_9()
    {
        usernameInput.text += "9";
    }
    public void Add_0()
    {
        usernameInput.text += "0";
    }
    public void Delete()
    {
        if (usernameInput.text.Length > 0)
        {
            usernameInput.text = usernameInput.text.Substring(0, usernameInput.text.Length - 1);
        }
    }

}
