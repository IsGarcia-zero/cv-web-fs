module CvWeb.Content.Cases.Physarum

open CvWeb.Domain

let private t = Text.make
let private metric value es en = { Value = value; Caption = t es en }

let value =
    { Key = "physarum"
      TabTitle = t "Robótica" "Robotics"
      Title = t "Navegación bioinspirada: de autómata celular a robot" "Bio-inspired navigation: from cellular automaton to robot"
      Tagline =
        t "Un autómata inspirado en el moho Physarum polycephalum que traza rutas en laberintos, cuevas y mapas reales, y un robot con LiDAR que las sigue."
          "An automaton modeled on the slime mold Physarum polycephalum that finds routes through mazes, caves and real maps, and a LiDAR robot that follows them."
      Metrics =
        [ metric "1000²" "celdas en el mapa de cuevas" "cells in the cave map"
          metric "5,264" "pasos para resolver las catacumbas" "steps to solve the catacombs"
          metric "4000²" "celdas con Vulkan" "cells with Vulkan"
          metric "3 h" "de prueba continua del robot" "of continuous robot testing" ]
      Challenge =
        t "Nuestro trabajo terminal (ESCOM-IPN, 2024, en coautoría) pedía un sistema autónomo que encontrara trayectos en espacios 2D en tiempo real. El modelo de referencia sólo existía como simulación; había que llevarlo a un robot con sensores."
          "Our undergraduate thesis (ESCOM-IPN, 2024, co-authored) called for an autonomous system that finds paths in 2D spaces in real time. The reference model existed only as a simulation; it had to run on a robot with sensors."
      Decisions =
        [ t "Autómata de 9 estados con memoria por celda y vecindad de Moore; un repelente virtual en las esquinas de 90° evita que el agente «se escape»."
            "A 9-state automaton with per-cell memory and a Moore neighborhood; a virtual repellent at 90° corners stops the agent from “leaking” through."
          t "Mapas generados desde imágenes con OpenCV, para usar planos reales."
            "Maps generated from images with OpenCV, so real floor plans can be used."
          t "Robot en Raspberry Pi 4: motores con pigpio, LiDAR y Kinect, telemetría por WebSocket y pruebas con hardware simulado (GoogleTest/GMock)."
            "Raspberry Pi 4 robot: pigpio motors, LiDAR and Kinect, WebSocket telemetry, and tests with mocked hardware (GoogleTest/GMock)."
          t "En 2026 porté el simulador a Vulkan: la simulación corre en compute shaders y sólo se actualiza lo que cambia."
            "In 2026 I ported the simulator to Vulkan: the simulation runs in compute shaders and only changed regions are re-uploaded." ]
      Outcome =
        [ t "Artículo publicado; lo presenté en persona en CANDAR 2024 (Okinawa, Japón)." "Paper published; I presented it in person at CANDAR 2024 (Okinawa, Japan)."
          t "El robot siguió en campo rutas generadas sobre el mapa, con unas 2.5 h de autonomía por batería."
            "In the field, the robot followed routes generated on the map, with about 2.5 h of battery life."
          t "Titulación con mención honorífica." "Graduated with honors." ]
      Role =
        t "Coautor: documento técnico y artículo, código del robot, pruebas de LiDAR/Kinect, servidores WebSocket y el port a Vulkan (~9,900 líneas, en solitario)."
          "Co-author: technical document and paper, robot code, LiDAR/Kinect tests, WebSocket servers, and the Vulkan port (~9,900 lines, solo)."
      Stack = [ "C++17"; "Vulkan/GLSL"; "SFML"; "ImGui"; "OpenCV"; "Raspberry Pi 4"; "LiDAR"; "Node.js"; "GoogleTest" ]
      Visual = LiveSimulation
      Note = None }
