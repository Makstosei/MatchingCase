using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawner : MonoBehaviour
{
    public List<GameObject> ObjectstoSpawn;
    public int xLenght = 8, Ylenght = 8;
    public GameObject SpawnedObjectParent;
    public List<ColumnClass> Columns;
    private int ItemIdCounter;

    private void OnEnable()
    {
        EventManager.onUpdateItems += UpdateItemsEvent;
    }

    private void OnDisable()
    {
        EventManager.onUpdateItems -= UpdateItemsEvent;
    }



    private void Awake()
    {

        Columns = new List<ColumnClass>(Ylenght);

        for (int i = 1; i < Ylenght + 1; i++)
        {
            GameObject tempgameobj = new GameObject("Column " + i);
            tempgameobj.transform.parent = SpawnedObjectParent.transform;
            Columns.Add(new ColumnClass());

            for (int x = 1; x < xLenght + 1; x++)
            {
                Vector3 SpawnPosition = new Vector3(1.5f * i, 1.5f * x, 0);
                int random = GenerateRandomInt(i, x);
                var spawnedObj = Instantiate(ObjectstoSpawn[random], SpawnPosition, Quaternion.identity);
                spawnedObj.name = "[" + (i - 1) + "," + (x - 1) + "]";
                spawnedObj.transform.parent = tempgameobj.transform;
                spawnedObj.GetComponent<SpawnedItem>().ColumnId = i - 1;
                spawnedObj.GetComponent<SpawnedItem>().ColumnPosition = x - 1;
                ItemIdCounter++;
                spawnedObj.GetComponent<SpawnedItem>().ItemId = ItemIdCounter;
                Columns[i - 1].Column.Add(spawnedObj);
                if (x > 1)
                {
                    spawnedObj.GetComponent<SpawnedItem>().downItem = Columns[i - 1].Column[x - 2].gameObject;
                    Columns[i - 1].Column[x - 2].GetComponent<SpawnedItem>().upItem = spawnedObj;
                }
                if (i > 1)
                {
                    spawnedObj.GetComponent<SpawnedItem>().leftItem = Columns[i - 2].Column[x - 1].gameObject;
                    Columns[i - 2].Column[x - 1].GetComponent<SpawnedItem>().rightItem = spawnedObj;
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

    void UpdateItemsEvent()
    {
        //foreach (var column in Columns)
        //{
        //    foreach (var item in column.Column)
        //    {
        //        if (Columns[item.GetComponent<SpawnedItem>().ColumnId - 1] != null && Columns[item.GetComponent<SpawnedItem>().ColumnId - 1].Column[item.GetComponent<SpawnedItem>().ColumnPosition] != null)
        //        {
        //            item.GetComponent<SpawnedItem>().leftItem = Columns[item.GetComponent<SpawnedItem>().ColumnId - 1].Column[item.GetComponent<SpawnedItem>().ColumnPosition];
        //        }
        //        if (Columns[item.GetComponent<SpawnedItem>().ColumnId + 1].Column[item.GetComponent<SpawnedItem>().ColumnPosition] != null)
        //        {
        //            item.GetComponent<SpawnedItem>().rightItem = Columns[item.GetComponent<SpawnedItem>().ColumnId + 1].Column[item.GetComponent<SpawnedItem>().ColumnPosition];
        //        }
        //        if (Columns[item.GetComponent<SpawnedItem>().ColumnId].Column[item.GetComponent<SpawnedItem>().ColumnPosition + 1] != null)
        //        {
        //            item.GetComponent<SpawnedItem>().upItem = Columns[item.GetComponent<SpawnedItem>().ColumnId].Column[item.GetComponent<SpawnedItem>().ColumnPosition + 1];
        //        }
        //        if (Columns[item.GetComponent<SpawnedItem>().ColumnId].Column[item.GetComponent<SpawnedItem>().ColumnPosition - 1] != null)
        //        {
        //            item.GetComponent<SpawnedItem>().downItem = Columns[item.GetComponent<SpawnedItem>().ColumnId].Column[item.GetComponent<SpawnedItem>().ColumnPosition - 1];
        //        }
        //    }
        //}
    }


    [System.Serializable]
    public class ColumnClass
    {
        [HideInInspector]
        public string FontName = "";
        public List<GameObject> Column = new List<GameObject>();
    }
}
