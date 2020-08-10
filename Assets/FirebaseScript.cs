using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;


using System.Collections;
using System.Collections.Generic;
using UnityEditor.Connect;
using UnityEngine;

public class FirebaseScript : MonoBehaviour
{

    DatabaseReference reference;
    FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    // Start is called before the first frame update
    void Start()
    {



        // #1
        
        
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)

            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        // #2
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://durakcard-2a29c.firebaseio.com/");


        // #3
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        auth= Firebase.Auth.FirebaseAuth.DefaultInstance;

        //string DebugMsg = saveDataToFirebase(002, "Asal Alghamdi ", "Jeddah");
        //Debug.Log(DebugMsg);

        //_________________________




        //auth.CreateUserWithEmailAndPasswordAsync("fhgfghf2@gmail.com", "fgghfdsffshgfghf").ContinueWith(task => {
        //    // print message in console
        //    if (task.IsCanceled)
        //    {
        //        Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
        //        return;
        //    }
        //    if (task.IsFaulted)
        //    {
        //        Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
        //        return;
        //    }



        //    // Firebase user has been created.
        //    Firebase.Auth.FirebaseUser newUser = task.Result;
        //    Debug.LogFormat("Firebase user created successfully: {0} ({1})",
        //        newUser.DisplayName, newUser.UserId);
        //});
        InitializeFirebase();

        auth.SignInWithEmailAndPasswordAsync("fhgfghf2@gmail.com", "fgghfdsffshgfghf").ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            Firebase.Auth.FirebaseUser newUser = task.Result;
            //Debug.LogFormat("User signed in successfully: {0} ({1})",
            //    newUser.DisplayName, newUser.UserId);
        });
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
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                auth.SignOut();
                //displayName = user.DisplayName ?? "";
                //emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";
            }
        }
    }

    public string saveDataToFirebase(int id, string name, string city) // نفرض اننا حابين نعمل قاعدة بيانات للموظفين كل موظف عنده بيانات مثل ID, Name, City
    {

        reference.Child(id.ToString()).Child("Name").SetValueAsync(name);
        reference.Child(id.ToString()).Child("City").SetValueAsync(city);
        return "Save data to firebase Done.";
    }

}
