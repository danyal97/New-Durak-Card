using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeCards : MonoBehaviour
{
    public IDictionary<string, int> dict = new Dictionary<string, int>();

    public GameManager gameManager;
    public GameObject[] gameobjects = new GameObject[12];
    public GameObject[] colidableObject = new GameObject[6];
    
    //colidable stack will return ushort the gamobject which is colided
    
    public GameObject[] cards = new GameObject[12];
    //public Vector3[] cardsPos = new Vector3[12];

    GameObject[] allCards = new GameObject[36];
    int noOfAvalibleCardsIntray = 0;
    public int positionalIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        allCards = GameObject.FindGameObjectsWithTag("Card");
        for (int i=0;i<allCards.Length;i++) {
            if (allCards[i].name.Contains("A")) {
                dict.Add(allCards[i].name, 10);
            }
            if (allCards[i].name.Contains("K"))
            {
                dict.Add(allCards[i].name, 9);
            }
            if (allCards[i].name.Contains("Q"))
            {
                dict.Add(allCards[i].name, 8);
            }
            if (allCards[i].name.Contains("J"))
            {
                dict.Add(allCards[i].name, 7);
            }
            if (allCards[i].name.Contains("10"))
            {
                dict.Add(allCards[i].name, 6);
            }
            if (allCards[i].name.Contains("9"))
            {
                dict.Add(allCards[i].name, 5);
            }
            if (allCards[i].name.Contains("8"))
            {
                dict.Add(allCards[i].name, 4);
            }
            if (allCards[i].name.Contains("7"))
            {
                dict.Add(allCards[i].name, 3);
            }
            if (allCards[i].name.Contains("6"))
            {
                dict.Add(allCards[i].name, 2);
            }
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        setTrumpSuit(gameManager.trumpcardString);
            for (int i = 0; i < positionalIndex; i++)
            {
                if (cards[i] != null && gameobjects[i].transform.position != null)
                {
                    cards[i].gameObject.transform.position = gameobjects[i].transform.position;
                    Debug.Log("Update mid");
                }
            }
        for (int j =0;j<colidableObject.Length;j++) {

            if (colidableObject[j].GetComponent<onCollision>().card != null)
            {
              if (noOfAvalibleCardsIntray == 0) {
                        cards[positionalIndex] = colidableObject[j].GetComponent<onCollision>().card;
                        cards[positionalIndex].GetComponent<moveCard>().isTouchable = false;
                        //colidableObject[j].SetActive(false);
                        //Debug.Log("noOfAvalibleCardsIntray "  + cards[positionalIndex].GetComponent<onCollision>().card.GetComponent<moveCard>().isTouchable);
                        
                        Debug.Log("noOfAvalibleCardsIntray ");
                            noOfAvalibleCardsIntray++;
                        positionalIndex++;
                        
                    }
              else
              {
                        //checked colidable object that is isActiveAndEnabled eligible to placed on card;
                        if (checkEligibility(colidableObject[j].GetComponent<onCollision>().card, positionalIndex, gameManager.trumpcardString))
                        {
                            cards[positionalIndex] = colidableObject[j].GetComponent<onCollision>().card;
                            cards[positionalIndex].GetComponent<moveCard>().isTouchable = false;     
//                            colidableObject[j].SetActive(false);
                            
                            noOfAvalibleCardsIntray++;
                            positionalIndex++;
                        }   
                        
              }
              if (positionalIndex >=12) {
                        Debug.Log("If Player cards are full then");
              }
            
            }
        }
    }
    void setTrumpSuit(string suit)
    {
        for (int i = 0; i < allCards.Length; i++)
        {
            if (allCards[i].name.Contains(suit))
            {
                if (allCards[i].name.Contains("A"))
                {
                    dict[allCards[i].name] = 20;
                }
                if (allCards[i].name.Contains("K"))
                {
                    dict[allCards[i].name] = 19;
                }
                if (allCards[i].name.Contains("Q"))
                {
                    dict[allCards[i].name] = 18;
                }
                if (allCards[i].name.Contains("J"))
                {
                    dict[allCards[i].name] = 17;
                }
                if (allCards[i].name.Contains("10"))
                {
                    dict[allCards[i].name] = 16;
                }
                if (allCards[i].name.Contains("9"))
                {
                    dict[allCards[i].name] = 15;
                }
                if (allCards[i].name.Contains("8"))
                {
                    dict[allCards[i].name] = 14;
                }
                if (allCards[i].name.Contains("7"))
                {
                    dict[allCards[i].name] = 13;
                }
                if (allCards[i].name.Contains("6"))
                {
                    dict[allCards[i].name] = 12;
                }
            }
        }
    }
    bool checkEligibility(GameObject obj ,int index, string TrumpSuit) {
        Debug.Log(cards[index - 1].name[0]);
        Debug.Log(obj.name[0]);

        if (obj.gameObject.name.Contains(TrumpSuit))
        {
            if (dict[cards[index - 1].name] < dict[obj.name])
            {
                return true;
            }
        }
        if (dict[cards[index - 1].name] < dict[obj.name]  && cards[index - 1].name[cards[index - 1].name.Length-1] == obj.name[obj.name.Length-1])
        {
            return true;
        }
        return false;
    }
}
