module CvWeb.Views.Experience

open Bolero.Html
open CvWeb.Domain
open CvWeb.Model
open CvWeb.Content
open CvWeb.Views.Common

/// Divulgación progresiva (NN/g): las primeras viñetas y un botón para el resto.
/// Las ocultas siguen en el HTML para el PDF y los buscadores.
let private highlights (model: Model) dispatch index (items: Highlight list) =
    let hidden = items.Length - Model.previewSize
    let expanded = model.Expanded.Contains index
    let listId = $"job-{index}-highlights"
    concat {
        ul {
            attr.id listId
            clsIf (not expanded) "bullets" "collapsed"
            forEachIndexed items (fun n h ->
                li {
                    cls (if n >= Model.previewSize then "extra" else null)
                    tr model.Lang h.Text
                })
        }
        whenTrue (hidden > 0) (fun () ->
            let label = if expanded then Labels.showLess else Labels.showMore hidden
            disclosure "more" expanded listId (tr model.Lang label) (fun () -> dispatch (ToggleJob index)))
    }

let private job (model: Model) dispatch index (j: Job) =
    let lang = model.Lang
    row (tr lang j.Period) (
        concat {
            h3 {
                tr lang j.Role
                span { cls "at"; text " · " }
                span { cls "org"; tr lang j.Company }
            }
            paragraph lang j.Summary
            highlights model dispatch index j.Highlights
            tags j.Stack
        }
    )

/// Trayectoria tipo Gantt con su leyenda de colores e hitos.
let private career lang (tl: Timeline) =
    let key kind (label: Text) = li { span { cls $"key {kind}" }; tr lang label }
    figure {
        cls "career card"
        h3 { tr lang Labels.career }
        Visuals.timeline lang tl
        figcaption {
            ul {
                cls "career-legend"
                key "study" Labels.study
                key "research" Labels.research
                key "job" Labels.job
                forEach tl.Milestones (fun m -> key "mark" m.Event)
            }
        }
    }

let view (model: Model) dispatch =
    block "experience" model.Lang Labels.experience (
        concat {
            career model.Lang model.Resume.Timeline
            rows (forEachIndexed model.Resume.Jobs (job model dispatch))
        }
    )
