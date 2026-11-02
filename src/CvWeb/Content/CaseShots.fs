module CvWeb.Content.CaseShots

open CvWeb.Domain

let private shot name width height es en =
    { Source = $"img/cases/{name}.webp"; Width = width; Height = height; Caption = Text.make es en }

let bleDesktop =
    shot "ble-groups" 1440 900
        "Gestión de grupos BLE y detalle de sus medidores en escritorio."
        "Desktop BLE group management and meter details."

let bleMobile =
    shot "ble-mobile" 430 860
        "La misma vista de grupos, adaptada a una pantalla móvil."
        "The same group view, adapted to a mobile screen."

let gatewayPanel =
    shot "gateway-panel" 1440 900
        "Panel operativo del gateway con el servicio DNP3 simulado."
        "Gateway operations dashboard with a simulated DNP3 service."

let gatewayMeasurements =
    shot "gateway-measurements" 1440 900
        "Mediciones analógicas y estados digitales de dispositivos simulados."
        "Analog measurements and digital states from simulated devices."

let keyAnalysis =
    shot "key-analysis" 1200 760
        "Aprovisionamiento de llaves electrónicas y bitácora del análisis en modo mock."
        "Electronic-key provisioning and analysis log in mock mode."

let deviceConfiguration =
    shot "device-configuration" 1200 850
        "Configuración de un equipo de medición con datos de demostración."
        "Metering-device configuration with demonstration data."

let qualityHistory =
    shot "quality-history" 1440 920
        "Historial de pruebas aprobadas y evidencia ficticia de control de calidad."
        "Passed test history and sample quality-control evidence."

let fieldHome =
    shot "field-home" 540 1200
        "Inicio de la app de campo con el avance de instalaciones y capacitaciones (cuenta de prueba)."
        "Field app home with installation and training progress (test account)."

let fieldInstalls =
    shot "field-installs" 540 1200
        "Instalaciones pendientes de envío en la cola sin conexión; beneficiarios y folios ficticios."
        "Installations waiting in the offline upload queue; sample beneficiaries and folios."

let disclosure =
    Text.make "Vistas reales con datos simulados; nombres y marcas anonimizados."
        "Real views with simulated data; names and branding anonymized."

let enlarge = Text.make "Ampliar captura" "Enlarge screenshot"
