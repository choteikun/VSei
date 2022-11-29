using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMeshRenderCtrl : MonoBehaviour
{
    public bool dropMeshRenderOpen;
    void Start()
    {
        dropMeshRenderOpen = true;
    }

    public void MeshRenderOpen()
    {
        dropMeshRenderOpen = true;
    }
    public void MeshRenderClose()
    {
        dropMeshRenderOpen = false;
    }
}
