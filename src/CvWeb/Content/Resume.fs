/// El CV completo, armado con las piezas de cada archivo de contenido.
module CvWeb.Content.Resume

open CvWeb.Domain

let private technologies =
    Skills.value |> List.collect (fun g -> g.Items) |> List.distinct |> List.length

let private stats =
    [ { Value = 3; Suffix = "+"; Label = Text.make "Años de experiencia" "Years of experience" }
      { Value = 9; Suffix = "+"; Label = Text.make "Años programando" "Years coding" }
      { Value = Activity.value.Commits; Suffix = ""; Label = Text.make "Commits desde 2020" "Commits since 2020" }
      { Value = Activity.value.Repositories; Suffix = ""; Label = Text.make "Repositorios" "Repositories" }
      { Value = List.length Projects.value; Suffix = ""; Label = Text.make "Proyectos" "Projects" }
      { Value = technologies; Suffix = "+"; Label = Text.make "Tecnologías" "Technologies" } ]

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
