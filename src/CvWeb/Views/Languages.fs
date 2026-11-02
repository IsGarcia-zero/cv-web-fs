module CvWeb.Views.Languages

open Bolero.Html
open CvWeb.Domain
open CvWeb.Content
open CvWeb.Views.Common

let private language lang (s: Spoken) =
    li {
        div {
            cls "lang-head"
            strong { tr lang s.Language }
            span { tr lang s.Level }
        }
        div {
            cls "bar"
            decorative
            div { cls "bar-fill"; attr.style $"--level: {s.Percent}%%" }
        }
    }

let view lang (items: Spoken list) =
    block "languages" lang Labels.spoken (ul { cls "langs-list"; forEach items (language lang) })
