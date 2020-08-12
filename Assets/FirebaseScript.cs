using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;


using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Connect;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseScript : MonoBehaviour
{

    DatabaseReference reference;
    FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    
    public static bool firebaseReady;
    // Start is called before the first frame update
    void Start()
    {


        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Firebase is ready for use.");
                firebaseReady = true;
            }
            else
            {
                firebaseReady = false;
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

     void Update()
    {
        if(firebaseReady)
        {

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://durakcard-2a29c.firebaseio.com/");
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            //auth.SignOut();
            InitializeFirebase();
        }
        
        
    }
    void InitializeFirebase()
    {
        
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {

        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {

                Debug.Log("Not Signed in " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("already Signed in " + user.UserId);
                //SceneManager.LoadScene("SampleScene");
                SceneManager.LoadScene("MenuScene");


                //auth.SignOut();
                //displayName = user.DisplayName ?? "";
                //emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";
            }
        }
    }

    public  void CheckIfReady()
    {

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            Firebase.DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {

                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                firebaseReady = true;

                Debug.Log("Firebase is ready for use.");

               

                // Create and hold a reference to your FirebaseApp, i.e.
                //   app = Firebase.FirebaseApp.DefaultInstance;
                // where app is a Firebase.FirebaseApp property of your application class.

                // Set a flag here indicating that Firebase is ready to use by your
                // application.
            }
            else
            {
                firebaseReady = false;
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }


}
