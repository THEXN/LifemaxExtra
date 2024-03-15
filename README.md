# LifemaxExtra 插件

## 简介

LifemaxExtra 是一个为 Terraria 服务器设计的 TShock 插件，它允许玩家使用生命水晶和生命果来提升生命值上限。这个插件提供了一个自定义的生命值上限设置，以适应不同的游戏需求。

## 功能

- 提供自定义的生命值上限设置。
- 允许玩家通过使用生命水晶和生命果来增加生命值(突破游戏的400/500上限)。

## 配置文件

LifemaxExtra 插件的配置文件 `LifemaxExtra.json` 位于 TShock 的保存路径下。这个 JSON 文件包含了生命值上限的配置。

配置文件的结构如下：

```json
{
  "LifeCrystalMaxLife": 400, // 使用生命水晶最高可提升至的生命值
  "LifeFruitMaxLife": 500  // 使用生命果最高可提升至的生命值
}
```

- **LifeCrystalMaxLife**：一个整数，表示使用生命水晶后玩家生命值的最大值。
- **LifeFruitMaxLife**：一个整数，表示使用生命果后玩家生命值的最大值。

## 使用

在配置文件中设置好生命值上限后，插件会在玩家使用生命水晶或生命果时自动调整生命值，并在玩家登录时检查并设置生命值。

## 开发者信息

- **作者**：佚名，肝帝熙恩添加自定义

## 支持与反馈

如果您在使用过程中遇到问题或有任何建议，欢迎在官方论坛或社区中提出 issues。

- GitHub 仓库：https://github.com/THEXN/LifemaxExtra
