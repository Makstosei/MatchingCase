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
                int random = CheckColumn(i, x);
                // int random = Random.Range(0, ObjectstoSpawn.Count);
                var spawnedObj = Instantiate(ObjectstoSpawn[random], SpawnPosition, Quaternion.identity);
                spawnedObj.name = "[" + (i - 1) + "," + (x - 1) + "]";
                spawnedObj.transform.parent = tempgameobj.transform;
                spawnedObj.GetComponent<SpawnedItem>().ColumnId = i - 1;
                spawnedObj.GetComponent<SpawnedItem>().ColumnPosition = x - 1;
                Columns[i - 1].Column.Add(spawnedObj);
                if (x>1)
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
    }

    


    int CheckColumn(int Columnid, int ColumnPosition)
    {
        while (true)
        {
            int random = Random.Range(0, ObjectstoSpawn.Count);
            if (ColumnPosition >= 3 && Columnid < 3)
            {
                if (ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId == Columns[Columnid - 1].Column[ColumnPosition - 2].GetComponent<SpawnedItem>().CategoryId
                    && ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId == Columns[Columnid - 1].Column[ColumnPosition - 3].GetComponent<SpawnedItem>().CategoryId)
                {
                    CheckColumn(Columnid, ColumnPosition);
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
                        CheckColumn(Columnid, ColumnPosition);
                    }

                }
                else
                {
                    CheckColumn(Columnid, ColumnPosition);
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
                    CheckColumn(Columnid, ColumnPosition);
                }
            }
            else
            {
                return random;
            }

        }
    }





    [System.Serializable]
    public class ColumnClass
    {
        [HideInInspector]
        public string FontName = "";
        public List<GameObject> Column = new List<GameObject>();
    }
}
