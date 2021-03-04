# SeerPacket

赛尔号封包demo c#版

![](http://blog.iyzyi.com/usr/uploads/2020/12/1246837924.jpg)

![](http://blog.iyzyi.com/usr/uploads/2020/12/1504073526.jpg)

## 介绍

* 逆向赛尔号的通信协议。
* vc写的hook.dll劫持tcp通信流量（inline hook x64）。
* c#写的主程序模拟与赛尔号服务器的通信交互（游戏封包的解析与伪造）。
* dll与c#主程序之间借助回调函数传递数据。
* 正在写相关的文章，写好后会发在吾爱破解（大概）。想写的东西太多了，慢慢来吧。

## 使用

VC++编译hook，生成的hook.dll放入Seer的debug或release目录下，然后运行主程序。

由于hook.dll没适配，所以只支持win10 x64.

> 2021.3.4 补充
> 近半年来flash禁用导致本程序无法正常运行，解决方法是下载并安装[flash中国特供版](https://www.flash.cn/)。不过这个会有广告，挺烦人的，所以还是建议虚拟机内测试运行。

## 说明

通信协议的逆向思路，部分参考了hcj师傅在吾爱破解发表的相关博文。

hook.dll的相关代码采用了黑月教主的hook messagebox的框架。原创和借鉴部分大概五五开吧。

## 致谢

感谢hcj师傅在赛尔号的通信协议的逆向方面做出的卓越贡献。

感谢achillis(黑月教主)在《加密与解密》一书中通俗易懂的hook教程。

## 声明

本程序仅供交流学习，不得出于任何目的，损坏上海淘米网络科技有限公司的合法权益。打击外挂，人人有责，从我做起。

## 碎碎念

最近真的是太太太太太忙了，比赛一大堆，要复现的题目一大堆，社团项目一大堆，作业一大堆，考试一大堆。看雪ctf防守方的题目还准备了将近一个月。啊啊啊啊，要是我有分身术就好啦🤢

这个项目是八月初写好的demo，但是到现在都鸽了好几个月了。以后的话，空闲时间会更少，随缘更新吧。本来还想留在private仓库里慢慢完善的。

第一次写这种类似游戏辅助的小项目，菜鸡我还是有颇有自知之明的，写得超烂，师傅们轻点骂呀orz.

2020.12.01

