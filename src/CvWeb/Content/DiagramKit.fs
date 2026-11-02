/// Fábricas para que los diagramas se escriban como datos cortos.
module CvWeb.Content.DiagramKit

open CvWeb.Domain

let box x y w h label detail =
    { BoxX = x; BoxY = y; BoxW = w; BoxH = h; Label = label; Detail = detail }

let zone x y w h name =
    { ZoneX = x; ZoneY = y; ZoneW = w; ZoneH = h; Name = name }

let wire x1 y1 x2 y2 = { X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; Packet = None }

/// Flecha recorrida por un paquete que sale `delay` segundos después de iniciar el ciclo.
let flow x1 y1 x2 y2 delay = { wire x1 y1 x2 y2 with Packet = Some delay }

let t = Text.make
let same = Text.same
let none = Text.same ""
