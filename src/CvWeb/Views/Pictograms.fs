/// Pictogramas de trazo (24×24) que identifican cada proyecto; heredan el color con `currentColor`.
module CvWeb.Views.Pictograms

let private shapes =
    dict
        [ // Investigación
          "bees", """<path d="M8 3.5l3.5 2v4L8 11.5l-3.5-2v-4z"/><path d="M15.5 3.5l3.5 2v4l-3.5 2-3.5-2v-4z"/><path d="M11.8 10.5l3.5 2v4l-3.5 2-3.5-2v-4z"/><circle cx="18.5" cy="17.5" r="2"/><path d="M17 16l-1.5-1.5M20 16l1.5-1.5"/>"""
          "sierpinski", """<path d="M12 3l9 16H3z"/><path d="M7.5 11h9M12 19l-4.5-8M12 19l4.5-8"/>"""
          "glider", """<rect x="9.5" y="3.5" width="5" height="5" rx="1"/><rect x="15.5" y="9.5" width="5" height="5" rx="1"/><rect x="3.5" y="15.5" width="5" height="5" rx="1"/><rect x="9.5" y="15.5" width="5" height="5" rx="1"/><rect x="15.5" y="15.5" width="5" height="5" rx="1"/>"""
          "traffic", """<path d="M9 2v20M15 2v20M2 9h20M2 15h20"/><rect x="10.5" y="4" width="3" height="3" rx=".6"/><rect x="17" y="10.5" width="3" height="3" rx=".6"/>"""
          // Trabajo
          "cloud", """<path d="M7 17a4 4 0 0 1-.5-8A5.5 5.5 0 0 1 17 8a4.5 4.5 0 0 1 .5 9z"/><path d="M8 20.5h2M11 20.5h2M14 20.5h2"/>"""
          "pipeline", """<rect x="2.5" y="8.5" width="5" height="7" rx="1"/><rect x="9.5" y="8.5" width="5" height="7" rx="1"/><rect x="16.5" y="8.5" width="5" height="7" rx="1"/><path d="M7.5 12h2M14.5 12h2M5 8.5V5h14v3.5"/>"""
          "bluetooth", """<path d="M7 7.5l10 9-5 4.5V3l5 4.5-10 9"/><circle cx="3.5" cy="12" r="1"/><circle cx="20.5" cy="12" r="1"/>"""
          "key", """<circle cx="7.5" cy="12" r="4.5"/><path d="M12 12h9.5M18 12v3.5M15 12v2.5"/>"""
          "cpp", """<path d="M10 8.5a4 4 0 1 0 0 7"/><path d="M13 12h4M15 10v4M18.5 12h3.5M20.25 10v4"/><path d="M2 21h20M2 3h20"/>"""
          // Personales
          "braces", """<path d="M8 3.5c-2 0-2.5 1-2.5 3v3c0 1-1 2.5-2.5 2.5 1.5 0 2.5 1.5 2.5 2.5v3c0 2 .5 3 2.5 3"/><path d="M16 3.5c2 0 2.5 1 2.5 3v3c0 1 1 2.5 2.5 2.5-1.5 0-2.5 1.5-2.5 2.5v3c0 2-.5 3-2.5 3"/><circle cx="9.5" cy="12" r=".9"/><circle cx="12" cy="12" r=".9"/><circle cx="14.5" cy="12" r=".9"/>"""
          "ball", """<circle cx="12" cy="12" r="9"/><path d="M12 8l3.3 2.4-1.3 3.9h-4l-1.3-3.9z"/><path d="M12 8V3.5M15.3 10.4l4.2-1.4M14 14.3l2.6 3.6M10 14.3l-2.6 3.6M8.7 10.4L4.5 9"/>"""
          "lambda", """<path d="M5 4h3l9 16h3"/><path d="M11 11.5L6 20"/>"""
          // Académicos
          "book", """<path d="M4 4.5A1.5 1.5 0 0 1 5.5 3H20v15H5.5A1.5 1.5 0 0 0 4 19.5z"/><path d="M4 19.5A1.5 1.5 0 0 0 5.5 21H20v-3"/><path d="M8 7h8M8 10h6"/>"""
          "chip", """<rect x="6" y="6" width="12" height="12" rx="1.5"/><rect x="9.5" y="9.5" width="5" height="5"/><path d="M9 2v4M15 2v4M9 18v4M15 18v4M2 9h4M2 15h4M18 9h4M18 15h4"/>"""
          "states", """<circle cx="6" cy="12" r="3.5"/><circle cx="18" cy="12" r="3.5"/><circle cx="18" cy="12" r="2"/><path d="M9.5 12h5M13 10.5l1.5 1.5-1.5 1.5"/><path d="M4 8.5C4 5 8 5 8 8.5"/>"""
          "puzzle", """<rect x="3" y="3" width="18" height="18" rx="2"/><path d="M9 3v18M15 3v12M3 9h18M3 15h12"/><rect x="16" y="16" width="4" height="4" rx=".5"/>"""
          "network", """<rect x="9" y="2.5" width="6" height="5" rx="1"/><rect x="2.5" y="16.5" width="6" height="5" rx="1"/><rect x="15.5" y="16.5" width="6" height="5" rx="1"/><path d="M12 7.5V12M5.5 16.5V12h13v4.5"/>"""
          "graph", """<circle cx="4.5" cy="6" r="2"/><circle cx="12" cy="4" r="2"/><circle cx="19.5" cy="9" r="2"/><circle cx="8" cy="18" r="2"/><circle cx="17" cy="19" r="2"/><path d="M6.4 6.6l11.2 2M5.2 7.9l2.1 8.2M10 18.2l5 .6M18.9 11l-1.4 6"/>"""
          "vr", """<path d="M3 8.5A1.5 1.5 0 0 1 4.5 7h15A1.5 1.5 0 0 1 21 8.5v6a1.5 1.5 0 0 1-1.5 1.5H15l-1.5-2.5h-3L9 16H4.5A1.5 1.5 0 0 1 3 14.5z"/><circle cx="8" cy="11.5" r="1.5"/><circle cx="16" cy="11.5" r="1.5"/>"""
          "wrench", """<path d="M14.5 5.5a4 4 0 0 0 5 5L12 18a2.1 2.1 0 1 1-3-3l7.5-7.5"/><path d="M19.5 10.5l-2-.5-.5-2 2.5-2.5a4 4 0 0 0-5 5"/><path d="M3 21l3-3"/>"""
          "metro", """<path d="M2 12h20"/><circle cx="5" cy="12" r="2"/><circle cx="12" cy="12" r="2"/><circle cx="19" cy="12" r="2"/><path d="M12 4v6M12 14v6"/><path d="M5 6l3 3M19 18l-3-3"/>"""
          "periodic", """<rect x="2.5" y="4" width="4" height="4"/><rect x="17.5" y="4" width="4" height="4"/><rect x="2.5" y="9" width="4" height="4"/><rect x="7.5" y="9" width="4" height="4"/><rect x="12.5" y="9" width="4" height="4"/><rect x="17.5" y="9" width="4" height="4"/><rect x="2.5" y="14" width="4" height="4"/><rect x="7.5" y="14" width="4" height="4"/><rect x="12.5" y="14" width="4" height="4"/><rect x="17.5" y="14" width="4" height="4"/>""" ]

/// SVG del pictograma o cadena vacía si la clave no existe.
let svg (key: string) =
    match shapes.TryGetValue key with
    | true, body ->
        $"""<svg class="pictogram" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.4" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true">{body}</svg>"""
    | _ -> ""
