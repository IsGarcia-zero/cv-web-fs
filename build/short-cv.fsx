// CV corto (1–2 hojas) para adjuntar o subir a portales: una columna, texto real y encabezados
// estándar para que los ATS y las IA de selección lo lean. Usa el mismo contenido F# que el sitio,
// ajustado por perfil (build/pdf-profiles.fsx) sin IA: sólo elige y ordena lo que ya está escrito.
// Se regenera solo al compilar CvWeb si cambia el contenido (target ShortCvPdf del .fsproj).
// Uso manual: dotnet fsi build/short-cv.fsx <carpeta-de-salida>   (escribe cv-<perfil>-<es|en>.html)
#load "../src/CvWeb/Domain/Text.fs"
#load "../src/CvWeb/Domain/Charts.fs"
#load "../src/CvWeb/Domain/Case.fs"
#load "../src/CvWeb/Domain/Concept.fs"
#load "../src/CvWeb/Domain/Cv.fs"
#load "../src/CvWeb/Content/Labels.fs"
#load "../src/CvWeb/Content/Profile.fs"
#load "../src/CvWeb/Content/Experience.fs"
#load "../src/CvWeb/Content/Skills.fs"
#load "../src/CvWeb/Content/Education.fs"
#load "pdf-profiles.fsx"
#load "pdf-tailor.fsx"

open System.IO
open System.Net
open CvWeb.Domain
open CvWeb.Content
open PdfProfiles

let site = "https://isgarcia-zero.github.io/"

let style = File.ReadAllText(Path.Combine(__SOURCE_DIRECTORY__, "short-cv.css"))

/// Texto del idioma pedido, ya escapado para HTML.
let pick lang text = WebUtility.HtmlEncode(Text.pick lang text)

let esc (s: string) = WebUtility.HtmlEncode s

let all f items = items |> List.map f |> String.concat ""

let heading lang es en = $"<h2>{pick lang (Text.make es en)}</h2>"

/// Puesto (o título) en su propio encabezado y debajo institución y fechas: los lectores de CV
/// y las IA de selección separan mejor cada campo así que con todo en una sola línea.
let titleLine (title: string) (place: string) (period: string) =
    $"""<h3>{title}</h3>
<div class="line"><span class="place">{place}</span><span>{period}</span></div>"""

/// Las viñetas y tecnologías que elige el perfil; el sitio muestra todas.
let job profile lang (j: Job) =
    let bullets = PdfTailor.bullets profile j |> all (fun h -> $"<li>{pick lang h.Text}</li>")
    let summary =
        match pick lang j.Summary with
        | "" -> ""
        | s -> $"<p>{s}</p>"
    let technologies = pick lang (Text.make "Tecnologías" "Technologies")
    $"""
<section class="job">
  {titleLine (pick lang j.Role) (pick lang j.Company) (pick lang j.Period)}
  {summary}
  <ul>{bullets}</ul>
  <p class="stack">{technologies}: {esc (String.concat ", " (PdfTailor.ranked profile j.Stack))}</p>
</section>"""

let study lang (s: Study) =
    $"""
{titleLine (pick lang s.Title) (pick lang s.School) (pick lang s.Period)}
<p>{s.Notes |> List.map (pick lang) |> String.concat " "}</p>"""

let achievement lang (a: Achievement) =
    let link =
        match a.Link with
        | Some l -> $""" <a href="{l.Url}">{esc l.Url}</a>;"""
        | None -> ""
    // El punto y coma final separa un logro del siguiente sin pegarse a la URL.
    $"""
<li><strong>{pick lang a.Title}</strong> ({pick lang a.Date}). {pick lang a.Subtitle}.{link}</li>"""

/// Cierra con punto: si un extractor junta los renglones, «Dart. Backend:» no se lee como «Dart Backend».
let skill lang (g: SkillGroup) =
    $"""
<li><strong>{pick lang g.Title}:</strong> {esc (String.concat ", " g.Items)}.</li>"""

// Con dos puntos: el nivel ya trae su propio paréntesis, como «Intermedio-avanzado (B2)».
let spoken lang (s: Spoken) = $"{pick lang s.Language}: {pick lang s.Level}"

/// Cada dato de contacto con su etiqueta y la URL completa, para que se reconozca sin adivinar.
let header profile lang (p: Profile) =
    let contact es en (value: string) = $"""{pick lang (Text.make es en)}: {value}"""
    let link (url: string) = $"""<a href="{url}">{esc url}</a>"""
    let telephone =
        PdfProfiles.phone profile
        |> Option.map (fun n -> contact "Teléfono" "Phone" $"""<a href="tel:{n.Replace(" ", "")}">{esc n}</a>""")
        |> Option.toList
    let contacts =
        [ contact "Ubicación" "Location" (pick lang p.Location) ]
        @ telephone
        @ [ contact "Correo" "Email" $"""<a href="mailto:{p.Email}">{p.Email}</a>"""
            contact "LinkedIn" "LinkedIn" (link p.LinkedIn.Url)
            contact "GitHub" "GitHub" (link p.GitHub.Url)
            contact "Portafolio" "Portfolio" (link site) ]
    $"""
<header>
  <h1>{esc p.Name}</h1>
  <p class="role">{pick lang profile.Headline}</p>
  <p class="contact">{String.concat " · " contacts}</p>
</header>"""

/// Competencias clave del perfil, justo bajo el resumen: lo primero que busca un filtro automático.
let core profile lang =
    match profile.Core with
    | [] -> ""
    | items ->
        $"""{heading lang "Competencias clave" "Core competencies"}
<p>{items |> List.map (pick lang) |> String.concat ", "}.</p>"""

/// Título del PDF: Edge sólo copia `<title>` a los metadatos (autor y palabras clave no).
let meta profile lang (p: Profile) =
    $"""<meta charset="utf-8">
<title>{esc p.Name} · CV · {pick lang profile.Headline}</title>"""

let page profile lang =
    let p = Profile.value
    let fullVersion = Text.make "Versión completa con casos de estudio y capturas:" "Full version with case studies and screenshots:"
    let experience =
        $"""{heading lang "Experiencia laboral" "Work experience"}
{all (job profile lang) Experience.value}"""
    let publications =
        $"""{heading lang "Publicaciones y reconocimientos" "Publications and awards"}
<ul class="awards">{all (achievement lang) Education.achievements}</ul>"""
    let first, second = if profile.PublicationsFirst then publications, experience else experience, publications
    $"""<!doctype html>
<html lang="{Lang.code lang}">
<head>{meta profile lang p}<style>{style}</style></head>
<body>
{header profile lang p}
{heading lang "Resumen profesional" "Professional summary"}
<p>{pick lang (profile.Summary |> Option.defaultValue p.Summary)}</p>
{core profile lang}
{first}
{second}
{heading lang "Educación" "Education"}
{all (study lang) Education.studies}
{heading lang "Habilidades técnicas" "Technical skills"}
<ul class="skills">{all (skill lang) (PdfTailor.skills profile PdfProfiles.skills)}</ul>
{heading lang "Idiomas" "Languages"}
<p>{Education.spoken |> List.map (spoken lang) |> String.concat " · "}</p>
<p class="stack footnote">{pick lang Labels.updated}. {pick lang fullVersion} <a href="{site}">{site}</a></p>
</body>
</html>"""

let out = fsi.CommandLineArgs |> Array.tryItem 1 |> Option.defaultValue "."
Directory.CreateDirectory out |> ignore
for profile in PdfProfiles.all do
    for lang in [ Es; En ] do
        File.WriteAllText(Path.Combine(out, $"cv-{profile.Key}-{Lang.code lang}.html"), page profile lang)
printfn "HTML del CV corto en %s (%d perfiles)" out PdfProfiles.all.Length
