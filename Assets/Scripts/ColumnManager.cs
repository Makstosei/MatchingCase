using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColumnManager : MonoBehaviour
{
    public List<Column> Columns = new List<Column>();

    public class Column
    {
        public List<GameObject> ColumnElements = new List<GameObject>();
    }


}
