## 🎮 CS2 Joinsounds

A plugin designed to play custom sounds when players with specific permissions join the server. 
The plugin allows server owners to assign different sounds to players with certain flags or specific SteamID's.

![GitHub tag (with filter)](https://img.shields.io/github/v/tag/asapverneri/CS2-Joinsounds?style=for-the-badge&label=Version)
![Last Commit](https://img.shields.io/github/last-commit/asapverneri/CS2-Joinsounds?style=for-the-badge)

---

## 📦 Installion

- Install [CounterStrike Sharp](https://github.com/roflmuffin/CounterStrikeSharp) & [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master)
- Download the latest release from the releases tab and copy it into the addons folder.

**Example config:**
```json
{
  // PLEASE FOLLOW ORDER OF IMPORTANCE: SteamID64 -> Highest flag -> Lowest flag -> Default
  "SoundSettings": [
    {
      "SteamID64": "76561198134597930", // with steamid
      "Sound": "sounds/training/timer_bell"
    },
    {
      "Flag": "@css/admin", // with flag
      "Sound": "sounds/training/timer_bell"
    },
    {
      "Flag": "@css/vip", // with flag
      "Sound": "sounds/training/timer_bell.vsnd"
    },
    {
      "Sound": "sounds/training/timer_bell.vsnd" // Default
    }
  ],
  "ConfigVersion": 1
}
```

---

## 📫 Contact

<div align="center">
  <a href="https://discordapp.com/users/367644530121637888">
    <img src="https://img.shields.io/badge/Discord-7289DA?style=for-the-badge&logo=discord&logoColor=white" alt="Discord" />
  </a>
  <a href="https://steamcommunity.com/id/vvernerii/">
    <img src="https://img.shields.io/badge/Steam-000000?style=for-the-badge&logo=steam&logoColor=white" alt="Steam" />
  </a>
</div>

---

## 💖 Support My Work

<div align="center">
  <a href="https://www.paypal.com/paypalme/PeliluolaCS2">
    <img src="https://img.shields.io/badge/Donate-PayPal-00457C?style=for-the-badge&logo=paypal&logoColor=white" alt="Donate via PayPal" />
  </a>
  <a href="https://buy.stripe.com/cN2dThbavflW05G7sz">
    <img src="https://img.shields.io/badge/Donate-Stripe-635BFF?style=for-the-badge&logo=stripe&logoColor=white" alt="Donate via Stripe" />
  </a>
</div>