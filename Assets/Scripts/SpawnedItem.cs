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
    public SpawnedItem leftItem, rightItem, upItem, downItem;
    public List<SpawnedItem> matchedItemsVerticalList;
    public List<SpawnedItem> matchedItemsHorizontonalList;



    private int tempItemId, tempCategoryId, tempColumnID, tempColumnPosition;
    private SpawnedItem tempLeft, tempRight, tempUp, tempDown;
    private Transform tempParent;
    public Sprite blank;



    public List<Spawner.ColumnClass> Columns;
    public bool ismatchingFound;
    private int moveID;
    public float switchingTime = 0.5f;



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
        CheckMatcedItems(matchedItemsVerticalList,matchedItemsHorizontonalList);
    }

    //inside movings before take temp values of this item
    public void CopyValuesToTemp()
    {
        tempItemId = ItemId;
        tempCategoryId = CategoryId;
        tempColumnID = ColumnId;
        tempColumnPosition = ColumnPosition;
        tempLeft = leftItem;
        tempRight = rightItem;
        tempDown = downItem;
        tempUp = upItem;
        tempParent = gameObject.transform.parent;
    }


    public void CheckMatcedItems(List<SpawnedItem> matchedVerticalItems, List<SpawnedItem> matchedHorizontonalItems)
    {
        matchedItemsVerticalList.Clear();
        matchedItemsHorizontonalList.Clear();
        CheckLeftMatchedItems(matchedHorizontonalItems);
        CheckRightMatchedItems(matchedHorizontonalItems);
        CheckDownMatcedItems(matchedVerticalItems);
        CheckUpMatchedItems(matchedVerticalItems);
    }

    public void CheckUpMatchedItems(List<SpawnedItem> matchedVerticalItems)
    {
        if (upItem != null)
        {
            if (upItem.CategoryId == CategoryId && !matchedVerticalItems.Contains(upItem))
            {
                matchedVerticalItems.Add(upItem);
                if (!matchedVerticalItems.Contains(this))
                {
                    matchedVerticalItems.Add(this);
                }
                upItem.CheckUpMatchedItems(matchedVerticalItems);
            }
        }
    }


    public void CheckDownMatcedItems(List<SpawnedItem> matchedVerticalItems)
    {
        if (downItem != null)
        {
            if (downItem.CategoryId == CategoryId && !matchedVerticalItems.Contains(downItem))
            {
                matchedVerticalItems.Add(downItem);
                if (!matchedVerticalItems.Contains(this))
                {
                    matchedVerticalItems.Add(this);
                }
                downItem.CheckDownMatcedItems(matchedVerticalItems);
            }
        }

    }

    public void CheckLeftMatchedItems(List<SpawnedItem> matchedHorizontonalItems)
    {
        if (leftItem != null)
        {
            if (leftItem.CategoryId == CategoryId && !matchedHorizontonalItems.Contains(leftItem))
            {
                matchedHorizontonalItems.Add(leftItem);
                if (!matchedHorizontonalItems.Contains(this))
                {
                    matchedHorizontonalItems.Add(this);
                }
                leftItem.CheckLeftMatchedItems(matchedHorizontonalItems);

            }
        }

    }


    public void CheckRightMatchedItems(List<SpawnedItem> matchedHorizontonalItems)
    {
        if (rightItem != null)
        {
            if (rightItem.GetComponent<SpawnedItem>().CategoryId == CategoryId && !matchedHorizontonalItems.Contains(rightItem))
            {
                matchedHorizontonalItems.Add(rightItem);
                if (!matchedHorizontonalItems.Contains(this))
                {
                    matchedHorizontonalItems.Add(this);
                }
                rightItem.CheckRightMatchedItems(matchedHorizontonalItems);
            }
        }
    }








}
