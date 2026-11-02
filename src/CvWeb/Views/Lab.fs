/// Laboratorio de autómatas famosos: el estado (cuál, si corre, semilla) vive en el modelo
/// y `wwwroot/js/lab.js` lee los atributos `data-*` del lienzo para simular y dibujar.
module CvWeb.Views.Lab

open Bolero.Html
open CvWeb.Domain
open CvWeb.Model
open CvWeb.Content
open CvWeb.Views.Common

let private controls (model: Model) dispatch =
    let lang = model.Lang
    div {
        cls "lab-controls"
        button {
            plainButton
            cls "ctl"
            "aria-pressed" => ariaBool (not model.Running)
            on.click (fun _ -> dispatch ToggleRunning)
            text (if model.Running then "❚❚  " else "▶  ")
            tr lang (if model.Running then Labels.pause else Labels.play)
        }
        button {
            plainButton
            cls "ctl"
            on.click (fun _ -> dispatch ResetAutomaton)
            text "↺  "
            tr lang Labels.restart
        }
    }

let private panel (model: Model) dispatch (a: AutomatonInfo) =
    let lang = model.Lang
    figure {
        attr.id (panelId "lab")
        cls "lab"
        "role" => "tabpanel"
        "aria-labelledby" => tabId "lab" model.Automaton
        div {
            cls "lab-stage"
            canvas {
                "role" => "img"
                "aria-label" => Text.pick lang a.Title
                "data-kind" => a.Kind
                "data-running" => ariaBool model.Running
                "data-seed" => model.Seed
            }
        }
        figcaption {
            div {
                cls "lab-head"
                h3 { tr lang a.Title }
                span { cls "lab-author"; text a.Author }
            }
            p { tr lang a.About }
            div {
                cls "lab-foot"
                p { cls "lab-hint"; tr lang a.Hint }
                controls model dispatch
            }
        }
    }

let view (model: Model) dispatch =
    let tabs =
        { Id = "lab"
          Label = Labels.lab
          Items = model.Resume.Lab
          Title = _.Title
          Selected = model.Automaton
          Select = SelectAutomaton >> dispatch }
    block "lab" model.Lang Labels.lab (tabbed model.Lang tabs (panel model dispatch))
