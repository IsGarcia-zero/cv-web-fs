/// Casos de estudio: pestañas con reto, decisiones, resultado, cifras y su visual.
module CvWeb.Views.Cases

open Bolero
open Bolero.Html
open CvWeb.Domain
open CvWeb.Model
open CvWeb.Content
open CvWeb.Views.Common

/// Un caso puede reusar un diagrama de "Sistemas"; se le cambia la clave para que sus
/// marcadores y animaciones SVG no choquen con los de esa sección.
let private visual (model: Model) (c: CaseStudy) =
    match c.Visual with
    | LiveSimulation -> Visuals.physarum
    | Screenshots images -> CaseGallery.view model.Lang images
    | SystemDiagram key ->
        whenSome (model.Resume.Systems |> List.tryFind (fun d -> d.Key = key)) (fun d ->
            div { cls "arch"; Visuals.diagram model.Lang { d with Key = $"case-{d.Key}" } })

let private titled lang (title: Text) (body: Node) =
    concat {
        h4 { cls "case-label"; tr lang title }
        body
    }

let private panel (model: Model) (c: CaseStudy) =
    let lang = model.Lang
    article {
        attr.id (panelId "case")
        cls "case system-panel"
        "role" => "tabpanel"
        attr.tabindex 0
        "aria-labelledby" => tabId "case" model.Case
        attr.key c.Key
        h3 { tr lang c.Title }
        p { cls "case-tagline"; tr lang c.Tagline }
        dl {
            cls "case-metrics"
            forEach c.Metrics (fun m -> div { dt { tr lang m.Caption }; dd { text m.Value } })
        }
        visual model c
        titled lang Labels.challenge (p { tr lang c.Challenge })
        titled lang Labels.decisions (bullets lang c.Decisions)
        titled lang Labels.outcome (bullets lang c.Outcome)
        p { cls "case-role"; strong { tr lang Labels.myRole; text ": " }; tr lang c.Role }
        tags c.Stack
        whenSome c.Note (fun n -> p { cls "status"; tr lang n })
    }

let view (model: Model) dispatch =
    let tabs =
        { Id = "case"
          Label = Labels.cases
          Items = model.Resume.Cases
          Title = _.TabTitle
          Selected = model.Case
          Select = SelectCase >> dispatch }
    block "cases" model.Lang Labels.cases (tabbed model.Lang tabs (panel model))
