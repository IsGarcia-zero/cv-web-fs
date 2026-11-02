/// Textos fijos de la interfaz.
module CvWeb.Content.Labels

open CvWeb.Domain

let private t = Text.make

// ── Títulos de sección ──

let about = t "Sobre mí" "About me"
let experience = t "Experiencia" "Experience"
let cases = t "Casos de estudio" "Case studies"
let systems = t "Sistemas que construí" "Systems I built"
let projects = t "Proyectos" "Projects"
let lab = t "Laboratorio de autómatas" "Automata lab"
let achievements = t "Logros" "Achievements"
let education = t "Educación" "Education"
let skills = t "Habilidades" "Skills"
let interests = t "Especialización" "Focus areas"
let spoken = t "Idiomas" "Languages"
let contact = t "Contacto" "Contact"
let foundations = t "Fundamentos" "Foundations"

/// Secciones de la barra de navegación (ancla, título), en el orden de la página.
let nav =
    [ "about", about
      "experience", experience
      "cases", cases
      "systems", systems
      "projects", projects
      "lab", lab
      "achievements", achievements
      "education", education
      "skills", skills
      "interests", interests
      "languages", spoken
      "contact", contact
      "foundations", foundations ]

// ── Barra lateral y controles ──

let sections = t "Secciones" "Sections"
let available = t "Abierto a ofertas" "Open to work"
let automaton = t "Autómata celular elemental en vivo" "Live elementary cellular automaton"
let language = t "Idioma" "Language"
let themeDark = t "Cambiar a tema oscuro" "Switch to dark theme"
let themeLight = t "Cambiar a tema claro" "Switch to light theme"
let copyEmail = t "Copiar correo" "Copy email"
let copied = t "Correo copiado al portapapeles" "Email copied to clipboard"
let skip = t "Saltar al contenido" "Skip to content"
let backToTop = t "Volver arriba" "Back to top"
let newTab = t "(abre en otra pestaña)" "(opens in a new tab)"

// ── Sobre mí ──

let activityTitle = t "Actividad en git" "Git activity"
let less = t "Menos" "Less"
let more = t "Más" "More"

/// "N commits en M repositorios desde X".
let activity commits repos (since: Text) =
    t $"{commits} commits en {repos} repositorios públicos y privados desde {since.Es}"
        $"{commits} commits across {repos} public and private repositories since {since.En}"

// ── Experiencia ──

let career = t "Trayectoria" "Career path"
let study = t "Formación" "Education"
let research = t "Investigación" "Research"
let job = t "Empleo" "Work"
let showMore n = t $"Ver {n} más" $"Show {n} more"
let showLess = t "Ver menos" "Show less"

// ── Casos de estudio y laboratorio ──

let challenge = t "El reto" "The challenge"
let decisions = t "Decisiones clave" "Key decisions"
let outcome = t "Resultado" "Outcome"
let myRole = t "Mi papel" "My role"
let pause = t "Pausar" "Pause"
let play = t "Reanudar" "Play"
let restart = t "Reiniciar" "Restart"

// ── Proyectos ──

let all = t "Todos" "All"
let allProjects n = t $"Ver los {n} proyectos" $"See all {n} projects"
let fewerProjects = t "Ver menos proyectos" "Show fewer projects"

let category c =
    match c with
    | Work -> t "Laboral" "Work"
    | Research -> t "Investigación" "Research"
    | Personal -> t "Personales" "Personal"
    | Academic -> t "Académicos" "Academic"

// ── Habilidades ──

let softSkills = t "Habilidades blandas" "Soft skills"
let byLanguage = t "Commits por lenguaje" "Commits by language"
let byLanguageNote =
    t "Cada commit cuenta para el lenguaje principal de su repositorio."
        "Each commit counts toward its repository's main language."

// ── Fundamentos ──

let references = t "Referencias" "References"
let appliedIn = t "Dónde lo apliqué:" "Where I applied it:"
let chomsky =
    t "Jerarquía de Chomsky: cada tipo de lenguaje contiene a los de adentro y tiene su autómata."
        "Chomsky hierarchy: each language class contains the inner ones and has its own automaton."

// ── Contacto, PDF y pie ──

let contactTitle = t "¿Hablamos?" "Let's talk"
let contactLead =
    t "Busco roles de backend, nube o desarrollo de software. Escríbeme y te respondo en menos de un día hábil."
        "I'm looking for backend, cloud, or software engineering roles. Write to me and I'll reply within one business day."
let downloadPdf = t "CV en PDF" "CV as PDF"
let printFull = t "Imprimir la versión completa" "Print the full version"

/// PDF corto (1–2 hojas) generado por build/short-cv.fsx con el mismo contenido.
let pdfPath lang = $"pdf/Eduardo_Hernandez_Vergara_CV_{(Lang.code lang).ToUpperInvariant()}.pdf"

let updated = t "Actualizado: octubre de 2026" "Updated: October 2026"
let builtWith = t "Hecho con F#, Bolero y Blazor WebAssembly." "Built with F#, Bolero, and Blazor WebAssembly."
let sourceCode = t "Ver código" "View source"
let sourceUrl = "https://github.com/IsGarcia-zero/cv-web-fs"
