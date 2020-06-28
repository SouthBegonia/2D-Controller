using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;

    [Header("对象信息")]
    public GameObject shadowPrefab;
    public int shadowCount;

    /// <summary>
    /// shadowSprite的对象池
    /// </summary>
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        // 初始化对象池
        FillPool();
    }

    public void FillPool()
    {
        for(int i=0;i<shadowCount;i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

            // 返回对象池
            ReturnPool(newShadow);
        }
    }

    /// <summary>
    /// 将对象返回对象池
    /// </summary>
    /// <param name="gameObject"></param>
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        availableObjects.Enqueue(gameObject);
    }

    public GameObject GetFromPool()
    {
        if(availableObjects.Count==0)
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    }
}