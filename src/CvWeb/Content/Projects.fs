module CvWeb.Content.Projects

open CvWeb.Domain
open CvWeb.Content.ProjectKit

let private research =
    [ project Research (Text.same "Physarum Automata") (Text.same "2024 – 2026")
          [ "C++"; "Vulkan"; "SFML"; "ImGui"; "OpenCV"; "Raspberry Pi" ]
          (Some "https://github.com/Jessenox/Physarum-Automata")
          ("Simulación de Physarum polycephalum con autómatas celulares para navegación autónoma de un robot (proyecto de tesis). Versión con SFML e ImGui y migración en 2026 a Vulkan con shaders de cómputo.",
           "Physarum polycephalum simulation with cellular automata for autonomous robot navigation (thesis project). SFML and ImGui version, moved to Vulkan compute shaders in 2026.")
      |> live
      project Research (Text.make "Ruta más corta — Colonia de Abejas Artificiales" "Shortest path — Artificial Bee Colony") (Text.same "2026")
          [ "C++"; "Swarm Intelligence"; "Metaheuristics" ] None
          ("Algoritmo de Colonia de Abejas Artificiales (ABC) para el problema de ruta más corta, comparado con algoritmos clásicos. Inteligencia de enjambre y metaheurísticas aplicadas.",
           "Artificial Bee Colony (ABC) algorithm for the shortest-path problem, compared against classic algorithms. Applied swarm intelligence and metaheuristics.")
      |> art "bees"
      |> withStatus unpublished
      project Research (Text.make "Autómatas celulares elementales" "Elementary cellular automata") (Text.make "2023 – 2024" "2023 – 2024")
          [ "C#"; ".NET"; "WinForms"; "LaTeX" ] (gh "cellularAutomataECARules")
          ("Análisis y visualización de las 256 reglas de Wolfram: evolución, densidad lineal y logarítmica, atractores y grafos, con reportes en LaTeX generados automáticamente.",
           "Analysis and visualization of all 256 Wolfram rules: evolution, linear and log density, attractors, and graphs, with auto-generated LaTeX reports.")
      |> art "sierpinski"
      project Research (Text.make "Juego de la Vida" "Game of Life") (Text.same "2023")
          [ "C#"; ".NET"; "WinForms"; "Graphviz" ] (gh "juegoDeLaVida")
          ("Autómata de Conway con reglas B/S configurables, gráficas de población y cálculo de atractores exportados a Graphviz.",
           "Conway's automaton with configurable B/S rules, population charts, and attractor analysis exported to Graphviz.")
      |> art "glider"
      project Research (Text.make "Control de tráfico con ECA" "Traffic control with ECA") (Text.same "2023")
          [ "C#"; ".NET"; "WinForms" ] (gh "traficoControlECARules")
          ("Simulación de un cruce vial de dos calles con la regla 184 y manejo de conflictos en la intersección.",
           "Two-street intersection simulation using rule 184, with conflict handling at the crossing.")
      |> art "traffic" ]

let private personal =
    [ project Personal (Text.make "Laboratorio de APIs en 26 lenguajes" "API lab in 26 languages") (Text.same "2025 – 2026")
          [ "Go"; "Rust"; "C++"; "F#"; "Kotlin"; "Elixir"; "Haskell"; "Kubernetes" ] None
          ("El mismo microservicio (API de medidores con comandos por NATS) en 26 lenguajes y frameworks, con pruebas de contrato compartidas y pruebas de carga en Kubernetes.",
           "The same microservice (a meters API with commands over NATS) in 26 languages and frameworks, with shared contract tests and Kubernetes load testing.")
      |> art "braces"
      |> withStatus localOnly
      project Personal (Text.make "Pronóstico Mundial 2026" "World Cup 2026 forecast") (Text.same "2026")
          [ "Python"; "XGBoost"; "scikit-learn" ] None
          ("Pronóstico de la Copa del Mundo 2026 con ELO, XGBoost y simulación Monte Carlo del cuadro, con resultados interactivos en HTML.",
           "World Cup 2026 forecast using ELO, XGBoost, and Monte Carlo bracket simulation, with interactive HTML output.")
      |> art "ball"
      |> withStatus localOnly
      project Personal (Text.make "CV en F#" "CV in F#") (Text.same "2026")
          [ "F#"; "Bolero"; "Blazor WebAssembly"; "Elmish" ] None
          ("Este sitio: arquitectura MVU con estado inmutable, contenido bilingüe tipado y efectos aislados.",
           "This site: MVU architecture with immutable state, typed bilingual content, and isolated effects.")
      |> art "lambda" ]

let private work =
    [ project Work (Text.make "Backend en AWS orientado a eventos" "Event-driven AWS backend") (Text.same "2025 – 2026")
          [ "Go"; "Java"; "AWS SQS"; "S3"; "CloudTrail"; "NATS"; "Kubernetes" ] None
          ("Microservicios que consumen colas de SQS y eventos de S3 y CloudTrail: ingesta y auditoría de logs, vigilancia de objetos y procesamiento de archivos de medición, desplegados en Kubernetes sobre Azure.",
           "Microservices consuming SQS queues and S3 and CloudTrail events: log ingestion and auditing, object monitoring, and metering-file processing, deployed to Kubernetes on Azure.")
      |> art "cloud"
      |> withStatus privateCode
      project Work (Text.make "Plataforma de solicitudes DevOps" "DevOps request platform") (Text.same "2026")
          [ "React"; "TypeScript"; "Go"; "NATS"; "PostgreSQL"; "Kubernetes" ] None
          ("Micro-frontends con Module Federation y microservicios en Go con flujo de aprobación por roles, campos dinámicos por tipo de solicitud y generación de formatos PDF.",
           "Module Federation micro-frontends and Go microservices with a role-based approval flow, dynamic fields per request type, and PDF form generation.")
      |> art "pipeline"
      |> withStatus privateCode
      project Work (Text.make "Gestión de equipos BLE" "BLE device management") (Text.same "2026")
          [ "C#"; "Avalonia"; "BLE"; "CBOR"; "FOTA"; "GitLab CI" ] None
          ("App para escritorio y móvil: aprovisionamiento mesh, comandos CBOR, firmware por aire, decodificación de mediciones y tableros con mapas. Unas 400 pruebas automatizadas.",
           "Desktop and mobile app: mesh provisioning, CBOR commands, firmware over the air, metering decoding, and map dashboards. About 400 automated tests.")
      |> art "bluetooth"
      |> withStatus privateCode
      project Work (Text.make "Llaves IoT en F#" "IoT keys in F#") (Text.same "2026")
          [ "F#"; "Avalonia.FuncUI"; "Elmish"; "Android"; "USB-serial" ] None
          ("Reescritura funcional pura de una app de aprovisionamiento de llaves electrónicas: funciones totales con Result, pasos tipo railway y E/S en los bordes.",
           "Pure functional rewrite of an electronic-key provisioning app: total functions returning Result, railway-style steps, and I/O at the edges.")
      |> art "key"
      |> withStatus privateCode
      project Work (Text.make "Pipeline de mediciones en C++20" "C++20 metering pipeline") (Text.same "2026")
          [ "C++20"; "NATS"; "PostgreSQL"; "S3"; "Docker" ] None
          ("Reescritura de cinco servicios Quarkus a C++20 (~30 mil líneas) con contrapresión, arrendamientos y persistencia idempotente, sin cambiar sus contratos.",
           "Rewrite of five Quarkus services in C++20 (~30k lines) with backpressure, leases, and idempotent persistence, keeping their contracts.")
      |> art "cpp"
      |> withStatus privateCode ]

let value = research @ work @ personal @ ProjectsAcademic.value
