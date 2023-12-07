using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float canMove;
    private Touch touch;
    private bool touched = false;
    //For checking swiped left or right.
    private bool swiped;
    Vector2 firstTouch;
    [SerializeField]private int speedFactor = 1;
    private float sideMoveSpeed = 0.08f;
    private GameLevelConttroller gameLevelController = new GameLevelConttroller();

    // Update is called once per frame


    void Update()
    {
        string direction = "";
        if(Input.touchCount > 0)
            touch = Input.GetTouch(0);

        //For getting the first checked point.
        if (touch.phase == TouchPhase.Began)
            firstTouch = touch.position;

        swiped = isSwiped(touch, out direction);


        if (Input.touchCount == 0)
            touched = true;
        if (touched && Input.touchCount == 1 && touch.phase == TouchPhase.Ended && Mathf.Abs(firstTouch.x - touch.position.x) < 5 && !swiped)
        {
            checkForShapeChange(touch);
            touched = false;
        }

        if(direction == "Left" && canMoveToLeftSide(transform))
        {
            transform.position += new Vector3(-sideMoveSpeed, 0, 0);
        }
        else if (direction == "Right" && canMoveToRightSide(transform))
        {
            transform.position += new Vector3(sideMoveSpeed, 0, 0);
        }


        //For checking the hit with obstacles.
        bool isHitted = Physics.Raycast(this.transform.position, new Vector3(0, 0, 1), out RaycastHit obstaclesHitted, 0.8f);
        if (isHitted)
            Debug.Log(obstaclesHitted.transform.parent);
        else
            movePlayer();
    }


    private bool isSwiped(Touch touch, out string direction)
    {
        bool swipped = false;
        direction = "";
        
        if(touch.phase == TouchPhase.Moved && Mathf.Abs(firstTouch.x - touch.position.x) > 5)
        {
            if (firstTouch.x > touch.position.x)
                direction = "Left";
            else if (firstTouch.x < touch.position.x)
                direction = "Right";

            swipped = true;

        }


        return swipped;
    }


    private void checkForShapeChange(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.transform.parent.transform.parent == this.transform)
            {
                changeShape();
            }
        }
    }


    public void changeShape()
    {
        int childrenCount = transform.childCount;
        
        for(int i = 0;i < childrenCount;i++)
        {
            bool a = transform.GetChild(i).gameObject.activeInHierarchy;
            if (transform.GetChild(i).gameObject.activeInHierarchy == true)
            {
                int haveToActive = i + 1;
                if (i == childrenCount - 1)
                    haveToActive = 0;
                transform.GetChild(haveToActive).gameObject.SetActive(true);
                transform.GetChild(i).gameObject.SetActive(false);
                break;
            }
        }
        //For destroying the current object and get another object.
        

        Debug.Log("Shape Changer was called");
    }

    private bool canMoveToLeftSide(Transform obj)
    {
        if (obj.position.x > -canMove)
            return true;
        return false;
    }

    private bool canMoveToRightSide(Transform obj)
    {
        if (obj.position.x < canMove)
            return true;
        return false;
    }

    private void movePlayer()
    {
        transform.position = new Vector3(
        transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * speedFactor);
    }
}
