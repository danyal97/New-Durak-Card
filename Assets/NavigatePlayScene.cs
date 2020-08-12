using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NavigatePlayScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void NavigateToPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
