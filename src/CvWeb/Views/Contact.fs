/// Cierre de la página: llamada a la acción con correo, redes y el CV en PDF.
module CvWeb.Views.Contact

open Bolero.Html
open CvWeb.Domain
open CvWeb.Model
open CvWeb.Content
open CvWeb.Views.Common

let view (model: Model) dispatch =
    let lang = model.Lang
    let profile = model.Resume.Profile
    let action className msg (label: Text) =
        button { plainButton; cls className; on.click (fun _ -> dispatch msg); tr lang label }
    block "contact" lang Labels.contact (
        div {
            cls "contact"
            h3 { tr lang Labels.contactTitle }
            p { tr lang Labels.contactLead }
            div {
                cls "contact-actions"
                a { cls "btn primary"; attr.href $"mailto:{profile.Email}"; text profile.Email }
                action "btn" CopyEmail Labels.copyEmail
                namedLink lang "btn" "LinkedIn" profile.LinkedIn.Url
                namedLink lang "btn" "GitHub" profile.GitHub.Url
                a { cls "btn"; attr.href (Labels.pdfPath lang); "download" => ""; tr lang Labels.downloadPdf }
                action "btn ghost" PrintRequested Labels.printFull
            }
        }
    )
