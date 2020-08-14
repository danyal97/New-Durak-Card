using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NavigatePlayScene : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void NavigateToPlayScene()
    {
        FirebaseScript addPlayerToGame = new FirebaseScript();
        addPlayerToGame.AddToGame();

        SceneManager.LoadScene("PlayScene");
    }
}
