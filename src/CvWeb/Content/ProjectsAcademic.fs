module CvWeb.Content.ProjectsAcademic

open CvWeb.Domain
open CvWeb.Content.ProjectKit

let value =
    [ project Academic (Text.same "BookVault") (Text.same "2023")
          [ "Node.js"; "Express"; "EJS"; "MySQL"; "Passport.js" ] (gh "BookVault")
          ("Biblioteca digital fullstack MVC en equipo: autenticación, administración de libros, lector, favoritos y búsqueda. Creé la estructura del proyecto, el lector y los favoritos.",
           "Team-built fullstack MVC digital library: authentication, book management, reader, favorites, and search. I set up the project and built the reader and favorites.")
      |> art "book"
      project Academic (Text.make "CPU en VHDL" "CPU in VHDL") (Text.same "2022 – 2023")
          [ "VHDL"; "FPGA"; "Quartus" ] (gh "ArquitecturaComputadoras")
          ("CPU simple con ALU, unidad de control y pila de instrucciones; el proyecto final corre un juego de gato en FPGA con matriz RGB y teclado.",
           "Simple CPU with ALU, control unit, and instruction stack; the final project runs tic-tac-toe on an FPGA with an RGB matrix and keypad.")
      |> art "chip"
      project Academic (Text.make "Teoría computacional" "Theory of computation") (Text.same "2022")
          [ "C#"; ".NET"; "React"; "LaTeX" ] (gh "Teoria-Computacional")
          ("Siete programas: máquina de Turing, autómata de pila, AFD, buscador de palabras y más, con animaciones y reportes en LaTeX.",
           "Seven programs: Turing machine, pushdown automaton, DFA, word finder, and more, with animations and LaTeX reports.")
      |> art "states"
      project Academic (Text.make "Laboratorios de IA" "AI labs") (Text.same "2023")
          [ "Python"; "NumPy"; "C++"; "SFML" ] None
          ("Búsqueda BFS/DFS y 8-puzzle, clasificadores 1-NN y euclidiano con validación hold-out y k-fold, gato con minimax y momentos invariantes de Hu.",
           "BFS/DFS search and 8-puzzle, 1-NN and Euclidean classifiers with hold-out and k-fold validation, minimax tic-tac-toe, and Hu invariant moments.")
      |> art "puzzle"
      project Academic (Text.make "Redes 1" "Networks 1") (Text.same "2022")
          [ "C"; "Packet Tracer" ] (gh "Redes1")
          ("Analizadores de tramas Ethernet, LLC, IP, ARP e ICMP en C, y diseño de subredes.",
           "Ethernet, LLC, IP, ARP, and ICMP frame analyzers in C, plus subnet design.")
      |> art "network"
      project Academic (Text.make "Ruta más corta y método ABC" "Shortest path and ABC method") (Text.same "2023")
          [ "Java"; "JavaFX"; "Maven" ] (Some "https://github.com/Jessenox/RutaMasCorta")
          ("Cálculo y dibujo de la ruta más corta en un grafo capturado por el usuario, y clasificación ABC de inventarios.",
           "Computes and draws the shortest path on a user-drawn graph, plus ABC inventory classification.")
      |> art "graph"
      project Academic (Text.make "Realidad virtual" "Virtual reality") (Text.same "2023")
          [ "Unity"; "C#"; "VR" ] None
          ("Juego inmersivo en Unity con interacciones de realidad virtual.",
           "Immersive Unity game with virtual reality interactions.")
      |> art "vr"
      project Academic (Text.same "Acoreson / WorkCore") (Text.same "2020 – 2021")
          [ "PHP"; "MySQL"; "Java"; "Android" ] None
          ("Plataforma web y app Android para talleres mecánicos: inventario, personal, siniestros, tareas y chat entre cliente y mecánico.",
           "Web platform and Android app for mechanical workshops: inventory, staff, claims, tasks, and customer–mechanic chat.")
      |> art "wrench"
      project Academic (Text.same "METROUTE") (Text.make "Bachillerato" "High school")
          [ "Java"; "JSP"; "Servlets" ] None
          ("Rutas alternativas del Metro de la Ciudad de México para descongestionar estaciones clave.",
           "Alternative Mexico City Metro routes to relieve congestion at key stations.")
      |> art "metro"
      project Academic (Text.make "HEJID — Tabla periódica" "HEJID — Periodic table") (Text.make "Bachillerato" "High school")
          [ "Java"; "Swing" ] None
          ("Aplicación de escritorio para aprender la tabla periódica con minijuegos.",
           "Desktop app for learning the periodic table through mini-games.")
      |> art "periodic" ]
