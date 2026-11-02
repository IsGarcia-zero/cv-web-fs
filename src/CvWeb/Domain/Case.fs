namespace CvWeb.Domain

/// Cifra destacada de un caso de estudio ("947" + "pruebas automatizadas").
type Metric = { Value: string; Caption: Text }

/// Captura de una vista con datos simulados y descripción accesible bilingüe.
type CaseShot = { Source: string; Caption: Text; Width: int; Height: int }

/// Qué se muestra junto al caso: simulación en vivo, un diagrama de "Sistemas" o capturas.
type CaseVisual =
    | LiveSimulation
    | SystemDiagram of key: string
    | Screenshots of images: CaseShot list

type CaseStudy =
    { Key: string
      Title: Text
      TabTitle: Text
      Tagline: Text
      Metrics: Metric list
      Challenge: Text
      Decisions: Text list
      Outcome: Text list
      Role: Text
      Stack: string list
      Visual: CaseVisual
      /// Aclaración honesta del alcance (por ejemplo, cifras de una validación previa).
      Note: Text option }
