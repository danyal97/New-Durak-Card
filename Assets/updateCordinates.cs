using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateCordinates : MonoBehaviour
{
    GameObject p1;
    GameObject p2;
    GameObject[] Player1Cards; 
    GameObject[] Player2Cards;
    int player1CardSize;
    int player2CardSize;

    // Start is called before the first frame update
    void Start()
    {
        p1 = GameManager.gameManager.player1Reciever.gameObject;
        p2 = GameManager.gameManager.player2Reciever.gameObject;
        Player1Cards = p1.GetComponent<addCards>().pl1;
        Player2Cards = p2.GetComponent<addCards>().pl2;
        player1CardSize = p1.GetComponent<addCards>().pl1Size;
        player2CardSize = p2.GetComponent<addCards>().pl2Size;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
