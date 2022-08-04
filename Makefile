.PHONY: run build

run:
	dotnet run

build:
	-mkdir Build >NUL 2>NUL
	-mkdir Build/Linux >NUL 2>NUL
	-mkdir Build/Windows >NUL 2>NUL
	-mkdir Build/MacOS >NUL 2>NUL

	dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained false -o Build/Linux/
	dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained false -o Build/Windows/
	dotnet publish -r osx-x64 -p:PublishSingleFile=true --self-contained false -o Build/MacOS/