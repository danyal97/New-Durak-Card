﻿using System.Collections;
using UnityEngine;
using System.Threading;
public class GameManager : MonoBehaviour
{
    public GameObject[] cards = new GameObject[36];
    
    private Vector3 topPosition;
    public float speed;
    private float waitForSecond = 5000;
    int carddCurrentIndex = 35;
    public GameObject player1, player2, player3, player4;
    
    // Start is called before the first frame update
    private void Start()
    {
        this.gameObject.transform.position =new  Vector3(15.3800001f, -1.10000002f, 20.1900005f);
        ShuffleCards();



    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    Debug.Log("Touch enter on " + hit.collider.name);
                }
            }
        }
        
        Distribute();
    
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
        cards[cards.Length - 1].GetComponent<moveCard>().isOnTop=  true;
        StackCards();
    }
    public void StackCards()
    {
        float height = 0.020627f;
        Vector3 pos = new Vector3(7.98999977f, -0.200000003f, -1.47000003f);    
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

        for (int i=35;i>=12;i--) {
            if (i == 35 || i == 31 || i == 27 || i == 23 ) {
                if (!cards[i].GetComponent<moveCard>().isRecieved)
                {
                    Debug.Log(" first");
                    //cards[i].transform.LookAt(player1.transform);
                    float distance = Vector3.Distance(cards[i].transform.position, player1.gameObject.transform.position);
                    if (distance > 0)
                        cards[i].transform.position += Vector3.forward * speed * Time.deltaTime;
                }
            }
            else if (i == 34 || i == 30 || i == 26 || i == 22 ) 
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
            else if (i == 33 ||i == 29 || i == 25 || i == 21 )
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
            if (i == 32 || i == 28 || i == 24 || i == 20)
            {
                if (!cards[i].GetComponent<moveCard>().isRecieved) {
                    Debug.Log(" fourth");
                    //cards[i].transform.LookAt(player4.transform);
                    float distance = Vector3.Distance(cards[i].transform.position, player4.gameObject.transform.position);
                    if (distance > 0)
                        //cards[i].transform.position += cards[i].transform.forward* speed * Time.deltaTime;
                        cards[i].transform.position += Vector3.back * speed * Time.deltaTime;
                }
            }
        }
        //get the distance between the chaser and the target

        //for (carddCurrentIndex =35; carddCurrentIndex > 14; carddCurrentIndex--) {
        //        cards[carddCurrentIndex].gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        //    }
    }
    public void ReStackCards()
    {
        float height = 0.020627f;
        Vector3 pos = new Vector3(7.98999977f, -0.200000003f, -1.47000003f);
        float lastDistance = pos.y;
        for (int i = 0; i < cards.Length; i++)
        {

            if (i == 0 && !cards[i].gameObject.GetComponent<moveCard>().isRecieved)
            {
                cards[i].gameObject.transform.position = new Vector3(pos.x, lastDistance + 1, pos.z);
                lastDistance = pos.y + (height) * i + 1;
            }
            else if(!cards[i].gameObject.GetComponent<moveCard>().isRecieved)
            {
                cards[i].gameObject.transform.position = new Vector3(pos.x, lastDistance, pos.z);
                lastDistance = pos.y + (height) * i + 1;
            }
            
        }
    }
}