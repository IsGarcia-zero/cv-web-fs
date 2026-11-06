/// Trayectoria para la línea de tiempo y reparto de commits por lenguaje.
module CvWeb.Content.Career

open CvWeb.Domain

let private span what where from until kind =
    { What = what; Where = where; From = from; Until = until; Kind = kind }

let timeline =
    { Start = 2017
      Finish = 2027
      Spans =
        [ span (Text.make "Técnico en Programación" "Programming Technician") "CECyT 9" 2017.6 2020.5 Schooling
          span (Text.make "Ing. en Sistemas Computacionales" "B.Sc. Computer Systems Eng.") "ESCOM · IPN" 2020.6 2024.95 Schooling
          span (Text.make "Investigación" "Research") "ALIROB" 2023.0 2025.05 Lab
          span (Text.make "Desarrollador .NET" ".NET Developer") "IMPI" 2025.25 2025.5 Employment
          span (Text.make "Backend & Cloud" "Backend & Cloud") "Cantilever" 2025.5 2026.83 Employment ]
      Milestones =
        [ { Event = Text.make "Primer commit" "First commit"; At = 2020.15 }
          { Event = Text.make "Finalista Talent Land" "Talent Land finalist"; At = 2022.3 }
          { Event = Text.same "CANDAR · Okinawa"; At = 2024.85 }
          { Event = Text.make "Titulación con honores" "Graduated with honors"; At = 2024.95 } ] }

/// Generado con un script: cada commit cuenta para el lenguaje principal de su repositorio.
let languages =
    [ { Language = "C#"; Commits = 162 }
      { Language = "JavaScript"; Commits = 78 }
      { Language = "Go"; Commits = 61 }
      { Language = "VHDL"; Commits = 54 }
      { Language = "C/C++"; Commits = 48 }
      { Language = "Dart"; Commits = 34 }
      { Language = "TypeScript"; Commits = 34 }
      { Language = "PHP"; Commits = 30 }
      { Language = "Java"; Commits = 8 }
      { Language = "Infra · Bash"; Commits = 8 }
      { Language = "Python"; Commits = 5 } ]
