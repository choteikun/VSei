using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

public class RythmGameCanvas : MonoBehaviour
{
    public GameObject perfectEffectPrefab;
    public GameObject goodEffectPrefab;
    public void PerfectEffect()
    {
        PoolManager.Release(perfectEffectPrefab);//生成PerfectEffect
    }
    public void GoodEffect()
    {
        PoolManager.Release(goodEffectPrefab);//生成GoodEffect
    }
    public void ResetAnimTime()
    {

    }
}
