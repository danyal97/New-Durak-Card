using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class take : MonoBehaviour
{
    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;
    [SerializeField]
    GameObject[] cardsOnTable;
    public enum Player { player1, player2, player3, player4 };
    public Player p = Player.player3;
    public GameObject colidersObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void onTake() {
        if (p == Player.player1) {
            for (int i =0;i< colidersObject.gameObject.GetComponent<placeCards>().positionalIndex;i++) {
                colidersObject.GetComponent<placeCards>().cards[i].GetComponent<moveCard>().isTouchable = true;
                if (colidersObject.GetComponent<placeCards>().cards[i]){
                    Debug.Log("Inside Iff");
                    player1.gameObject.GetComponent<addCards>().isRecievabe = true;
                    if (colidersObject.gameObject.GetComponent<placeCards>().cards[i].gameObject.tag != "Player1Cards")
                    {
                        player1.gameObject.GetComponent<addCards>().onEnterReplica(colidersObject.gameObject.GetComponent<placeCards>().cards[i].gameObject);
                    }
                    else {
                        //player1.gameObject.GetComponent<addCards>().updateVariable = true;
                    }
                    deleteCard(colidersObject.gameObject.GetComponent<placeCards>().cards[i].name, colidersObject.gameObject.GetComponent<placeCards>().cards);
                    player1.gameObject.GetComponent<addCards>().arangecards();
                    player1.gameObject.GetComponent<addCards>().arangecards();
                }
                Debug.Log("On Take");
                
            }
            colidersObject.SetActive(false);
            colidersObject.gameObject.GetComponent<placeCards>().positionalIndex = 0;
            int lenth =colidersObject.gameObject.GetComponent<placeCards>().colidableObject.Length;
            for (int i =0;i< lenth; i++) {
                colidersObject.gameObject.GetComponent<placeCards>().colidableObject[i].GetComponent<onCollision>().card = null;
            }




        }
    }
    public GameObject deleteCard(string cardName, GameObject[] array)
    {
        GameObject gb = null;
        int foundIndex = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
            {
                if (array[i].name == cardName)
                {
                    foundIndex = i;
                    gb = array[i];
                    break;
                }
            }
        }
        if (foundIndex != -1)
        {
            for (int i = foundIndex; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
        }
        if (gb != null)
            return gb;
        return null;
    }
}