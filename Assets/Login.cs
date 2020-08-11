using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Text email;
    public Text password;
    public InputField pass;
    // Start is called before the first frame update
    public void onSubmit() {

        Debug.Log("Email" + email.text +" Password"+pass.text);
    }
}
