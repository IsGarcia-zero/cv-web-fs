namespace CvWeb.Domain

/// Caja de un diagrama de arquitectura (coordenadas del viewBox).
type Box =
    { BoxX: int
      BoxY: int
      BoxW: int
      BoxH: int
      Label: Text
      Detail: Text }

/// Zona punteada que agrupa cajas (nube, clúster, dispositivo…).
type Zone =
    { ZoneX: int
      ZoneY: int
      ZoneW: int
      ZoneH: int
      Name: Text }

/// Flecha; si lleva `Packet`, un punto de luz la recorre con ese retraso dentro del ciclo.
type Wire =
    { X1: int
      Y1: int
      X2: int
      Y2: int
      Packet: float option }

type Diagram =
    { Key: string
      Title: Text
      Summary: Text
      Height: int
      Zones: Zone list
      Boxes: Box list
      Wires: Wire list }

type SpanKind =
    | Schooling
    | Lab
    | Employment

/// Periodo de la trayectoria, en años fraccionarios (2025.5 = julio de 2025).
type Span =
    { What: Text
      Where: string
      From: float
      Until: float
      Kind: SpanKind }

type Milestone = { Event: Text; At: float }

type Timeline =
    { Start: int
      Finish: int
      Spans: Span list
      Milestones: Milestone list }

/// Autómata del laboratorio; `Kind` es la clave que entiende `wwwroot/js/lab.js`.
type AutomatonInfo =
    { Kind: string
      Title: Text
      Author: string
      About: Text
      Hint: Text }

/// Commits atribuidos al lenguaje principal de cada repositorio.
type LanguageShare = { Language: string; Commits: int }
