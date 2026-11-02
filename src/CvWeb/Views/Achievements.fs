module CvWeb.Views.Achievements

open Bolero.Html
open CvWeb.Domain
open CvWeb.Content
open CvWeb.Views.Common

let private achievement lang (a: Achievement) =
    row (tr lang a.Date) (
        concat {
            rowTitle lang a.Title (a.Link |> Option.map _.Url)
            p { cls "org"; tr lang a.Subtitle }
            p { tr lang a.Description }
        }
    )

let view lang (items: Achievement list) =
    block "achievements" lang Labels.achievements (rows (forEach items (achievement lang)))
