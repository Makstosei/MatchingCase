using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RowManager : MonoBehaviour
{
    public List<Row> Rows = new List<Row>();

    public class Row
    {
        public List<GameObject> RowElements = new List<GameObject>();
    }


}
