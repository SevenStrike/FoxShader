## XShader 高级着色器包
[![license](https://img.shields.io/badge/license-AGPLv3.0-red.svg)](https://github.com/SevenStrike/XShader/blob/main/LICENSE)
[![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/SevenStrike/XShader)](https://github.com/SevenStrike/XShader/releases/latest)
[![supported](https://img.shields.io/badge/Supported-Unity-success.svg)](https://unity.com/)

### 概述
------------
这是一套基于 Unity ShaderGraph 的深度可定制、模块化材质着色器资源包。所有功能均拆分为独立的 SubGraph 与 Master Graph，开发者可以按需拖入项目、自由拼装，快速实现从车漆、玻璃、建筑内饰到科幻扫描、流动噪波等 30 + 常见视觉效果
<br>
<br>
| 开源不易，您的支持是持续更新的动力，<br>这个小工具倾注了我无数个深夜的调试与优化，它永远免费，但绝非无成本，如果您觉得这个工具<br>能为您节省时间、解决问题，甚至带来一丝愉悦，请考虑赞助一杯咖啡，让我知道：有人在乎这份付出，而这将成为我熬夜修复Bug、<br>添加新功能的最大动力。开源不是用爱发电，您的认可会让它走得更远|![](Docs/donate.jpg) |
|:-|-:|
| **欢迎加入技术研讨群，在这里可以和我以及大家一起探讨插件的优化以及相关的技术实现思路，同时在做项目时遇到的众多问题以及瓶颈<br>阻碍都可以互相探讨学习**|![](Docs/qqgroups.jpg) |

<br>

## ⚙️ 不透明 - fs_Opaque (Lit / Unlit)

适用于模拟不透明质感表面的着色器

![s](Docs/fs_Opaque.png)

## ⚙️ 透明 - fs_Transparent (Lit / Unlit)

适用于模拟透明质感表面的着色器

![s](Docs/fs_Transparent.png)

## 🚗 车漆 - fs_CarPainter

适用于汽车外壳表面的着色器

![s](Docs/fs_CarPainter.png)

![s](Docs/fs_CarPainter0.png)

![s](Docs/fs_CarPainter1.png)

![s](Docs/fs_CarPainter2.png)

## 🎯 全息视差 - fs_HoloSight

适用于视差视觉的着色器（例如光电准心以及瞄准镜）

![s](Docs/fs_HoloSight_Reflex.gif)

![s](Docs/fs_HoloSight_Scope.gif)

## 🌋 地形 - fs_Map

适用于模拟地形质感表面的着色器

![s](Docs/fs_Map.png)

## 🩹 遮罩透明贴片 - fs_MaskOpacity

适用于在物体表面贴片的遮罩形态的着色器（例如水洼、污迹等）

![s](Docs/fs_MaskOpacity_0.png)

![s](Docs/fs_MaskOpacity_1.png)

## 🚀 贴花 - fs_Decal

适用于贴花投影到表面的着色器

![s](Docs/fs_Decal.png)

## 💡 深度扫描 - fs_DepthScanner

适用于在表面之间形成体积边缘探测的着色器

![s](Docs/fs_DepthScanner.png)

## 🏝 虚拟深度雾效 - fs_FakeFog

适用于模拟场景深度雾气的着色器

![s](Docs/fs_FakeFog_0.png)

![s](Docs/fs_FakeFog_1.png)

![s](Docs/fs_FakeFog_2.png)

![s](Docs/fs_FakeFog_3.gif)

## ⛰ 视差空间 - fs_FakeInterior

适用于使用最佳性能的多面贴图来模拟深度空间的着色器

![s](Docs/fs_FakeInterior_0.png)

![s](Docs/fs_FakeInterior_1.png)

![s](Docs/fs_FakeInterior_2.gif)

## 🧿 虚拟流动贴图 - fs_FlowMap

适用于模拟贴图流动感的着色器

![s](Docs/fs_FlowMap_0.gif)

![s](Docs/fs_FlowMap_1.gif)

## ⚙️ 模拟玻璃 - fs_Glass

适用于模拟折射玻璃质感的着色器

![s](Docs/fs_Glass_0.gif)

![s](Docs/fs_Glass_1.gif)
