# cv-web-fs

Mi CV como sitio web bilingüe (español / inglés), escrito en F# con [Bolero](https://fsbolero.io/) sobre Blazor WebAssembly.

**En línea:** https://isgarcia-zero.github.io/

*English: my résumé as a bilingual static site in F# (Bolero, Blazor WebAssembly, Elmish MVU). The short PDF is generated from the same F# content on every build.*

## Qué tiene

- Experiencia, casos de estudio con diagramas de arquitectura animados, proyectos con filtros y un laboratorio de autómatas celulares en vivo (hormiga de Langton, Juego de la Vida, Cerebro de Brian, autómata cíclico, Wireworld y reglas elementales de Wolfram) y la simulación de Physarum de mi tesis.
- Una sección de fundamentos (autómatas, lexemas, jerarquía de Chomsky) con citas IEEE.
- Tema claro y oscuro, contraste WCAG AA, navegación con teclado y respeto a «reducir movimiento».
- HTML pre-renderizado: el contenido se ve antes de que cargue WebAssembly y los buscadores lo leen.
- Un CV corto en PDF, apto para ATS, que se regenera solo cuando cambia el contenido.

## Arquitectura

Elmish MVU con efectos como record de funciones: `update` es pura y los efectos del navegador se inyectan al componer la app.

| Carpeta | Responsabilidad |
| --- | --- |
| `src/CvWeb/Domain` | Tipos del CV (`Text` bilingüe, empleos, casos, diagramas) y el autómata celular elemental que dibuja las portadas. |
| `src/CvWeb/Content` | El contenido como datos F#: perfil, experiencia, proyectos, diagramas y textos de la interfaz. |
| `src/CvWeb/Model`, `Update` | Estado, mensajes, `update` y el record de efectos. |
| `src/CvWeb/Views` | Vistas en Bolero, una por sección; los gráficos son SVG generados desde los datos. |
| `src/CvWeb/Composition` | Efectos reales (JS interop) y arranque. |
| `src/CvWeb/wwwroot` | CSS por tema, JS de animaciones y simulaciones, imágenes y los PDF generados. |
| `build` | Script del CV corto en PDF y publicación para GitHub Pages. |

## Uso

Requiere el SDK de .NET 10.

```bash
dotnet run --project src/CvWeb
```

**CV en PDF.** En Windows, el build ejecuta `build/short-cv.fsx`, que arma un HTML de una columna con el mismo contenido, y Edge sin ventana lo imprime en `wwwroot/pdf`. Sólo corre si cambió algún archivo de `Domain`, `Content` o los scripts.

**Publicar.** `build/publish.ps1` publica en Release, dibuja la página con Edge sin ventana, inserta ese HTML en `index.html` y agrega `.nojekyll`. El resultado queda en `publish/wwwroot`, listo para GitHub Pages.

## Licencia

El código es libre de consultar como referencia. El contenido del CV, la foto y las capturas son personales.
