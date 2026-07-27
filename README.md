# BYD-MES 通用版系统

面向产线设备的 Windows MES 客户端，连接 PLC、扫码器、RFID、打印机和 BYD MES，完成条码验证、配方切换、生产数据采集上传、标签打印、本地追溯、公告展示及客户端升级。

## 项目概览

- 技术栈：C# / .NET Framework 4.7.2 / WinForms
- 开发环境：Visual Studio 2022（.NET 桌面开发工作负载）
- 主解决方案：`MesDatas.sln`
- 主程序：`MesDatas`，输出为 `bydMes.exe`
- 当前业务分支：`GeneralSystem`
- 外部依赖：BYD MES COM、PLC 通讯库、BarTender、RFID、Access/MDB、SqlSugar

## 解决方案组成

| 工程 | 职责 |
| --- | --- |
| `MesDatas` | 主 WinForms 客户端及生产业务入口 |
| `MesDatasCore` | 公共模型与核心能力 |
| `PlcCommunication` | PLC 配置、读写与通讯封装 |
| `BydMesTool` | BYD MES 接口工具库 |
| `BulletinBoard` / `MBullData` | 产线公告板及数据组件 |
| `Save2MdbTool` | 本地 MDB 追溯数据保存 |
| `UpdateBYDServer` | 客户端升级服务 |
| `ToolSample` | 工具库调用示例 |

## 文档

- [项目内部交接文档](Docs/BYD-MES项目内部交接文档.docx)
- [程序迭代清单](Docs/程序迭代清单.md)

## 构建

1. 使用 Visual Studio 2022 打开 `MesDatas.sln`，还原 `packages.config` 中的 NuGet 包。
2. 优先验证 `Debug|x86`，再按现场发布配置构建。
3. 核对厂商 DLL、COM 组件、BarTender、PLC/RFID 驱动及历史绝对 `HintPath`。
4. 解决方案包含旧式 COM 引用，须使用 Visual Studio 自带的 .NET Framework MSBuild，不能用 .NET Core 版 MSBuild 完整构建。

## 分支说明

业务历史位于 `general-line`、`master` 和 `GeneralSystem`。默认分支 `main` 是独立的初始提交历史，与 `GeneralSystem` 没有共同祖先，不能直接创建普通 Pull Request。详情见[程序迭代清单](Docs/程序迭代清单.md)。

## 安全说明

生产账户、密码、真实 IP、数据库连接串、客户数据和现场配置不得写入 README 或普通提交，应通过公司授权的保密渠道管理。
