namespace CvWeb.Domain

type Link = { Label: string; Url: string }

type Profile =
    { Name: string
      /// Ruta de la foto dentro de wwwroot.
      Photo: string
      Role: Text
      Tagline: Text
      Location: Text
      Summary: Text
      Highlights: Text list
      Email: string
      LinkedIn: Link
      GitHub: Link }

/// Logro de un puesto. `InPdf` decide si también entra al CV corto en PDF (build/short-cv.fsx).
type Highlight = { Text: Text; InPdf: bool }

type Job =
    { Role: Text
      Company: Text
      Period: Text
      Summary: Text
      Highlights: Highlight list
      Stack: string list }

type Category =
    | Work
    | Research
    | Personal
    | Academic

/// Imagen de la tarjeta: pictograma (clave en Views/Pictograms.fs) sobre patrón de autómata,
/// o la simulación de Physarum en vivo.
type ProjectVisual =
    | Art of string
    | LivePhysarum

type Project =
    { Name: Text
      Category: Category
      Description: Text
      Period: Text
      Stack: string list
      Url: string option
      Visual: ProjectVisual
      /// Proyectos sin publicar o en curso.
      Status: Text option }

type SkillGroup = { Title: Text; Items: string list }

/// Tarjeta con ícono: habilidades blandas e intereses.
type Card =
    { Icon: string
      Title: Text
      Description: Text
      Tags: string list }

type Study =
    { Title: Text
      School: Text
      Period: Text
      Notes: Text list }

type Achievement =
    { Icon: string
      Title: Text
      Subtitle: Text
      Date: Text
      Description: Text
      Link: Link option }

/// Idiomas que habla la persona (no los de la interfaz).
type Spoken = { Language: Text; Level: Text; Percent: int }

type Stat = { Value: int; Suffix: string; Label: Text }

/// Commits por mes: (año, 12 valores de enero a diciembre).
type Activity =
    { Commits: int
      Repositories: int
      Since: Text
      Years: (int * int list) list }

type Resume =
    { Profile: Profile
      Stats: Stat list
      Activity: Activity
      Timeline: Timeline
      Languages: LanguageShare list
      Systems: Diagram list
      Lab: AutomatonInfo list
      Foundations: Foundations
      Cases: CaseStudy list
      Jobs: Job list
      Studies: Study list
      Achievements: Achievement list
      Skills: SkillGroup list
      SoftSkills: Card list
      Spoken: Spoken list
      Interests: Card list
      Projects: Project list }
