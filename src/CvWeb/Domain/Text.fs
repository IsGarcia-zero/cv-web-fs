namespace CvWeb.Domain

/// Idiomas en los que se muestra el CV.
type Lang =
    | Es
    | En

/// Texto con su versión en cada idioma.
type Text = { Es: string; En: string }

module Lang =

    let code lang =
        match lang with
        | Es -> "es"
        | En -> "en"

    /// Cualquier código que no empiece con "en" cae en español.
    let ofCode (code: string) =
        match code with
        | null -> Es
        | c when c.StartsWith "en" -> En
        | _ -> Es

    let other lang =
        match lang with
        | Es -> En
        | En -> Es

module Text =

    let make es en = { Es = es; En = en }

    /// Mismo texto en ambos idiomas (nombres propios, tecnologías).
    let same s = { Es = s; En = s }

    let pick lang text =
        match lang with
        | Es -> text.Es
        | En -> text.En
