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
    public SpawnedItem selectedObject;
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
                    if (Distance.x < -swipeRange && selectedObject.rightItem != null )
                    {
                        moving = true;
                        EventManager.Instance.MoveItems(selectedObject, selectedObject.rightItem, 2, false);
                    }
                    else if (Distance.x > swipeRange && selectedObject.leftItem != null)
                    {
                        moving = true;
                        EventManager.Instance.MoveItems(selectedObject, selectedObject.leftItem, 1, false);
                    }
                }
                else
                {
                    if (Distance.y < -swipeRange && selectedObject.upItem != null)
                    {
                        moving = true;
                        EventManager.Instance.MoveItems(selectedObject, selectedObject.upItem, 4, false);
                    }
                    else if (Distance.y > swipeRange && selectedObject.downItem != null)
                    {
                        moving = true;
                        EventManager.Instance.MoveItems(selectedObject, selectedObject.downItem, 3, false);
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


    SpawnedItem TryHit()
    {
        Vector3 touchPostoWorldpos = Camera.main.ScreenToWorldPoint(firstPressPos);
        Vector2 touchPosWorld2D = new Vector2(touchPostoWorldpos.x, touchPostoWorldpos.y);
        RaycastHit2D hitinformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

        if (hitinformation.collider != null)
        {
            GameObject touchedObject = hitinformation.transform.gameObject;
            return touchedObject.GetComponent<SpawnedItem>();
        }
        else { return null; }
    }

    void TryChange(GameObject selectedObject)
    {

    }





}
