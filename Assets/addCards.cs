

using UnityEngine;
using System.Collections;

public class addCards : MonoBehaviour
{
    GameObject [] myCards = new GameObject[6];
    int myCardsSize = 0;
    public float x;
    public float y;
    public float z;
    
    
    public enum Player { player1, player2, player3, player4 };
    
    public Player p = Player.player1;
    


    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnTriggerEnter");
        if (collision.gameObject.tag == "Card")
        {
            collision.gameObject.SetActive(false);
        }
    }*/
    
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
                }
                else
                {
                    myCards[i].gameObject.transform.position = new Vector3(lastDistance, lastheight, pos.z);
                    myCards[i].gameObject.SetActive(true);
                }
                lastDistance = pos.x + (distance) * i + 1;
                lastheight = pos.y + 0.1f;
                Debug.Log(myCards[i].gameObject.transform.position);
            }
        else if (p == Player.player2)
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
                lastheight = pos.y + 0.1f;
                Debug.Log(myCards[i].gameObject.transform.position);
            }
        }
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
                lastheight = pos.y + 0.1f;
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
                lastheight = pos.y + 0.1f;
                Debug.Log(myCards[i].gameObject.transform.position);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter------------------------------------");
        if (other.gameObject.tag == "Card")
        {
            Debug.Log(other.gameObject.name);
            myCards[myCardsSize] = other.gameObject;
            other.gameObject.SetActive(false);
            other.GetComponent<moveCard>().isRecieved = true;

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
