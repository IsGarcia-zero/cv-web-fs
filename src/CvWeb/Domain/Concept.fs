namespace CvWeb.Domain

/// Referencia bibliográfica ya escrita en formato IEEE; su número es el orden en la lista.
type Reference = { Key: string; Ieee: string; Url: string option }

/// Concepto explicado como en la tesis y los reportes: definición, notación formal,
/// citas (claves de `Reference`) y dónde lo apliqué.
type Concept =
    { Key: string
      Term: Text
      Body: Text
      Formal: Text
      Cites: string list
      AppliedIn: Text }

type Foundations =
    { Intro: Text
      Concepts: Concept list
      References: Reference list }
