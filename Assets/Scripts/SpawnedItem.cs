using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SpawnedItem : MonoBehaviour
{
    public int ItemId;
    public int CategoryId;
    public int ColumnId;
    public int ColumnPosition;
    public GameObject leftItem, rightItem, upItem, downItem;
    private int tempColumnID, tempColumnPosition;
    private GameObject tempLeft, tempRight, tempUp, tempDown;
    private Transform tempParent;
    public float switchingTime = 0.5f;
    public Sprite blank;
    public List<GameObject> matchedItemsVertical;
    public List<GameObject> matchedItemsHorizontonal;
    public List<Spawner.ColumnClass> Columns;
    public bool ismatchingFound;
    private int moveID;

    private void OnEnable()
    {
        EventManager.onGameStart += GameStartingEvent;

    }

    private void OnDisable()
    {
        EventManager.onGameStart -= GameStartingEvent;
    }


    void GameStartingEvent()
    {
        CheckVerticalMatchedItems();
        CheckHorizontonalMatchedItems();
    }





    public void moveLeft()
    {
        moveID = 1;
        ClearList();
        Columns = GameObject.Find("GameManager").GetComponent<Spawner>().Columns;
        SpawnedItem tempItemInfo = leftItem.GetComponent<SpawnedItem>();
        CopyValuesToTemp();

        //move transition
        Vector3 tempItemPosition = tempItemInfo.transform.position;
        Vector3 currentItemPosition = gameObject.transform.position;
        tempItemInfo.transform.DOMove(currentItemPosition, switchingTime);
        gameObject.transform.DOMove(tempItemPosition, switchingTime);
        gameObject.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);
        tempItemInfo.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);


        //holding item info replaced with left
        ColumnId = tempItemInfo.ColumnId;
        ColumnPosition = tempItemInfo.ColumnPosition;
        leftItem = tempItemInfo.leftItem;
        rightItem = tempLeft;
        upItem = tempItemInfo.upItem;
        downItem = tempItemInfo.downItem;
        gameObject.transform.name = "[" + (tempItemInfo.ColumnId) + "," + (tempItemInfo.ColumnPosition) + "]";
        gameObject.transform.parent = tempItemInfo.transform.parent;

        //left item info replaced with tempholdingitem --- pay attention to the code sequences
        tempItemInfo.ColumnId = tempColumnID;
        tempItemInfo.ColumnPosition = tempColumnPosition;
        tempItemInfo.leftItem = tempItemInfo.rightItem;
        tempItemInfo.rightItem = tempRight;
        tempItemInfo.upItem = tempUp;
        tempItemInfo.downItem = tempDown;
        tempItemInfo.gameObject.transform.name = "[" + (tempColumnID) + "," + (tempColumnPosition) + "]";
        tempItemInfo.transform.parent = tempParent;

        //updating effected items
        if (tempRight != null)
        {
            tempRight.GetComponent<SpawnedItem>().leftItem = tempLeft;
        }
        if (tempDown != null)
        {
            tempDown.GetComponent<SpawnedItem>().upItem = tempLeft;
        }
        if (tempUp != null)
        {
            tempUp.GetComponent<SpawnedItem>().downItem = tempLeft;
        }
        if (leftItem != null)
        {
            leftItem.GetComponent<SpawnedItem>().rightItem = tempItemInfo.leftItem;
        }
        if (upItem != null)
        {
            upItem.GetComponent<SpawnedItem>().downItem = tempItemInfo.leftItem;
        }
        if (downItem != null)
        {
            downItem.GetComponent<SpawnedItem>().upItem = tempItemInfo.leftItem;
        }

        //GameManager column List Update just in case.
        Columns[ColumnId].Column[ColumnPosition] = GameObject.Find("[" + (ColumnId) + "," + (ColumnPosition) + "]");
        Columns[tempColumnID].Column[tempColumnPosition] = GameObject.Find("[" + (tempColumnID) + "," + (tempColumnPosition) + "]");
        StartCoroutine(Reset());
    }
    public void moveRight()
    {
        moveID = 2;
        ClearList();
        Columns = GameObject.Find("GameManager").GetComponent<Spawner>().Columns;
        SpawnedItem tempItemInfo = rightItem.GetComponent<SpawnedItem>();
        CopyValuesToTemp();

        //move transition
        Vector3 tempItemPosition = tempItemInfo.transform.position;
        Vector3 currentItemPosition = gameObject.transform.position;
        tempItemInfo.transform.DOMove(currentItemPosition, switchingTime);
        gameObject.transform.DOMove(tempItemPosition, switchingTime);
        gameObject.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);
        tempItemInfo.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);


        //holding item info replaced with temp
        ColumnId = tempItemInfo.ColumnId;
        ColumnPosition = tempItemInfo.ColumnPosition;
        leftItem = tempRight;
        rightItem = tempItemInfo.rightItem;
        upItem = tempItemInfo.upItem;
        downItem = tempItemInfo.downItem;
        gameObject.transform.name = "[" + (tempItemInfo.ColumnId) + "," + (tempItemInfo.ColumnPosition) + "]";
        gameObject.transform.parent = tempItemInfo.transform.parent;

        //tempitem info replaced with holdingitem --- pay attention to the code sequences
        tempItemInfo.ColumnId = tempColumnID;
        tempItemInfo.ColumnPosition = tempColumnPosition;
        tempItemInfo.rightItem = tempItemInfo.leftItem;
        tempItemInfo.leftItem = tempLeft;
        tempItemInfo.upItem = tempUp;
        tempItemInfo.downItem = tempDown;
        tempItemInfo.gameObject.transform.name = "[" + (tempColumnID) + "," + (tempColumnPosition) + "]";
        tempItemInfo.transform.parent = tempParent;


        //updating effected items
        if (tempLeft != null)
        {
            tempLeft.GetComponent<SpawnedItem>().rightItem = tempRight;
        }
        if (tempDown != null)
        {
            tempDown.GetComponent<SpawnedItem>().upItem = tempRight;
        }
        if (tempUp != null)
        {
            tempUp.GetComponent<SpawnedItem>().downItem = tempRight;
        }
        if (rightItem != null)
        {
            rightItem.GetComponent<SpawnedItem>().leftItem = tempItemInfo.rightItem;
        }
        if (upItem != null)
        {
            upItem.GetComponent<SpawnedItem>().downItem = tempItemInfo.rightItem;
        }
        if (downItem != null)
        {
            downItem.GetComponent<SpawnedItem>().upItem = tempItemInfo.rightItem;
        }

        //GameManager column List Update just in case.
        Columns[ColumnId].Column[ColumnPosition] = GameObject.Find("[" + (ColumnId) + "," + (ColumnPosition) + "]");
        Columns[tempColumnID].Column[tempColumnPosition] = GameObject.Find("[" + (tempColumnID) + "," + (tempColumnPosition) + "]");
        StartCoroutine(Reset());

    }

    public void moveDown()
    {
        moveID = 3;
        ClearList();
        Columns = GameObject.Find("GameManager").GetComponent<Spawner>().Columns;
        SpawnedItem tempItemInfo = downItem.GetComponent<SpawnedItem>();
        CopyValuesToTemp();

        //move transition
        Vector3 tempItemPosition = tempItemInfo.transform.position;
        Vector3 currentItemPosition = gameObject.transform.position;
        tempItemInfo.transform.DOMove(currentItemPosition, switchingTime);
        gameObject.transform.DOMove(tempItemPosition, switchingTime);
        gameObject.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
        tempItemInfo.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);


        //holding item info replaced with temp
        ColumnId = tempItemInfo.ColumnId;
        ColumnPosition = tempItemInfo.ColumnPosition;
        leftItem = tempItemInfo.leftItem;
        rightItem = tempItemInfo.rightItem;
        upItem = tempDown;
        downItem = tempItemInfo.downItem;
        gameObject.transform.name = "[" + (tempItemInfo.ColumnId) + "," + (tempItemInfo.ColumnPosition) + "]";
        gameObject.transform.parent = tempItemInfo.transform.parent;

        //tempitem info replaced with holdingitem ---pay attention to the code sequences
        tempItemInfo.ColumnId = tempColumnID;
        tempItemInfo.ColumnPosition = tempColumnPosition;
        tempItemInfo.downItem = tempItemInfo.upItem;
        tempItemInfo.leftItem = tempLeft;
        tempItemInfo.rightItem = tempRight;
        tempItemInfo.upItem = tempUp;
        tempItemInfo.gameObject.transform.name = "[" + (tempColumnID) + "," + (tempColumnPosition) + "]";
        tempItemInfo.transform.parent = tempParent;

        //updating effected items
        if (tempLeft != null)
        {
            tempLeft.GetComponent<SpawnedItem>().rightItem = tempDown;
        }
        if (tempRight != null)
        {
            tempRight.GetComponent<SpawnedItem>().leftItem = tempDown;
        }
        if (tempUp != null)
        {
            tempUp.GetComponent<SpawnedItem>().downItem = tempDown;
        }
        if (rightItem != null)
        {
            rightItem.GetComponent<SpawnedItem>().leftItem = tempItemInfo.downItem;
        }
        if (leftItem != null)
        {
            leftItem.GetComponent<SpawnedItem>().rightItem = tempItemInfo.downItem;
        }
        if (downItem != null)
        {
            downItem.GetComponent<SpawnedItem>().upItem = tempItemInfo.downItem;
        }


        //GameManager column List Update just in case.
        Columns[ColumnId].Column[ColumnPosition] = GameObject.Find("[" + (ColumnId) + "," + (ColumnPosition) + "]");
        Columns[tempColumnID].Column[tempColumnPosition] = GameObject.Find("[" + (tempColumnID) + "," + (tempColumnPosition) + "]");
        StartCoroutine(Reset());
    }

    public void moveUp()
    {
        moveID = 4;
        ClearList();
        Columns = GameObject.Find("GameManager").GetComponent<Spawner>().Columns;
        SpawnedItem tempItemInfo = upItem.GetComponent<SpawnedItem>();
        CopyValuesToTemp();

        //move transition
        Vector3 tempItemPosition = tempItemInfo.transform.position;
        Vector3 currentItemPosition = gameObject.transform.position;
        tempItemInfo.transform.DOMove(currentItemPosition, switchingTime);
        gameObject.transform.DOMove(tempItemPosition, switchingTime);
        gameObject.transform.DOBlendableRotateBy(new Vector3(-360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
        tempItemInfo.transform.DOBlendableRotateBy(new Vector3(-360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);

        //holding item info replaced with temp
        ColumnId = tempItemInfo.ColumnId;
        ColumnPosition = tempItemInfo.ColumnPosition;
        leftItem = tempItemInfo.leftItem;
        rightItem = tempItemInfo.rightItem;
        upItem = tempItemInfo.upItem;
        downItem = tempUp;
        gameObject.transform.name = "[" + (tempItemInfo.ColumnId) + "," + (tempItemInfo.ColumnPosition) + "]";
        gameObject.transform.parent = tempItemInfo.transform.parent;

        //tempitem info replaced with holdingitem --- pay attention to the code sequences
        tempItemInfo.ColumnId = tempColumnID;
        tempItemInfo.ColumnPosition = tempColumnPosition;
        tempItemInfo.leftItem = tempLeft;
        tempItemInfo.rightItem = tempRight;
        tempItemInfo.upItem = tempItemInfo.downItem;
        tempItemInfo.downItem = tempDown;
        tempItemInfo.gameObject.transform.name = "[" + (tempColumnID) + "," + (tempColumnPosition) + "]";
        tempItemInfo.transform.parent = tempParent;

        //updating effected items
        if (tempLeft != null)
        {
            tempLeft.GetComponent<SpawnedItem>().rightItem = tempUp;
        }
        if (tempRight != null)
        {
            tempRight.GetComponent<SpawnedItem>().leftItem = tempUp;
        }
        if (tempDown != null)
        {
            tempDown.GetComponent<SpawnedItem>().upItem = tempUp;
        }
        if (rightItem != null)
        {
            rightItem.GetComponent<SpawnedItem>().leftItem = tempItemInfo.upItem;
        }
        if (leftItem != null)
        {
            leftItem.GetComponent<SpawnedItem>().rightItem = tempItemInfo.upItem;
        }
        if (upItem != null)
        {
            upItem.GetComponent<SpawnedItem>().downItem = tempItemInfo.upItem;
        }

        //GameManager column List Update just in case.
        Columns[ColumnId].Column[ColumnPosition] = GameObject.Find("[" + (ColumnId) + "," + (ColumnPosition) + "]");
        Columns[tempColumnID].Column[tempColumnPosition] = GameObject.Find("[" + (tempColumnID) + "," + (tempColumnPosition) + "]");
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
      
        if (leftItem != null)
        {
            leftItem.GetComponent<SpawnedItem>().CheckVerticalMatchedItems();
            leftItem.GetComponent<SpawnedItem>().CheckHorizontonalMatchedItems();
        }
        if (rightItem != null)
        {
            rightItem.GetComponent<SpawnedItem>().CheckVerticalMatchedItems();
            rightItem.GetComponent<SpawnedItem>().CheckHorizontonalMatchedItems();
        }
        if (upItem != null)
        {
            upItem.GetComponent<SpawnedItem>().CheckVerticalMatchedItems();
            upItem.GetComponent<SpawnedItem>().CheckHorizontonalMatchedItems();
        }
        if (downItem != null)
        {
            downItem.GetComponent<SpawnedItem>().CheckVerticalMatchedItems();
            downItem.GetComponent<SpawnedItem>().CheckHorizontonalMatchedItems();
        }
        if (tempDown != null)
        {
            tempDown.GetComponent<SpawnedItem>().CheckVerticalMatchedItems();
            tempDown.GetComponent<SpawnedItem>().CheckHorizontonalMatchedItems();
        }
        if (tempUp != null)
        {
            tempUp.GetComponent<SpawnedItem>().CheckVerticalMatchedItems();
            tempUp.GetComponent<SpawnedItem>().CheckHorizontonalMatchedItems();
        }
        if (tempLeft != null)
        {
            tempLeft.GetComponent<SpawnedItem>().CheckVerticalMatchedItems();
            tempLeft.GetComponent<SpawnedItem>().CheckHorizontonalMatchedItems();
        }
        if (tempRight != null)
        {
            tempRight.GetComponent<SpawnedItem>().CheckVerticalMatchedItems();
            tempRight.GetComponent<SpawnedItem>().CheckHorizontonalMatchedItems();
        }

        CheckAll();


        yield return new WaitForSecondsRealtime(switchingTime);

    }


    //inside movings before take temp values of this item
    void CopyValuesToTemp()
    {
        tempColumnID = ColumnId;
        tempColumnPosition = ColumnPosition;
        tempLeft = leftItem;
        tempRight = rightItem;
        tempDown = downItem;
        tempUp = upItem;
        tempParent = gameObject.transform.parent;
    }

    public void CheckAll()
    {      
        CheckVerticalMatchedItems();
        CheckHorizontonalMatchedItems();
        StartCoroutine(DestroyOnMatch());
    }


    public void CheckVerticalMatchedItems()
    {
        if (upItem != null)
        {
            if (upItem.GetComponent<SpawnedItem>().CategoryId == CategoryId && !matchedItemsVertical.Contains(upItem))
            {
                matchedItemsVertical.Add(upItem);
                if (!matchedItemsVertical.Contains(this.gameObject))
                {
                    matchedItemsVertical.Add(this.gameObject);
                }
                foreach (var item in upItem.GetComponent<SpawnedItem>().matchedItemsVertical)
                {
                    if (!matchedItemsVertical.Contains(item))
                    {
                        matchedItemsVertical.Add(item);
                        item.GetComponent<SpawnedItem>().matchedItemsVertical.Add(this.gameObject);
                    }
                }
            }
        }
        if (downItem != null)
        {
            if (downItem.GetComponent<SpawnedItem>().CategoryId == CategoryId && !matchedItemsVertical.Contains(downItem))
            {
                matchedItemsVertical.Add(downItem);
                if (!matchedItemsVertical.Contains(this.gameObject))
                {
                    matchedItemsVertical.Add(this.gameObject);
                }
                foreach (var item in downItem.GetComponent<SpawnedItem>().matchedItemsVertical)
                {
                    if (!matchedItemsVertical.Contains(item))
                    {
                        matchedItemsVertical.Add(item);
                        item.GetComponent<SpawnedItem>().matchedItemsVertical.Add(this.gameObject);
                    }
                }
            }
        }

    }

    public void CheckHorizontonalMatchedItems()
    {
        if (leftItem != null)
        {
            if (leftItem.GetComponent<SpawnedItem>().CategoryId == CategoryId && !matchedItemsHorizontonal.Contains(leftItem))
            {
                matchedItemsHorizontonal.Add(leftItem);
                if (!matchedItemsHorizontonal.Contains(this.gameObject))
                {
                    matchedItemsHorizontonal.Add(this.gameObject);

                }
                foreach (var item in leftItem.GetComponent<SpawnedItem>().matchedItemsHorizontonal)
                {
                    if (!matchedItemsHorizontonal.Contains(item))
                    {
                        matchedItemsHorizontonal.Add(item);
                        item.GetComponent<SpawnedItem>().matchedItemsHorizontonal.Add(this.gameObject);
                    }
                }
            }
        }
        if (rightItem != null)
        {
            if (rightItem.GetComponent<SpawnedItem>().CategoryId == CategoryId)
            {
                if (!matchedItemsHorizontonal.Contains(rightItem))
                {
                    matchedItemsHorizontonal.Add(rightItem);
                    if (!matchedItemsHorizontonal.Contains(this.gameObject))
                    {
                        matchedItemsHorizontonal.Add(this.gameObject);
                    }
                    foreach (var item in rightItem.GetComponent<SpawnedItem>().matchedItemsHorizontonal)
                    {
                        if (!matchedItemsHorizontonal.Contains(item))
                        {
                            matchedItemsHorizontonal.Add(item);
                            item.GetComponent<SpawnedItem>().matchedItemsHorizontonal.Add(this.gameObject);
                        }
                    }
                }
            }
        }
    }



    IEnumerator DestroyOnMatch()
    {
        yield return new WaitForSecondsRealtime(.5f);
        if (matchedItemsVertical.Count >= 3)
        {
            ismatchingFound = true;
            foreach (var item in matchedItemsVertical)
            {
                if (item != null && item != this.gameObject)
                {
                    item.GetComponent<SpawnedItem>().ismatchingFound = true;
                    item.GetComponent<SpriteRenderer>().sprite = blank;
                    item.GetComponent<SpawnedItem>().CategoryId = 0;
                }
            }
        }

        if (matchedItemsHorizontonal.Count >= 3)
        {
            ismatchingFound = true;
            foreach (var item in matchedItemsHorizontonal)
            {
                if (item != null && item != this.gameObject)
                {
                    item.GetComponent<SpawnedItem>().ismatchingFound = true;
                    item.GetComponent<SpriteRenderer>().sprite = blank;
                    item.GetComponent<SpawnedItem>().CategoryId = 0;
                }
            }
        }
        if (ismatchingFound)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = blank;
            gameObject.GetComponent<SpawnedItem>().CategoryId = 0;

            switch (moveID)
            {
                case 1:
                    if (rightItem != null)
                    {
                        rightItem.GetComponent<SpawnedItem>().CheckAll();
                        moveID = 0;
                    }
                    break;
                case 2:
                    if (leftItem != null)
                    {
                        leftItem.GetComponent<SpawnedItem>().CheckAll();
                        moveID = 0;
                    }

                    break;
                case 3:
                    if (upItem != null)
                    {
                        upItem.GetComponent<SpawnedItem>().CheckAll();
                        moveID = 0;
                    }

                    break;
                case 4:
                    if (downItem != null)
                    {
                        downItem.GetComponent<SpawnedItem>().CheckAll();
                        moveID = 0;
                    }
                    break;


                default:
                    break;
            }
        }
        else if (!ismatchingFound)
        {
            switch (moveID)
            {
                case 1:
                    if (rightItem != null)
                    {
                        if (!rightItem.GetComponent<SpawnedItem>().ismatchingFound)
                        {
                            moveRight();
                            moveID = 0;
                        }
                    }
                    break;
                case 2:
                    if (leftItem != null)
                    {
                        if (!leftItem.GetComponent<SpawnedItem>().ismatchingFound)
                        {
                            moveLeft();
                            moveID = 0;
                        }
                    }

                    break;
                case 3:
                    if (upItem != null)
                    {
                        if (!upItem.GetComponent<SpawnedItem>().ismatchingFound)
                        {
                            moveUp();
                            moveID = 0;
                        }
                    }

                    break;
                case 4:
                    if (downItem != null)
                    {
                        if (!downItem.GetComponent<SpawnedItem>().ismatchingFound)
                        {
                            moveDown();
                            moveID = 0;
                        }
                    }
                    break;


                default:
                    break;
            }


        }

        FindObjectOfType<SelectManager>().moving = false;
    }



    public void ClearList()
    {
        matchedItemsVertical.Clear();
        matchedItemsHorizontonal.Clear();
    }



    public void AddHorizontonalMatchedItems(GameObject matchedItemref)
    {
        foreach (var item in matchedItemref.GetComponent<SpawnedItem>().matchedItemsHorizontonal)
        {
            matchedItemsHorizontonal.Add(item);
        }
    }

    public void AddVerticalMatchedItems(GameObject matchedItemref)
    {
        foreach (var item in matchedItemref.GetComponent<SpawnedItem>().matchedItemsVertical)
        {
            matchedItemsVertical.Add(item);
        }
    }
}
