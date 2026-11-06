module CvWeb.Content.Experience

open CvWeb.Domain

/// Viñeta que aparece en el sitio y en el CV corto en PDF.
let private pdf es en = { Text = Text.make es en; InPdf = true }

/// Viñeta sólo del sitio: el PDF tiene que caber en dos hojas.
let private web es en = { Text = Text.make es en; InPdf = false }

let private cantilever =
    { Role = Text.make "Desarrollador Backend" "Backend Developer"
      Company = Text.same "Distribuciones Cantilever"
      Period = Text.make "Jul 2025 – actual" "Jul 2025 – Present"
      Summary =
        Text.make
            "Responsable del backend en la nube y de las aplicaciones de plataformas IoT de medición y monitoreo de la red eléctrica, de AWS al equipo en campo."
            "Owner of the cloud backend and apps for IoT metering and power-grid monitoring platforms, from AWS down to field devices."
      Highlights =
        [ pdf
              "Diseñé y construí todo el backend en la nube: microservicios en Go y Java que consumen colas de AWS SQS y eventos de S3 y CloudTrail, se comunican por NATS y se despliegan en Kubernetes sobre Azure. La actividad de AWS queda auditada con CloudTrail; hoy mueven unos 10 mil mensajes diarios entre varias colas de SQS y NATS."
              "Designed and built the entire cloud backend: Go and Java microservices that consume AWS SQS queues and S3 and CloudTrail events, talk over NATS, and deploy to Kubernetes on Azure. AWS activity is audited through CloudTrail; today they move about 10,000 messages a day across several SQS and NATS queues."
          pdf
              "Administración de AWS: CloudTrail, EC2, S3, EKS, DynamoDB, EventBridge, SQS e IAM; ingesta y auditoría de logs, vigilancia de objetos en S3 y un agente de Kubernetes que certifica que cada despliegue corre con la imagen esperada."
              "AWS administration: CloudTrail, EC2, S3, EKS, DynamoDB, EventBridge, SQS, and IAM; log ingestion and auditing, S3 object monitoring, and a Kubernetes agent that certifies each deployment runs the expected image."
          web
              "Diseñé y construí de punta a punta una plataforma interna de solicitudes DevOps: micro-frontends en React y TypeScript (Module Federation), microservicios en Go comunicados por NATS, PostgreSQL y JWT, desplegada en Kubernetes."
              "Designed and built an internal DevOps request platform end to end: React and TypeScript micro-frontends (Module Federation), Go microservices over NATS request/reply, PostgreSQL and JWT, deployed on Kubernetes."
          pdf
              "Construí un pipeline orientado a eventos en Java/Quarkus para archivos binarios de medición (FTPS/S3, decodificación a NDJSON, persistencia idempotente, reintentos con arrendamientos) y lo reescribí en C++20 (~30 mil líneas) conservando sus contratos."
              "Built an event-driven Java/Quarkus pipeline for binary metering files (FTPS/S3, NDJSON decoding, idempotent persistence, lease-based retries) and rewrote it in C++20 (~30k lines) keeping every contract intact."
          pdf
              "Migré unas ocho aplicaciones de escritorio WPF y WinForms a Avalonia en .NET 8/10: una sola base de código para Windows, Linux, macOS, Android e iOS, con Clean Architecture, inyección de dependencias e infraestructura simulada para probar sin hardware. Cada migración tomó unas 2–3 semanas, y dejé una plantilla dotnet new para el equipo."
              "Migrated about eight WPF and WinForms desktop apps to Avalonia on .NET 8/10: one codebase for Windows, Linux, macOS, Android, and iOS, with Clean Architecture, dependency injection, and mock infrastructure for hardware-free testing. Each migration took about 2–3 weeks, and I left the team a dotnet new template."
          pdf
              "Construí una app multiplataforma para administrar equipos BLE: aprovisionamiento de red mesh, protocolo de comandos CBOR, actualizaciones de firmware por aire (FOTA), decodificación de archivos de medición y tableros con gráficas y mapas, con unas 400 pruebas automatizadas en GitLab CI."
              "Built a cross-platform app to manage BLE devices: mesh provisioning, a CBOR command protocol, firmware-over-the-air (FOTA) updates, metering-file decoding, and dashboards with charts and maps, backed by about 400 automated tests in GitLab CI."
          web
              "Desarrollé una app de aprovisionamiento de llaves electrónicas IoT por USB-serie y USB-OTG en Android, con protocolo binario cifrado, y la reescribí en F# con MVU (Elmish) y programación funcional pura. Su depurador de protocolo corre en escritorio y como web en Blazor WebAssembly con Web Serial."
              "Developed an IoT electronic-key provisioning app over USB-serial and Android USB-OTG with an encrypted binary protocol, then rewrote it in F# with MVU (Elmish) and pure functional programming. Its protocol debugger runs on desktop and as a Blazor WebAssembly web app using Web Serial."
          web
              "Rediseñé un gateway de telemetría DNP3 para SCADA: el motor OpenDNP3 corre en un proceso nativo aislado por sistema operativo y una Web API con llaves por zona evita que los equipos de campo guarden credenciales de base de datos."
              "Re-architected a DNP3 telemetry gateway for SCADA: the OpenDNP3 engine runs in an isolated native process per OS, and a Web API with zone-scoped keys keeps database credentials off field gateways."
          pdf
              "Publiqué en Android e iOS una app Flutter para operación en campo: inicio de sesión sin conexión con caché cifrada (ChaCha20), cola de sincronización, GPS/GNSS, lectura de códigos y gRPC sobre TLS."
              "Shipped a Flutter field-operations app to Android and iOS: offline login with an encrypted cache (ChaCha20), a sync queue, GPS/GNSS capture, barcode scanning, and gRPC over TLS."
          pdf
              "Creé un servicio en Go que estima la probabilidad de cortes largos por equipo a 6 y 24 horas con datos abiertos de clima y sismos, con su tablero web."
              "Created a Go service that estimates the 6- and 24-hour probability of long outages per grid device from open weather and seismic data, with its own web dashboard."
          web
              "Escribí servicios en Go para pipelines de comandos de medidores e IoT, endurecí el monitoreo de conectividad LTE (límites de tasa, reintentos, detección de anomalías) y mantuve bots de Telegram en TypeScript para técnicos de campo, con almacenamiento intercambiable entre Azure Blob y S3."
              "Wrote Go services for smart-meter and IoT command pipelines, hardened LTE connectivity monitoring (rate limiting, retries, anomaly detection), and maintained TypeScript Telegram bots for field technicians with storage that switches between Azure Blob and S3." ]
      Stack = [ "AWS"; "SQS"; "S3"; "Go"; "NATS"; "Kubernetes"; "Azure"; "Docker"; "C#"; "F#"; "Avalonia"; "C++20"; "Java/Quarkus"; "Flutter"; "TypeScript"; "React"; "PostgreSQL"; "SQL Server" ] }

let private impi =
    { Role = Text.make "Desarrollador .NET Sr." "Sr .NET Developer"
      Company = Text.make "IMPI (Instituto Mexicano de la Propiedad Industrial)" "IMPI (Mexican Institute of Industrial Property)"
      Period = Text.make "Abr 2025 – Jun 2025" "Apr 2025 – Jun 2025"
      Summary = Text.make "" ""
      Highlights =
        [ pdf
              "Aplicaciones WinForms y APIs REST en .NET C#, con procesamiento multihilo."
              "Built .NET C# WinForms applications and REST APIs, including multithreaded processing."
          pdf
              "Administración de servidores Linux, respaldos, benchmarks tecnológicos y repositorios en Azure DevOps."
              "Administered Linux servers, ran backups and technology benchmarks, and managed repositories in Azure DevOps." ]
      Stack = [ "C#"; ".NET"; "WinForms"; "Azure DevOps"; "Linux" ] }

let private alirob =
    { Role = Text.make "Investigador en Robótica e IA" "Robotics and AI Researcher"
      Company = Text.same "ALIROB, Instituto Politécnico Nacional"
      Period = Text.make "Ene 2023 – Ene 2025" "Jan 2023 – Jan 2025"
      Summary = Text.make "" ""
      Highlights =
        [ pdf
              "Simulaciones de autómatas celulares en C# (.NET 6 y 8) aplicadas a vida artificial y computación natural."
              "Built cellular automata simulations in C# (.NET 6 and 8) applied to artificial life and natural computing."
          pdf
              "Diseño y validación de modelos bioinspirados en Physarum polycephalum para navegación autónoma, con simulador en C++ y un robot con Raspberry Pi."
              "Designed and validated bio-inspired models based on Physarum polycephalum for autonomous navigation, with a C++ simulator and a Raspberry Pi robot." ]
      Stack = [ "C#"; "C++"; "SFML"; "Raspberry Pi"; "Java"; "JavaScript" ] }

let value = [ cantilever; impi; alirob ]
