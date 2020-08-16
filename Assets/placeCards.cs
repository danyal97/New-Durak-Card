using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeCards : MonoBehaviour
{
    IDictionary<string, int> dict = new Dictionary<string, int>();
    

    public GameObject[] gameobjects = new GameObject[12];

    public GameObject[] cards = new GameObject[12];
    int noOfAvalibleCardsIntray = 0;
    // Start is called before the first frame update
    void Start()
    {
//        dict.Add("");

    }

    // Update is called once per frame
    void Update()
    { 
        for(int i =0;i<gameobjects.Length;i++)
        {
            if (gameobjects[i].GetComponent<OnCollision>().card != null)
            {
              if (noOfAvalibleCardsIntray == 0) {
                    gameobjects[0].SetActive(false);
                    gameobjects[0].GetComponent<OnCollision>().card = gameobjects[i].GetComponent<OnCollision>().card;
                    gameobjects[i].GetComponent<OnCollision>().card = null;
                    gameobjects[0].GetComponent<OnCollision>().card.transform.position = gameobjects[noOfAvalibleCardsIntray].transform.position;
                    cards[noOfAvalibleCardsIntray] = gameobjects[0].GetComponent<OnCollision>().card;
                    noOfAvalibleCardsIntray++;
               }
              else if(noOfAvalibleCardsIntray == 1)
              { 
                    //if(gameobjects[i].GetComponent<OnCollision>().card )


              }

            }
        }
    }
    bool checkTheCrdPlacing(int indexRecieved) {
        
        //if()
        return true;
    }
}
