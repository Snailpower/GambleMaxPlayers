# GambleMaxPlayers

Removes the 6-player lobby cap in **Gamble With Your Friends**. The limit is configurable — default is 10, up to 100.

---

## Installation

### Option A — Thunderstore Mod Manager (recommended)

Install via [r2modman](https://thunderstore.io/package/ebkr/r2modman/) or the Thunderstore Mod Manager. Search for **GambleMaxPlayers**, click Install. BepInEx is pulled in automatically as a dependency.

### Option B — Manual

#### Step 1 — Install BepInEx

1. Go to the [BepInEx releases page](https://github.com/BepInEx/BepInEx/releases) and download the latest **BepInEx_win_x64** zip (e.g. `BepInEx_win_x64_5.x.x.x.zip`).
2. Open the zip and extract **all contents** directly into your game folder:
   ```
   C:\Program Files (x86)\Steam\steamapps\common\Gamble With Your Friends\
   ```
   After extracting you should see a `BepInEx\` folder and a `winhttp.dll` sitting next to the game executable.
3. Launch the game once through Steam, wait until you reach the main menu, then close it. This lets BepInEx finish its first-time setup and create the `BepInEx\plugins\` folder.

### Step 2 — Install the mod

1. Download `GambleMaxPlayers.dll` from the releases.
2. Place it in:
   ```
   C:\Program Files (x86)\Steam\steamapps\common\Gamble With Your Friends\BepInEx\plugins\
   ```
3. Launch the game — the mod is now active.

---

## Configuration

On the first launch with the mod installed, a config file is created at:
```
BepInEx\config\com.gamblewithfriends.maxplayers.cfg
```

Open it with any text editor and set `MaxPlayers` to whatever you want (2–100):
```
[General]
MaxPlayers = 10
```

Changes take effect the next time you launch the game.

---

## Uninstall

Delete `GambleMaxPlayers.dll` from `BepInEx\plugins\`. To also remove BepInEx entirely, delete the `BepInEx\` folder and `winhttp.dll` from the game directory.
