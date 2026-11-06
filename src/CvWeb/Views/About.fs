module CvWeb.Views.About

open Bolero.Html
open CvWeb.Domain
open CvWeb.Content
open CvWeb.Views.Common

/// Cifras que `cv.js` anima al entrar en pantalla; sin JS se ve el valor final.
let private stats lang today (items: Stat list) =
    dl {
        cls "stats"
        forEach items (fun s ->
            let value, suffix = Stat.figure today s
            div {
                cls "stat"
                dt { tr lang s.Label }
                dd { span { "data-count" => value; text (string value) }; text suffix }
            })
    }

let private legend lang =
    div {
        cls "legend"
        decorative
        tr lang Labels.less
        forEach [ 0..4 ] (fun l -> span { cls $"swatch l{l}" })
        tr lang Labels.more
    }

/// Mapa de calor con los commits reales por mes.
let private activity lang (a: Activity) =
    figure {
        cls "activity card"
        div {
            cls "activity-head"
            h3 { tr lang Labels.activityTitle }
            p { tr lang (Labels.activity (Stat.atLeast 50 a.Commits) (Stat.atLeast 10 a.Repositories) a.Since) }
        }
        Visuals.heatmap lang a
        legend lang
    }

let view lang today (profile: Profile) (items: Stat list) (a: Activity) =
    block "about" lang Labels.about (
        concat {
            p { cls "lead"; tr lang profile.Summary }
            ul { cls "highlights"; forEach profile.Highlights (fun h -> li { tr lang h }) }
            stats lang today items
            activity lang a
        }
    )
