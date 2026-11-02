module CvWeb.Content.Cases.BleApp

open CvWeb.Domain
open CvWeb.Content

let private t = Text.make
let private metric value es en = { Value = value; Caption = t es en }

let value =
    { Key = "ble"
      TabTitle = t "Redes BLE" "BLE networks"
      Title = t "App multiplataforma para redes BLE de medidores" "Cross-platform app for BLE meter networks"
      Tagline =
        t "Una sola base de código en .NET y Avalonia para aprovisionar, configurar y actualizar redes BLE Mesh en escritorio y móvil."
          "One .NET and Avalonia codebase to provision, configure and update BLE Mesh networks on desktop and mobile."
      Metrics =
        [ metric "5" "plataformas" "platforms"
          metric "947" "pruebas automatizadas" "automated tests"
          metric "52k" "líneas de C# y AXAML" "lines of C# and AXAML"
          metric "68" "documentos y ADRs" "docs and ADRs" ]
      Challenge =
        t "Los operadores de campo crean grupos BLE Mesh de medidores, configuran LTE y servidores, actualizan firmware y descargan mediciones, muchas veces sin conexión. La herramienta anterior sólo corría en Windows."
          "Field operators build BLE Mesh groups of meters, configure LTE and servers, update firmware and download measurements, often offline. The previous tool ran on Windows only."
      Decisions =
        [ t "Avalonia con MVVM en capas Core, Application, Infrastructure y un host por plataforma."
            "Avalonia with MVVM layered into Core, Application, Infrastructure, and one host per platform."
          t "Transporte BLE nativo por plataforma (WinRT, BlueZ, CoreBluetooth, Android) detrás de fábricas; el protocolo CBOR y la orquestación se comparten."
            "Native BLE transport per platform (WinRT, BlueZ, CoreBluetooth, Android) behind factories; the CBOR protocol and orchestration are shared."
          t "Inicio de sesión sin conexión con hash PBKDF2-SHA256 en el almacén seguro de cada sistema; nunca texto plano."
            "Offline login with PBKDF2-SHA256 hashes in each platform's secure store; never plain text."
          t "Firmware por BLE Mesh con CRC16-X25, verificación posterior y recuperación ante desconexiones."
            "Firmware over BLE Mesh with CRC16-X25, post-upload verification, and recovery from disconnects." ]
      Outcome =
        [ t "Corre en Windows, Linux, macOS, Android e iOS, y compila sin advertencias." "Runs on Windows, Linux, macOS, Android and iOS, and builds warning-free."
          t "Reemplaza al cliente WPF anterior, que también escribí, con más del doble de alcance." "Replaces the earlier WPF client, which I also wrote, with more than twice the scope."
          t "Documentación viva con ADRs, diagramas y CI en GitLab." "Living documentation with ADRs, diagrams, and GitLab CI." ]
      Role = t "Autor único de la app y de su predecesora en WPF." "Sole author of the app and of its WPF predecessor."
      Stack = [ "C#"; ".NET 8/10"; "Avalonia"; "MVVM"; "BLE"; "CBOR"; "LiveCharts2"; "Mapsui"; "xUnit"; "GitLab CI" ]
      Visual = Screenshots [ CaseShots.bleDesktop; CaseShots.bleMobile ]
      Note = Some(t "En etapa de demo: el hardware de los equipos sigue en pruebas." "In demo stage: the device hardware is still being tested.") }
