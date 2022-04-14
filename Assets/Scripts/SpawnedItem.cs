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
    public List<Spawner.ColumnClass> Columns;



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
        CheckMatcedItems(matchedItemsVerticalList, matchedItemsHorizontonalList);
    }

    public void UpdateItemPosition()
    {
        Columns = FindObjectOfType<Spawner>().Columns;
        ColumnPosition = Columns[ColumnId].Column.IndexOf(this);
    }

    public void UpdateNearItems()
    {
        Spawner spawnerRef = FindObjectOfType<Spawner>();
        Columns = FindObjectOfType<Spawner>().Columns;

        if (ColumnId >= 1 && ColumnPosition <= Columns[ColumnId - 1].Column.Count - 1)
        {
            leftItem = Columns[ColumnId - 1].Column[ColumnPosition];
            leftItem.rightItem = this;
        }
        else
        {
            leftItem = null;
        }


        if (ColumnId < spawnerRef.XLenght - 1 && ColumnPosition <= Columns[ColumnId + 1].Column.Count - 1)
        {
            rightItem = Columns[ColumnId + 1].Column[ColumnPosition];
            rightItem.leftItem = this;
        }
        else
        {
            rightItem = null;
        }





        if (ColumnPosition >= 1 && Columns[ColumnId].Column[ColumnPosition - 1] != null)
        {
            downItem = Columns[ColumnId].Column[ColumnPosition - 1];
            downItem.upItem = this;
        }
        else
        {
            downItem = null;
        }



        if (ColumnPosition <= Columns[ColumnId].Column.Count - 2)
        {
            upItem = Columns[ColumnId].Column[ColumnPosition+1];
            upItem.downItem = this;
        }
        else
        {
            upItem = null;
        }
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
