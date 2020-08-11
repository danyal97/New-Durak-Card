using System.Collections;
using UnityEngine;

public class DecDistributor : MonoBehaviour
{
    public GameObject[] cards = new GameObject[36];

    public GameObject plane;
    private Vector3 topPosition;
    public GameObject player1Cards;
    public float speed;
    private float waitForSeconds = 5000;
    int carddCurrentIndex = 35;
    // Start is called before the first frame update
    private void Start()
    {
        ShuffleCards();

        
        //Distribute();

        //if (cards[12].gameObject.tag == "8C")
        //{
        //cards[12].transform.position = Vector3.left* Time.deltaTime*5;
        //Debug.Log("in Stack Cards Loop-----------------------------------------------------------");
        //}
        //topPosition = new Vector3((float)-1.759953, (float)-3, (float)3.694574);
        //cards[0].transform.position.Set(topPosition.x, topPosition.y, topPosition.z);
        //shuffleCards();
        //stackCards();
    }

    // Update is called once per frame
    void Update()
    {

        //Distribute();
        //StackCards();
        /*for (int i = 0; i < 36; i++)
        {
            cards[12].transform.position.Set((float)-1.759953, (float)0, (float)3.694574);
            Debug.Log("in Stack Cards Loop-----------------------------------------------------------");
        }*/


        /*for (int i = 0;i<cards.Length;i++) {
            Debug.Log("card " + cards[i].gameObject.tag);
        }*/
    }
    public void ShuffleCards()
    {
        int num = Random.Range(0, cards.Length);
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject temp = cards[i];
            cards[i] = cards[num];
            cards[num] = temp;
            num = Random.Range(0, cards.Length);
        }
        StackCards();
    }
    public void StackCards()
    {
        float height = 0.020627f;
        Vector3 pos = new Vector3(0, -0.995f, 0);
        float lastDistance = pos.y;
        for (int i = 0; i < cards.Length; i++)
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
    public void Distribute()
    {
        while (true) {
            Invoke("DistributeDec", 1f);
        }
    }
    public void DistributeDec() {
        if(!(carddCurrentIndex <14))
        cards[carddCurrentIndex].gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        cards[carddCurrentIndex - 1].gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        cards[carddCurrentIndex - 2].gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        cards[carddCurrentIndex - 3].gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        carddCurrentIndex -= 4;
    }
}