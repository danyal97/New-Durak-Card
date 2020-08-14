

using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class addCards : MonoBehaviour
{
    GameObject[] myCards = new GameObject[6];
    int myCardsSize = 0;
    public float x;
    public float y;
    public float z;
    GameObject[] pl1 = new GameObject[36];
    GameObject[] pl2 = new GameObject[36];
    GameObject[] pl3 = new GameObject[36];
    GameObject[] pl4 = new GameObject[36];
    DatabaseReference reference;
    float time;
    public enum Player { player1, player2, player3, player4 };

    public Player p = Player.player1;

    public void RotateMyCards() {
        Quaternion vector = new Quaternion(0, 0.707106769f, -0.707106769f, 0);
        float speed = 5f;
        //Vector3(90, 180, 0)
        int maxAngle;
        for (int i=0;i<=pl1Size;i++) {
            pl1[i].gameObject.transform.Rotate(Vector3.right*Time.deltaTime*speed);
        }
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time < 5f )
        {
            Debug.Log("Time : " + time);
            //RotateMyCards();
        }
    }
    private void showCards(string player) {
        if (player == "player1") {
            for (int i = 0; i < pl1.Length; i++) {
                Debug.Log(pl1[i].gameObject.name);
            }
        } else if (player == "player1")
        {
            for (int i = 0; i < pl2.Length; i++)
            {
                Debug.Log(pl2[i].gameObject.name);
            }
        }
    }
    public void arangecards(){
        float distance = 1f;
        Vector3 pos = new Vector3(x, y, z);
        
        float lastDistance = pos.x;
        float lastheight = pos.y;
        if (p == Player.player1)
            for (int i = 0; i <= myCardsSize; i++)
            {
                if (i == 0)
                {
                    myCards[i].gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
                    myCards[i].gameObject.SetActive(true);
                    pl1[i] = myCards[i].gameObject;
                }
                else
                {
                    myCards[i].gameObject.transform.position = new Vector3(lastDistance, lastheight, pos.z);
                    myCards[i].gameObject.SetActive(true);
                    pl1[i] = myCards[i].gameObject;
                }
                lastDistance = pos.x + (distance) * i + 1;
                lastheight = lastheight+ 0.020627f;
                Debug.Log(myCards[i].gameObject.transform.position);
            }
        else if (p == Player.player2)
        {
            lastheight = pos.y;
            for (int i = 0; i < myCardsSize; i++)
            {
                if (i == 0)
                {
                    myCards[i].gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
                    myCards[i].gameObject.SetActive(true);
                    pl2[i] = myCards[i].gameObject;
                }
                else
                {
                    myCards[i].gameObject.transform.position = new Vector3(lastDistance, lastheight, pos.z);
                    myCards[i].gameObject.SetActive(true);
                    pl2[i] = myCards[i].gameObject;
                }
                lastDistance = pos.x + (distance) * i + 1;
                lastheight = lastheight + 0.020627f;
                Debug.Log(myCards[i].gameObject.transform.position);
            }
        }/*
        else if (p == Player.player3)
        {
            lastheight = pos.y;
            for (int i = 0; i <= myCardsSize; i++)
             {
                if (i == 0)
                {
                    myCards[i].gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
                    //myCards[i].gameObject.transform.Rotate(myCards[i].gameObject.transform.rotation.x, 90, myCards[i].gameObject.transform.rotation.y);
                    myCards[i].gameObject.SetActive(true);
                }
                else
                {
                    myCards[i].gameObject.transform.position = new Vector3(lastDistance, lastheight, pos.z);
                    //myCards[i].gameObject.transform.Rotate(myCards[i].gameObject.transform.rotation.x, 90, myCards[i].gameObject.transform.rotation.y);
                    myCards[i].gameObject.SetActive(true);
                }
                lastDistance = pos.x + (distance) * i + 1;
                lastheight = lastheight + 0.020627f;
                Debug.Log(myCards[i].gameObject.transform.position);
            }
        }
        else if (p == Player.player4)
        {
            lastheight = pos.y;
            for (int i = 0; i <= myCardsSize; i++)
            {
                if (i == 0)
                {
                    myCards[i].gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
                    myCards[i].gameObject.SetActive(true);
                }
                else
                {
                    myCards[i].gameObject.transform.position = new Vector3(lastDistance, lastheight, pos.z);
                    myCards[i].gameObject.SetActive(true);
                }
                lastDistance = pos.x + (distance) * i + 1;
                lastheight = lastheight + 0.020627f;
                Debug.Log(myCards[i].gameObject.transform.position);
            }
        }*/
    }
    int pl1Size = 0;
    int pl2Size = 0;
    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("OnTriggerEnter------------------------------------");
        if (other.gameObject.tag == "Card")
        {
            Debug.Log(other.gameObject.name);
            myCards[myCardsSize] = other.gameObject;
            other.gameObject.SetActive(false);
            other.GetComponent<moveCard>().isRecieved = true;

            if (this.gameObject.tag== "player1") {
                pl1[pl1Size] = other.gameObject;
                pl1Size++;
            }
            if (this.gameObject.tag == "player2")
            {
                pl2[pl2Size] = other.gameObject;
                pl2Size++;
            }
            myCardsSize++; 
            arangecards();
        }
    }
    public void onEnterReplica(GameObject other) {
        Debug.Log("OnTriggerEnter------------------------------------");
        if (other.gameObject.tag == "Card")
        {
            Debug.Log(other.gameObject.name);
            myCards[myCardsSize] = other.gameObject;
            other.gameObject.SetActive(false);
            other.GetComponent<moveCard>().isRecieved = true;

            if (this.gameObject.tag == "player1")
            {
                
                pl1[pl1Size] = other.gameObject;
                other.gameObject.tag = "Player1Cards";
                pl1Size++;
            }
            if (this.gameObject.tag == "player2")
            {
                pl2[pl2Size] = other.gameObject;
                other.gameObject.tag = "Player2Cards";
                pl2Size++;
            }
            myCardsSize++;
            arangecards();
        }
    }
    /*void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");    
        if (other.gameObject.tag == "Card") {
            other.gameObject.SetActive(false);
        }
    }*/
}
