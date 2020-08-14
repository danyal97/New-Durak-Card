using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class Login : MonoBehaviour
{

    public InputField email;
    public InputField pass;
    public string PreviousEmail;
    public string PreviousPassword;
    FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    public Text validation;
    // Start is called before the first frame update
    public void onSubmit() {



        //validateInput();

        //if (validation.text == "")
        //{
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            //Debug.Log("Email : " + email.text + " Password : " + pass.text);

            auth.SignInWithEmailAndPasswordAsync(email.text, pass.text).ContinueWith(task =>
            {
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

        //}
    }
    void validateInput()
    {
        if (email.text == "")
        {
            validation.text = "Please Enter Valid Email And Password Length \nshould be Greater Than 6";
        }
        if (!email.text.Contains("@"))
        {
            validation.text = "Please Enter Valid Email And Password Length \nshould be Greater Than 6";
        }
        if (pass.text.Length < 6)
        {
            validation.text = "Please Enter Valid Email And Password Length \nshould be Greater Than 6";
        }
    }

}
