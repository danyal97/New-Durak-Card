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
        
        //player1.GetComponent<addCards>.player1
    }
    public void onTake() {
        if (p == Player.player1) {
            //for (int i =0; i<) {
                //player1.GetComponent<addCards>().onEnterReplica(cardsOnTable[i]);
            //}
        }
    }
}

