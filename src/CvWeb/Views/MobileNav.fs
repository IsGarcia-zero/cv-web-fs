/// Índice fijo para pantallas angostas, donde no cabe la columna lateral: las secciones se
/// deslizan de lado y `cv.js` marca y centra la actual.
module CvWeb.Views.MobileNav

open Bolero.Html
open CvWeb.Domain
open CvWeb.Content
open CvWeb.Views.Common

let view lang =
    nav {
        cls "mobile-nav"
        "aria-label" => Text.pick lang Labels.sections
        ul { forEach Labels.nav (fun (id, label) -> li { a { attr.href $"#{id}"; tr lang label } }) }
    }
