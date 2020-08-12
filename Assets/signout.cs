using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using System.Reflection;

public class signout : MonoBehaviour
{
    FirebaseAuth auth;
    // Start is called before the first frame update
    public void Signout()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Debug.Log("Signout Called");
        auth.SignOut();
        //auth.StateChanged -= AuthStateChanged;
        SceneManager.LoadScene("StartScene");

    }
}
