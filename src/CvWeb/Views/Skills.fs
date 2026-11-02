module CvWeb.Views.Skills

open Bolero.Html
open CvWeb.Domain
open CvWeb.Content
open CvWeb.Views.Common

let private group lang (g: SkillGroup) = row (tr lang g.Title) (tags g.Items)

/// Rejilla compacta de título y descripción: habilidades blandas y especialización.
let notes lang (items: Card list) =
    ul {
        cls "notes"
        forEach items (fun c ->
            li {
                h4 { tr lang c.Title }
                p { tr lang c.Description }
                tags c.Tags
            })
    }

let private byLanguage lang (shares: LanguageShare list) =
    figure {
        cls "card lang-chart"
        h3 { tr lang Labels.byLanguage }
        Visuals.languageBars shares
        figcaption { tr lang Labels.byLanguageNote }
    }

let view lang (groups: SkillGroup list) (soft: Card list) (shares: LanguageShare list) =
    block "skills" lang Labels.skills (
        concat {
            byLanguage lang shares
            rows (forEach groups (group lang))
            h3 { cls "sub-title"; tr lang Labels.softSkills }
            notes lang soft
        }
    )
