/// Fábrica de proyectos para que los archivos de contenido sólo lleven datos.
module CvWeb.Content.ProjectKit

open CvWeb.Domain

let gh repo = Some $"https://github.com/IsGarcia-zero/{repo}"

let project category name period stack url (es, en) =
    { Name = name
      Category = category
      Description = Text.make es en
      Period = period
      Stack = stack
      Url = url
      Visual = Art ""
      Status = None }

let withStatus status (p: Project) = { p with Status = Some status }

/// Pictograma que identifica el proyecto sobre la portada.
let art key (p: Project) = { p with Visual = Art key }

/// La tarjeta muestra la simulación de Physarum en lugar de la portada.
let live (p: Project) = { p with Visual = LivePhysarum }

let unpublished = Text.make "En desarrollo · sin publicar" "In progress · not published yet"
let privateCode = Text.make "Proyecto laboral · código privado" "Work project · private code"
let localOnly = Text.make "Sin publicar" "Not published yet"
