module CvWeb.Views.Page

open Bolero.Html
open CvWeb.Domain
open CvWeb.Model
open CvWeb.Content
open CvWeb.Views.Common

let private siteFooter lang (name: string) =
    footer {
        cls "footer"
        p {
            text $"© 2026 {name}. "
            tr lang Labels.updated
            text ". "
            tr lang Labels.builtWith
            text " "
            externalLink "" Labels.sourceUrl (concat { tr lang Labels.sourceCode; newTabHint lang })
        }
    }

/// Aviso no bloqueante (snackbar de Material 3) anunciado a lectores de pantalla.
let private toast (model: Model) =
    div {
        clsIf model.Toast "toast" "show"
        "role" => "status"
        "aria-live" => "polite"
        whenTrue model.Toast (fun () -> tr model.Lang Labels.copied)
    }

let private toTop lang =
    let label = Text.pick lang Labels.backToTop
    a { cls "to-top"; attr.href "#profile"; attr.title label; "aria-label" => label; "↑" }

let view (model: Model) dispatch =
    let lang = model.Lang
    let r = model.Resume
    concat {
        a { cls "skip"; attr.href "#content"; tr lang Labels.skip }
        div { cls "progress"; decorative }
        div { cls "spotlight"; decorative }
        MobileNav.view lang
        div {
            cls "shell"
            Sidebar.view model dispatch
            main {
                attr.id "content"
                cls "content"
                attr.tabindex -1
                About.view lang r.Profile r.Stats r.Activity
                Experience.view model dispatch
                Cases.view model dispatch
                Systems.view model dispatch
                Projects.view model dispatch
                Lab.view model dispatch
                Achievements.view lang r.Achievements
                Education.view lang r.Studies
                Skills.view lang r.Skills r.SoftSkills r.Languages
                Interests.view lang r.Interests
                Languages.view lang r.Spoken
                Contact.view model dispatch
                Foundations.view lang r.Foundations
                siteFooter lang r.Profile.Name
            }
        }
        toTop lang
        toast model
    }
