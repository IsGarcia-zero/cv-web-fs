module CvWeb.Content.Cases.Gateway

open CvWeb.Domain
open CvWeb.Content

let private t = Text.make
let private metric value es en = { Value = value; Caption = t es en }

let value =
    { Key = "gateway"
      TabTitle = t "Telemetría" "Telemetry"
      Title = t "Migración multiplataforma de gateways de telemetría DNP3" "Cross-platform migration of DNP3 telemetry gateways"
      Tagline =
        t "Dos gateways sólo-Windows reescritos en Avalonia y .NET 8 para Windows, Ubuntu y macOS, sin licencias comerciales y con paridad documentada."
          "Two Windows-only gateways rebuilt in Avalonia and .NET 8 for Windows, Ubuntu and macOS, with no commercial licenses and documented parity."
      Metrics =
        [ metric "3" "sistemas operativos" "operating systems"
          metric "39/39" "pruebas en Windows y Ubuntu" "tests on Windows and Ubuntu"
          metric "0" "licencias comerciales" "commercial licenses"
          metric "~2k" "gateways en el sistema" "gateways in the system" ]
      Challenge =
        t "Los gateways heredados leen telemetría de líneas eléctricas desde SQL Server y la publican como outstations DNP3 hacia SCADA. Dependían de un adaptador C++/CLI exclusivo de Windows y guardaban cadenas de conexión en archivos."
          "The legacy gateways read power-line telemetry from SQL Server and publish it as DNP3 outstations to SCADA. They depended on a Windows-only C++/CLI adapter and kept connection strings in files."
      Decisions =
        [ t "Avalonia y .NET 8 LTS con MVVM; una matriz de migración documenta cada pantalla original y su equivalente."
            "Avalonia and .NET 8 LTS with MVVM; a migration matrix maps every legacy screen to its replacement."
          t "OpenDNP3 (Apache-2.0) corre en un proceso C++ aparte con un protocolo versionado por stdin/stdout, aislando la dependencia nativa."
            "OpenDNP3 (Apache-2.0) runs in a separate C++ process with a versioned stdin/stdout protocol, isolating the native dependency."
          t "Se conservan las reglas originales: direccionamiento de puntos, estados de comunicación, recargas y último estado ante fallas."
            "Original rules preserved: point addressing, communication status, reloads, and last known state on failures."
          t "Endurecimiento: SQL parametrizado, sin credenciales en archivos y una Web API con llaves por zona guardadas como hash."
            "Hardening: parameterized SQL, no credentials in files, and a Web API with hashed, zone-scoped keys." ]
      Outcome =
        [ t "Paquetes autocontenidos por sistema operativo con SHA-256 y CI en Windows, Ubuntu y macOS."
            "Self-contained per-OS packages with SHA-256 and CI on Windows, Ubuntu and macOS."
          t "Pruebas con el host DNP3 real en Windows y Ubuntu 22.04, y auditoría de dependencias sin vulnerabilidades conocidas."
            "Tests with the real DNP3 host on Windows and Ubuntu 22.04, and a dependency audit with no known vulnerabilities." ]
      Role = t "Hice los dos ports a Avalonia y la Web API; en los gateways heredados agregué cifrado, multizona, compilaciones x86/x64 y pruebas."
              "I did both Avalonia ports and the Web API; on the legacy gateways I added encryption, multi-zone support, x86/x64 builds, and tests."
      Stack = [ "C#"; ".NET 8"; "Avalonia"; "C++"; "OpenDNP3"; "SQL Server"; "ASP.NET Core"; "xUnit"; "GitHub Actions" ]
      Visual = Screenshots [ CaseShots.gatewayMeasurements; CaseShots.gatewayPanel ]
      Note =
        Some(t "Cifras de la validación previa a producción (v4.0.0)." "Figures from the pre-production validation (v4.0.0).") }
