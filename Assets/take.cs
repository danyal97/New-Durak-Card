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
                    player1.gameObject.GetComponent<addCards>().onEnterReplica(colidersObject.gameObject.GetComponent<placeCards>().cards[i].gameObject);
                    //colidersObject.gameObject.GetComponent<placeCards>().positionalIndex;
                }
                Debug.Log("On Take");
                
            }
        }
    }
}