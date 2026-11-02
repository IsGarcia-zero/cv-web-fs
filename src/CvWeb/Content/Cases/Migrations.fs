module CvWeb.Content.Cases.Migrations

open CvWeb.Domain
open CvWeb.Content

let private t = Text.make
let private metric value es en = { Value = value; Caption = t es en }

let value =
    { Key = "migrations"
      Title = t "De herramientas Windows a aplicaciones multiplataforma" "From Windows tools to cross-platform apps"
      TabTitle = t "Migraciones" "Migrations"
      Tagline =
        t "Unas ocho aplicaciones WPF y WinForms migradas a Avalonia entre junio y octubre de 2026: aprovisionamiento, configuración y pruebas de equipos IoT."
          "About eight WPF and WinForms apps migrated to Avalonia between June and October 2026: IoT device provisioning, configuration and testing."
      Metrics =
        [ metric "~8" "aplicaciones migradas" "apps migrated"
          metric "2–3" "semanas por app, estimadas" "estimated weeks per app"
          metric "8/10" "versiones de .NET usadas" ".NET versions used"
          metric "2026" "junio a octubre" "June through October" ]
      Challenge =
        t "Las herramientas de escritorio dependían de Windows y se comunicaban con equipos físicos y servicios remotos. Había que conservar sus flujos y protocolos al separar la interfaz de cada plataforma."
          "The desktop tools depended on Windows and communicated with physical devices and remote services. Their workflows and protocols had to be preserved while separating the UI from each platform."
      Decisions =
        [ t "Separar dominio, casos de uso, infraestructura e interfaz, con inyección de dependencias."
            "Separate domain, use cases, infrastructure and UI, with dependency injection."
          t "Aislar los transportes de hardware detrás de interfaces e incorporar implementaciones simuladas para probar sin equipos conectados."
            "Isolate hardware transports behind interfaces and add simulated implementations for testing without connected devices."
          t "Compartir las vistas de Avalonia y adaptar el host y los servicios a las plataformas que necesita cada herramienta."
            "Share Avalonia views and adapt the host and services to the platforms each tool needs." ]
      Outcome =
        [ t "Herramientas de aprovisionamiento de llaves, configuración de medidores y control de calidad con vistas reutilizables y modos de demostración."
            "Key-provisioning, meter-configuration and quality-control tools with reusable views and demonstration modes."
          t "Una plantilla dotnet new para que el equipo pueda iniciar nuevas aplicaciones con la misma estructura."
            "A dotnet new template so the team can start new apps with the same structure." ]
      Role = t "Realicé las migraciones y preparé la plantilla compartida para el equipo."
               "I carried out the migrations and prepared the team's shared template."
      Stack = [ "C#"; ".NET 8/10"; "Avalonia"; "MVVM"; "USB"; "Serial"; "DI" ]
      Visual = Screenshots [ CaseShots.keyAnalysis; CaseShots.deviceConfiguration; CaseShots.qualityHistory ]
      Note = Some(t "Las 2–3 semanas por aplicación son una estimación. Las plataformas de destino varían según la herramienta."
                    "The 2–3 weeks per app are an estimate. Target platforms vary by tool.") }
