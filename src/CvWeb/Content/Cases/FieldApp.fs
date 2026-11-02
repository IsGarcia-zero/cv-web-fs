module CvWeb.Content.Cases.FieldApp

open CvWeb.Domain
open CvWeb.Content

let private t = Text.make
let private metric value es en = { Value = value; Caption = t es en }

let value =
    { Key = "field"
      TabTitle = t "App de campo" "Field app"
      Title = t "App móvil offline-first para operaciones en campo" "Offline-first mobile app for field operations"
      Tagline =
        t "App Flutter para registrar instalaciones y capacitaciones en sitio, con fotos, GPS y escaneo, que funciona sin señal y nunca duplica ni pierde registros."
          "A Flutter app to log on-site installations and training, with photos, GPS and scanning, that works without signal and never duplicates or loses records."
      Metrics =
        [ metric "2" "tiendas: Android e iOS" "stores: Android and iOS"
          metric "182" "pruebas automatizadas" "automated tests"
          metric "30k" "líneas de Dart" "lines of Dart"
          metric "≤5 m" "precisión GPS objetivo" "target GPS accuracy" ]
      Challenge =
        t "Las cuadrillas registran instalaciones (fotos antes, durante y después, GPS, números de serie) y capacitaciones en sitios con mala conectividad. Nada podía perderse ni duplicarse, y los datos en el teléfono debían ir cifrados."
          "Crews log installations (before/during/after photos, GPS, serial numbers) and training at sites with poor connectivity. Nothing could be lost or duplicated, and data on the phone had to be encrypted."
      Decisions =
        [ t "Flutter con MVVM (estado inmutable), repositorios y servicios; gRPC y autenticación con tokens de acceso y renovación."
            "Flutter with MVVM (immutable state), repositories and services; gRPC and access/refresh-token authentication."
          t "Offline-first: almacén local cifrado, fotos cifradas por archivo y una cola persistente con reintentos que concilia con el servidor antes de reenviar."
            "Offline-first: encrypted local store, per-file encrypted photos, and a persistent retrying queue that reconciles with the server before resending."
          t "Inicio de sesión sin conexión con ChaCha20-Poly1305 y contraseñas en Argon2id."
            "Offline login with ChaCha20-Poly1305 and Argon2id password hashes."
          t "Cifrado y serialización en isolates para que la interfaz no se trabe; builds de release que fallan si TLS está apagado."
            "Encryption and serialization in isolates so the UI never stalls; release builds fail if TLS is off." ]
      Outcome =
        [ t "Publicada en Google Play y App Store para un proyecto específico, con variantes demo y producción independientes." "Published on Google Play and the App Store for a specific project, with independent demo and production variants."
          t "La conciliación antes de reenviar evita registros duplicados tras una interrupción." "Reconcile-before-resend prevents duplicate records after interruptions." ]
      Role = t "33 de 36 commits; un colega se encargó de la firma para iOS." "33 of 36 commits; a colleague handled iOS signing."
      Stack = [ "Flutter"; "Dart"; "gRPC"; "protobuf"; "JWT"; "ChaCha20"; "Argon2id"; "GPS"; "Android"; "iOS" ]
      Visual = Screenshots [ CaseShots.fieldHome; CaseShots.fieldInstalls ]
      Note = None }

let all = [ Physarum.value; BleApp.value; Gateway.value; Migrations.value; value ]
