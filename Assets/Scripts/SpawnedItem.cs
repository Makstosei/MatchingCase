using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SpawnedItem : MonoBehaviour
{
    public int CategoryId;
    public int ColumnId;
    public int ColumnPosition;
    public GameObject leftItem, rightItem, upItem, downItem;
    private int tempColumnID, tempColumnPosition;
    private GameObject tempLeft, tempRight, tempUp, tempDown;
    private Transform tempParent;
    public List<Spawner.ColumnClass> Columns;
    public float switchingTime = 0.5f;


    public void moveLeft()
    {
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
        yield return new WaitForSecondsRealtime(switchingTime + .2f);
        FindObjectOfType<SelectManager>().moving = false;

    }


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

}
