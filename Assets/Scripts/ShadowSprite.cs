using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 残影对象的类
/// </summary>
public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer shadowSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("时间控制参数")]
    public float activeTime;       // 残影的生命周期
    public float activeStart;      // 残影生命周期的起始点

    [Header("不透明度控制")]
    private float alpha;           // 待设定Alpha值
    public float alphaSet;         // 初始Alpha值
    public float alphaMultiplier;  // Alpha乘积

    /// <summary>
    /// 当把残影对象从对象池中取出（激活）时触发
    /// </summary>
    private void OnEnable()
    {
        // 取得Player标识，及各自SpriteRenderer组件
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shadowSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        // 设定Alpha初始值
        alpha = alphaSet;

        // 设定当前残影对象的组件信息
        shadowSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        // 残影生命周期开始
        activeStart = Time.time;
    }

    void Update()
    {
        // 残影Alpha、color值变化
        alpha *= alphaMultiplier;
        color = new Color(0.5f, 0.5f, 0.5f, alpha);
        shadowSprite.color = color;

        // 残影生命周期结束，返回对象池
        if (Time.time >= activeStart + activeTime)
        {
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}