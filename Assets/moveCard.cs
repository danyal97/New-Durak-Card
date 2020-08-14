using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCard : MonoBehaviour
{
    //public GameObject target;
    public bool isOnTop=false;
    public bool isRecieved = false;
    int speed = 5;
    bool shouldBeMoved = false;
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag== "Card") {
                    //hitInfo.collider.gameObject.transform.Translate(target.gameObject.transform.position);
                    hitInfo.collider.gameObject.transform.position += Vector3.forward * speed * Time.deltaTime;
                }
            }
        }
    }
}
