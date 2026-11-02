/// Columna fija de la izquierda (arriba en móvil): identidad, navegación, controles y contacto.
module CvWeb.Views.Sidebar

open Bolero.Html
open CvWeb.Domain
open CvWeb.Model
open CvWeb.Content
open CvWeb.Views.Common

/// Navegación con indicador de línea que crece en la sección activa (`aria-current` lo pone cv.js).
let private navigation lang =
    nav {
        cls "side-nav"
        "aria-label" => Text.pick lang Labels.sections
        ul {
            forEach Labels.nav (fun (id, label) ->
                li {
                    a {
                        attr.href $"#{id}"
                        span { cls "nav-line"; decorative }
                        span { cls "nav-text"; tr lang label }
                    }
                })
        }
    }

let private langButton current dispatch lang =
    button {
        plainButton
        clsIf (lang = current) "seg" "on"
        "aria-pressed" => ariaBool (lang = current)
        on.click (fun _ -> dispatch (SetLang lang))
        text ((Lang.code lang).ToUpperInvariant())
    }

let private controls (model: Model) dispatch =
    let lang = model.Lang
    let themeLabel = Text.pick lang (if model.Theme = Dark then Labels.themeLight else Labels.themeDark)
    div {
        cls "controls"
        div {
            cls "segmented"
            "role" => "group"
            "aria-label" => Text.pick lang Labels.language
            forEach [ Es; En ] (langButton lang dispatch)
        }
        button {
            plainButton
            cls "ctl"
            attr.title themeLabel
            "aria-label" => themeLabel
            on.click (fun _ -> dispatch ToggleTheme)
            text (if model.Theme = Dark then "☀" else "☾")
        }
        a { cls "ctl"; attr.href (Labels.pdfPath lang); "download" => ""; tr lang Labels.downloadPdf }
    }

let private socials lang dispatch (profile: Profile) =
    ul {
        cls "socials"
        li { namedLink lang "" "GitHub" profile.GitHub.Url }
        li { namedLink lang "" "LinkedIn" profile.LinkedIn.Url }
        li { a { attr.href $"mailto:{profile.Email}"; text profile.Email } }
        li {
            button {
                plainButton
                cls "link-btn"
                on.click (fun _ -> dispatch CopyEmail)
                tr lang Labels.copyEmail
            }
        }
    }

let private identity lang (profile: Profile) =
    concat {
        div {
            cls "identity"
            div {
                cls "portrait"
                img {
                    attr.src profile.Photo
                    attr.alt profile.Name
                    attr.width 96
                    attr.height 96
                    "decoding" => "async"
                }
            }
            h1 { a { attr.href "#content"; text profile.Name } }
        }
        p { cls "role"; tr lang profile.Role }
        p { cls "tagline"; tr lang profile.Tagline }
        p {
            cls "location"
            span { cls "badge"; span { cls "pulse"; decorative }; tr lang Labels.available }
            tr lang profile.Location
        }
    }

let view (model: Model) dispatch =
    let lang = model.Lang
    let profile = model.Resume.Profile
    header {
        attr.id "profile"
        cls "side"
        div {
            cls "side-top"
            figure {
                cls "automaton"
                canvas { decorative }
                figcaption { tr lang Labels.automaton }
            }
            identity lang profile
            navigation lang
        }
        div {
            cls "side-bottom"
            controls model dispatch
            socials lang dispatch profile
        }
    }
