using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 残影对象池的管理类
/// </summary>
public class ShadowPool : MonoBehaviour
{
    // 单例
    public static ShadowPool instance;

    [Header("对象信息")]
    public GameObject shadowPrefab; // 残影对象
    public int shadowCount;         // 对象池内残影数量

    /// <summary>
    /// shadowSprite的对象池队列
    /// </summary>
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        // 初始化对象池
        FillPool();
    }

    /// <summary>
    /// 填充对象池
    /// </summary>
    public void FillPool()
    {
        // 根据预设数量，在对象池中生成对象
        for(int i=0;i<shadowCount;i++)
        {
            // 实例化残影对象
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

            // 将其返回对象池
            ReturnPool(newShadow);
        }
    }

    /// <summary>
    /// 将对象返回对象池
    /// </summary>
    /// <param name="gameObject"></param>
    public void ReturnPool(GameObject gameObject)
    {
        // 关闭激活状态，移至对象池的队列
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
    }

    /// <summary>
    /// 从对象池取得残影
    /// </summary>
    /// <returns></returns>
    public GameObject GetFromPool()
    {
        // 若对象池空，则重新填充
        if(availableObjects.Count==0)
            FillPool();

        // 对象池出队一个残影对象
        var outShadow = availableObjects.Dequeue();
        outShadow.SetActive(true);

        return outShadow;
    }
}