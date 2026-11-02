module CvWeb.Content.Profile

open CvWeb.Domain

let value =
    { Name = "Eduardo Hernández Vergara"
      Photo = "img/eduardo.webp"
      Role = Text.make "Ingeniero en Sistemas Computacionales" "Computer Systems Engineer"
      Tagline =
        Text.make
            "Construyo backends en la nube, apps multiplataforma y herramientas para equipos IoT. Backend & Cloud · Go · AWS · .NET · F#."
            "I build cloud backends, cross-platform apps, and tooling for IoT devices. Backend & Cloud · Go · AWS · .NET · F#."
      Location = Text.make "Ciudad de México, México" "Mexico City, Mexico"
      Summary =
        Text.make
            "Ingeniero de software enfocado en backend y nube. Diseñé y construí solo el backend en AWS de una plataforma IoT para la red eléctrica: microservicios en Go sobre SQS y NATS, desplegados en Kubernetes y auditados con CloudTrail, más un pipeline de archivos de medición con persistencia idempotente y reintentos. También llevé unas ocho apps de escritorio a una sola base de código multiplataforma y escribí apps que hablan con hardware por BLE y USB. Vengo de la investigación en sistemas complejos y autómatas celulares: tesis con mención honorífica y un artículo en CANDAR 2024. Busco roles de backend, nube o desarrollo de software, presenciales, híbridos o remotos; también disponible para reubicación."
            "Software engineer focused on backend and cloud. I designed and built, on my own, the AWS backend of an IoT platform for the power grid: Go microservices on SQS and NATS, deployed to Kubernetes and audited with CloudTrail, plus a metering-file pipeline with idempotent persistence and retries. I also moved about eight desktop apps to a single cross-platform codebase and wrote apps that talk to hardware over BLE and USB. I come from research in complex systems and cellular automata: an honors thesis and a CANDAR 2024 paper. Looking for backend, cloud, or software engineering roles, on-site, hybrid, or remote; also open to relocation."
      Highlights =
        [ Text.make
              "Publicación internacional en CANDAR 2024 (Okinawa, Japón): algoritmo bioinspirado en Physarum polycephalum"
              "International publication at CANDAR 2024 (Okinawa, Japan): a Physarum polycephalum-inspired algorithm"
          Text.make
              "Titulación con mención honorífica por tesis en sistemas autónomos con autómatas celulares"
              "Graduated with honors with a thesis on autonomous systems built on cellular automata"
          Text.make
              "Backend en la nube hecho por mí de punta a punta: microservicios en Go sobre AWS SQS y NATS, auditados con CloudTrail, con unos 10 mil mensajes al día"
              "Cloud backend built end to end by me: Go microservices on AWS SQS and NATS, audited with CloudTrail, handling about 10k messages a day"
          Text.make
              "Apps multiplataforma con Avalonia, Flutter y F# para equipos IoT por BLE y USB"
              "Cross-platform apps with Avalonia, Flutter, and F# for IoT devices over BLE and USB"
          Text.make
              "Sistemas de bajo nivel: VHDL, FPGA, Assembly, C++20, Vulkan y Raspberry Pi"
              "Low-level systems: VHDL, FPGA, Assembly, C++20, Vulkan, and Raspberry Pi" ]
      Email = "eduardo.hernandezv@proton.me"
      LinkedIn =
        { Label = "LinkedIn"
          Url = "https://www.linkedin.com/in/eduardo-hernandez-vergara-369366246" }
      GitHub = { Label = "GitHub"; Url = "https://github.com/IsGarcia-zero" } }
