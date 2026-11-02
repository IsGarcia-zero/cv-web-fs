/// Gráficos SVG generados a partir de los datos del CV. Se arman como cadena (`rawHtml`):
/// para cientos de celdas es más ligero que un nodo de Blazor por elemento.
module CvWeb.Views.Visuals

open Bolero.Html
open CvWeb.Domain
open CvWeb.Views.Common

let private concatAll (parts: string seq) = String.concat "" parts

/// Portada de proyecto: autómata celular elemental sembrado con el nombre (fondo tenue)
/// y, encima, el pictograma que identifica al proyecto.
let cover (name: string) (pictogram: string) =
    let seed = Automaton.hash name
    let path = Automaton.seedRow 48 seed |> Automaton.evolve (Automaton.ruleFor seed) 24 |> Automaton.toPath
    let icon = Pictograms.svg pictogram
    let className = if icon = "" then "cover-art" else "cover-art has-icon"
    rawHtml
        $"""
<div class="{className}">
  <svg class="cover" viewBox="0 0 48 24" preserveAspectRatio="xMidYMid slice" aria-hidden="true"><path d="{path}"/></svg>
  {icon}
</div>"""

/// Simulación de Physarum polycephalum por agentes (cv.js la anima en el lienzo).
let physarum =
    div {
        cls "physarum"
        canvas { decorative }
        span { "Physarum · live" }
    }

// ── Mapa de calor de commits ──

let private level count =
    match count with
    | 0 -> 0
    | c when c <= 5 -> 1
    | c when c <= 15 -> 2
    | c when c <= 30 -> 3
    | _ -> 4

let private monthNames lang =
    match lang with
    | Es -> [| "ene"; "feb"; "mar"; "abr"; "may"; "jun"; "jul"; "ago"; "sep"; "oct"; "nov"; "dic" |]
    | En -> [| "Jan"; "Feb"; "Mar"; "Apr"; "May"; "Jun"; "Jul"; "Aug"; "Sep"; "Oct"; "Nov"; "Dec" |]

/// Celdas de 14 px separadas cada 17 px; deja 34 px a la izquierda para el año.
let private pitch = 17

let private cell (months: string[]) row year (col, count) =
    let x, y = 34 + col * pitch, 16 + row * pitch
    // --i escalona la aparición: las celdas se llenan en orden cronológico.
    $"""
<rect class="l{level count}" style="--i:{row * 12 + col}" x="{x}" y="{y}" width="14" height="14" rx="3">
  <title>{months[col]} {year}: {count} commits</title>
</rect>"""

/// Mapa de calor de commits: un renglón por año, una celda por mes.
let heatmap lang (activity: Activity) =
    let months = monthNames lang
    let header =
        months |> Seq.mapi (fun i m -> $"""<text x="{41 + i * pitch}" y="10">{m[0]}</text>""")
    let yearRow row (year, counts) =
        seq {
            $"""<text class="year" x="0" y="{27 + row * pitch}">{year}</text>"""
            yield! counts |> List.indexed |> List.map (cell months row year)
        }
    let body = activity.Years |> Seq.mapi yearRow |> Seq.concat
    let height = 20 + activity.Years.Length * pitch
    rawHtml
        $"""
<svg class="heatmap" viewBox="0 0 240 {height}" role="img" aria-label="{activity.Commits} commits">
  {concatAll header}
  {concatAll body}
</svg>"""

// ── Diagramas de arquitectura ──

/// Caja con su rótulo y, si hay, una línea de detalle debajo.
let private boxSvg lang (b: Box) =
    let cx, cy = b.BoxX + b.BoxW / 2, b.BoxY + b.BoxH / 2
    let label = esc (Text.pick lang b.Label)
    let lines =
        match esc (Text.pick lang b.Detail) with
        | "" -> $"""<text x="{cx}" y="{cy + 4}">{label}</text>"""
        | detail ->
            $"""
<text x="{cx}" y="{cy - 3}">{label}</text>
<text class="detail" x="{cx}" y="{cy + 12}">{detail}</text>"""
    $"""
<rect class="node" x="{b.BoxX}" y="{b.BoxY}" width="{b.BoxW}" height="{b.BoxH}" rx="8"/>{lines}"""

let private zoneSvg lang (z: Zone) =
    $"""
<rect class="group" x="{z.ZoneX}" y="{z.ZoneY}" width="{z.ZoneW}" height="{z.ZoneH}" rx="14"/>
<text class="group-label" x="{z.ZoneX + 16}" y="{z.ZoneY - 6}">{esc (Text.pick lang z.Name)}</text>"""

/// Flecha y, si corresponde, un punto de luz sincronizado con el ciclo `{key}Loop` de 4 s.
let private wireSvg key (w: Wire) =
    let line =
        $"""
<line class="flow" x1="{w.X1}" y1="{w.Y1}" x2="{w.X2}" y2="{w.Y2}" marker-end="url(#{key}Tip)"/>"""
    match w.Packet with
    | None -> line
    | Some delay ->
        let timing = $"""dur="0.9s" begin="{delay}s;{key}Loop.end+{delay}s" """
        line
        + $"""
<circle class="packet" r="3.2" opacity="0">
  <animateMotion {timing}path="M{w.X1} {w.Y1}L{w.X2} {w.Y2}" fill="freeze"/>
  <animate attributeName="opacity" values="0;1;1;0" {timing}/>
</circle>"""

let diagram lang (d: Diagram) =
    let defs =
        $"""
<defs>
  <marker id="{d.Key}Tip" viewBox="0 0 10 10" refX="9" refY="5" markerWidth="7" markerHeight="7" orient="auto-start-reverse">
    <path d="M0 0L10 5L0 10z"/>
  </marker>
</defs>"""
    // Reloj invisible: todas las flechas del diagrama se sincronizan con su fin.
    let loop =
        $"""
<rect width="0" height="0">
  <animate id="{d.Key}Loop" attributeName="x" from="0" to="0" dur="4s" begin="0s;{d.Key}Loop.end"/>
</rect>"""
    let parts =
        seq {
            defs
            loop
            yield! d.Zones |> List.map (zoneSvg lang)
            yield! d.Boxes |> List.map (boxSvg lang)
            yield! d.Wires |> List.map (wireSvg d.Key)
        }
    rawHtml
        $"""
<svg viewBox="0 0 640 {d.Height}" role="img" aria-label="{esc (Text.pick lang d.Title)}">{concatAll parts}
</svg>"""

// ── Trayectoria ──

let private kindClass kind =
    match kind with
    | Schooling -> "study"
    | Lab -> "research"
    | Employment -> "job"

/// Línea de tiempo tipo Gantt: un carril por periodo y rombos para los hitos.
let timeline lang (tl: Timeline) =
    let width, left = 640.0, 8.0
    let scale year = left + (year - float tl.Start) / float (tl.Finish - tl.Start) * (width - 2.0 * left)
    let tick year =
        let x = scale (float year)
        // El último año sólo marca el fin del eje: sin etiqueta para que no se corte.
        let label =
            if year < tl.Finish then $"""<text class="tick-label" x="{x + 3.0}" y="12">{year}</text>""" else ""
        $"""
<line class="tick" x1="{x}" y1="18" x2="{x}" y2="196"/>{label}"""
    let bar i (s: Span) =
        let x, w = scale s.From, scale s.Until - scale s.From
        let y = 26 + i * 30
        // Las etiquetas que llegan al borde derecho se alinean al final de su barra.
        let anchor, tx = if x + w > 450.0 then "end", x + w else "start", x
        $"""
<g class="span {kindClass s.Kind}" style="--i:{i}">
  <rect x="{x}" y="{y}" width="{max w 4.0}" height="12" rx="6"/>
  <text x="{tx}" y="{y + 25}" text-anchor="{anchor}">{esc (Text.pick lang s.What)} · {esc s.Where}</text>
</g>"""
    let milestone i (m: Milestone) =
        let x = scale m.At
        $"""
<g class="milestone" style="--i:{i}">
  <path d="M{x} 200l6 6l-6 6l-6-6z"/>
  <title>{esc (Text.pick lang m.Event)}</title>
</g>"""
    let parts =
        seq {
            yield! [ tl.Start .. tl.Finish ] |> List.map tick
            yield! tl.Spans |> List.mapi bar
            yield! tl.Milestones |> List.mapi milestone
        }
    rawHtml
        $"""
<svg class="timeline" viewBox="0 0 640 216" role="img" aria-label="Timeline">{concatAll parts}
</svg>"""

// ── Commits por lenguaje ──

let languageBars (shares: LanguageShare list) =
    let top = shares |> List.map _.Commits |> List.max |> float
    ul {
        cls "lang-bars"
        forEachIndexed shares (fun i s ->
            li {
                attr.style $"--w: {float s.Commits / top * 100.0:F1}%%; --i: {i}"
                span { cls "lb-name"; text s.Language }
                span { cls "lb-track"; span { cls "lb-fill" } }
                span { cls "lb-value"; text (string s.Commits) }
            })
    }
