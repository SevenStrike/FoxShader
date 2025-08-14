# XShader 高级着色器包
这是一套基于 Unity ShaderGraph 的深度可定制、模块化材质着色器资源包。所有功能均拆分为独立的 SubGraph 与 Master Graph，开发者可以按需拖入项目、自由拼装，快速实现从车漆、玻璃、建筑内饰到科幻扫描、流动噪波等 30 + 常见视觉效果

### :zap: **支援赞助**
#### **我会将每一位赞助者的名字（请备注好您的赞助名字）公示于赞助者名录并公示于每一个版本的赞助者清单中。
"开源不易，您的支持是持续更新的动力！  这个小工具倾注了我无数个深夜的调试与优化，它永远免费，但绝非无成本。基础生活开支费用、开发工具订阅、测试代码&维护… 这些都在默默消耗着资源。  如果您觉得这个工具为您节省了时间、解决了问题，甚至只是带来了一丝愉悦——请考虑赞助一杯咖啡。哪怕微小如5元的支持，都能让我知道：有人在乎这份付出，而这将成为我熬夜修复Bug、添加新功能的最大动力。  开源不是用爱发电，您的认可会让它走得更远。 ——衷心感谢每一位使用者"**

![s](Docs/donate.jpg)

### :zap: **技术群交流**
![s](Docs/qqgroups.jpg)

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
