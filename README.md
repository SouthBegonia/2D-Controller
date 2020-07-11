2D 冲锋残影效果

![](https://gitee.com/southbegonia/BlogImage/blob/master/2020/20200711130119.gif)

# 前言

本项目基于 [2D-Controller - M-Studio-M](https://github.com/M-Studio-M/2D-Controller) 修改而来，不用于任何商业用途，仅供学习交流。

- [利用对象池设计制作Dash冲锋残影效果 - Michael Wang](https://mp.weixin.qq.com/s?__biz=MzU5MjQ1NTEwOA==&mid=2247509607&idx=1&sn=8ddc37539d013b5b7a735ad60e0c2f93&chksm=fe1d92ccc96a1bda2d857f1ca80be0dd003dc311481d329754c12626cc0906d3d2fe1d2f2997&mpshare=1&scene=23&srcid=&sharer_sharetime=1593328804698&sharer_shareid=3700fe0c888383356811eb94c58328eb#rd)
- [Unity教程:利用对象池设计制作Dash冲锋残影效果！(多P合集) - M-Studio](https://www.bilibili.com/video/av83771678?p=2)



# 冲锋残影原理

## 对象池的应用



# 特性

## 优势

- 较少代码量实现对象池应用
- 能较好控制冲锋残影相关参数

## 可改进之处

- 冲锋残影功能的模块化、泛用化：冲锋残影参数写入基类中
- 冲锋残影的参数控制：当前项目中残影生成都是基于FixedUpdate，或许可以增加相应参数控制残影生成的时间间隔等
- 冲锋残影的影响因素：是否基于Gravity、技能释放时如何控制、特殊Animation如何应对
- 冲锋残影的物理运动：例如带增速的冲锋
- 冲锋残影的视觉特效：虚拟相机的跟随参数、残影特效