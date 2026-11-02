module CvWeb.Views.Projects

open Bolero.Html
open CvWeb.Domain
open CvWeb.Model
open CvWeb.Content
open CvWeb.Views.Common

let private categories = [ None; Some Work; Some Research; Some Personal; Some Academic ]

let private filterLabel filter =
    match filter with
    | None -> Labels.all
    | Some c -> Labels.category c

/// Chips de filtro de Material 3: palomita cuando está activo y conteo visible.
let private chip (model: Model) dispatch filter =
    let selected = filter = model.Filter
    let count = Model.projectsIn filter model.Resume.Projects |> List.length
    button {
        plainButton
        clsIf selected "chip" "on"
        "aria-pressed" => ariaBool selected
        on.click (fun _ -> dispatch (SetFilter filter))
        span { cls "chip-check"; decorative; "✓" }
        tr model.Lang (filterLabel filter)
        span { cls "chip-count"; text (string count) }
    }

let private meta lang (proj: Project) =
    let category = (string proj.Category).ToLowerInvariant()
    concat {
        cond proj.Visual <| function
            | LivePhysarum -> Visuals.physarum
            | Art key -> Visuals.cover proj.Name.En key
        span { cls $"category cat-{category}"; tr lang (Labels.category proj.Category) }
        span { cls "meta-date"; tr lang proj.Period }
    }

let private project lang (proj: Project) =
    row (meta lang proj) (
        concat {
            rowTitle lang proj.Name proj.Url
            p { tr lang proj.Description }
            tags proj.Stack
            whenSome proj.Status (fun status -> p { cls "status"; tr lang status })
        }
    )

/// Con el filtro "Todos" sólo se ven los destacados hasta pedir el resto.
let private showAll (model: Model) dispatch =
    let label =
        if model.AllProjects then Labels.fewerProjects
        else Labels.allProjects model.Resume.Projects.Length
    disclosure "more all-projects" model.AllProjects "project-list" (tr model.Lang label) (fun () ->
        dispatch ToggleAllProjects)

let view (model: Model) dispatch =
    block "projects" model.Lang Labels.projects (
        concat {
            div {
                cls "chips"
                "role" => "group"
                "aria-label" => Text.pick model.Lang Labels.projects
                forEach categories (chip model dispatch)
            }
            div {
                attr.id "project-list"
                "aria-live" => "polite"
                rows (forEach (Model.visibleProjects model) (project model.Lang))
            }
            whenTrue model.Filter.IsNone (fun () -> showAll model dispatch)
        }
    )
