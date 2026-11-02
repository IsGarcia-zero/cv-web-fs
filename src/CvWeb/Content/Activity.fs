/// Commits propios por mes en todos los repositorios git (Windows, WSL y GitHub), sin duplicados por hash.
/// Generado con un script a partir de `git log`; antes de 2020 el código vivía en zips.
module CvWeb.Content.Activity

open CvWeb.Domain

let value =
    { Commits = 524
      Repositories = 80
      Since = Text.make "febrero de 2020" "February 2020"
      Years =
        [ 2020, [ 0; 1; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0 ]
          2021, [ 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 29; 0 ]
          2022, [ 0; 0; 3; 0; 0; 2; 21; 4; 0; 28; 14; 4 ]
          2023, [ 3; 0; 0; 0; 3; 17; 0; 0; 3; 5; 8; 9 ]
          2024, [ 4; 0; 2; 2; 4; 0; 0; 0; 6; 13; 8; 0 ]
          2025, [ 2; 0; 0; 0; 0; 0; 2; 17; 41; 28; 18; 15 ]
          2026, [ 23; 17; 9; 30; 23; 15; 8; 44; 22; 17; 0; 0 ] ] }
