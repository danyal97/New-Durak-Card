using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gm;
    public void Draw_Cards(){
        GameObject []p1 =GameObject.FindGameObjectsWithTag("Player1Cards");
        GameObject[] p2 = GameObject.FindGameObjectsWithTag("Player2Cards");
        if (p1.Length<6) {
            gm.DrawCards(6-p1.Length , GameManager.Player.player1);
        }
        if (p2.Length < 6)
        {
            gm.DrawCards(6 -p2.Length, GameManager.Player.player2);
        }

    }
}
