module CvWeb.Update.Update

open Elmish
open CvWeb.Model

let init (effects: Effects) resume = Model.init resume, effects.LoadPrefs

let private setTheme (effects: Effects) theme (model: Model) =
    { model with Theme = theme }, effects.SaveTheme theme

let update (effects: Effects) msg (model: Model) =
    match msg with
    | SetLang lang -> { model with Lang = lang }, effects.SaveLang lang
    | SelectSystem index -> { model with System = Model.wrap model.Resume.Systems.Length index }, Cmd.none
    | SelectCase index -> { model with Case = Model.wrap model.Resume.Cases.Length index }, Cmd.none
    | SelectAutomaton index ->
        { model with Automaton = Model.wrap model.Resume.Lab.Length index; Running = true; Seed = model.Seed + 1 }, Cmd.none
    | ToggleRunning -> { model with Running = not model.Running }, Cmd.none
    | ResetAutomaton -> { model with Seed = model.Seed + 1; Running = true }, Cmd.none
    | SetTheme theme -> setTheme effects theme model
    | ToggleTheme -> setTheme effects (Theme.other model.Theme) model
    | SetFilter filter -> { model with Filter = filter }, Cmd.none
    | ToggleAllProjects -> { model with AllProjects = not model.AllProjects }, Cmd.none
    | ToggleJob index -> { model with Expanded = Model.toggle index model.Expanded }, Cmd.none
    | CopyEmail -> model, effects.Copy model.Resume.Profile.Email
    | EmailCopied -> { model with Toast = true }, effects.HideLater
    | HideToast -> { model with Toast = false }, Cmd.none
    | PrintRequested -> model, effects.Print
    | JsFailed _ -> model, Cmd.none
