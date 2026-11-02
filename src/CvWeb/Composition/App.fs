module CvWeb.Composition.App

open Elmish
open Bolero
open CvWeb.Model
open CvWeb.Update

type CvApp() =
    inherit ProgramComponent<Model, Msg>()

    override this.Program =
        let effects = LiveEffects.create this.JSRuntime
        Program.mkProgram
            (fun _ -> Update.init effects CvWeb.Content.Resume.value)
            (Update.update effects)
            CvWeb.Views.Page.view
