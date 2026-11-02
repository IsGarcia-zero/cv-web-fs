/// Piezas que repiten todas las secciones.
module CvWeb.Views.Common

open Bolero
open Bolero.Html
open CvWeb.Domain
open CvWeb.Content

let tr lang t = text (Text.pick lang t)

// ── Atributos ──

let cls (name: string) = attr.``class`` name

/// Clase con un modificador que se agrega cuando `flag` es verdadero: `tab` / `tab on`.
let clsIf flag (name: string) (modifier: string) =
    cls (if flag then $"{name} {modifier}" else name)

/// ARIA espera "true"/"false"; un bool de Bolero dejaría el atributo vacío.
let ariaBool (value: bool) = if value then "true" else "false"

/// Decorativo: los lectores de pantalla lo saltan.
let decorative = "aria-hidden" => "true"

/// Botón que no envía formularios.
let plainButton = attr.``type`` "button"

// ── Contenido condicional ──
// `cond` le dice a Blazor qué rama cambió; un `if` o `match` sueltos confunden su diff.

let whenSome (value: 'a option) (view: 'a -> Node) =
    cond value <| function
        | Some x -> view x
        | None -> empty ()

let whenTrue flag (view: unit -> Node) =
    cond flag <| function
        | true -> view ()
        | false -> empty ()

let forEachIndexed (items: 'a list) (view: int -> 'a -> Node) =
    forEach (List.indexed items) (fun (i, item) -> view i item)

/// Escapa texto para el SVG que se arma como cadena: `rawHtml` lo inserta tal cual.
let esc (s: string) =
    s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;")

// ── Estructura ──

/// Sección con ancla, rótulo y aparición al hacer scroll (`cv.js`).
let block id lang (title: Text) (body: Node) =
    section {
        attr.id id
        cls "block reveal"
        "aria-labelledby" => $"{id}-title"
        h2 { attr.id $"{id}-title"; cls "block-title"; tr lang title }
        body
    }

/// Fila de lista: columna de metadatos (fecha, categoría) y columna de contenido.
let row (meta: Node) (body: Node) =
    article {
        cls "row"
        div { cls "row-meta"; meta }
        div { cls "row-body"; body }
    }

/// Contenedor de filas: al pasar el puntero por una, las demás se atenúan.
let rows (items: Node) = div { cls "rows"; items }

/// Párrafo que no se dibuja si el texto está vacío.
let paragraph lang (t: Text) =
    match Text.pick lang t with
    | "" -> empty ()
    | s -> p { text s }

let bullets lang (items: Text list) =
    match items with
    | [] -> empty ()
    | _ -> ul { cls "bullets"; forEach items (fun i -> li { tr lang i }) }

/// Tecnologías como etiquetas.
let tags (items: string list) =
    match items with
    | [] -> empty ()
    | _ -> ul { cls "tags"; "aria-label" => "Stack"; forEach items (fun t -> li { text t }) }

// ── Enlaces y botones ──

/// Aviso sólo para lectores de pantalla en enlaces que abren otra pestaña.
let newTabHint lang =
    span { cls "sr-only"; text $" {Text.pick lang Labels.newTab}" }

let externalLink className (url: string) (label: Node) =
    a {
        cls className
        attr.href url
        attr.target "_blank"
        attr.rel "noopener"
        label
    }

/// Enlace externo con nombre fijo (GitHub, LinkedIn) y el aviso de pestaña nueva.
let namedLink lang className (name: string) (url: string) =
    externalLink className url (concat { text name; newTabHint lang })

/// Título de fila que es enlace cuando hay URL, con flecha que se mueve al pasar el puntero.
let rowTitle lang (title: Text) (url: string option) =
    match url with
    | Some u ->
        h3 {
            externalLink "row-link" u (concat {
                tr lang title
                span { cls "arrow"; decorative; " ↗" }
                newTabHint lang
            })
        }
    | None -> h3 { tr lang title }

/// "Ver más / ver menos" de divulgación progresiva, con su flecha que gira.
let disclosure className expanded (controls: string) (label: Node) (toggle: unit -> unit) =
    button {
        plainButton
        cls className
        "aria-expanded" => ariaBool expanded
        "aria-controls" => controls
        on.click (fun _ -> toggle ())
        label
        span { clsIf expanded "chev" "up"; decorative }
    }

// ── Pestañas (patrón de WAI-ARIA: clic, flechas izquierda/derecha, Inicio y Fin) ──

/// `Id` da nombre a las pestañas (`{Id}-tab-{n}`) y al panel (`{Id}-panel`).
/// `Select` recibe el índice deseado; quien lo maneje da la vuelta en los extremos.
type Tabs<'item> =
    { Id: string
      Label: Text
      Items: 'item list
      Title: 'item -> Text
      Selected: int
      Select: int -> unit }

let tabId id index = $"{id}-tab-{index}"
let panelId id = $"{id}-panel"

/// Botón ‹ o › de las pestañas desplazables (Material 3). `tabs.js` lo muestra sólo si hay más
/// pestañas hacia ese lado; el teclado no lo necesita porque las flechas ya recorren las pestañas.
let private scrollButton (direction: int) (glyph: string) =
    button {
        plainButton
        cls (if direction < 0 then "tabs-btn prev" else "tabs-btn next")
        "data-scroll" => direction
        decorative
        attr.tabindex -1
        text glyph
    }

let private tab lang (tabs: Tabs<'item>) index (item: 'item) =
    let isOn = index = tabs.Selected
    button {
        plainButton
        attr.id (tabId tabs.Id index)
        clsIf isOn "tab" "on"
        "role" => "tab"
        "aria-selected" => ariaBool isOn
        "aria-controls" => panelId tabs.Id
        attr.tabindex (if isOn then 0 else -1)
        on.click (fun _ -> tabs.Select index)
        on.keydown (fun e ->
            match e.Key with
            | "ArrowRight" -> tabs.Select(index + 1)
            | "ArrowLeft" -> tabs.Select(index - 1)
            | "Home" -> tabs.Select 0
            | "End" -> tabs.Select(tabs.Items.Length - 1)
            | _ -> ())
        tr lang (tabs.Title item)
    }

/// Barra de pestañas y el panel de la elegida.
let tabbed lang (tabs: Tabs<'item>) (panel: 'item -> Node) =
    concat {
        div {
            cls "tabs-wrap"
            scrollButton -1 "‹"
            div {
                cls "tabs"
                "role" => "tablist"
                "aria-label" => Text.pick lang tabs.Label
                forEachIndexed tabs.Items (tab lang tabs)
            }
            scrollButton 1 "›"
        }
        whenSome (List.tryItem tabs.Selected tabs.Items) panel
    }
