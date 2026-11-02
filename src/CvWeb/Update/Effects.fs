namespace CvWeb.Update

open Elmish
open CvWeb.Domain
open CvWeb.Model

/// Efectos del navegador como record de funciones; `update` sólo los compone.
type Effects =
    { LoadPrefs: Cmd<Msg>
      SaveLang: Lang -> Cmd<Msg>
      SaveTheme: Theme -> Cmd<Msg>
      Copy: string -> Cmd<Msg>
      /// Manda `HideToast` después de unos segundos.
      HideLater: Cmd<Msg>
      Print: Cmd<Msg> }

module Effects =

    /// Sin navegador: para pruebas o render previo.
    let none =
        { LoadPrefs = Cmd.none
          SaveLang = fun _ -> Cmd.none
          SaveTheme = fun _ -> Cmd.none
          Copy = fun _ -> Cmd.none
          HideLater = Cmd.none
          Print = Cmd.none }
