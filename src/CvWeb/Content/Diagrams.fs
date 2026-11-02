/// Arquitecturas de los sistemas que construí, descritas de forma genérica (sin nombres internos).
module CvWeb.Content.Diagrams

open CvWeb.Domain
open CvWeb.Content.DiagramKit

let private cloud =
    { Key = "cloud"
      Title = t "Backend en la nube" "Cloud backend"
      Summary = t "Microservicios que consumen colas y eventos de AWS y se despliegan en Kubernetes sobre Azure." "Microservices consuming AWS queues and events, deployed to Kubernetes on Azure."
      Height = 180
      Zones = [ zone 6 22 300 152 (same "AWS"); zone 326 22 308 152 (same "Kubernetes · Azure") ]
      Boxes =
        [ box 22 44 124 40 (same "S3") (t "eventos" "events")
          box 22 112 124 40 (same "CloudTrail") (t "auditoría" "audit")
          box 172 78 118 40 (same "SQS") (t "colas" "queues")
          box 342 44 158 48 (t "Microservicios" "Microservices") (same "Go · Java")
          box 520 44 98 48 (same "NATS") (t "mensajería" "messaging")
          box 342 116 276 40 (same "SQL Server · PostgreSQL") none ]
      Wires = [ flow 146 64 172 92 0.0; flow 146 132 172 104 0.3; flow 290 98 342 74 1.0; flow 500 68 520 68 1.9; flow 421 92 421 116 2.2 ] }

let private iotKeys =
    { Key = "keys"
      Title = t "Llaves IoT" "IoT keys"
      Summary = t "Una sola base de código (Avalonia y F#) aprovisiona llaves electrónicas por USB y sincroniza con el backend." "One codebase (Avalonia and F#) provisions electronic keys over USB and syncs with the backend."
      Height = 180
      Zones = [ zone 6 22 220 152 (t "Apps · una base de código" "Apps · one codebase") ]
      Boxes =
        [ box 22 44 188 44 (t "Escritorio" "Desktop") (same "Windows · Linux · macOS")
          box 22 112 188 44 (same "Android") (same "USB-OTG")
          box 262 78 150 48 (t "Protocolo cifrado" "Encrypted protocol") (same "USB-serial · PKCS7")
          box 450 30 170 48 (t "Llave electrónica" "Electronic key") (t "permisos y horarios" "permissions · schedules")
          box 450 112 170 48 (same "Backend GraphQL") (t "eventos de apertura" "opening events") ]
      Wires = [ flow 210 66 262 96 0.0; flow 210 134 262 108 0.4; flow 412 92 450 56 1.1; flow 450 64 412 100 2.0; flow 210 140 450 136 2.6 ] }

let private ble =
    { Key = "ble"
      Title = t "Equipos BLE" "BLE devices"
      Summary = t "App multiplataforma que aprovisiona redes mesh, actualiza firmware por aire y grafica mediciones." "Cross-platform app that provisions mesh networks, updates firmware over the air, and charts measurements."
      Height = 180
      Zones = [ zone 6 22 200 152 (t "Avalonia · 5 plataformas" "Avalonia · 5 platforms"); zone 410 22 224 152 (t "Red mesh" "Mesh network") ]
      Boxes =
        [ box 22 44 168 44 (t "Aprovisionamiento" "Provisioning") (same "mesh · FOTA")
          box 22 112 168 44 (t "Tableros y mapas" "Dashboards · maps") (t "archivos de medición" "metering files")
          box 236 78 140 48 (same "BLE GATT") (same "CBOR · CRC16")
          box 426 40 90 40 (t "Equipo" "Device") (same "A")
          box 528 40 90 40 (t "Equipo" "Device") (same "B")
          box 476 116 90 40 (t "Equipo" "Device") (same "C") ]
      Wires = [ flow 190 66 236 96 0.0; flow 376 96 426 64 0.8; wire 516 60 528 60; wire 471 80 500 116; wire 573 80 540 116; flow 236 108 190 134 2.0 ] }

let private dnp3 =
    { Key = "dnp3"
      Title = t "Gateway DNP3" "DNP3 gateway"
      Summary = t "Telemetría de líneas eléctricas hacia SCADA, sin credenciales de base de datos en el equipo de campo." "Power-line telemetry to SCADA, with no database credentials on the field device."
      Height = 180
      Zones = [ zone 6 22 190 152 (t "Centro de datos" "Data center"); zone 214 22 268 152 (t "Equipo de campo · Avalonia" "Field device · Avalonia") ]
      Boxes =
        [ box 22 44 158 40 (same "SQL Server") (t "lecturas" "readings")
          box 22 112 158 44 (same "Web API") (t "llaves por zona" "zone-scoped keys")
          box 230 78 110 48 (same "Gateway") (same ".NET 8")
          box 356 78 110 48 (same "OpenDNP3") (t "proceso nativo" "native process")
          box 510 78 120 48 (t "Maestro SCADA" "SCADA master") (same "DNP3 / TCP") ]
      Wires = [ flow 101 84 101 112 0.0; flow 180 134 230 102 0.6; flow 340 102 356 102 1.4; flow 466 102 510 102 2.0 ] }

let private devops =
    { Key = "devops"
      Title = t "Plataforma DevOps" "DevOps platform"
      Summary = t "Solicitudes de infraestructura con flujo por roles, de los micro-frontends a los microservicios." "Infrastructure requests with a role-based flow, from micro-frontends to microservices."
      Height = 180
      Zones = [ zone 6 22 196 152 (same "React · Module Federation"); zone 222 22 412 152 (same "Go · Kubernetes") ]
      Boxes =
        [ box 22 44 164 40 (same "Shell") (t "navegación" "navigation")
          box 22 112 164 44 (same "Micro-frontends") (t "solicitudes · tablero · admin" "requests · dashboard · admin")
          box 238 78 110 48 (same "Gateway") (same "JWT")
          box 368 78 90 48 (same "NATS") (same "request/reply")
          box 478 40 140 38 (same "Auth") none
          box 478 88 140 38 (t "Tickets" "Tickets") none
          box 478 136 140 30 (t "Catálogos" "Catalogs") none ]
      Wires = [ flow 104 84 104 112 0.0; flow 186 120 238 102 0.5; flow 348 102 368 102 1.2; flow 458 94 478 59 1.7; flow 458 102 478 107 1.8; flow 458 110 478 151 1.9 ] }

let private field =
    { Key = "field"
      Title = t "App de campo" "Field app"
      Summary = t "Registro sin conexión con cola cifrada que concilia con el servidor antes de reenviar." "Offline logging with an encrypted queue that reconciles with the server before resending."
      Height = 180
      Zones = [ zone 6 22 300 152 (t "Teléfono · Flutter" "Phone · Flutter"); zone 340 22 294 152 (same "Backend") ]
      Boxes =
        [ box 22 44 124 40 (t "Fotos · GPS" "Photos · GPS") (t "escaneo" "scanning")
          box 22 112 124 44 (t "Almacén cifrado" "Encrypted store") (same "AES")
          box 168 78 124 48 (t "Cola persistente" "Persistent queue") (t "reintentos" "retries")
          box 356 44 128 48 (same "gRPC · TLS") (same "JWT")
          box 500 44 118 48 (t "Conciliar" "Reconcile") (t "sin duplicados" "no duplicates")
          box 356 116 262 40 (t "Registros e imágenes" "Records and images") none ]
      Wires = [ flow 146 64 168 92 0.0; flow 84 84 84 112 0.4; flow 292 98 356 70 1.0; flow 484 68 500 68 1.7; flow 487 92 487 116 2.3 ] }

let value = [ cloud; iotKeys; ble; dnp3; devops; field ]
