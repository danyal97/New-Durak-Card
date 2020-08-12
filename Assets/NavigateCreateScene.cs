using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateCreateScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void navigate_create_User()
    {
        SceneManager.LoadScene("CreateScene");
    }
}
