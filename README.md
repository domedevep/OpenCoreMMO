

<h1 align="center">
  <img align="center" width="120px" src="https://cdn.discordapp.com/attachments/1071163970167263343/1151615520395767879/solarserver0.png" target="_blank"  />
  <br>
  SOLAR SERVER</h1>
<p align="center">
  <a href="https://codecov.io/gh/caioavidal/OpenCoreMMO">
  <img align="center" src="https://codecov.io/gh/caioavidal/OpenCoreMMO/branch/develop/graph/badge.svg" />
</a>
<a href="https://www.codefactor.io/repository/github/caioavidal/opencoremmo"><img  align="center"  src="https://www.codefactor.io/repository/github/caioavidal/opencoremmo/badge" alt="CodeFactor" /></a>
<a href="https://discord.gg/Kazq9z2">
  <img align="center" src="https://badgen.net/badge/icon/discord?icon=discord&label" />
</a>
<a href="https://github.com/caioavidal/opencoremmo/stargazers">
  <img align="center" src="https://img.shields.io/github/stars/caioavidal/opencoremmo?label=stargazers&logoColor=yellow&style=social" />
  </a>
  <a href="https://github.com/caioavidal/OpenCoreMMO/blob/develop/LICENSE">
  <img align="center" src="https://badgen.net/github/license/caioavidal/opencoremmo" />
  </a>
</p>

<p align="center">
  <img align="center" src="https://sonarcloud.io/api/project_badges/measure?project=caioavidal_OpenCoreMMO&metric=sqale_index" />
  <img align="center" src="https://sonarcloud.io/api/project_badges/measure?project=caioavidal_OpenCoreMMO&metric=sqale_rating" />
  <img align="center" src="https://sonarcloud.io/api/project_badges/measure?project=caioavidal_OpenCoreMMO&metric=ncloc" />
  <img align="center" src="https://sonarcloud.io/api/project_badges/measure?project=caioavidal_OpenCoreMMO&metric=code_smells" />
  <img align="center" src="https://sonarcloud.io/api/project_badges/measure?project=caioavidal_OpenCoreMMO&metric=security_rating" />
</p>

> Modern, free, and open-source MMORPG server emulator written in C#.
> 
> It was written from scratch and development on the project began in January 2020.
> <br>To connect to the server, you can use either [OTClient](https://github.com/edubart/otclient) or [OpenTibiaUnity](https://github.com/slavidodo/OpenTibia-Unity) for version 8.6.

## Demo

<p align="center">
  <img width="700" align="center" src="https://github.com/caioavidal/OpenCoreMMO/blob/develop/opencoremmo.gif?raw=true" alt="demo"/>
</p>

## Latest Builds

| Enviroment | Status |
|------------|--------|
|![win](https://badgen.net/badge/icon/Windows,.NET%207?icon=windows&label&list=1)|[![OpenCoreMMO](https://github.com/caioavidal/OpenCoreMMO/actions/workflows/opencoremmo-validation.yaml/badge.svg?event=push)](https://github.com/caioavidal/OpenCoreMMO/actions/workflows/opencoremmo-validation.yaml)        |
|![linux](https://badgen.net/badge/icon/Ubuntu%20Linux%2022.04%20x64?icon=terminal&label&color=orange)|[![OpenCoreMMO](https://github.com/caioavidal/OpenCoreMMO/actions/workflows/opencoremmo-validation.yaml/badge.svg?event=push)](https://github.com/caioavidal/OpenCoreMMO/actions/workflows/opencoremmo-validation.yaml)|
|![mac](https://badgen.net/badge/icon/macOS%20Latest?icon=apple&label&color=purple&list=1)|[![OpenCoreMMO](https://github.com/caioavidal/OpenCoreMMO/actions/workflows/opencoremmo-validation.yaml/badge.svg?event=push)](https://github.com/caioavidal/OpenCoreMMO/actions/workflows/opencoremmo-validation.yaml)|

## Usage

```sh
download and install .NET 7: https://dotnet.microsoft.com/download/dotnet/7.0
git clone https://github.com/caioavidal/OpenCoreMMO.git
cd src
dotnet run --project "Standalone"
```
To connect to the self-hosted server for development, please use the following connection details:
1. IP Address: 127.0.0.1
2. Port: 7171
3. Account Name: 1
4. Password: 1

## What we have done so far

- Reading OTB and OTBM File structure: :heavy_check_mark:
- Loading OTBM Tile Area, Towns and Waypoints: :heavy_check_mark:
- Loading Spawn Areas: :heavy_check_mark:
- Loading OTBM House Tiles: :warning:
- Log In/Out Player on Game: :heavy_check_mark:
- All player movements: :heavy_check_mark:
- Multiplayer connection: :heavy_check_mark:
- Spawn and Respawn Monsters: :heavy_check_mark:
- PvM Combat: :heavy_check_mark:
- Depot: :heavy_check_mark:
- Chats: :heavy_check_mark:
- Guilds: :heavy_check_mark:
- War System: :warning:
- Public Channels: :heavy_check_mark:
  - Loot and Death Channels: :heavy_check_mark:
  - Vip List: :heavy_check_mark:
- PvP Combat: :warning:
- Party: :heavy_check_mark:
  - Basics: :heavy_check_mark:
  - Share Loot: :heavy_check_mark:
  - Shared Experience: :heavy_check_mark:
- NPC System: :heavy_check_mark:
- Lua Scripting: :heavy_check_mark:
- C# Extensions: :heavy_check_mark:
- In-Memory Cache: :warning:

## Technologies

* C#
* .Net 7
* Database support: InMemory, MySQL, and SQLite
* Console Debug Logging
* XUnit Testing

 [![My Skills](https://skillicons.dev/icons?i=dotnet,cs,docker,git,mysql,sqlite)](https://skillicons.dev)

## Links

* Documentation: https://caioavidal.gitbook.io/opencoremmo/
* Discord Invite: https://discord.gg/Kazq9z2
* TibiaKing Topic: https://tibiaking.com/forums/topic/101402-open-source-tibia-server-c-net-5/

## Author

👤 **Caio Vidal**

* Github: [@caioavidal](https://github.com/caioavidal)
* LinkedIn: [https:\/\/www.linkedin.com\/in\/caiovidal](https:\/\/www.linkedin.com\/in\/caiovidal)

## Contributors

Thank you to all the people who already contributed to OpenCoreMMO!

* 👤 **[Marcus Vinicius(MarcusViniciusSS)](https://github.com/MarcusViniciusSS)**
* 👤 **[Shelby115](https://github.com/Shelby115)**
* 👤 **[Mun1z](https://github.com/Mun1z)**
* 👤 **[alissonfabiano](https://github.com/alissonfabiano)**
* 👤 **[elewental](https://github.com/elewental)**
* 👤 **[VictorAmaral](https://github.com/VictorAmaral)**
* 👤 **[jahazielhigareda](https://github.com/jahazielhigareda)**
* 👤 **[emidiovictor](https://github.com/emidiovictor)**
* 👤 **[themaoci](https://github.com/themaoci)**

## Show your support

Give a ⭐️ if this project helped you!
