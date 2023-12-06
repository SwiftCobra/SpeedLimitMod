# Cities: Skylines 2 - More Demand Mod

## Initial Commit:
   Increase demand for buildings in zones.

## Does not affect company or households moving into the city

### Built using Cities: Skylines 2 - C# Mod template by Captain-Of-Coit [Mod Template](https://github.com/Captain-Of-Coit/cities-skylines-2-mod-template)

### Requirements

- [Cities: Skylines 2](https://store.steampowered.com/app/949230/Cities_Skylines_II/) (duh)
- [BepInEx 5.4.22](https://github.com/BepInEx/BepInEx/releases) or later

# Usage
- Run `make build`

# Incrementing version number

- Update `.csproj` file with new version number
- Update `thunderstore.toml` file with new version number
- Update `CHANGELOG` to describe the changes you've made between this and previous version
- Commit version bump
- Do a git tag with the new version number
    - `git tag -a v0.2.0 -m v0.2.0`
- Push your changes + tags
    - `git push origin master --tags`


# Regarding BepInEx version 5 (Stable) VS 6 (Alpha/Unstable/Nightly)

Currently, this mod template defaults to building against BepInEx version 6 (unstable pre-release). If you'd like to instead use Stable BepInEx version 5, you can run the build like this:

```
$ make build BEPINEX_VERSION=5
```

# Credits

- Thanks to Cities Skylines 2 Unofficial Modding Discord
- Captain-Of-Coit [Mod Template](https://github.com/Captain-Of-Coit/cities-skylines-2-mod-template)
- Particular thanks to [@StudioLE](https://github.com/StudioLE) who helped with feedback and improving .csproj setup

# Community

Looking to discuss Cities: Skylines 2 Unofficial modding together with other modders? You're welcome to join our "Cities 2 Modding" Discord, which you can find here: https://discord.gg/vd7HXnpPJf
