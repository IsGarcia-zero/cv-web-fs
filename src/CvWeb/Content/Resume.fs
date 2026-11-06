/// El CV completo, armado con las piezas de cada archivo de contenido.
module CvWeb.Content.Resume

open CvWeb.Domain

/// Nada que actualizar a mano: los años avanzan con la fecha (industria desde abril de 2025,
/// programación desde agosto de 2017), commits y repositorios se muestran como mínimos ("500+")
/// y los proyectos se cuentan de la lista. La investigación (ene 2023 – ene 2025) ya terminó.
let private stats =
    [ { Amount = YearsSince(2025, 4); Label = Text.make "Años en industria" "Years in industry" }
      { Amount = Count(2, ""); Label = Text.make "Años en investigación" "Years in research" }
      { Amount = WholeYearsSince(2017, 8); Label = Text.make "Años programando" "Years coding" }
      { Amount = Count(Stat.atLeast 50 Activity.value.Commits, "+"); Label = Text.make "Commits desde 2020" "Commits since 2020" }
      { Amount = Count(Stat.atLeast 10 Activity.value.Repositories, "+"); Label = Text.make "Repositorios" "Repositories" }
      { Amount = Count(List.length Projects.value, ""); Label = Text.make "Proyectos" "Projects" } ]

let value =
    { Profile = Profile.value
      Stats = stats
      Activity = Activity.value
      Timeline = Career.timeline
      Languages = Career.languages
      Systems = Diagrams.value
      Lab = Lab.value
      Foundations = Foundations.value
      Cases = Cases.FieldApp.all
      Jobs = Experience.value
      Studies = Education.studies
      Achievements = Education.achievements
      Skills = Skills.value
      SoftSkills = Skills.soft
      Spoken = Education.spoken
      Interests = Skills.interests
      Projects = Projects.value }
