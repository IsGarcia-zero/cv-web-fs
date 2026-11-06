module CvWeb.Content.Skills

open CvWeb.Domain

let private group es en items = { Title = Text.make es en; Items = items }

/// Lo que uso a diario va en sus grupos; lo que sólo he usado queda al final, aparte.
let value =
    [ group "Lenguajes de programación" "Programming languages" [ "Go"; "C#"; "F#"; "C/C++"; "Java"; "TypeScript"; "JavaScript"; "Dart" ]
      group "Backend" "Backend" [ "NATS"; "REST / OpenAPI"; "gRPC"; "Quarkus"; "ASP.NET Core"; "Node.js"; "JWT" ]
      group "Nube (AWS)" "Cloud (AWS)" [ "SQS"; "S3"; "EC2"; "EKS"; "CloudTrail"; "EventBridge"; "DynamoDB"; "IAM / STS" ]
      group "Infraestructura" "Infrastructure" [ "Kubernetes"; "Kustomize"; "Docker"; "Azure"; "GitLab CI"; "GitHub Actions"; "Linux"; "Git" ]
      group "Bases de datos" "Databases" [ "PostgreSQL"; "SQL Server"; "DynamoDB"; "SQLite" ]
      group "Escritorio y móvil" "Desktop & mobile" [ "Avalonia"; "WPF"; "WinForms"; "Flutter"; "Android"; "iOS" ]
      group "Frontend" "Frontend" [ "React"; "Vite"; "Module Federation"; "Blazor WebAssembly"; "Bolero" ]
      group "Protocolos e IoT" "Protocols & IoT" [ "BLE / mesh"; "CBOR"; "FOTA"; "DNP3"; "USB-serial"; "Web Serial"; "TCP/UDP"; "FTPS" ]
      group "Arquitectura y calidad" "Architecture & quality" [ "Clean Architecture"; "MVU / Elmish"; "MVVM"; "Microservices"; "xUnit"; "Contract tests"; "ADRs" ]
      group "También he usado" "Also used"
          [ "Python"; "Lua"; "VHDL"; "Assembly"; "GraphQL"; "Express"; "MySQL"; "Oracle DB"; "JavaFX"; "SFML"; "PHP"; "JSP"; "Azure DevOps" ] ]

let private card icon title description tags =
    { Icon = icon
      Title = title
      Description = description
      Tags = tags }

let soft =
    [ card "🤝" (Text.make "Trabajo en equipo" "Teamwork")
          (Text.make "Colaboración con equipos multidisciplinarios de investigación y desarrollo." "Collaboration with multidisciplinary research and development teams.") []
      card "🔄" (Text.make "Adaptabilidad" "Adaptability")
          (Text.make "Me adapto rápido a nuevos entornos, tecnologías y retos." "Quick to adapt to new environments, technologies, and challenges.") []
      card "🧩" (Text.make "Resolución de problemas" "Problem solving")
          (Text.make "Análisis y solución de problemas complejos bajo presión y plazos cortos." "Analyzing and solving complex problems under pressure and tight deadlines.") []
      card "🎯" (Text.make "Liderazgo" "Leadership")
          (Text.make "Organicé torneos de más de 32 participantes: planeación, equipos y calendario." "Organized tournaments of 32+ participants: planning, teams, and schedules.") []
      card "📝" (Text.make "Comunicación técnica" "Technical communication")
          (Text.make "Presentación de investigación en simposios internacionales y documentación técnica (ADRs, guías, reportes)." "Presenting research at international symposia and writing technical docs (ADRs, guides, reports).") []
      card "📚" (Text.make "Aprendizaje continuo" "Continuous learning")
          (Text.make "Autodidacta en lenguajes, frameworks y paradigmas: el mismo servicio en 26 lenguajes." "Self-taught across languages, frameworks, and paradigms: the same service in 26 languages.") [] ]

let interests =
    [ card "🧬" (Text.make "Sistemas bioinspirados" "Bio-inspired systems")
          (Text.make "Autómatas celulares, Physarum polycephalum, inteligencia de enjambre y computación no convencional." "Cellular automata, Physarum polycephalum, swarm intelligence, and unconventional computing.")
          [ "Cellular Automata"; "Physarum"; "Swarm Intelligence"; "Emergent Systems" ]
      card "🎮" (Text.make "Desarrollo de videojuegos" "Game development")
          (Text.make "Simulación, renderizado en tiempo real y servidores de juego con C++ y Lua." "Simulation, real-time rendering, and game servers with C++ and Lua.")
          [ "C++"; "SFML"; "Vulkan"; "Lua"; "Game Engines" ]
      card "⚙️" (Text.make "Sistemas de bajo nivel" "Low-level systems")
          (Text.make "Arquitectura de computadoras, diseño digital en FPGA, Assembly y cómputo en GPU." "Computer architecture, FPGA digital design, Assembly, and GPU computing.")
          [ "VHDL"; "FPGA"; "Assembly"; "CUDA"; "C++20" ]
      card "☁️" (Text.make "Nube y sistemas distribuidos" "Cloud & distributed systems")
          (Text.make "Backends orientados a eventos sobre colas y almacenamiento de AWS, desplegados en Kubernetes." "Event-driven backends on AWS queues and storage, deployed to Kubernetes.")
          [ "AWS"; "SQS"; "Kubernetes"; "Go"; "NATS"; "Microservices" ]
      card "📱" (Text.make "Apps multiplataforma e IoT" "Cross-platform apps & IoT")
          (Text.make "Una sola base de código para escritorio y móvil que habla con hardware por BLE y USB." "One codebase for desktop and mobile that talks to hardware over BLE and USB.")
          [ "Avalonia"; "F#"; "Flutter"; "BLE"; "USB-serial" ] ]
