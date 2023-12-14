# Cities: Skylines 2 - Speed Limit Mod

## V0.1.3
- Updating to require Latest HookUI for CSII V1.0.18f1
- Removing UI label.
### Known Issue with All versions, After changing road speeds, adding buildings, or zone buildings will reset road back to default speed.

## V0.1.1
- Fixed bug for when road was set to 0 speed, you could not change it again.
 - Changed lowest speed possible to 1
 - Change unselected speed to -1

## Initial Commit:
- Change Speed limits on roads and highways
- Displays speeds in kph and mph

- You must open the tool from the hookui button (backpack) before selecting a road/highway.

# Usage
- Open Speed Limit tool through the HookUI button (Backpack)
- Select a road, by click on the road name
- Change speed slider, the roads speed will start updating.

### Built using Cities: Skylines 2 - C# Mod template by Captain-Of-Coit [Mod Template](https://github.com/Captain-Of-Coit/cities-skylines-2-mod-template)

### Requirements

- [Cities: Skylines 2](https://store.steampowered.com/app/949230/Cities_Skylines_II/) (duh)
- [BepInEx 5.4.22](https://github.com/BepInEx/BepInEx/releases) or later
- [HookUI 0.3](https://github.com/Captain-Of-Coit/hookui)

# Regarding BepInEx version 5 (Stable) VS 6 (Alpha/Unstable/Nightly)

Currently, this mod template defaults to building against BepInEx version 6 (unstable pre-release). If you'd like to instead use Stable BepInEx version 5, you can run the build like this:

```
$ make build BEPINEX_VERSION=5
```
This mod is released on Github and Thunderstore with Bepinex 5.
If you can build from the github sources, using the makefile is easiest. You will need to change paths in the .csproj, and makefile to your local environment before building.

# Credits

- Thanks to Cities Skylines 2 Unofficial Modding Discord
- Captain-Of-Coit [Mod Template](https://github.com/Captain-Of-Coit/cities-skylines-2-mod-template)

# Community

Looking to discuss Cities: Skylines 2 Unofficial modding together with other modders? You're welcome to join our "Cities 2 Modding" Discord, which you can find here: https://discord.gg/vd7HXnpPJf
