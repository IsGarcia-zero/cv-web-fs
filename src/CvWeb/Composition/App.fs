module CvWeb.Composition.App

open Elmish
open Bolero
open CvWeb.Model
open CvWeb.Update

type CvApp() =
    inherit ProgramComponent<Model, Msg>()

    override this.Program =
        let effects = LiveEffects.create this.JSRuntime
        // La fecha se lee aquí, en el borde: el modelo y las vistas sólo la reciben.
        let today = System.DateOnly.FromDateTime System.DateTime.Today
        Program.mkProgram
            (fun _ -> Update.init effects CvWeb.Content.Resume.value today)
            (Update.update effects)
            CvWeb.Views.Page.view
