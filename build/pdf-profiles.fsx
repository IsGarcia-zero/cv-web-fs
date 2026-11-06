/// Perfiles del CV corto para subir a vacantes. El sitio no cambia: sólo el PDF.
/// Cada perfil trae su titular, resumen y competencias (escritos a mano) y las palabras clave
/// con que pdf-tailor.fsx ordena lo que ya está en Content/ (sin IA: sólo coincidencias).
module PdfProfiles

open CvWeb.Domain
open CvWeb.Content

type PdfProfile =
    { /// Parte del nombre del archivo; "General" es el PDF que enlaza el sitio.
      Key: string
      Headline: Text
      /// `None`: el mismo resumen del sitio.
      Summary: Text option
      /// Competencias clave bajo el resumen; vacío: no se muestra la sección.
      Core: Text list
      /// Raíces que ordenan viñetas y habilidades; vacío: viñetas marcadas `pdf`, en su orden.
      Keywords: string list
      /// Investigación: publicaciones antes que experiencia.
      PublicationsFirst: bool }

let private t = Text.make
let private same = List.map Text.same

/// Lo que domina y no aparece en el sitio; va en todos los PDF, antes de "También he usado".
let private systems =
    { Title = t "Sistemas y redes" "Systems & networking"
      Items = [ "Windows Server"; "DNS"; "Firewalls"; "Grafana"; "PowerShell"; "Bash" ] }

let skills =
    let main, also = List.splitAt (Skills.value.Length - 1) Skills.value
    main @ [ systems ] @ also

let general =
    { Key = "General"
      Headline =
        t "Ingeniero en Sistemas Computacionales · Backend & Cloud · Go · AWS · Sistemas complejos y autómatas celulares"
            "Computer Systems Engineer · Backend & Cloud · Go · AWS · Complex Systems & Cellular Automata"
      Summary = None
      Core = []
      Keywords = []
      PublicationsFirst = false }

let backend =
    { general with
        Key = "Backend"
        Headline =
          t "Ingeniero de software backend y nube · Go · AWS · Kubernetes · Sistemas complejos y autómatas celulares"
              "Backend & Cloud Software Engineer · Go · AWS · Kubernetes · Complex Systems & Cellular Automata"
        Summary =
          Some(t
                "Ingeniero de software backend y nube. Diseñé y construí solo el backend en AWS de una plataforma IoT para la red eléctrica: microservicios en Go y Java sobre SQS y NATS, desplegados en Kubernetes y auditados con CloudTrail, y un pipeline orientado a eventos para archivos de medición, con persistencia idempotente y reintentos, que después reescribí en C++20. También construí una plataforma interna de solicitudes DevOps con microservicios en Go y PostgreSQL. Vengo de la investigación en sistemas complejos y autómatas celulares: tesis con mención honorífica y un artículo en CANDAR 2024. Busco roles de backend o nube, presenciales, híbridos o remotos; disponible para reubicación."
                "Backend and cloud software engineer. I designed and built, on my own, the AWS backend of an IoT platform for the power grid: Go and Java microservices on SQS and NATS, deployed to Kubernetes and audited with CloudTrail, plus an event-driven pipeline for metering files with idempotent persistence and retries, which I later rewrote in C++20. I also built an internal DevOps request platform with Go microservices and PostgreSQL. I come from research in complex systems and cellular automata: an honors thesis and a CANDAR 2024 paper. Looking for backend or cloud roles, on-site, hybrid, or remote; open to relocation.")
        Core =
          same [ "Go"; "Java"; "C++20"; "AWS (SQS, S3, EC2, EKS, IAM, CloudTrail)"; "NATS"; "Kubernetes"; "Docker"; "PostgreSQL"; "REST / gRPC" ]
          @ [ t "microservicios" "microservices"; t "arquitectura orientada a eventos" "event-driven architecture"
              Text.same "CI/CD (GitLab CI, GitHub Actions)" ]
        Keywords =
          [ "go$"; "aws"; "sqs"; "s3"; "nats"; "kubernetes"; "microservic"; "backend"; "event"; "pipeline"; "postgres"
            "quarkus"; "java"; "c++"; "jwt"; "idempot"; "api" ] }

let cloudSupport =
    { general with
        Key = "CloudSupport"
        Headline =
          t "Ingeniero de sistemas y nube · AWS · Azure · Linux · Windows Server · Dispositivos IoT · Sistemas complejos y autómatas celulares"
              "Cloud & Systems Engineer · AWS · Azure · Linux · Windows Server · IoT Devices · Complex Systems & Cellular Automata"
        Summary =
          Some(t
                "Ingeniero de sistemas con experiencia en nube, servidores y dispositivos IoT en campo. Administro servicios de AWS (EC2, S3, IAM, CloudTrail, SQS) y despliegues en Kubernetes sobre Azure, y busco la causa de las fallas con logs, auditoría y monitoreo de conectividad. Construí herramientas para aprovisionar, configurar y actualizar el firmware de equipos por BLE y USB, y bots para técnicos de campo. Administré servidores Linux y respaldos. Explico temas técnicos con claridad en español e inglés: presenté mi investigación en sistemas complejos y autómatas celulares en CANDAR 2024 (Japón). Busco roles de soporte o ingeniería de nube y sistemas, presenciales, híbridos o remotos."
                "Systems engineer with hands-on experience in cloud, servers, and IoT field devices. I administer AWS services (EC2, S3, IAM, CloudTrail, SQS) and Kubernetes deployments on Azure, and I track down root causes through logs, auditing, and connectivity monitoring. I built tools to provision, configure, and update firmware on devices over BLE and USB, plus bots for field technicians. I administered Linux servers and backups. I explain technical topics clearly in Spanish and English: I presented my research on complex systems and cellular automata at CANDAR 2024 (Japan). Looking for cloud support or cloud and systems engineering roles, on-site, hybrid, or remote.")
        Core =
          same [ "AWS (EC2, S3, IAM, CloudTrail, SQS)"; "Azure"; "Linux"; "Windows Server"; "Kubernetes"; "Docker"
                 "SQL (PostgreSQL, SQL Server, MySQL)"; "DNS"; "Firewalls"; "TCP/IP"; "REST / JSON" ]
          @ [ t "logs y monitoreo (CloudTrail, Grafana)" "logging and monitoring (CloudTrail, Grafana)"
              t "scripting (Python, Bash, PowerShell)" "scripting (Python, Bash, PowerShell)"
              t "aprovisionamiento de dispositivos IoT" "IoT device provisioning"
              t "análisis de causa raíz" "root cause analysis" ]
        Keywords =
          [ "aws"; "azure"; "linux"; "servidor"; "server"; "log"; "monitor"; "vigilan"; "audit"; "aprovision"; "provision"
            "equipo"; "device"; "firmware"; "fota"; "campo"; "field"; "tecnic"; "technician"; "conectividad"; "connectivity"
            "anomal"; "respaldo"; "backup"; "sql"; "postgres"; "mysql"; "kubernetes"; "iam"; "ec2"; "windows"; "dns"
            "firewall"; "grafana"; "powershell"; "bash"; "python"; "red$"; "redes"; "network"; "tcp" ] }

let apps =
    { general with
        Key = "Apps"
        Headline =
          t "Desarrollador de apps multiplataforma · .NET · Avalonia · Flutter · BLE / USB · Sistemas complejos y autómatas celulares"
              "Cross-platform App Developer · .NET · Avalonia · Flutter · BLE / USB · Complex Systems & Cellular Automata"
        Summary =
          Some(t
                "Desarrollador de aplicaciones multiplataforma para equipos IoT. Migré unas ocho apps de escritorio WPF y WinForms a Avalonia en .NET 8/10, con una sola base de código para Windows, Linux, macOS, Android e iOS; construí una app de gestión de equipos BLE (aprovisionamiento mesh, CBOR, FOTA) con unas 400 pruebas automatizadas, y publiqué en Android e iOS una app Flutter para operación en campo. Reescribí en F# con MVU una app de aprovisionamiento de llaves electrónicas por USB. También diseñé el backend en AWS que respalda estas apps. Vengo de la investigación en sistemas complejos y autómatas celulares: tesis con mención honorífica y un artículo en CANDAR 2024. Busco roles de desarrollo de apps o de software, presenciales, híbridos o remotos."
                "Cross-platform app developer for IoT devices. I migrated about eight WPF and WinForms desktop apps to Avalonia on .NET 8/10, with one codebase for Windows, Linux, macOS, Android, and iOS; built a BLE device-management app (mesh provisioning, CBOR, FOTA) backed by about 400 automated tests; and shipped a Flutter field-operations app to Android and iOS. I rewrote a USB electronic-key provisioning app in F# with MVU. I also designed the AWS backend behind these apps. I come from research in complex systems and cellular automata: an honors thesis and a CANDAR 2024 paper. Looking for app or software development roles, on-site, hybrid, or remote.")
        Core =
          same [ "C#"; ".NET 8/10"; "Avalonia"; "WPF"; "WinForms"; "F# (MVU / Elmish)"; "Flutter / Dart"; "Android"; "iOS"
                 "BLE / mesh"; "USB-serial"; "CBOR"; "FOTA"; "MVVM"; "Clean Architecture"; "xUnit"; "GitLab CI" ]
        Keywords =
          [ "avalonia"; "flutter"; "android"; "ios$"; "ble$"; "usb"; "multiplataforma"; "cross-platform"; ".net"; "f#"
            "wpf"; "winforms"; "prueba"; "test"; "mvu"; "app$"; "apps$"; "firmware" ] }

let research =
    { general with
        Key = "Research"
        Headline =
          t "Ingeniero en Sistemas Computacionales · Sistemas complejos y autómatas celulares · Simulación · C++ · Vulkan"
              "Computer Systems Engineer · Complex Systems & Cellular Automata · Simulation · C++ · Vulkan"
        Summary =
          Some(t
                "Ingeniero en sistemas computacionales con investigación en sistemas complejos y autómatas celulares. En coautoría diseñé un modelo bioinspirado en Physarum polycephalum para navegación autónoma, publicado y presentado en CANDAR 2024 (Okinawa, Japón), con el que me titulé con mención honorífica. Llevé el modelo de la simulación en C++ a un robot con Raspberry Pi y LiDAR, y después porté el simulador a Vulkan con compute shaders. Hoy diseño backends en la nube y apps para equipos IoT. Busco roles de investigación y desarrollo, simulación o software, presenciales, híbridos o remotos."
                "Computer systems engineer with research in complex systems and cellular automata. I co-designed a Physarum polycephalum-inspired model for autonomous navigation, published and presented at CANDAR 2024 (Okinawa, Japan), which earned my degree with honors. I took the model from a C++ simulation to a Raspberry Pi robot with LiDAR, and later ported the simulator to Vulkan compute shaders. Today I design cloud backends and apps for IoT devices. Looking for R&D, simulation, or software roles, on-site, hybrid, or remote.")
        Core =
          [ t "autómatas celulares" "cellular automata"; t "sistemas complejos" "complex systems"
            t "modelos bioinspirados (Physarum polycephalum)" "bio-inspired models (Physarum polycephalum)"
            t "simulación" "simulation" ]
          @ same [ "C++17/20"; "Vulkan / GLSL"; "OpenCV"; "Raspberry Pi"; "C#"; ".NET"; "Python"; "LaTeX" ]
          @ [ t "redacción científica" "scientific writing" ]
        Keywords =
          [ "automat"; "physarum"; "simulac"; "simulat"; "c++"; "raspberry"; "robot"; "bioinspir"; "bio-inspired"
            "investig"; "research"; "model"; "vida artificial"; "artificial life"; "vulkan" ]
        PublicationsFirst = true }

let all = [ general; backend; cloudSupport; apps; research ]

/// Teléfono desde build/telefono.txt (fuera de git: el repositorio es público). Sólo va en los PDF
/// por perfil; el General lo enlaza el sitio.
let phone profile =
    let file = System.IO.Path.Combine(__SOURCE_DIRECTORY__, "telefono.txt")
    if profile.Key = general.Key || not (System.IO.File.Exists file) then None
    else Some(System.IO.File.ReadAllText(file).Trim()) |> Option.filter (fun n -> n <> "")
