using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawner : MonoBehaviour
{
    public List<GameObject> ObjectstoSpawn;
    public int xLenght = 8, Ylenght = 8;
    public GameObject SpawnedObjectParent;
    public List<RowClass> Rows;

    void Start()
    {
        Rows = new List<RowClass>(Ylenght);

        for (int i = 1; i < Ylenght + 1; i++)
        {
            GameObject tempgameobj = new GameObject("Row " + i);
            tempgameobj.transform.parent = SpawnedObjectParent.transform;
            Rows.Add(new RowClass());

            for (int x = 1; x < xLenght + 1; x++)
            {
                Vector3 SpawnPosition = new Vector3(1.5f * x, 1.5f * i, 0);
                int random = CheckRow(i, x);
                // int random = Random.Range(0, ObjectstoSpawn.Count);
                var spawnedObj = Instantiate(ObjectstoSpawn[random], SpawnPosition, Quaternion.identity);
                spawnedObj.transform.parent = tempgameobj.transform;
                spawnedObj.GetComponent<SpawnedItem>().Rowid = i - 1;
                spawnedObj.GetComponent<SpawnedItem>().RowPosition = x - 1;
                Rows[i - 1].Row.Add(spawnedObj);
            }

        }
    }



    int CheckRow(int rowid, int rowPosition)
    {
        while (true)
        {
            int random = Random.Range(0, ObjectstoSpawn.Count);
            if (rowPosition >= 3 && rowid < 3)
            {
                if (ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId == Rows[rowid - 1].Row[rowPosition - 2].GetComponent<SpawnedItem>().CategoryId
                    && ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId == Rows[rowid - 1].Row[rowPosition - 3].GetComponent<SpawnedItem>().CategoryId)
                {
                    CheckRow(rowid, rowPosition);
                }
                else
                {
                    return random;
                }
            }
            else if (rowPosition >= 3 && rowid >= 3)
            {
                if (ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Rows[rowid - 1].Row[rowPosition - 2].GetComponent<SpawnedItem>().CategoryId
                || ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Rows[rowid - 1].Row[rowPosition - 3].GetComponent<SpawnedItem>().CategoryId)
                {
                    if (ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Rows[rowid - 2].Row[rowPosition - 1].GetComponent<SpawnedItem>().CategoryId
                    || ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Rows[rowid - 3].Row[rowPosition - 1].GetComponent<SpawnedItem>().CategoryId)
                    {
                        return random;
                    }
                    else
                    {
                        CheckRow(rowid, rowPosition);
                    }

                }
                else
                {
                    CheckRow(rowid, rowPosition);
                }
            }
            else if (rowPosition < 3 && rowid >= 3)
            {
                if (ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Rows[rowid - 2].Row[rowPosition - 1].GetComponent<SpawnedItem>().CategoryId
                || ObjectstoSpawn[random].GetComponent<SpawnedItem>().CategoryId != Rows[rowid - 3].Row[rowPosition - 1].GetComponent<SpawnedItem>().CategoryId)
                {
                    return random;
                }
                else
                {
                    CheckRow(rowid, rowPosition);
                }
            }
            else
            {
                return random;
            }

        }
    }





    [System.Serializable]
    public class RowClass
    {
        [HideInInspector]
        public string FontName = "";
        public List<GameObject> Row = new List<GameObject>();
    }
}
