# LifemaxExtra Plugin
## Overview
LifemaxExtra is a TShock plugin designed for Terraria servers that allows players to increase their maximum health using Life Crystals and Life Fruit. This plugin provides a customizable maximum health setting to accommodate various gaming needs.
## Features
- Offers customizable maximum health settings.
- Allows players to increase their health by using Life Crystals and Life Fruit (beyond the game's default 400/500 limit).
## Configuration File
The LifemaxExtra plugin's configuration file, `LifemaxExtra.json`, is located in the TShock save path. This JSON file contains the settings for the maximum health values.
The structure of the configuration file is as follows:
```json
{
  "LifeCrystalMaxLife": 400, // Maximum health that can be achieved using Life Crystals
  "LifeFruitMaxLife": 500   // Maximum health that can be achieved using Life Fruit
}
```
- **LifeCrystalMaxLife**: An integer representing the maximum health a player can have after using Life Crystals.
- **LifeFruitMaxLife**: An integer representing the maximum health a player can have after using Life Fruit.
## Usage
After setting the maximum health values in the configuration file, the plugin will automatically adjust the player's health when they use Life Crystals or Life Fruit, and it will check and set the health upon player login.
## Developer Information
- **Author**: Anonymous, custom settings added by Xi'en, the liver emperor
## Support and Feedback
If you encounter any issues or have any suggestions while using the plugin, please feel free to raise them in the official forums or community issues section.
- GitHub Repository: https://github.com/THEXN/LifemaxExtra
-This README has been translated from Chinese to English using translation software.
