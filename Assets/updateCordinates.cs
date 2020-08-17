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
    public FirebaseScript2 playerCardInfo;
    float time1 = 0;
    float time2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerCardInfo = new FirebaseScript2();
    }
    // Update is called once per frame
    int times = 0;
    void Update()
    {
        times++;
        //FirebaseScript playerCardInfo = new FirebaseScript();
        p1 = gameManager.player1Reciever.gameObject;
        p2 = gameManager.player2Reciever.gameObject;
        Player1Cards = p1.GetComponent<addCards>().pl1;
        Player2Cards = p2.GetComponent<addCardsPlayer2>().pl2;
        player1CardSize = p1.GetComponent<addCards>().pl1Size;
        player2CardSize = p2.GetComponent<addCardsPlayer2>().pl2Size;


        time1 += Time.deltaTime;
        time2 += Time.deltaTime+0.1f;
        //if (time1 > 3)
        //{
            playerCardInfo.RetreiveCoordinateFromDatabse();
        //    time1 = 0;
        //}

        //if (time1 > 3) {
            /*
            for (int i = 0; i < player1CardSize; i++)
            {
                string posx = Player1Cards[i].gameObject.transform.position.x.ToString();
                string posy = Player1Cards[i].gameObject.transform.position.y.ToString();
                string posz = Player1Cards[i].gameObject.transform.position.z.ToString();
                playerCardInfo.AddCoordinatesToDatabse("1", Player1Cards[i].gameObject.name.ToString(), posx, posy, posz);
            }
            time1 = 0;
            */
        //}

        //if (times == 1)
        //{
            for (int i = 0; i < player2CardSize; i++)
            {
                string posx = Player2Cards[i].gameObject.transform.position.x.ToString();
                string posy = Player2Cards[i].gameObject.transform.position.y.ToString();
                string posz = Player2Cards[i].gameObject.transform.position.z.ToString();
                playerCardInfo.AddCoordinatesToDatabse("2", Player1Cards[i].gameObject.name.ToString(), posx, posy, posz);
            }
        //    time2 = 0;
        //}
    }
}
