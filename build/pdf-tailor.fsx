/// Ajuste del CV corto a un perfil, sin IA: elige y ordena por palabras clave lo que ya está escrito.
/// Nunca redacta ni agrega nada; con un perfil sin palabras clave deja todo como el sitio.
module PdfTailor

open System
open System.Globalization
open System.Text
open System.Text.RegularExpressions
open CvWeb.Domain
open PdfProfiles

/// Minúsculas y sin acentos: «Auditoría» y «auditoria» cuentan igual.
let normalize (s: string) =
    let letters =
        s.Normalize(NormalizationForm.FormD)
        |> Seq.filter (fun c -> CharUnicodeInfo.GetUnicodeCategory c <> UnicodeCategory.NonSpacingMark)
        |> Seq.toArray
    String(letters).ToLowerInvariant()

/// Una clave empata al inicio de una palabra ("monitor" → monitoreo, monitoring);
/// con "$" al final, sólo la palabra completa ("go$" no empata «gobierno»).
let private pattern (keyword: string) =
    let tail = if keyword.EndsWith "$" then @"(?![\p{L}\p{N}])" else ""
    Regex(@"(?<![\p{L}\p{N}])" + Regex.Escape(normalize (keyword.TrimEnd '$')) + tail)

/// Cuántas palabras clave distintas del perfil aparecen en el texto.
let score (profile: PdfProfile) (text: string) =
    let normalized = normalize text
    profile.Keywords |> List.filter (fun k -> (pattern k).IsMatch normalized) |> List.length

/// Máximo de viñetas por puesto: el PDF tiene que caber en dos hojas.
let maxBullets = 7

/// Sin palabras clave: las viñetas marcadas `pdf`. Con ellas: las más afines primero
/// (en ambos idiomas, para que los dos PDF elijan lo mismo) y los empates en su orden.
let bullets (profile: PdfProfile) (job: Job) =
    match profile.Keywords with
    | [] -> job.Highlights |> List.filter _.InPdf
    | _ ->
        job.Highlights
        |> List.mapi (fun i h -> h, score profile $"{h.Text.Es} {h.Text.En}", i)
        |> List.sortBy (fun (_, s, i) -> -s, i)
        |> List.truncate maxBullets
        |> List.map (fun (h, _, _) -> h)

/// Lo que coincide con el perfil va primero; el resto conserva su orden (`sortBy` es estable).
let ranked profile (items: string list) =
    items |> List.sortBy (fun item -> if score profile item > 0 then 0 else 1)

/// Grupos con más coincidencias primero; "También he usado" se queda al final.
let skills profile (groups: SkillGroup list) =
    let hits (g: SkillGroup) = g.Items |> List.filter (fun i -> score profile i > 0) |> List.length
    let tailored (g: SkillGroup) = { g with Items = ranked profile g.Items }
    let main, also = List.splitAt (groups.Length - 1) groups
    (main |> List.sortByDescending hits) @ also |> List.map tailored
