module CvWeb.Content.Education

open CvWeb.Domain

let studies =
    [ { Title = Text.make "Ingeniería en Sistemas Computacionales" "B.Sc. in Computer Systems Engineering"
        School = Text.same "Escuela Superior de Cómputo (ESCOM), Instituto Politécnico Nacional"
        Period = Text.same "2020 – 2024"
        Notes =
          [ Text.make "Titulado con mención honorífica por tesis (diciembre de 2024)." "Graduated with honors by thesis (December 2024)."
            Text.make
                "Tesis: «Diseño de un sistema autónomo para monitoreo basado en autómatas celulares y Physarum polycephalum»."
                "Thesis: “Design of an autonomous monitoring system based on cellular automata and Physarum polycephalum.”" ] }
      { Title = Text.make "Bachillerato Técnico en Programación" "Programming Technician (high school)"
        School = Text.same "CECyT 9 «Juan de Dios Bátiz», Instituto Politécnico Nacional"
        Period = Text.same "2017 – 2020"
        Notes = [] } ]

let achievements =
    [ { Icon = "🏆"
        Title = Text.make "Publicación internacional — CANDAR 2024" "International publication — CANDAR 2024"
        Subtitle =
          Text.make
              "Workshop ASON, International Symposium on Computing and Networking, Okinawa, Japón"
              "ASON workshop, International Symposium on Computing and Networking, Okinawa, Japan"
        Date = Text.make "Noviembre 2024" "November 2024"
        Description =
          Text.make
              "«Physarum polycephalum inspired routing for cave and catacomb navigation»: algoritmo de enrutamiento paralelo para mapear y navegar entornos subterráneos, robusto ante obstáculos y variaciones topográficas. Publicado en el Bulletin of Networking, Computing, Systems, and Software, vol. 14, núm. 1, pp. 4–9 (enero 2025)."
              "“Physarum polycephalum inspired routing for cave and catacomb navigation”: a parallel routing algorithm to map and navigate underground environments, robust to obstacles and terrain changes. Published in the Bulletin of Networking, Computing, Systems, and Software, vol. 14, no. 1, pp. 4–9 (January 2025)."
        Link =
          Some
              { Label = "bncss.org"
                Url = "http://bncss.org/index.php/bncss/article/view/183" } }
      { Icon = "🎓"
        Title = Text.make "Titulación con mención honorífica" "Graduated with honors"
        Subtitle = Text.same "Escuela Superior de Cómputo, IPN"
        Date = Text.make "Diciembre 2024" "December 2024"
        Description =
          Text.make
              "Titulación por tesis con distinción por el trabajo en sistemas autónomos basados en autómatas celulares y Physarum polycephalum."
              "Degree by thesis with honors for work on autonomous systems based on cellular automata and Physarum polycephalum."
        Link = None }
      { Icon = "⚡"
        Title = Text.make "Finalista — Hackathon Talent Land 2022" "Finalist — Talent Land Hackathon 2022"
        Subtitle = Text.same "Talent Land México"
        Date = Text.same "2022"
        Description =
          Text.make
              "Desarrollo de una plataforma web de refuerzo educativo en equipo."
              "Built an online learning-support web platform as part of a team."
        Link = None } ]

let spoken =
    [ { Language = Text.make "Español" "Spanish"; Level = Text.make "Nativo" "Native"; Percent = 100 }
      { Language = Text.make "Inglés" "English"; Level = Text.make "Intermedio-avanzado (B2)" "Upper intermediate (B2)"; Percent = 70 }
      { Language = Text.make "Japonés" "Japanese"; Level = Text.make "Básico (A2)" "Basic (A2)"; Percent = 25 }
      { Language = Text.make "Chino" "Chinese"; Level = Text.make "Básico (A1)" "Basic (A1)"; Percent = 15 } ]
