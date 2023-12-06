all: build
BEPINEX_VERSION = 5

clean:
	@dotnet clean

restore:
	@dotnet restore

build-ui:
	@npm install
	@npx esbuild ui_src/speed_limit_editor.jsx --bundle --outfile=dist/bundle.js

dev-ui:
	@npx esbuild ui_src/speed_limit_editor.jsx --watch --bundle --outfile="D:\SteamLibrary\steamapps\common\Cities Skylines II\Cities2_Data\StreamingAssets\~UI~\HookUI\Extensions\panel.example.speed_limit_editor.js"

build: clean restore build-ui
	@dotnet build /p:BepInExVersion=$(BEPINEX_VERSION)

package-win: build
	@-mkdir dist
	@cmd /c copy /y "bin\Debug\netstandard2.1\0Harmony.dll" "dist\"
	@cmd /c copy /y "bin\Debug\netstandard2.1\SpeedLimitEditor.dll" "dist\"
	@echo Packaged to dist/

package-unix: build
	@-rm -r dist/
	@-mkdir dist
	@cp bin/Debug/netstandard2.1/0Harmony.dll dist
	@cp bin/Debug/netstandard2.1/SpeedLimitEditor.dll dist
	@echo Packaged to dist/

package-dev: package-unix
	@cp -r dist\SpeedLimitEditor.dll %USERPROFILE%\AppData\Roaming\Thunderstore Mod Manager\DataFolder\CitiesSkylines2\profiles\Default\BepInEx\plugins\SpeedLimitEditor\SpeedLimitEditor.dll