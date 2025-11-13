# pvz_temp

一个基于 Unity 的《植物大战僵尸》风格原型框架，实现了数据驱动的单位定义、技能系统与基础 Prefab。

## 功能概览

- **单位数据**：通过 `UnitDefinition` ScriptableObject 定义植物与僵尸的基础属性，并关联技能配置。
- **技能系统**：所有行为都以技能实现，包含调度器、移动、攻击与功能性（产阳光）技能，并支持独立冷却时间。
- **运行时模型**：`UnitEntity` 负责实例化数据、维护生命值、广播状态事件并驱动技能调度。
- **示例资源**：提供向日葵、豌豆射手与普通僵尸的定义、技能配置和 Prefab，以及豌豆子弹。
- **测试**：使用 Unity Test Framework 的 EditMode 测试覆盖核心逻辑。

## 目录结构

```
Assets/
  Data/            // ScriptableObject 数据资产
  Prefabs/         // 示例单位与子弹的 Prefab
  Scripts/         // 核心逻辑与技能实现
  Tests/EditMode/  // Unity Test Framework 测试
```

## 运行测试

在 Unity 编辑器中打开项目后，可通过 **Test Runner > Edit Mode** 执行测试。

## 后续扩展建议

- 实现更复杂的目标选择与路径系统。
- 为技能调度器接入 Update/FixedUpdate 的可配置时间源。
- 丰富技能类型，例如减速、范围攻击等，通过新增 ScriptableObject 配置即可扩展。
