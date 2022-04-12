using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[System.Serializable]
public class Spawner : MonoBehaviour
{
    public List<GameObject> ObjectstoSpawn;
    public int YLenght = 8, XLenght = 8;
    public GameObject SpawnedObjectParent;
    public List<ColumnClass> Columns;
    private int ItemIdCounter;
    public List<string> UpdateColumnIdList = new List<string>();

    private void OnEnable()
    {
        EventManager.onSomeObjectsDestroyed += UpdateItemsPositionsEvent;
    }

    private void OnDisable()
    {
        EventManager.onSomeObjectsDestroyed -= UpdateItemsPositionsEvent;
    }



    private void Awake()
    {
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {

        Columns = new List<ColumnClass>(XLenght);

        for (int i = 1; i < XLenght + 1; i++)
        {
            GameObject tempgameobj = new GameObject("Column " + (i - 1));
            tempgameobj.transform.parent = SpawnedObjectParent.transform;
            Columns.Add(new ColumnClass());

            for (int x = 1; x < YLenght + 1; x++)
            {
                yield return new WaitForSecondsRealtime(.01f);
                Vector3 SpawnPosition = new Vector3(1.5f * i, 1.5f * x, 0);
                int random = GenerateRandomInt(i, x);
                var spawnedObj = Instantiate(ObjectstoSpawn[random], SpawnPosition, Quaternion.identity);
                spawnedObj.name = "[" + (i - 1) + "," + (x - 1) + "]";
                spawnedObj.transform.parent = tempgameobj.transform;
                spawnedObj.GetComponent<SpawnedItem>().ColumnId = i - 1;
                spawnedObj.GetComponent<SpawnedItem>().ColumnPosition = x - 1;
                ItemIdCounter++;
                spawnedObj.GetComponent<SpawnedItem>().ItemId = ItemIdCounter;
                Columns[i - 1].Column.Add(spawnedObj.GetComponent<SpawnedItem>());
                if (x > 1)
                {
                    spawnedObj.GetComponent<SpawnedItem>().downItem = Columns[i - 1].Column[x - 2];
                    Columns[i - 1].Column[x - 2].GetComponent<SpawnedItem>().upItem = spawnedObj.GetComponent<SpawnedItem>();
                }
                if (i > 1)
                {
                    spawnedObj.GetComponent<SpawnedItem>().leftItem = Columns[i - 2].Column[x - 1];
                    Columns[i - 2].Column[x - 1].GetComponent<SpawnedItem>().rightItem = spawnedObj.GetComponent<SpawnedItem>();
                }
            }
        }
        EventManager.Instance.GameStart();
    }


    int GenerateRandomInt(int Columnid, int ColumnPosition)
    {
        while (true)
        {
            int random = Random.Range(0, ObjectstoSpawn.Count);
            if (ColumnPosition >= 3 && Columnid < 3)
            {
                if (ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId == Columns[Columnid - 1].Column[ColumnPosition - 2].GetComponent<SpawnedItem>().CategoryId
                    && ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId == Columns[Columnid - 1].Column[ColumnPosition - 3].GetComponent<SpawnedItem>().CategoryId)
                {
                    GenerateRandomInt(Columnid, ColumnPosition);
                }
                else
                {
                    return random;
                }
            }
            else if (ColumnPosition >= 3 && Columnid >= 3)
            {
                if (ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Columns[Columnid - 1].Column[ColumnPosition - 2].GetComponent<SpawnedItem>().CategoryId
                || ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Columns[Columnid - 1].Column[ColumnPosition - 3].GetComponent<SpawnedItem>().CategoryId)
                {
                    if (ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Columns[Columnid - 2].Column[ColumnPosition - 1].GetComponent<SpawnedItem>().CategoryId
                    || ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Columns[Columnid - 3].Column[ColumnPosition - 1].GetComponent<SpawnedItem>().CategoryId)
                    {
                        return random;
                    }
                    else
                    {
                        GenerateRandomInt(Columnid, ColumnPosition);
                    }

                }
                else
                {
                    GenerateRandomInt(Columnid, ColumnPosition);
                }
            }
            else if (ColumnPosition < 3 && Columnid >= 3)
            {
                if (ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Columns[Columnid - 2].Column[ColumnPosition - 1].GetComponent<SpawnedItem>().CategoryId
                || ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Columns[Columnid - 3].Column[ColumnPosition - 1].GetComponent<SpawnedItem>().CategoryId)
                {
                    return random;
                }
                else
                {
                    GenerateRandomInt(Columnid, ColumnPosition);
                }
            }
            else
            {
                return random;
            }

        }
    }

    void UpdateItemsPositionsEvent(List<SpawnedItem> DestroyedObjects)
    {
        CheckColumnsItemsCount(DestroyedObjects);
    }

    void CheckColumnsItemsCount(List<SpawnedItem> DestroyedObjects)
    {
        UpdateColumnIdList.Clear();
        foreach (var item in DestroyedObjects)
        {
            Columns[item.ColumnId].Column.Remove(item);
            Destroy(item);         
        }

        CheckAndUpdatePositions();
    }

    void CheckAndUpdatePositions()
    {
        foreach (var itemList in Columns)
        {
            if (itemList.Column.Count<YLenght)
            {
                foreach (var spawnedItem in itemList.Column)
                {
                    int columnId = Columns.IndexOf(itemList);
                    int itemId = Columns[columnId].Column.IndexOf(spawnedItem);
                    Vector3 newPosition = new Vector3(1.5f + 1.5f *columnId, 1.5f + 1.5f *itemId, 0);
                    spawnedItem.transform.DOMove(newPosition, 1f);
                }


            }



           

           
        }

        FindObjectOfType<SelectManager>().moving = false;

    }









    [System.Serializable]
    public class ColumnClass
    {
        [HideInInspector]
        public string FontName = "";
        public List<SpawnedItem> Column = new List<SpawnedItem>();
    }
}
