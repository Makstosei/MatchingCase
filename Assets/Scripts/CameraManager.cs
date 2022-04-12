using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    int lensDifference;

    



    void Start()
    {
        Spawner ColumnsRef = FindObjectOfType<Spawner>();
        Vector3 pos1 = new Vector3(1.5f,1.5f,0);
        Vector3 pos2 = new Vector3(ColumnsRef.YLenght*1.5f,ColumnsRef.XLenght*1.5f,0);
        GameObject cameraPositionRef = new GameObject("cameraPositionRef");
        Vector3 targetpos = (pos1 + pos2) / 2;
        cameraPositionRef.gameObject.transform.position = targetpos;
        GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = cameraPositionRef.transform;
        GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = cameraPositionRef.transform;
        if (ColumnsRef.YLenght > ColumnsRef.XLenght)
        {
            lensDifference = ColumnsRef.YLenght - 8;
         
        }
        else if (ColumnsRef.XLenght > ColumnsRef.YLenght)
        {
            lensDifference = ColumnsRef.XLenght - 8;
        }
        else
        {
            lensDifference = ColumnsRef.YLenght - 8;
        }
        GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.OrthographicSize = 13 + Mathf.RoundToInt(1.3f * lensDifference);


    }


}
