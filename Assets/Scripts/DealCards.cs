using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealCards : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    public Button button;
    
    public void onDeal() {
        gameManager.isDistrbuting = true;
        button.gameObject.SetActive(false);

    }
}
