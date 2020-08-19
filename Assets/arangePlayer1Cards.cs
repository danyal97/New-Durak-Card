using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arangePlayer1Cards : MonoBehaviour
{
    [SerializeField]
    GameObject gb;

    public void onArrange() {
        gb.gameObject.GetComponent<addCards>().arangecards();
    }
}
