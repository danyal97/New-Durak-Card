using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCard : MonoBehaviour
{
    PlayBoard playBoard = new PlayBoard();
    public Vector3 pos1a = new Vector3(5.61999989f, -3.51999998f, 6.57999992f);
    Place place1a  = new Place();
    public Vector3 pos1b = new Vector3(7.34000015f, -3.41000009f, 6.01999998f);
    Place place1b = new Place();
    public Vector3 pos2a = new Vector3(12.0799999f, -3.51999998f, 6.57999992f);
    Place place2a = new Place();
    public Vector3 pos2b = new Vector3(13.8000002f, -3.41000009f, 6.01999998f);
    Place place2b = new Place();
    public Vector3 pos3a = new Vector3(18.2199993f, -3.51999998f, 6.57999992f);
    Place place3a = new Place();
    public Vector3 pos3b = new Vector3(19.9399986f, -3.41000009f, 6.01999998f);
    Place place3b = new Place();
    public Vector3 pos4a = new Vector3(5.61999989f, -3.51999998f, -4f);
    Place place4a = new Place();
    public Vector3 pos4b = new Vector3(7.34000015f, -3.41000009f, -4.55999994f);
    Place place4b = new Place();
    public Vector3 pos5a = new Vector3(12.0799999f, -3.51999998f, -4f);
    Place place5a = new Place();
    public Vector3 pos5b = new Vector3(13.8000002f, -3.41000009f, -4.55999994f);
    Place place5b = new Place();
    public Vector3 pos6a = new Vector3(18.2199993f, -3.51999998f, -4f);
    Place place6a = new Place();
    public Vector3 pos6b = new Vector3(19.9399986f, -3.41000009f, -4.55999994f);
    Place place6b = new Place();
    public bool isOnTop=false;
    public bool isRecieved = false;
    bool shouldBeMoved = false;

    public float speed = 20.0f;
    public float minDist = 0f;
    public Transform target;
    Touch touch;
    float speedModifier = 0.0011f;
    GameObject selected;
    void Start()
    {
        place1a.position = pos1a;
        place1a.isAvalible = true;
        place2a.position = pos2a;
        place2a.isAvalible = true;
        place3a.position = pos3a;
        place3a.isAvalible = true;
        place4a.position = pos4a;
        place4a.isAvalible = true;
        place5a.position = pos5a;
        place5a.isAvalible = true;
        place6a.position = pos6a;
        place6a.isAvalible = true;

        place1b.position = pos1b;
        place1b.isAvalible = true;
        place2b.position = pos2b;
        place2b.isAvalible = true;
        place3b.position = pos3b;
        place3b.isAvalible = true;
        place4b.position = pos4b;
        place4b.isAvalible = true;
        place5b.position = pos5b;
        place5b.isAvalible = true;
        place6b.position = pos6b;
        place6b.isAvalible = true;
        playBoard.playboardItems = new List<Place>();
        playBoard.addPlayboardItem(place1a);
        playBoard.addPlayboardItem(place1b);
        playBoard.addPlayboardItem(place2a);
        playBoard.addPlayboardItem(place2b);
        playBoard.addPlayboardItem(place3a);
        playBoard.addPlayboardItem(place3b);
        playBoard.addPlayboardItem(place4a);
        playBoard.addPlayboardItem(place4b);
        playBoard.addPlayboardItem(place5a);
        playBoard.addPlayboardItem(place5b);
        playBoard.addPlayboardItem(place6a);
        playBoard.addPlayboardItem(place6b);
        

        // if no target specified, assume the player
        if (target == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
        
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag== "Card") {
                    GameObject gb = hitInfo.collider.gameObject;
                    selected = gb;
                    
                }
            }
        }
        if (Input.touchCount > 0)
            {
            if (selected)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    selected.transform.position = new Vector3(selected.transform.position.x + touch.deltaPosition.x * speedModifier,
                                selected.transform.position.y,
                                selected.transform.position.z + touch.deltaPosition.y * speedModifier
                                );
                }
            }
        }
    }
    public void MoveTheCardTowardSelectedCard() {
        if (target == null)
            return;
        // face the target
        transform.LookAt(target);

        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);

        //so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
        if (distance > minDist)
            transform.position += transform.forward * speed * Time.deltaTime;
    }
    
}
class Place
{
    public Vector3 position;
    public bool isAvalible;
}
class PlayBoard {
    public List<Place> playboardItems;
    public PlayBoard(List<Place> playboardItems) {
        this.playboardItems = playboardItems;
    }
    public PlayBoard()
    {}
    public void addPlayboardItem(Place p) {
        Place g = new Place();
        g = p;
        playboardItems.Add(g);
    }
}

