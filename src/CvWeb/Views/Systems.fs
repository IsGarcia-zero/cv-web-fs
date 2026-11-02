/// "Sistemas que construí": pestañas con un diagrama de arquitectura cada una.
module CvWeb.Views.Systems

open Bolero.Html
open CvWeb.Domain
open CvWeb.Model
open CvWeb.Content
open CvWeb.Views.Common

let private panel lang index (d: Diagram) =
    div {
        attr.id (panelId "system")
        cls "system-panel"
        "role" => "tabpanel"
        "aria-labelledby" => tabId "system" index
        // La clave fuerza a Blazor a crear el panel de nuevo: así se repite la animación de entrada.
        attr.key d.Key
        p { cls "system-summary"; tr lang d.Summary }
        div { cls "arch"; Visuals.diagram lang d }
    }

let view (model: Model) dispatch =
    let tabs =
        { Id = "system"
          Label = Labels.systems
          Items = model.Resume.Systems
          Title = _.Title
          Selected = model.System
          Select = SelectSystem >> dispatch }
    block "systems" model.Lang Labels.systems (tabbed model.Lang tabs (panel model.Lang model.System))
