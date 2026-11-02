/// Autómatas celulares famosos para el laboratorio interactivo.
module CvWeb.Content.Lab

open CvWeb.Domain

let private automaton kind title author about hint =
    { Kind = kind; Title = title; Author = author; About = about; Hint = hint }

let private paint = Text.make "Haz clic en el lienzo para sembrar células." "Click the canvas to seed cells."

let value =
    [ automaton "ant" (Text.make "Hormiga de Langton" "Langton's ant") "Chris Langton, 1986"
          (Text.make
              "Tres reglas mínimas: en blanco gira a la derecha, en negro a la izquierda, voltea la celda y avanza. Tras miles de pasos caóticos, cada hormiga construye una «autopista»."
              "Three tiny rules: on white turn right, on black turn left, flip the cell, move. After thousands of chaotic steps, each ant builds a “highway”.")
          (Text.make "Haz clic para soltar otra hormiga." "Click to drop another ant.")
      automaton "life" (Text.make "Juego de la Vida" "Game of Life") "John Conway, 1970"
          (Text.make
              "Una célula nace con 3 vecinas y sobrevive con 2 o 3. De ahí salen planeadores, osciladores y hasta computadoras universales."
              "A cell is born with 3 neighbors and survives with 2 or 3. That alone yields gliders, oscillators, and even universal computers.")
          paint
      automaton "brain" (Text.make "Cerebro de Brian" "Brian's Brain") "Brian Silverman"
          (Text.make
              "Tres estados: apagada, encendida y en reposo. Una célula se enciende con exactamente 2 vecinas encendidas; casi todo se vuelve naves que viajan."
              "Three states: off, firing, and refractory. A cell fires with exactly 2 firing neighbors; almost everything turns into traveling ships.")
          paint
      automaton "cyclic" (Text.make "Autómata cíclico" "Cyclic automaton") "David Griffeath"
          (Text.make
              "Cada célula avanza a su siguiente estado si una vecina ya está ahí. Del ruido aleatorio emergen ondas y espirales que se autoorganizan."
              "Each cell advances to its next state if a neighbor is already there. Random noise self-organizes into waves and spirals.")
          (Text.make "Haz clic para sembrar ruido." "Click to sprinkle noise.")
      automaton "wireworld" (Text.same "Wireworld") "Brian Silverman, 1987"
          (Text.make
              "Electrones que viajan por cables: con sólo cuatro estados se pueden construir diodos, compuertas lógicas y relojes."
              "Electrons traveling along wires: with just four states you can build diodes, logic gates, and clocks.")
          (Text.make "Haz clic sobre un cable para lanzar un electrón." "Click a wire to launch an electron.")
      automaton "eca" (Text.make "Elementales: reglas 30, 90, 110 y 184" "Elementary: rules 30, 90, 110, and 184") "Stephen Wolfram, 1983"
          (Text.make
              "Una fila de células y 8 casos posibles. La regla 30 es caótica, la 90 dibuja el triángulo de Sierpiński, la 110 es Turing-completa y la 184 modela tráfico."
              "One row of cells and 8 possible cases. Rule 30 is chaotic, 90 draws Sierpiński's triangle, 110 is Turing-complete, and 184 models traffic.")
          (Text.make "Haz clic para cambiar de regla." "Click to switch rules.") ]
