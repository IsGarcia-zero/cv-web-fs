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

/// Cifra de la portada: un conteo fijo, o los años desde un mes, que crecen solos con la fecha
/// (con un decimal, o en años cumplidos con "+").
type Amount =
    | Count of value: int * suffix: string
    | YearsSince of year: int * month: int
    | WholeYearsSince of year: int * month: int

type Stat = { Amount: Amount; Label: Text }

module Stat =

    /// Parte entera (la que anima `cv.js`) y el resto ya escrito. Los años llevan un decimal truncado,
    /// así que avanzan casi cada mes: 19 meses se leen 1.5 y 20 meses, 1.6.
    let figure (today: System.DateOnly) stat =
        let monthsSince year month = max 0 ((today.Year - year) * 12 + today.Month - month)
        match stat.Amount with
        | Count(value, suffix) -> value, suffix
        | YearsSince(year, month) ->
            let tenths = monthsSince year month * 10 / 12
            let decimal = if tenths % 10 = 0 then "" else $".{tenths % 10}"
            tenths / 10, decimal
        | WholeYearsSince(year, month) -> monthsSince year month / 12, "+"

    /// Mínimo redondeado hacia abajo (524 con paso 50 → 500): sigue siendo cierto aunque el total crezca.
    let atLeast step n = n / step * step

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
