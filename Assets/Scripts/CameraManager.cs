using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    void Start()
    {
        Spawner ColumnsRef = FindObjectOfType<Spawner>();
        Vector3 pos1 = ColumnsRef.Columns[0].Column[0].transform.position;
        Vector3 pos2 = ColumnsRef.Columns[ColumnsRef.Columns.Count - 1].Column[ColumnsRef.Columns[ColumnsRef.Columns.Count - 1].Column.Count - 1].transform.position;
        GameObject cameraPositionRef = new GameObject("cameraPositionRef");
        Vector3 targetpos = (pos1 + pos2) / 2;
        cameraPositionRef.gameObject.transform.position = targetpos;
        GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = cameraPositionRef.transform;
        GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = cameraPositionRef.transform;
    }


}
