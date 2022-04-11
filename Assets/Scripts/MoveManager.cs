using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveManager : MonoBehaviour
{
    public Sprite blank;
    public float switchingTime = 0.5f;
    private bool ismatchingFound;
    private int moveDirection;
    public List<SpawnedItem> destroyedObjectsList;
    private List<Spawner.ColumnClass> Columns;

    public int effecterItemId;
    public int effecterCategoryId;
    public int effecterColumnId;
    public int effecterColumnPosition;
    public SpawnedItem effecterleftItem, effecterrightItem, effecterupItem, effecterdownItem,effecterTemp;
    public Transform effecterParent;
    public Vector3 effecterPosition;

    public int effectedItemId;
    public int effectedCategoryId;
    public int effectedColumnId;
    public int effectedColumnPosition;
    public SpawnedItem effectedleftItem, effectedrightItem, effectedupItem, effecteddownItem,effectedTemp;
    public Transform effectedParent;
    public Vector3 effectedPosition;
    private void OnEnable()
    {
        EventManager.onMoveItems += MoveItems;
    }
    private void OnDisable()
    {
        EventManager.onMoveItems -= MoveItems;
    }


    void GetTempValuesEffecter(SpawnedItem effecter)
    {
        effecterTemp = effecter;
        effecterItemId = effecter.ItemId;
        effecterCategoryId = effecter.CategoryId;
        effecterColumnId = effecter.ColumnId;
        effecterColumnPosition = effecter.ColumnPosition;
        effecterleftItem = effecter.leftItem;
        effecterrightItem = effecter.rightItem;
        effecterupItem = effecter.upItem;
        effecterdownItem = effecter.downItem;
        effecterParent = effecter.transform.parent;
        effecterPosition = effecter.transform.position;
    }
    void GetTempValuesEffected(SpawnedItem effected)
    {
        effectedItemId = effected.ItemId;
        effectedCategoryId = effected.CategoryId;
        effectedColumnId = effected.ColumnId;
        effectedColumnPosition = effected.ColumnPosition;
        effectedleftItem = effected.leftItem;
        effectedrightItem = effected.rightItem;
        effectedupItem = effected.upItem;
        effecteddownItem = effected.downItem;
        effectedParent = effected.transform.parent;
        effectedPosition = effected.transform.position;
        effectedTemp = effected;
    }

    void SetValuesEffecter(SpawnedItem effecter)
    {
        effecter.ItemId = effecterItemId;
        effecter.CategoryId = effecterCategoryId;
        effecter.ColumnId = effectedColumnId;
        effecter.ColumnPosition = effectedColumnPosition;
        effecter.transform.parent = effectedParent;
        effecter.name = ("[" + (effecter.ColumnId) + "," + (effecter.ColumnPosition) + "]");
    }
    void SetValuesEffected(SpawnedItem effected)
    {
        effected.ItemId = effectedItemId;
        effected.CategoryId = effectedCategoryId;
        effected.ColumnId = effecterColumnId;
        effected.ColumnPosition = effecterColumnPosition;
        effected.transform.parent = effecterParent;
        effected.name = ("[" + (effected.ColumnId) + "," + (effected.ColumnPosition) + "]");
    }


    void MoveItems(SpawnedItem effecter, SpawnedItem effected, int moveDirection, bool isReverse)
    {
        Debug.Log("MoveItems");
        GetTempValuesEffecter(effecter);
        GetTempValuesEffected(effected);
        Columns = GameObject.Find("GameManager").GetComponent<Spawner>().Columns;
        SetValuesEffecter(effecter);
        SetValuesEffected(effected);
        effecter.ClearList();
        effected.ClearList();
        effecter.transform.DOMove(effectedPosition, switchingTime);
        effected.transform.DOMove(effecterPosition, switchingTime);




        Columns[effecter.ColumnId].Column[effecter.ColumnPosition] = GameObject.Find("[" + (effecter.ColumnId) + "," + (effecter.ColumnPosition) + "]").GetComponent<SpawnedItem>();
        Columns[effected.ColumnId].Column[effected.ColumnPosition] = GameObject.Find("[" + (effected.ColumnId) + "," + (effected.ColumnPosition) + "]").GetComponent<SpawnedItem>();

        switch (moveDirection)
        {
            case 1://move left
                effecter.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);
                effected.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);
                effecter.rightItem = effected;
                effected.leftItem = effecter;
                if (effectedleftItem != null)
                {
                    effecter.leftItem = effectedleftItem;
                    effectedleftItem.rightItem = effecter;
                }
                if (effecteddownItem != null)
                {
                    effecter.downItem = effecteddownItem;
                    effecteddownItem.upItem = effecter;
                }
                if (effecter.upItem != null)
                {
                    effecter.upItem = effecteddownItem;
                    effectedupItem.downItem = effecter;
                }

                if (effecteddownItem != null)
                {
                    effected.rightItem = effecterrightItem;
                    effecterrightItem.leftItem = effected;
                }
                if (effecteddownItem != null)
                {
                    effected.downItem = effecteddownItem;
                    effecterdownItem.upItem = effected;
                }
                if (effecteddownItem != null)
                {
                    effected.upItem = effecteddownItem;
                    effecterupItem.downItem = effected;
                }
                break;
            case 2://move right
                effecter.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);
                effected.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);
                effecter.leftItem = effected;
                effected.rightItem = effecter;
                if (effectedrightItem != null)
                {
                    effecter.rightItem = effectedrightItem;
                    effectedrightItem.leftItem = effecter;
                }
                if (effecteddownItem != null)
                {
                    effecter.downItem = effecteddownItem;
                    effecteddownItem.upItem = effecter;
                }
                if (effectedupItem != null)
                {
                    effecter.upItem = effectedupItem;
                    effectedupItem.downItem = effecter;
                }

                if (effecterleftItem != null)
                {
                    effected.leftItem = effecterleftItem;
                    effecterleftItem.rightItem = effected;
                }
                if (effecterdownItem != null)
                {
                    effected.downItem = effecterdownItem;
                    effecterdownItem.upItem = effected;
                }
                if (effecterupItem != null)
                {
                    effected.upItem = effecterupItem;
                    effecterupItem.downItem = effected;
                }
                break;
            case 3://move down
                effecter.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
                effected.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
                effecter.upItem = effected;
                effected.downItem = effecter;
                if (effectedleftItem != null)
                {
                    effecter.leftItem = effectedleftItem;
                    effectedleftItem.rightItem = effecter;
                }
                if (effectedrightItem != null)
                {
                    effecter.rightItem = effectedrightItem;
                    effectedrightItem.leftItem = effecter;
                }
                if (effecteddownItem != null)
                {
                    effecter.downItem = effecteddownItem;
                    effecteddownItem.upItem = effecter;
                }

                if (effecterrightItem != null)
                {
                    effected.rightItem = effecterrightItem;
                    effecterrightItem.leftItem = effected;
                }
                if (effecterleftItem != null)
                {
                    effected.leftItem = effecterleftItem;
                    effecterleftItem.rightItem = effected;
                }
                if (effecterupItem != null)
                {
                    effected.upItem = effecterupItem;
                    effecterupItem.downItem = effected;
                }
                break;
            case 4://move up
                effecter.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
                effected.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
                effecter.downItem = effected;
                effected.upItem = effecter;
                if (effectedleftItem != null)
                {
                    effecter.leftItem = effectedleftItem;
                    effectedleftItem.rightItem = effecter;
                }
                if (effectedrightItem != null)
                {
                    effecter.rightItem = effectedrightItem;
                    effectedrightItem.leftItem = effecter;
                }
                if (effectedupItem != null)
                {
                    effecter.upItem = effectedupItem;
                    effectedupItem.downItem = effecter;
                }

                if (effecterrightItem != null)
                {
                    effected.rightItem = effecterrightItem;
                    effecterrightItem.leftItem = effected;
                }
                if (effecterleftItem != null)
                {
                    effected.leftItem = effecterleftItem;
                    effecterleftItem.rightItem = effected;
                }
                if (effecterdownItem != null)
                {
                    effected.downItem = effecterdownItem;
                    effecterdownItem.upItem = effected;
                }
                break;
            default:
                break;
        }

        CheckMatches(effecter, effected, isReverse);
    }


    void CheckMatches(SpawnedItem effecter, SpawnedItem effected, bool isReverse)
    {
        if (effecter.leftItem != null && effecter.leftItem.CategoryId == effecter.CategoryId)
        {
            if (!effecter.matchedItemsHorizontonal.Contains(effecter.leftItem))
            {
                effecter.matchedItemsHorizontonal.Add(effecter.leftItem);
            }
            if (effecter.leftItem.leftItem != null && effecter.leftItem.leftItem.CategoryId == effecter.CategoryId)
            {
                if (!effecter.matchedItemsHorizontonal.Contains(effecter.leftItem.leftItem))
                {
                    effecter.matchedItemsHorizontonal.Add(effecter.leftItem.leftItem);
                }
            }
            if (!effecter.matchedItemsHorizontonal.Contains(effecter))
            {
                effecter.matchedItemsHorizontonal.Add(effecter);
            }
        }
        if (effecter.rightItem != null && effecter.rightItem.CategoryId == effecter.CategoryId)
        {
            if (!effecter.matchedItemsHorizontonal.Contains(effecter.rightItem))
            {
                effecter.matchedItemsHorizontonal.Add(effecter.rightItem);
            }

            if (effecter.rightItem.rightItem != null && effecter.rightItem.rightItem.CategoryId == effecter.CategoryId)
            {
                if (!effecter.matchedItemsHorizontonal.Contains(effecter.rightItem.rightItem))
                {
                    effecter.matchedItemsHorizontonal.Add(effecter.rightItem.rightItem);
                }
            }
            if (!effecter.matchedItemsHorizontonal.Contains(effecter))
            {
                effecter.matchedItemsHorizontonal.Add(effecter);
            }
        }


        if (effecter.upItem != null && effecter.upItem.CategoryId == effecter.CategoryId)
        {
            if (!effecter.matchedItemsVertical.Contains(effecter.upItem))
            {
                effecter.matchedItemsVertical.Add(effecter.upItem);
            }
            if (effecter.upItem.upItem != null && effecter.upItem.upItem.CategoryId == effecter.CategoryId)
            {
                if (!effecter.matchedItemsVertical.Contains(effecter.upItem.upItem))
                {
                    effecter.matchedItemsVertical.Add(effecter.upItem.upItem);
                }
            }
            if (!effecter.matchedItemsVertical.Contains(effecter))
            {
                effecter.matchedItemsVertical.Add(effecter);
            }
        }
        if (effecter.downItem != null && effecter.downItem.CategoryId == effecter.CategoryId)
        {
            if (!effecter.matchedItemsVertical.Contains(effecter.downItem))
            {
                effecter.matchedItemsVertical.Add(effecter.downItem);
            }

            if (effecter.downItem.downItem != null && effecter.downItem.downItem.CategoryId == effecter.CategoryId)
            {
                if (!effecter.matchedItemsVertical.Contains(effecter.downItem.downItem))
                {
                    effecter.matchedItemsVertical.Add(effecter.downItem.downItem);
                }

            }
            if (!effecter.matchedItemsVertical.Contains(effecter))
            {
                effecter.matchedItemsVertical.Add(effecter);
            }
        }

        Debug.Log("CheckMatches");


        StartCoroutine(DestoryMatchedObjectList(effecter, effected, isReverse));
    }



    public IEnumerator DestoryMatchedObjectList(SpawnedItem effecter, SpawnedItem effected, bool isReverse)
    {
        yield return new WaitForSecondsRealtime(0.01f);
        if (effecter.matchedItemsVertical.Count >= 3)
        {
            ismatchingFound = true;
            foreach (var item in effecter.matchedItemsVertical)
            {
                if (!destroyedObjectsList.Contains(item))
                {
                    destroyedObjectsList.Add(item);
                }

            }
        }

        if (effecter.matchedItemsHorizontonal.Count >= 3)
        {
            ismatchingFound = true;
            foreach (var item in effecter.matchedItemsHorizontonal)
            {
                if (!destroyedObjectsList.Contains(item))
                {
                    destroyedObjectsList.Add(item);
                }
            }
        }
        Debug.Log("DestroyObjectsCheck");
        yield return new WaitForSecondsRealtime(switchingTime);
        if (!isReverse)
        {
            if (ismatchingFound)
            {
                foreach (var item in destroyedObjectsList)
                {
                    item.gameObject.GetComponent<SpriteRenderer>().sprite = blank;
                    item.CategoryId = 0;
                }
            }
            else if (!ismatchingFound)
            {
                switch (moveDirection)
                {
                    case 1:
                        EventManager.Instance.MoveItems(effecter, effected, 2, true);
                        break;
                    case 2:
                        EventManager.Instance.MoveItems(effecter, effected, 1, true);
                        break;
                    case 3:
                        EventManager.Instance.MoveItems(effecter, effected, 4, true);
                        break;
                    case 4:
                        EventManager.Instance.MoveItems(effecter, effected, 3, true);
                        break;
                    default:
                        break;
                }
            }
            Debug.Log("Reverse");
        }

        FindObjectOfType<SelectManager>().moving = false;
    }




}
