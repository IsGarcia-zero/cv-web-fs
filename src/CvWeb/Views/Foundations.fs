/// "Fundamentos": conceptos explicados como en la tesis, con citas IEEE numeradas que
/// enlazan a la lista de referencias, y la jerarquía de Chomsky como diagrama.
module CvWeb.Views.Foundations

open Bolero.Html
open CvWeb.Domain
open CvWeb.Content
open CvWeb.Views.Common

/// Niveles de afuera hacia adentro: cada lenguaje contiene a los de abajo.
let private levels =
    [ Text.make "Tipo 0 · Recursivamente enumerables" "Type 0 · Recursively enumerable",
      Text.make "Máquina de Turing" "Turing machine"

      Text.make "Tipo 1 · Sensibles al contexto" "Type 1 · Context-sensitive",
      Text.make "Máquina de Turing linealmente acotada" "Linear-bounded automaton"

      Text.make "Tipo 2 · Libres de contexto" "Type 2 · Context-free",
      Text.make "Autómata de pila" "Pushdown automaton"

      Text.make "Tipo 3 · Regulares" "Type 3 · Regular",
      Text.make "Autómata finito (AFD / AFN)" "Finite automaton (DFA / NFA)" ]

/// Rectángulos anidados: cada nivel se mete `inset` por los lados y `step` desde arriba.
let private chomsky lang =
    let width, height, inset, step = 480, 280, 14, 46
    let level i (grammar, machine) =
        let x, y = i * inset + 1, i * step + 1
        $"""
<g class="lvl lvl-{i}">
  <rect x="{x}" y="{y}" width="{width - 2 * x}" height="{height - y - i * inset - 1}" rx="10"/>
  <text x="{x + 14}" y="{y + 20}" class="g">{esc (Text.pick lang grammar)}</text>
  <text x="{x + 14}" y="{y + 36}" class="m">{esc (Text.pick lang machine)}</text>
</g>"""
    let label = esc (Text.pick lang Labels.chomsky)
    rawHtml
        $"""
<figure class="chomsky">
  <svg viewBox="0 0 {width} {height}" role="img" aria-label="{label}">{levels |> List.mapi level |> String.concat ""}
  </svg>
  <figcaption>{label}</figcaption>
</figure>"""

/// Número IEEE de cada referencia: su posición en la lista, desde 1.
let private numbers (refs: Reference list) =
    refs |> List.mapi (fun i r -> r.Key, i + 1) |> Map.ofList

let private cites (index: Map<string, int>) (keys: string list) =
    forEach keys (fun key ->
        let n = index[key]
        concat {
            text " "
            a { cls "cite"; attr.href $"#ref-{n}"; text $"[{n}]" }
        })

let private card lang index (n: int) (c: Concept) =
    article {
        cls "concept"
        attr.id $"concept-{c.Key}"
        header {
            span { cls "concept-num"; decorative; text $"%02d{n}" }
            h3 { tr lang c.Term }
        }
        p { tr lang c.Body; cites index c.Cites }
        code { cls "formal"; tr lang c.Formal }
        p { cls "applied"; span { tr lang Labels.appliedIn }; text " "; tr lang c.AppliedIn }
    }

let private reference i (r: Reference) =
    li {
        attr.id $"ref-{i + 1}"
        span { cls "ref-num"; text $"[{i + 1}]" }
        span {
            text r.Ieee
            whenSome r.Url (fun url -> concat { text " "; externalLink "ref-link" url (text url) })
        }
    }

let view lang (f: Foundations) =
    let index = numbers f.References
    block "foundations" lang Labels.foundations (
        concat {
            p { cls "section-intro"; tr lang f.Intro }
            chomsky lang
            div { cls "concepts"; forEachIndexed f.Concepts (fun i c -> card lang index (i + 1) c) }
            h3 { cls "sub-title"; tr lang Labels.references }
            ol { cls "refs"; forEachIndexed f.References reference }
        }
    )
