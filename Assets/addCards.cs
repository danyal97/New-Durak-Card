

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
    
    public  GameObject[] myCards = new GameObject[36];
    public int myCardsSize = 0;
    public float x;
    public float y;
    public float z;
    public GameObject[] pl1 = new GameObject[36];
    public bool updateVariable =true;
    DatabaseReference reference;
    float time;
    public enum Player { player1, player2, player3};
    public int pl1Size = 0;
    
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
    public void Update()
    {
        

        
        
    }
    private void showCards(string player) {
        if (player == "player1") {
            for (int i = 0; i < pl1.Length; i++) {
                Debug.Log(pl1[i].gameObject.name);
            }
        }
    }
    public void arangecards(){

        float distance = 3f;
        if (myCardsSize >6 && myCardsSize < 10) {
            distance = 2.3f;
        }
        Vector3 pos = new Vector3(x, y, z);
        float lastDistance = 0;
        float lastheight = 0;
        
      
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
                    myCards[i].gameObject.SetActive(true);
                    pl1[i] = myCards[i].gameObject;
                    lastDistance += (distance);
                    lastheight = lastheight + 0.020627f;
                }
                else
                {
                    Debug.Log("Positional Vactor  " + myCards[i].gameObject.transform.position);
                    myCards[i].gameObject.transform.position = new Vector3(lastDistance, lastheight, -19.6f);
                    myCards[i].gameObject.transform.rotation = new Quaternion(90, 0, 0, 90);
                    myCards[i].gameObject.SetActive(true);
                    pl1[i] = myCards[i].gameObject;
                    lastDistance += (distance);
                    lastheight = lastheight + 0.020627f;
                }
                
                //Debug.Log("Last Distance: "+lastDistance +"i: "+ i);
                Debug.Log(myCards[i].gameObject.transform.position);
            }
        }
        /*
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
        onEnterReplica(other.gameObject);
    }
    public void onEnterReplica(GameObject other) {
        if (!isRecievabe)
        {
            this.GetComponent<BoxCollider>().isTrigger = false;
            return;
        }
        else
        {
            Debug.Log("OnTriggerEnter------------------------------------");
            
            if (other.gameObject.tag == "Card" || other.gameObject.tag == "Player1Cards" || other.gameObject.tag == "Player2Cards")
            {
                Debug.Log(other.gameObject.name);
                if (p == Player.player1)
                {
                    myCards[myCardsSize] = other.gameObject;
                    other.gameObject.tag = "Player1Cards";
                    other.gameObject.SetActive(false);
                    other.GetComponent<moveCard>().isRecieved = true;
                    pl1[pl1Size] = other.gameObject;
                    ++pl1Size;
                    Debug.Log("Pl1 SIze " + pl1Size);
                    ++myCardsSize;
                    Debug.Log("player1 Cardsize :"+myCardsSize);
                    arangecards();
                    if (myCardsSize == 6)
                    {
                        isRecievabe = false;
                        Debug.Log("condition in triger start");
                        gameManger.SetTrumpCard();
                        Debug.Log("condition in triger end-------------------------");
                    }
                }
            }/*else if (p == Player.player1 && other.gameObject.tag == "Player1Cards")
            {
                    Debug.Log("MycardSize :" + myCardsSize);
                    myCards[myCardsSize] = other.gameObject;
                    other.gameObject.tag = "Player1Cards";
                    other.gameObject.SetActive(false);
                    other.GetComponent<moveCard>().isRecieved = true;
                    pl1[pl1Size] = other.gameObject;
                    ++pl1Size;
                    Debug.Log("Pl1 SIze " + pl1Size);
                    ++myCardsSize;
                    arangecards();
            }*/
        }
    }
}
