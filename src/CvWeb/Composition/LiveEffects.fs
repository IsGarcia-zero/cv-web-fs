module CvWeb.Composition.LiveEffects

open Elmish
open Bolero.Remoting.Client
open Microsoft.JSInterop
open CvWeb.Domain
open CvWeb.Model
open CvWeb.Update

let private loadLang js =
    Cmd.OfJS.either js "cvSite.preferredLang" [||] (fun (code: string) -> SetLang(Lang.ofCode code)) JsFailed

let private loadTheme js =
    Cmd.OfJS.either js "cvSite.preferredTheme" [||] (fun (code: string) -> SetTheme(Theme.ofCode code)) JsFailed

/// Efectos reales: `wwwroot/js/cv.js` guarda idioma y tema, e imprime.
let create (js: IJSRuntime) : Effects =
    { LoadPrefs = Cmd.batch [ loadLang js; loadTheme js ]
      SaveLang = fun lang -> Cmd.OfJS.attempt js "cvSite.saveLang" [| Lang.code lang |] JsFailed
      SaveTheme = fun theme -> Cmd.OfJS.attempt js "cvSite.saveTheme" [| Theme.code theme |] JsFailed
      Copy = fun value -> Cmd.OfJS.either js "cvSite.copy" [| value |] (fun (_: bool) -> EmailCopied) JsFailed
      HideLater = Cmd.OfAsync.perform (fun (ms: int) -> Async.Sleep ms) 2500 (fun () -> HideToast)
      Print = Cmd.OfJS.attempt js "cvSite.print" [||] JsFailed }
