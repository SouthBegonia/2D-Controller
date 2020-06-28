using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer shadowSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("时间控制参数")]
    public float activeTime;
    public float activeStart;

    [Header("不透明度控制")]
    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;


    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shadowSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        shadowSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }

    void Update()
    {
        // Alpha变化
        alpha *= alphaMultiplier;
        color = new Color(0.5f, 0.5f, 0.5f, alpha);
        shadowSprite.color = color;

        // 超时返回对象池
        if(Time.time >=activeStart+activeTime)
        {
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
