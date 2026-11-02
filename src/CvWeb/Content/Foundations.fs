/// Las bases de mi trabajo explicadas como en la tesis (Physarum) y en los reportes de Teoría
/// Computacional, con las mismas fuentes. Las citas siguen IEEE, igual que la tesis.
module CvWeb.Content.Foundations

open CvWeb.Domain

let private t = Text.make

let private reference key ieee url = { Key = key; Ieee = ieee; Url = url }

let references =
    [ reference "hopcroft"
        "J. E. Hopcroft and J. D. Ullman, Introduction to Automata Theory, Languages, and Computation. Reading, MA, USA: Addison-Wesley, 1979."
        None
      reference "aranda"
        "J. Aranda, N. Duro, J. L. Fernández, J. Jiménez y F. Morilla, Fundamentos de lógica matemática y computación. Madrid, España: Sanz y Torres, 2006."
        None
      reference "aho"
        "A. V. Aho, M. S. Lam, R. Sethi and J. D. Ullman, Compilers: Principles, Techniques, and Tools, 2nd ed. Boston, MA, USA: Addison-Wesley, 2006."
        None
      reference "codd" "E. F. Codd, Cellular Automata. New York, NY, USA: Academic Press, 1968." None
      reference "wolfram" "S. Wolfram, A New Kind of Science. Champaign, IL, USA: Wolfram Media, 2002."
        (Some "https://www.wolframscience.com/nks/")
      reference "gardner"
        "M. Gardner, \"Mathematical games: The fantastic combinations of John Conway's new solitaire game \"life\",\" Scientific American, vol. 223, no. 4, pp. 120–123, 1970."
        (Some "https://doi.org/10.1038/scientificamerican1070-120")
      reference "adamatzky"
        "A. Adamatzky, Physarum Machines: Computers from Slime Mould. Singapore: World Scientific, 2010."
        None
      reference "jones"
        "J. Jones, From Pattern Formation to Material Computation: Multi-agent Modelling of Physarum Polycephalum. Cham, Switzerland: Springer, 2015."
        None ]

let private concept key term body formal cites applied =
    { Key = key; Term = term; Body = body; Formal = formal; Cites = cites; AppliedIn = applied }

let concepts =
    [ concept "alphabet"
        (t "Alfabeto, cadena y lenguaje" "Alphabet, string and language")
        (t "Un alfabeto es un conjunto de símbolos finito y no vacío; convencionalmente lo designamos con Σ. Una cadena es una secuencia finita de símbolos de ese alfabeto, como 01101 sobre el alfabeto binario, y un lenguaje es un conjunto de cadenas. Con las potencias de Σ construimos el universo Σ*, todas las cadenas posibles."
           "An alphabet is a finite, non-empty set of symbols, conventionally written Σ. A string is a finite sequence of symbols from that alphabet, such as 01101 over the binary alphabet, and a language is a set of strings. The powers of Σ build the universe Σ*, every possible string.")
        (Text.same "Σ = {0, 1}    Σ* = Σ⁰ ∪ Σ¹ ∪ Σ² ∪ …")
        [ "hopcroft" ]
        (t "Práctica 1 de Teoría Computacional: el universo de cadenas binarias y su gráfica."
           "Theory of Computation lab 1: the universe of binary strings and its chart.")
      concept "dfa"
        (t "Autómata finito determinista (AFD)" "Deterministic finite automaton (DFA)")
        (t "Un autómata finito es un modelo computacional que realiza cómputos de forma automática sobre una entrada para producir una salida. Lo forman un alfabeto, un conjunto finito de estados, una función de transición, un estado inicial y un conjunto de estados finales: lee la cadena símbolo por símbolo y, si se detiene en un estado de aceptación, la cadena pertenece al lenguaje. Reconoce los lenguajes regulares, los más simples de la jerarquía de Chomsky."
           "A finite automaton is a computational model that computes automatically over an input to produce an output. It consists of an alphabet, a finite set of states, a transition function, a start state and a set of final states: it reads the string symbol by symbol and, if it halts in an accepting state, the string belongs to the language. It recognizes the regular languages, the simplest in the Chomsky hierarchy.")
        (Text.same "M = (Q, Σ, δ, q₀, F)    δ : Q × Σ → Q")
        [ "hopcroft" ]
        (t "Práctica 3: un protocolo validado con el autómata de paridad." "Lab 3: a protocol validated with the parity automaton.")
      concept "lexeme"
        (t "AFN, palabras clave y lexemas" "NFA, keywords and lexemes")
        (t "Un autómata finito no determinista puede estar en varios estados a la vez, como si conjeturara que ahí empieza una de las palabras que busca. Para hallar palabras clave en un texto basta un estado inicial que se cicla con cualquier símbolo y una cadena de estados por palabra que la comprueba carácter por carácter. Es la misma idea del analizador léxico de un compilador: cada secuencia de caracteres que coincide con el patrón de un token (un identificador, un número, una palabra reservada) es un lexema, y ese patrón se reconoce con un autómata finito."
           "A nondeterministic finite automaton can be in several states at once, as if guessing that one of the words it looks for starts right there. To find keywords in a text it is enough to have a start state that loops on every symbol and one chain of states per word that checks it character by character. A compiler's lexical analyzer uses the same idea: each character sequence that matches a token's pattern (an identifier, a number, a reserved word) is a lexeme, and that pattern is recognized by a finite automaton.")
        (Text.same "δ : Q × Σ → 𝒫(Q)    \"x1\" → ⟨id, x1⟩    \"42\" → ⟨num, 42⟩")
        [ "hopcroft"; "aho" ]
        (t "Práctica 4: buscador de palabras con un AFN y su grafo en React." "Lab 4: a word finder built on an NFA, with its graph in React.")
      concept "pda"
        (t "Autómata de pila" "Pushdown automaton")
        (t "Fundamentalmente es un AFN con transiciones ε y una capacidad adicional: una pila donde guarda símbolos. Gracias a ella puede «recordar» una cantidad ilimitada de información, pero sólo la consulta por el tope: el último en entrar es el primero en salir. Con eso ya reconoce lenguajes libres de contexto, como el de n ceros seguidos de n unos, que ningún autómata finito puede contar."
           "Essentially it is an NFA with ε-transitions and one extra capability: a stack where it stores symbols. Thanks to it, it can \"remember\" an unbounded amount of information, but only reads it from the top: last in, first out. That is enough to recognize context-free languages, such as n zeros followed by n ones, which no finite automaton can count.")
        (Text.same "L = { 0ⁿ1ⁿ | n ≥ 0 }    δ : Q × (Σ ∪ {ε}) × Γ → 𝒫(Q × Γ*)")
        [ "hopcroft" ]
        (t "Práctica 6: autómata de pila que acepta 0ⁿ1ⁿ." "Lab 6: a pushdown automaton that accepts 0ⁿ1ⁿ.")
      concept "turing"
        (t "Máquina de Turing y jerarquía de Chomsky" "Turing machine and the Chomsky hierarchy")
        (t "La máquina de Turing es el precursor histórico de las computadoras de programa almacenado: una cinta, un cabezal que lee y escribe, y una tabla de estados. Junto con los autómatas anteriores forma una escalera: cada tipo de gramática de la jerarquía de Chomsky tiene asociado un autómata, y cada escalón reconoce más lenguajes que el anterior."
           "The Turing machine is the historical forerunner of stored-program computers: a tape, a head that reads and writes, and a state table. Together with the previous automata it forms a ladder: each grammar type in the Chomsky hierarchy has an associated automaton, and each step recognizes more languages than the one below.")
        (Text.same "δ : Q × Γ → Q × Γ × {L, R}")
        [ "hopcroft"; "aranda" ]
        (t "Práctica 7: máquina de Turing." "Lab 7: a Turing machine.")
      concept "ca"
        (t "Autómata celular" "Cellular automaton")
        (t "Los autómatas celulares también forman parte de la teoría de autómatas, pero en vez de procesar una cadena de forma secuencial usan una red de células cuyos estados evolucionan en paralelo siguiendo reglas locales. Formalmente es una tupla con el espacio, los estados, la vecindad y una función de transición local que se aplica a todas las celdas al mismo tiempo; por eso son tan buenos para modelar fenómenos dinámicos y patrones espaciales."
           "Cellular automata are part of automata theory too, but instead of processing a string sequentially they use a lattice of cells whose states evolve in parallel under local rules. Formally it is a tuple of space, states, neighborhood and a local transition function applied to every cell at once, which is why they are so good at modeling dynamic phenomena and spatial patterns.")
        (Text.same "A = (ℤⁿ, S, N, f)    f : Sᴺ → S    c′(x) = f(c|ₓ₊ₙ)")
        [ "codd" ]
        (t "Mi tesis y el laboratorio de autómatas de esta página." "My thesis and the automata lab on this page.")
      concept "eca"
        (t "Reglas elementales de Wolfram (ECA)" "Wolfram's elementary rules (ECA)")
        (t "En una dimensión tenemos una línea de celdas con dos estados. Cada celda se mira a sí misma y a sus dos vecinos, así que hay 2³ = 8 vecindades posibles y 2⁸ = 256 reglas; el número de la regla es su tabla de salidas leída en binario. La regla 30, por ejemplo, produce un patrón caótico a partir de una sola celda viva."
           "In one dimension we have a line of two-state cells. Each cell looks at itself and its two neighbors, so there are 2³ = 8 possible neighborhoods and 2⁸ = 256 rules; the rule number is its output table read in binary. Rule 30, for instance, grows a chaotic pattern from a single live cell.")
        (Text.same "111 110 101 100 011 010 001 000 → 0 0 0 1 1 1 1 0₂ = 30")
        [ "wolfram" ]
        (t "Los repositorios de reglas ECA y de control de tráfico, y las portadas de los proyectos de este CV."
           "The ECA rules and traffic-control repositories, and the project covers on this CV.")
      concept "neighborhood"
        (t "Vecindades y condiciones frontera" "Neighborhoods and boundary conditions")
        (t "La vecindad es el conjunto de celdas que se toman en cuenta para actualizar una celda: la de von Neumann usa cuatro vecinos, la de Moore ocho y la hexagonal seis. Por definición los autómatas celulares son infinitos, pero en la práctica no, así que hay que fijar qué pasa en los extremos: frontera abierta, reflejante, con valor fijo o periódica. En la tesis usamos la periódica, en la que la cuadrícula se cierra como un toroide."
           "The neighborhood is the set of cells considered when updating a cell: von Neumann uses four neighbors, Moore eight and hexagonal six. By definition cellular automata are infinite, but in practice they are not, so we must decide what happens at the edges: open, reflective, fixed-value or periodic boundaries. The thesis uses the periodic one, where the grid closes into a torus.")
        (t "Moore |N| = 8    von Neumann |N| = 4    periódica ≅ toroide" "Moore |N| = 8    von Neumann |N| = 4    periodic ≅ torus")
        [ "codd" ]
        (t "Tesis: cuadrícula toroidal con vecindad de Moore." "Thesis: a toroidal grid with the Moore neighborhood.")
      concept "life"
        (t "Juego de la vida" "Game of Life")
        (t "El ejemplo clásico de autómata celular en dos dimensiones. Con la vecindad de Moore, una celda muerta nace si tiene exactamente tres vecinas vivas y una viva sobrevive con dos o tres; en cualquier otro caso muere. De reglas tan simples salen planeadores, osciladores y hasta estructuras capaces de computar."
           "The classic two-dimensional cellular automaton. With the Moore neighborhood, a dead cell is born with exactly three live neighbors and a live one survives with two or three; otherwise it dies. Such simple rules give rise to gliders, oscillators and even structures able to compute.")
        (t "f = 1 si C = 0 ∧ N = 3;  1 si C = 1 ∧ N ∈ {2, 3};  0 en otro caso" "f = 1 if C = 0 ∧ N = 3;  1 if C = 1 ∧ N ∈ {2, 3};  0 otherwise")
        [ "gardner" ]
        (t "El repositorio juegoDeLaVida y una sorpresa en esta página: escribe «life»."
           "The juegoDeLaVida repository and a surprise on this page: type \"life\".")
      concept "physarum"
        (t "Physarum polycephalum y su modelo multiagente" "Physarum polycephalum and its multi-agent model")
        (t "El Physarum polycephalum, «The Blob», es un mixomiceto cuyo plasmodio forma una red de tubos capaz de resolver laberintos y de encontrar rutas cortas entre fuentes de alimento. Computacionalmente lo aproximamos con miles de agentes que sensan un rastro químico, giran hacia donde es más fuerte, avanzan y depositan más rastro; el rastro se difunde y se evapora, y de ahí emergen redes de transporte."
           "Physarum polycephalum, \"The Blob\", is a slime mold whose plasmodium forms a tube network that can solve mazes and find short routes between food sources. Computationally we approximate it with thousands of agents that sense a chemical trail, turn toward the strongest side, move and deposit more trail; the trail diffuses and decays, and transport networks emerge.")
        (t "sensar → girar → avanzar → depositar → difundir · evaporar" "sense → rotate → move → deposit → diffuse · decay")
        [ "adamatzky"; "jones" ]
        (t "Tesis, el artículo de CANDAR 2024 y la simulación del caso de estudio." "Thesis, the CANDAR 2024 paper and the case-study simulation.") ]

let value =
    { Intro =
        t "Así explico las bases de mi trabajo: con las mismas definiciones y fuentes de mi tesis y de mis reportes de Teoría Computacional. Las citas siguen el formato IEEE."
          "This is how I explain the foundations of my work, with the same definitions and sources as my thesis and my Theory of Computation reports. Citations follow the IEEE style."
      Concepts = concepts
      References = references }
