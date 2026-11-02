// CV corto (1–2 hojas) para adjuntar o subir a portales: una columna, texto real y encabezados
// estándar para que los ATS lo lean. Usa el mismo contenido F# que el sitio.
// Se regenera solo al compilar CvWeb si cambia el contenido (target ShortCvPdf del .fsproj).
// Uso manual: dotnet fsi build/short-cv.fsx <carpeta-de-salida>   (escribe cv-es.html y cv-en.html)
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

open System.IO
open System.Net
open CvWeb.Domain
open CvWeb.Content

let site = "https://isgarcia-zero.github.io/"

let style =
    """
@page { size: Letter; margin: 13mm 15mm; }
* { box-sizing: border-box; }
body { margin: 0; font: 9.6pt/1.42 "Segoe UI", Arial, sans-serif; color: #111; }
h1 { margin: 0; font-size: 19pt; letter-spacing: -.01em; }
.role { margin: 1px 0 4px; font-size: 11pt; color: #00527f; font-weight: 600; }
.contact { margin: 0; color: #333; }
.contact a { color: inherit; text-decoration: none; }
h2 { margin: 10px 0 4px; padding-bottom: 2px; border-bottom: 1px solid #bbb; font-size: 10.5pt;
     text-transform: uppercase; letter-spacing: .06em; color: #00527f; }
h3 { margin: 0; font-size: 10pt; }
p { margin: 2px 0; }
ul { margin: 2px 0 0; padding-left: 15px; }
li { margin: 1px 0; }
.line { display: flex; justify-content: space-between; gap: 12px; align-items: baseline; }
.line span { white-space: nowrap; color: #333; }
.job { margin-bottom: 7px; break-inside: avoid-page; }
.stack { color: #444; font-size: 8.8pt; }
.skills { list-style: none; padding: 0; }
a { color: #00527f; }
"""

/// Texto del idioma pedido, ya escapado para HTML.
let pick lang text = WebUtility.HtmlEncode(Text.pick lang text)

let esc (s: string) = WebUtility.HtmlEncode s

let all f items = items |> List.map f |> String.concat ""

let heading lang es en = $"<h2>{pick lang (Text.make es en)}</h2>"

/// Título a la izquierda y fecha a la derecha, en la misma línea.
let titleLine (title: string) (period: string) =
    $"""<div class="line"><h3>{title}</h3><span>{period}</span></div>"""

/// Sólo las viñetas marcadas con `pdf` en Content/Experience.fs; el sitio muestra todas.
let job lang (j: Job) =
    let bullets = j.Highlights |> List.filter _.InPdf |> all (fun h -> $"<li>{pick lang h.Text}</li>")
    let summary =
        match pick lang j.Summary with
        | "" -> ""
        | s -> $"<p>{s}</p>"
    $"""
<section class="job">
  {titleLine $"{pick lang j.Role} · {pick lang j.Company}" (pick lang j.Period)}
  {summary}
  <ul>{bullets}</ul>
  <p class="stack">{esc (String.concat ", " j.Stack)}</p>
</section>"""

let study lang (s: Study) =
    $"""
{titleLine $"{pick lang s.Title} · {pick lang s.School}" (pick lang s.Period)}
<p>{s.Notes |> List.map (pick lang) |> String.concat " "}</p>"""

let achievement lang (a: Achievement) =
    let link =
        match a.Link with
        | Some l -> $""" <a href="{l.Url}">{esc l.Label}</a>"""
        | None -> ""
    $"""
<li><strong>{pick lang a.Title}</strong> ({pick lang a.Date}). {pick lang a.Subtitle}.{link}</li>"""

let skill lang (g: SkillGroup) =
    $"""
<li><strong>{pick lang g.Title}:</strong> {esc (String.concat ", " g.Items)}</li>"""

let spoken lang (s: Spoken) = $"{pick lang s.Language} ({pick lang s.Level})"

let header lang (p: Profile) =
    let contacts =
        [ pick lang p.Location
          $"""<a href="mailto:{p.Email}">{p.Email}</a>"""
          $"""<a href="{p.LinkedIn.Url}">linkedin.com/in/eduardo-hernandez-vergara-369366246</a>"""
          $"""<a href="{p.GitHub.Url}">github.com/IsGarcia-zero</a>"""
          $"""<a href="{site}">isgarcia-zero.github.io</a>""" ]
    $"""
<header>
  <h1>{esc p.Name}</h1>
  <p class="role">{pick lang p.Role} · Backend &amp; Cloud · Go · AWS · .NET</p>
  <p class="contact">{String.concat " · " contacts}</p>
</header>"""

let page lang =
    let p = Profile.value
    let fullVersion = Text.make "Versión completa con casos de estudio y capturas:" "Full version with case studies and screenshots:"
    $"""<!doctype html>
<html lang="{Lang.code lang}">
<head><meta charset="utf-8"><title>{esc p.Name} · CV</title><style>{style}</style></head>
<body>
{header lang p}
{heading lang "Perfil" "Summary"}
<p>{pick lang p.Summary}</p>
{heading lang "Experiencia" "Experience"}
{all (job lang) Experience.value}
{heading lang "Publicación y logros" "Publication and awards"}
<ul>{all (achievement lang) Education.achievements}</ul>
{heading lang "Educación" "Education"}
{all (study lang) Education.studies}
{heading lang "Habilidades" "Skills"}
<ul class="skills">{all (skill lang) Skills.value}</ul>
{heading lang "Idiomas" "Languages"}
<p>{Education.spoken |> List.map (spoken lang) |> String.concat " · "}</p>
<p class="stack">{pick lang Labels.updated}. {pick lang fullVersion} <a href="{site}">{site}</a></p>
</body>
</html>"""

let out = fsi.CommandLineArgs |> Array.tryItem 1 |> Option.defaultValue "."
Directory.CreateDirectory out |> ignore
for lang in [ Es; En ] do
    File.WriteAllText(Path.Combine(out, $"cv-{Lang.code lang}.html"), page lang)
printfn "HTML del CV corto en %s" out
