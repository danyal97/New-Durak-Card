using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateCordinates : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    GameObject p1;
    GameObject p2;
    GameObject[] Player1Cards; 
    GameObject[] Player2Cards;
    int player1CardSize;
    int player2CardSize;
    public FirebaseScript playerCardInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //FirebaseScript playerCardInfo = new FirebaseScript();
        p1 = gameManager.player1Reciever.gameObject;
        p2 = gameManager.player2Reciever.gameObject;
        Player1Cards = p1.GetComponent<addCards>().pl1;
        Player2Cards = p2.GetComponent<addCards>().pl2;
        player1CardSize = p1.GetComponent<addCards>().pl1Size;
        player2CardSize = p2.GetComponent<addCards>().pl2Size;
        for (int i = 0; i < player1CardSize; i++)
        {
            string posx = Player1Cards[i].gameObject.transform.position.x.ToString();
            string posy = Player1Cards[i].gameObject.transform.position.y.ToString();
            string posz = Player1Cards[i].gameObject.transform.position.z.ToString();
            playerCardInfo.AddCoordinatesToDatabse("1", Player1Cards[i].gameObject.transform.tag.ToString(), posx, posy, posz);
        }
        for (int i = 0; i < player2CardSize; i++)
        {
            string posx = Player2Cards[i].gameObject.transform.position.x.ToString();
            string posy = Player2Cards[i].gameObject.transform.position.y.ToString();
            string posz = Player2Cards[i].gameObject.transform.position.z.ToString();
            playerCardInfo.AddCoordinatesToDatabse("2", Player2Cards[i].gameObject.transform.tag.ToString(), posx, posy, posz);
        }
    }
}
