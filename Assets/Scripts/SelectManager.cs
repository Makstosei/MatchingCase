using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    private bool stopTouch;
    private Vector3 secondPressPos;
    private Vector3 firstPressPos;
    private Vector3 currentPos;
    public int swipeRange;
    public float tapRange;
    public GameObject selectedObject;
    public bool moving;

    private void Awake()
    {
    }

    void Update()
    {
        MoveCheck();
    }

    void MoveCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stopTouch = false;
            firstPressPos = Input.mousePosition;
            selectedObject = TryHit();
        }

        if (Input.GetMouseButton(0))
        {
            currentPos = Input.mousePosition;
            Vector2 Distance = firstPressPos - currentPos;
            if (!stopTouch&&!moving && selectedObject != null)
            {
                
                if (Mathf.Abs(Distance.x) >= Mathf.Abs(Distance.y))
                {
                    if (Distance.x < -swipeRange && selectedObject.GetComponent<SpawnedItem>().rightItem != null && selectedObject.GetComponent<SpawnedItem>().rightItem.GetComponent<SpawnedItem>().CategoryId!=0)//categoryid mevcut halde bozulma olmamasý icin
                    {
                        moving = true;
                        selectedObject.GetComponent<SpawnedItem>().moveRight();
                    }
                    else if (Distance.x > swipeRange && selectedObject.GetComponent<SpawnedItem>().leftItem != null && selectedObject.GetComponent<SpawnedItem>().leftItem.GetComponent<SpawnedItem>().CategoryId != 0)
                    {
                        moving = true;
                        selectedObject.GetComponent<SpawnedItem>().moveLeft();
                    }
                }
                else
                {
                    if (Distance.y < -swipeRange && selectedObject.GetComponent<SpawnedItem>().upItem != null && selectedObject.GetComponent<SpawnedItem>().upItem.GetComponent<SpawnedItem>().CategoryId != 0)
                    {
                        moving = true;
                        selectedObject.GetComponent<SpawnedItem>().moveUp();
                    }
                    else if (Distance.y > swipeRange && selectedObject.GetComponent<SpawnedItem>().downItem != null && selectedObject.GetComponent<SpawnedItem>().downItem.GetComponent<SpawnedItem>().CategoryId != 0)
                    {
                        moving = true;
                        selectedObject.GetComponent<SpawnedItem>().moveDown();
                    }
                }


            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            stopTouch = true;
            secondPressPos = Input.mousePosition;
            Vector2 Distance = secondPressPos - firstPressPos;
            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {

            }
        }
    }


    GameObject TryHit()
    {
        Vector3 touchPostoWorldpos = Camera.main.ScreenToWorldPoint(firstPressPos);
        Vector2 touchPosWorld2D = new Vector2(touchPostoWorldpos.x, touchPostoWorldpos.y);
        RaycastHit2D hitinformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

        if (hitinformation.collider != null)
        {
            GameObject touchedObject = hitinformation.transform.gameObject;
            return touchedObject;
        }
        else { return null; }
    }

    void TryChange(GameObject selectedObject)
    {

    }





}
