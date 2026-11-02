module CvWeb.Views.Education

open Bolero.Html
open CvWeb.Domain
open CvWeb.Content
open CvWeb.Views.Common

let private study lang (s: Study) =
    row (tr lang s.Period) (
        concat {
            h3 { tr lang s.Title }
            p { cls "org"; tr lang s.School }
            bullets lang s.Notes
        }
    )

let view lang (studies: Study list) =
    block "education" lang Labels.education (rows (forEach studies (study lang)))
