

using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class addCards : MonoBehaviour
{
    [SerializeField]
    GameManager gameManger;
    GameObject[] myCards = new GameObject[36];
    public int myCardsSize = 0;
    public float x;
    public float y;
    public float z;
    public GameObject[] pl1 = new GameObject[36];
    public GameObject[] pl2 = new GameObject[36];
    GameObject[] pl3 = new GameObject[36];
    GameObject[] pl4 = new GameObject[36];
    DatabaseReference reference;
    float time;
    public enum Player { player1, player2, player3, player4 };
    public int pl1Size = 0;
    public int pl2Size = 0;
    public Player p = Player.player3;

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
            //Debug.Log("Time : " + time);
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
        float distance = 3f;
        Vector3 pos = new Vector3(x, y, z);
        float lastDistance = 0;
        float lastheight = 0;
        Vector3 currentEulerAngles;
        if (p == Player.player1)
        {
            lastDistance = pos.x;
            lastheight = pos.y;
            for (int i = 0; i < myCardsSize; i++)
            {
                if (i == 0)
                {
                    myCards[i].gameObject.transform.position = new Vector3(pos.x, pos.y, -19.6f);
                    myCards[i].gameObject.transform.rotation=  new Quaternion(90,0,0,90);
                    //myCards[i].transform.eulerAngles = currentEulerAngles;
                    myCards[i].gameObject.SetActive(true);
                    pl1[i] = myCards[i].gameObject;
                }
                else
                {
                    Debug.Log("Positional Vactor  " + myCards[i].gameObject.transform.position);
                    
                    myCards[i].gameObject.transform.position = new Vector3(lastDistance, lastheight, -19.6f);
                    myCards[i].gameObject.transform.rotation = new Quaternion(90, 0, 0, 90);
                    myCards[i].gameObject.SetActive(true);
                    pl1[i] = myCards[i].gameObject;
                }
                lastDistance = pos.x + (distance) * i + 1;
                lastheight = lastheight + 0.020627f;
                Debug.Log(myCards[i].gameObject.transform.position);
            }
        }
        else if (p == Player.player2)
        {
            lastDistance = pos.x;

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
    
    public bool isRecievabe =true;
    private void OnTriggerEnter(Collider other)
    {
        if (!isRecievabe)
        {
            return;
        }
        else
        {
            Debug.Log("OnTriggerEnter------------------------------------");
            if (other.gameObject.tag == "Card")
            {
                Debug.Log(other.gameObject.name);
                if (p== Player.player1)
                {
                    myCards[myCardsSize] = other.gameObject;
                    other.gameObject.tag = "Player1Cards";
                    
                    other.gameObject.SetActive(false);
                    other.GetComponent<moveCard>().isRecieved = true;
                    pl1[pl1Size] = other.gameObject;
                    ++pl1Size;
                    Debug.Log("Pl1 SIze " + pl1Size);
                    ++myCardsSize;
                    arangecards();

                    if (pl1Size >= 6)
                    {
                        isRecievabe = false;
                    }
                    if (myCardsSize == 6)
                    {
                        Debug.Log("condition in triger start");
                        gameManger.SetTrumpCard();
                        Debug.Log("condition in triger end-------------------------");
                    }
                }
                else if (p == Player.player2)
                {
                    myCards[myCardsSize] = other.gameObject;
                    other.gameObject.SetActive(false);
                    other.gameObject.tag = "Player2Cards";
                    other.GetComponent<moveCard>().isRecieved = true;
                    pl2[pl2Size] = other.gameObject;
                    ++pl2Size;
                    Debug.Log("Pl2 SIze " + pl2Size);
                    ++myCardsSize;
                    arangecards();
                    if (pl2Size >= 6)
                    {
                        isRecievabe = false;
                    }
                }
                
                /*++myCardsSize;
                arangecards();*/
            }
        }
    }
    public void onEnterReplica(GameObject other) {
        /*Debug.Log("OnTriggerEnter------------------------------------");
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
                Debug.Log("" + other.gameObject.tag);
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
        }*/
    }
}
