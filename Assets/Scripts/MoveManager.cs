using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveManager : MonoBehaviour
{
    public Sprite blank;
    public float switchingTime = 0.5f;
    private bool ismatchingFound;
    public List<SpawnedItem> destroyedObjectsList;
    private List<Spawner.ColumnClass> Columns;

    private int effecterItemId;
    private int effecterCategoryId;
    private int effecterColumnId;
    private int effecterColumnPosition;
    private SpawnedItem effecterleftItem, effecterrightItem, effecterupItem, effecterdownItem, effecterTemp;
    private Transform effecterParent;
    private Vector3 effecterPosition;

    private int effectedItemId;
    private int effectedCategoryId;
    private int effectedColumnId;
    private int effectedColumnPosition;
    private SpawnedItem effectedleftItem, effectedrightItem, effectedupItem, effecteddownItem, effectedTemp;
    private Transform effectedParent;
    private Vector3 effectedPosition;
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
        DOTween.ClearCachedTweens();
        DOTween.Clear();
        GetTempValuesEffecter(effecter);
        GetTempValuesEffected(effected);
        Columns = GameObject.Find("GameManager").GetComponent<Spawner>().Columns;
        SetValuesEffecter(effecter);
        SetValuesEffected(effected);

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

                effecter.leftItem = effectedleftItem;
                effecter.downItem = effecteddownItem;
                effecter.upItem = effectedupItem;

                effected.rightItem = effecterrightItem;
                effected.downItem = effecterdownItem;
                effected.upItem = effecterupItem;

                if (effectedleftItem != null)
                {
                    effectedleftItem.rightItem = effecter;
                }
                if (effecteddownItem != null)
                {
                    effecteddownItem.upItem = effecter;
                }
                if (effectedupItem != null)
                {
                    effectedupItem.downItem = effecter;
                }

                if (effecterrightItem != null)
                {
                    effecterrightItem.leftItem = effected;
                }
                if (effecterdownItem != null)
                {
                    effecterdownItem.upItem = effected;
                }
                if (effecterupItem != null)
                {
                    effecterupItem.downItem = effected;
                }
                break;
            case 2://move right
                effecter.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);
                effected.transform.DOBlendableRotateBy(new Vector3(0, 360, 0), switchingTime, RotateMode.LocalAxisAdd);
                effecter.leftItem = effected;
                effected.rightItem = effecter;

                effecter.rightItem = effectedrightItem;
                effecter.downItem = effecteddownItem;
                effecter.upItem = effectedupItem;

                effected.leftItem = effecterleftItem;
                effected.downItem = effecterdownItem;
                effected.upItem = effecterupItem;

                if (effectedrightItem != null)
                {
                    effectedrightItem.leftItem = effecter;
                }
                if (effecteddownItem != null)
                {
                    effecteddownItem.upItem = effecter;
                }
                if (effectedupItem != null)
                {
                    effectedupItem.downItem = effecter;
                }

                if (effecterleftItem != null)
                {
                    effecterleftItem.rightItem = effected;
                }
                if (effecterdownItem != null)
                {
                    effecterdownItem.upItem = effected;
                }
                if (effecterupItem != null)
                {
                    effecterupItem.downItem = effected;
                }
                break;
            case 3://move down
                effecter.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
                effected.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
                effecter.upItem = effected;
                effected.downItem = effecter;

                effecter.leftItem = effectedleftItem;
                effecter.rightItem = effectedrightItem;
                effecter.downItem = effecteddownItem;

                effected.rightItem = effecterrightItem;
                effected.leftItem = effecterleftItem;
                effected.upItem = effecterupItem;

                if (effectedleftItem != null)
                {
                    effectedleftItem.rightItem = effecter;
                }
                if (effectedrightItem != null)
                {
                    effectedrightItem.leftItem = effecter;
                }
                if (effecteddownItem != null)
                {
                    effecteddownItem.upItem = effecter;
                }

                if (effecterrightItem != null)
                {
                    effecterrightItem.leftItem = effected;
                }
                if (effecterleftItem != null)
                {
                    effecterleftItem.rightItem = effected;
                }
                if (effecterupItem != null)
                {
                    effecterupItem.downItem = effected;
                }
                break;
            case 4://move up
                effecter.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
                effected.transform.DOBlendableRotateBy(new Vector3(360, 0, 0), switchingTime, RotateMode.LocalAxisAdd);
                effecter.downItem = effected;
                effected.upItem = effecter;

                effecter.leftItem = effectedleftItem;
                effecter.rightItem = effectedrightItem;
                effecter.upItem = effectedupItem;

                effected.rightItem = effecterrightItem;
                effected.leftItem = effecterleftItem;
                effected.downItem = effecterdownItem;

                if (effectedleftItem != null)
                {
                    effectedleftItem.rightItem = effecter;
                }
                if (effectedrightItem != null)
                {
                    effectedrightItem.leftItem = effecter;
                }
                if (effectedupItem != null)
                {
                    effectedupItem.downItem = effecter;
                }

                if (effecterrightItem != null)
                {
                    effecterrightItem.leftItem = effected;
                }
                if (effecterleftItem != null)
                {
                    effecterleftItem.rightItem = effected;
                }
                if (effecterdownItem != null)
                {
                    effecterdownItem.upItem = effected;
                }
                break;
            default:
                break;
        }
        CheckMatches(effecter, effected, moveDirection, isReverse);
    }

    void CheckMatches(SpawnedItem effecter, SpawnedItem effected, int moveDirection, bool isReverse)
    {

        effecter.CheckMatcedItems(effecter.matchedItemsVerticalList, effecter.matchedItemsHorizontonalList);
        effected.CheckMatcedItems(effected.matchedItemsVerticalList, effected.matchedItemsHorizontonalList);

        StartCoroutine(DestoryMatchedObjectList(effecter, effected, moveDirection, isReverse));
    }

    public IEnumerator DestoryMatchedObjectList(SpawnedItem effecter, SpawnedItem effected, int moveDirection, bool isReverse)
    {
        destroyedObjectsList.Clear();

        if (effecter.matchedItemsVerticalList.Count >= 3)
        {
            ismatchingFound = true;
            foreach (var item in effecter.matchedItemsVerticalList)
            {
                if (!destroyedObjectsList.Contains(item))
                {
                    destroyedObjectsList.Add(item);
                }
            }
        }

        if (effected.matchedItemsVerticalList.Count >= 3)
        {
            ismatchingFound = true;
            foreach (var item in effected.matchedItemsVerticalList)
            {
                if (!destroyedObjectsList.Contains(item))
                {
                    destroyedObjectsList.Add(item);
                }
            }
        }

        if (effecter.matchedItemsHorizontonalList.Count >= 3)
        {
            ismatchingFound = true;
            foreach (var item in effecter.matchedItemsHorizontonalList)
            {
                if (!destroyedObjectsList.Contains(item))
                {
                    destroyedObjectsList.Add(item);
                }
            }
        }

        if (effected.matchedItemsHorizontonalList.Count >= 3)
        {
            ismatchingFound = true;
            foreach (var item in effected.matchedItemsHorizontonalList)
            {
                if (!destroyedObjectsList.Contains(item))
                {
                    destroyedObjectsList.Add(item);
                }
            }
        }

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
                EventManager.Instance.SomeObjectsDestroyed(destroyedObjectsList);
                destroyedObjectsList.Clear();
                ismatchingFound = false;
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
        }
        else if (isReverse)
        {
            FindObjectOfType<SelectManager>().moving = false;
        }

    }


    

    public void DestroyMatchedItemsAtFall()
    {
        if (destroyedObjectsList.Count>=3)
        {
            foreach (var item in destroyedObjectsList)
            {
                item.gameObject.GetComponent<SpriteRenderer>().sprite = blank;
                item.CategoryId = 0;
            }
            EventManager.Instance.SomeObjectsDestroyed(destroyedObjectsList);
            destroyedObjectsList.Clear();
        }
        else
        {
            FindObjectOfType<SelectManager>().moving = false;
        }
        FindObjectOfType<Spawner>().UpdatePositions();
    }

}
