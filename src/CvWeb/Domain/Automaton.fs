/// Autómatas celulares elementales (reglas de Wolfram), funciones puras.
/// Dan a cada proyecto una portada propia y reproducible a partir de su nombre.
module CvWeb.Domain.Automaton

/// Reglas con patrones vistosos (caóticas, fractales o complejas).
let private rules = [| 30; 45; 54; 73; 90; 105; 110; 126; 150; 22 |]

/// Siguiente generación con bordes periódicos.
let step (rule: int) (cells: bool[]) =
    let n = cells.Length
    Array.init n (fun i ->
        let bit b = if b then 1 else 0
        let index = 4 * bit cells.[(i + n - 1) % n] + 2 * bit cells.[i] + bit cells.[(i + 1) % n]
        (rule >>> index) &&& 1 = 1)

/// Hash FNV-1a: el mismo nombre siempre da la misma portada.
let hash (text: string) =
    text |> Seq.fold (fun h c -> (h ^^^ uint32 c) * 16777619u) 2166136261u

/// Fila inicial pseudoaleatoria derivada de la semilla (xorshift).
let seedRow width (seed: uint32) =
    let next (x: uint32) =
        let x = x ^^^ (x <<< 13)
        let x = x ^^^ (x >>> 17)
        x ^^^ (x <<< 5)
    Seq.unfold (fun x -> let y = next x in Some((y &&& 3u) = 0u, y)) (seed ||| 1u)
    |> Seq.take width
    |> Seq.toArray

let ruleFor (seed: uint32) = rules.[int (seed % uint32 rules.Length)]

let evolve rule steps (first: bool[]) =
    List.scan (fun row _ -> step rule row) first [ 2..steps ]

/// Celdas vivas como un solo trazo SVG (un elemento por portada en vez de cientos).
let toPath (rows: bool[] list) =
    rows
    |> List.indexed
    |> List.collect (fun (y, row) ->
        row |> Array.toList |> List.indexed |> List.choose (fun (x, alive) -> if alive then Some $"M{x} {y}h1v1h-1z" else None))
    |> String.concat ""
