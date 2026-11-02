module CvWeb.Views.CaseGallery

open Bolero.Html
open CvWeb.Domain
open CvWeb.Content
open CvWeb.Views.Common

/// Captura que se abre en grande en otra pestaña.
let private shot lang (image: CaseShot) =
    figure {
        cls "shot"
        a {
            attr.href image.Source
            attr.target "_blank"
            attr.rel "noopener"
            attr.title (Text.pick lang CaseShots.enlarge)
            img {
                attr.src image.Source
                attr.alt (Text.pick lang image.Caption)
                attr.width image.Width
                attr.height image.Height
                "loading" => "lazy"
                "decoding" => "async"
            }
            span { cls "shot-expand"; tr lang CaseShots.enlarge; text " ↗"; newTabHint lang }
        }
        figcaption { tr lang image.Caption }
    }

let view lang (images: CaseShot list) =
    div {
        cls "case-gallery"
        p { cls "shot-disclosure"; tr lang CaseShots.disclosure }
        div { cls "shots"; forEach images (shot lang) }
    }
