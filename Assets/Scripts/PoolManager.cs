using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] pools;
    [SerializeField] UIPool[] uiPools;

    public static Dictionary<GameObject, Pool> dictionary;
    public static Dictionary<GameObject, UIPool> uiDictionary;

    Transform canvasTransform;

    private void Start()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        uiDictionary = new Dictionary<GameObject, UIPool>();

        canvasTransform = GameObject.Find("Canvas").transform;

        Initialize(pools, uiPools);

    }

    #if UNITY_EDITOR
    void OnDestroy()
    {
        CheckPoolSize(pools, uiPools);
    }
    #endif
    void CheckPoolSize(Pool[] pools,UIPool[]uiPools)
    {
        foreach(var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(string.Format("Pool: {0} has a runtime size {1} bigger than its initial size {2}!",
                    pool.Prefab.name,
                    pool.RuntimeSize,
                    pool.Size));
            }
        }
        foreach (var uiPool in uiPools)
        {
            if (uiPool.RuntimeSize > uiPool.Size)
            {
                Debug.LogWarning(string.Format("Pool: {0} has a runtime size {1} bigger than its initial size {2}!",
                    uiPool.Prefab.name,
                    uiPool.RuntimeSize,
                    uiPool.Size));
            }
        }
    }

    void Initialize(Pool[] pools, UIPool[] uiPools)
    {
        foreach(var pool in pools)
        {
         #if UNITY_EDITOR
            //#if ...#endif，只會在指定的平台上運行時才會進行編譯(打包後忽略這段程式碼)
            if (dictionary.ContainsKey(pool.Prefab))//如果prefab重複，則跳過
            {
                Debug.LogError("在多個對象池裡發現了相同的prefab ! Prefab: " + pool.Prefab.name);
                continue;
            }
         #endif
            dictionary.Add(pool.Prefab, pool);

            Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;     

            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
        foreach(var uiPool in uiPools)
        {
         #if UNITY_EDITOR
            //#if ...#endif，只會在指定的平台上運行時才會進行編譯(打包後忽略這段程式碼)
            if (dictionary.ContainsKey(uiPool.Prefab))//如果prefab重複，則跳過
            {
                Debug.LogError("在多個對象池裡發現了相同的prefab ! Prefab: " + uiPool.Prefab.name);
                continue;
            }
         #endif
            uiDictionary.Add(uiPool.Prefab, uiPool);

            Transform uiPoolParent = new GameObject("Pool: " + uiPool.Prefab.name).transform;

            uiPoolParent.SetParent(canvasTransform, false);// worldPositionStays = false to keep UI objects spawning consistently
            uiPool.Initialize(uiPoolParent);
        }
    }

    /// <summary>
    /// 根據傳入的prefab參數，返回對象池中預備好的遊戲對象
    /// </summary>
    /// <param name="prefab">
    /// <para>指定的遊戲對象預制體</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)//UI相關的prefab用它就好
    {
        //#if UNITY_EDITOR
        //if (!dictionary.ContainsKey(prefab))
        //{
        //    Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);

        //    return null;
        //}
        //#endif
        //return dictionary[prefab].PreparedObject();
        if (dictionary.ContainsKey(prefab))
        {
            return dictionary[prefab].PreparedObject();
        }
        else if (uiDictionary.ContainsKey(prefab))
        {
            return uiDictionary[prefab].PreparedObject();
        }
        #if UNITY_EDITOR
        Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);
        return null;
        #endif
    }
    /// <summary>
    /// 根據傳入的prefab參數，在position的參數位置釋放對象池中預備好的遊戲對象
    /// </summary>
    /// <param name="prefab">
    /// <para>指定的遊戲對象預制體</para>
    /// </param>
    /// <param name="position">
    /// <para>指定釋放位置</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
        //#if UNITY_EDITOR
        //if (!dictionary.ContainsKey(prefab))
        //{
        //    Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);

        //    return null;
        //}
        //#endif
        //return dictionary[prefab].PreparedObject(position);
        if (dictionary.ContainsKey(prefab))
        {
            return dictionary[prefab].PreparedObject(position);
        }
        else if (uiDictionary.ContainsKey(prefab))
        {
            return uiDictionary[prefab].PreparedObject(position);
        }
        #if UNITY_EDITOR
        Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);
        return null;
        #endif
    }
    /// <summary>
    /// 根據傳入的prefab參數和rotation參數，在position的參數位置釋放對象池中預備好的遊戲對象
    /// </summary>
    /// <param name="prefab">
    /// <para>指定的遊戲對象預制體</para>
    /// </param>
    /// <param name="position">
    /// <para>指定釋放位置</para>
    /// </param>
    /// <param name="rotation">
    /// <para>指定的旋轉值</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        //#if UNITY_EDITOR
        //if (!dictionary.ContainsKey(prefab))
        //{
        //    Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);

        //    return null;
        //}
        //#endif
        //return dictionary[prefab].PreparedObject(position, rotation);
        if (dictionary.ContainsKey(prefab))
        {
            return dictionary[prefab].PreparedObject(position, rotation);
        }
        else if (uiDictionary.ContainsKey(prefab))
        {
            return uiDictionary[prefab].PreparedObject(position, rotation);
        }
        #if UNITY_EDITOR
        Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);
        return null;
        #endif
    }
    /// <summary>
    /// 根據傳入的prefab參數，rotation參數和localScale參數，在position的參數位置釋放對象池中預備好的遊戲對象
    /// </summary>
    /// <param name="prefab">
    /// <para>指定的遊戲對象預制體</para>
    /// </param>
    /// <param name="position">
    /// <para>指定釋放位置</para>
    /// </param>
    /// <param name="rotation">
    /// <para>指定的旋轉值</para>
    /// </param>
    /// <param name="localScale">
    /// <para>指定的縮放值</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        //#if UNITY_EDITOR
        //if (!dictionary.ContainsKey(prefab))
        //{
        //    Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);

        //    return null;
        //}
        //#endif
        //return dictionary[prefab].PreparedObject(position, rotation, localScale);
        if (dictionary.ContainsKey(prefab))
        {
            return dictionary[prefab].PreparedObject(position, rotation, localScale);
        }
        else if (uiDictionary.ContainsKey(prefab))
        {
            return uiDictionary[prefab].PreparedObject(position, rotation, localScale);
        }
        #if UNITY_EDITOR
        Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);
        return null;
        #endif
    }
}
