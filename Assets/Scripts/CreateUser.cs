using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class CreateUser : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Text password;
    public InputField email;
    public InputField pass;
    public Text validation;
    FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    public void Create_User() {

        validation.text = "";
        validateInput();


        if (validation.text=="")
        {
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            Debug.Log("Email : " + email.text + " Password : " + pass.text);

            auth.CreateUserWithEmailAndPasswordAsync(email.text, pass.text).ContinueWith(task => {
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
    void validateInput()
    {
        if(email.text=="")
        {
            validation.text = "Please Enter Valid Email";
        }
        else if(!email.text.Contains("@"))
        {
            validation.text = "Please Enter Valid Email example@gmail.com";
        }
        else if(password.text.Length < 6)
        {
            validation.text = "Please Enter Valid Password And Charachters Length should be Greater Than 6";
        }
    }
   
}
