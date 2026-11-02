namespace CvWeb.Model

open CvWeb.Domain

type Theme =
    | Dark
    | Light

type Model =
    { Lang: Lang
      Theme: Theme
      /// Categoría elegida en Proyectos; `None` muestra todos.
      Filter: Category option
      /// Índices de los empleos con todas sus viñetas abiertas.
      Expanded: Set<int>
      /// Con el filtro "Todos" se muestran pocos proyectos hasta pedir el resto.
      AllProjects: bool
      /// Aviso breve de "correo copiado".
      Toast: bool
      /// Pestaña abierta en "Sistemas que construí".
      System: int
      /// Pestaña abierta en "Casos de estudio".
      Case: int
      /// Laboratorio: autómata elegido, si corre y una semilla que cambia al reiniciar.
      Automaton: int
      Running: bool
      Seed: int
      Resume: Resume }

type Msg =
    | SetLang of Lang
    | SelectSystem of int
    | SelectCase of int
    | SelectAutomaton of int
    | ToggleRunning
    | ResetAutomaton
    | SetTheme of Theme
    | ToggleTheme
    | SetFilter of Category option
    | ToggleAllProjects
    | ToggleJob of int
    | CopyEmail
    | EmailCopied
    | HideToast
    | PrintRequested
    | JsFailed of exn

module Theme =

    let code theme =
        match theme with
        | Dark -> "dark"
        | Light -> "light"

    let ofCode (code: string) =
        match code with
        | "light" -> Light
        | _ -> Dark

    let other theme =
        match theme with
        | Dark -> Light
        | Light -> Dark

module Model =

    let init resume =
        { Lang = Es
          Theme = Dark
          Filter = None
          Expanded = Set.empty
          AllProjects = false
          Toast = false
          System = 0
          Case = 0
          Automaton = 0
          Running = true
          Seed = 0
          Resume = resume }

    /// Índice válido de pestaña, dando la vuelta en los extremos (flechas del teclado).
    let wrap count index = if count = 0 then 0 else ((index % count) + count) % count

    /// Viñetas visibles antes de "ver más" (divulgación progresiva).
    let previewSize = 4

    let projectsIn filter (projects: Project list) =
        match filter with
        | None -> projects
        | Some c -> projects |> List.filter (fun p -> p.Category = c)

    /// Proyectos destacados que se ven antes de "ver todos".
    let projectPreview = 6

    /// Proyectos que pasan el filtro actual; con "Todos" sólo los primeros hasta pedir el resto.
    let visibleProjects model =
        let matching = projectsIn model.Filter model.Resume.Projects
        match model.Filter, model.AllProjects with
        | None, false -> List.truncate projectPreview matching
        | _ -> matching

    let toggle item set =
        if Set.contains item set then Set.remove item set else Set.add item set
