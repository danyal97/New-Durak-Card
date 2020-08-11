using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class Login : MonoBehaviour
{
    public Text email;
    public Text password;
    public InputField pass;
    FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    // Start is called before the first frame update
    public void onSubmit() {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Debug.Log("Email : " + email.text + " Password : " + pass.text);

        auth.SignInWithEmailAndPasswordAsync(email.text, pass.text).ContinueWith(task => {
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
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });


    }
}
