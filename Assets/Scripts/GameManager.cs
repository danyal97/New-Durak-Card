using System.Collections;
using System.Threading;

using Firebase;
using Firebase.Auth;

using Firebase.Database;

using Firebase.Unity.Editor;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;
    DatabaseReference reference;
    FirebaseAuth auth;
    public GameObject[] cards = new GameObject[36];
    public GameObject[] newCards = new GameObject[24];
    public enum Player { player1, player2, player3, player4 }
    public GameObject player1Reciever;
    public GameObject player2Reciever;
    public bool isDistrbuting = false;
    public Player p = Player.player1;
    public Vector3 pos = new Vector3(7.98999977f, -0.200000003f, -1.47000003f);
    private Vector3 topPosition;
    public float speed;
    private float waitForSecond = 5000;
    int carddCurrentIndex = 35;
    public GameObject player1, player2, player3, player4;
    public GameObject trumpCard;
    public string trumpcardString="";
    bool isRestakingDone = false;
    int currentPositionOffCards = 22;
    bool isStacked = true;

    // Start is called before the first frame update
    private void Start() {
        gameManager = this;
        newCards = new GameObject[25];
    //this.gameObject.transform.position =new  Vector3(15.3800001f, -1.10000002f, 20.1900005f);
        ShuffleCards();
    }
    // Update is called once per frame
    void Update() {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        if (isDistrbuting)
        {
            Distribute();
        }
        if (!isStacked) {
            StackCards();
            isStacked = !isStacked;
        }
    }
    //public string saveDataToFirebase(int id, string name, string city) // نفرض اننا حابين نعمل قاعدة بيانات للموظفين كل موظف عنده بيانات مثل ID, Name, City
    //{
    //    reference.Child(auth.CurrentUser.UserId).Child("Postion").SetValueAsync();
    //    reference.Child(id.ToString()).Child("City").SetValueAsync(city);
    //    return "Save data to firebase Done.";
    //}
    public void ShuffleCards() {
        int num = Random.Range(0, cards.Length);
        for (int i = 0; i < cards.Length; i++) {
            GameObject temp = cards[i];
            cards[i] = cards[num];
            cards[num] = temp;
            num = Random.Range(0, cards.Length);
        }
        cards[cards.Length - 1].GetComponent<moveCard>().isOnTop = true;
        StackCards();
    }
    public void DrawCards(int cardsRequired, Player p) {
        int newCurrentPostionOfCards = currentPositionOffCards - cardsRequired;
        if (p == Player.player1) {
            for (int i = currentPositionOffCards; i >= newCurrentPostionOfCards; i--) {
                player1Reciever.gameObject.GetComponent<addCards>().onEnterReplica(newCards[i]);
            }
            currentPositionOffCards -= cardsRequired;
        }
        if (p == Player.player2) {
            for (int i = currentPositionOffCards; i >= newCurrentPostionOfCards; i--) {
                player2Reciever.gameObject.GetComponent<addCardsPlayer2>().onEnterReplica(newCards[i]);
            }
            currentPositionOffCards -= cardsRequired;
        }
    }

    public void Distribute() {
        
        for (int i = 35; i >= 24; i--) {
            if (i == 35 || i == 33 || i == 31 || i == 29 || i == 27 || i == 25)
            {
                if (!cards[i].GetComponent<moveCard>().isRecieved)
                {
                    //Debug.Log(" first");
                    //cards[i].transform.LookAt(player1.transform);
                    float distance = Vector3.Distance(cards[i].transform.position, player1.gameObject.transform.position);
                    if (distance > 0)
                        cards[i].transform.position += Vector3.forward * speed * Time.deltaTime;
                    //Debug.Log(cards[i].gameObject.name);
                }
            }
            else if (i == 34 || i == 32 || i == 30 || i == 28 || i == 26 || i == 24)
            {
                if (!cards[i].GetComponent<moveCard>().isRecieved)
                {
                    //Debug.Log(" first");
                    //cards[i].transform.LookAt(player1.transform);
                    float distance = Vector3.Distance(cards[i].transform.position, player1.gameObject.transform.position);
                    if (distance > 0)
                        cards[i].transform.position += Vector3.back * speed * Time.deltaTime;
                    //Debug.Log(cards[i].gameObject.name);
                }
            }
            /*
            if (i == 35 || i == 31 || i == 27 || i == 23 || i == 19 || i == 15)
            {
                if (!cards[i].GetComponent<moveCard>().isRecieved)
                {
                    Debug.Log(" first");
                    //cards[i].transform.LookAt(player1.transform);
                    float distance = Vector3.Distance(cards[i].transform.position, player1.gameObject.transform.position);
                    if (distance > 0)
                        cards[i].transform.position += Vector3.forward * speed * Time.deltaTime;
                    Debug.Log(cards[i].gameObject.name);

                }
            }
            else if (i == 34 || i == 30 || i == 26 || i == 22 || i == 18 || i == 14)
            {
                if (!cards[i].GetComponent<moveCard>().isRecieved)
                {
                    Debug.Log("second");
                    //cards[i].transform.LookAt(player2.transform);
                    float distance = Vector3.Distance(cards[i].transform.position, player2.gameObject.transform.position);
                    if (distance > 0)
                        cards[i].transform.position += cards[i].transform.right * speed * Time.deltaTime;
                }
            }
            else if (i == 33 || i == 29 || i == 25 || i == 21 || i == 17 || i == 13)
            {
                if (!cards[i].GetComponent<moveCard>().isRecieved)
                {
                    Debug.Log(" third");
                    //cards[i].transform.LookAt(player3.transform);
                    float distance = Vector3.Distance(cards[i].transform.position, player3.gameObject.transform.position);
                    if (distance > 0)
                        cards[i].transform.position += -cards[i].transform.right * speed * Time.deltaTime;
                }
            }
            else if (i == 32 || i == 28 || i == 24 || i == 20 || i == 16 || i == 12)
            {
                if (!cards[i].GetComponent<moveCard>().isRecieved)
                {
                    Debug.Log(" fourth");
                    //cards[i].transform.LookAt(player4.transform);
                    float distance = Vector3.Distance(cards[i].transform.position, player4.gameObject.transform.position);
                    if (distance > 0)
                        //cards[i].transform.position += cards[i].transform.forward* speed * Time.deltaTime;
                        cards[i].transform.position += Vector3.back * speed * Time.deltaTime;
                }
            }*/
            

        }
    }
    public void SetTrumpCard(){

        Debug.Log("start");

        GameObject temp = cards[23].gameObject;
            cards[23] = cards[0].gameObject;
            cards[0] = temp;
            trumpCard = cards[0].gameObject;
            for (int j = 23; j >= 1; j--)
            {
                Debug.Log("j"  +j);
                ///newCards[j] = cards[j];
                newCards[j] = cards[j];
                Debug.Log(""+ newCards[j]);
            }
        Debug.Log("end");
        StackCards();
        cards[0].gameObject.transform.rotation = new Quaternion(90, 0, 0, 90);

        //Debug.Log("Quratation " + cards[0].gameObject.transform.rotation);
        if (cards[0].name.Contains("C"))
        {
            trumpcardString = "C";
        }
        if (cards[0].name.Contains("S"))
        {
            trumpcardString = "S";
        }
        if (cards[0].name.Contains("D"))
        {
            trumpcardString = "D";
        }
        if (cards[0].name.Contains("H"))
        {
            trumpcardString = "H";
        }
    }
    public void StackCards()
    {
        float height = 0.020627f;
        //Vector3 pos = new Vector3(7.98999977f, -0.200000003f, -1.47000003f);
        float lastDistance = pos.y;
        for (int i = 0; i < cards.Length; i++)
        {
            if (!cards[i].GetComponent<moveCard>().isRecieved)
            {
                if (i == 0)
                {
                    cards[i].gameObject.transform.position = new Vector3(pos.x, lastDistance + 1, pos.z);
                }
                else
                {
                    cards[i].gameObject.transform.position = new Vector3(pos.x, lastDistance, pos.z);
                }
                lastDistance = pos.y + (height) * i + 1;
            }
        }
    }
}