// Easter egg: código Konami (↑↑↓↓←→←→BA) o teclear "life" convierte la página en un Juego
// de la Vida que nace de tu nombre escrito con células. Esc para salir, Espacio pausa,
// arrastrar dibuja células.
(() => {
    const css = "color:#67e8f9;font:600 12px/1.4 monospace";
    console.log("%c  ·█·\n  ··█\n  ███   ¿Curioso? Prueba ↑↑↓↓←→←→BA, o escribe «life».", css);

    const CELL = 6;
    let open = false;

    const play = () => {
        if (open) return;
        open = true;
        const reduced = matchMedia("(prefers-reduced-motion: reduce)").matches;
        const root = getComputedStyle(document.documentElement);
        const accent = root.getPropertyValue("--accent").trim(), bg = root.getPropertyValue("--bg").trim();
        const overlay = document.createElement("div");
        overlay.className = "life-egg";
        overlay.innerHTML = `<canvas></canvas><p>${document.documentElement.lang === "en"
            ? "Game of Life · drag to draw · Space pauses · Esc exits"
            : "Juego de la Vida · arrastra para dibujar · Espacio pausa · Esc sale"}</p>`;
        document.body.append(overlay);
        const canvas = overlay.querySelector("canvas"), ctx = canvas.getContext("2d");
        const W = Math.ceil(innerWidth / CELL), H = Math.ceil(innerHeight / CELL);
        canvas.width = W * CELL; canvas.height = H * CELL;
        let cells = new Uint8Array(W * H), paused = false;

        // El nombre se escribe con células: se rasteriza el texto a la resolución de la rejilla.
        const text = document.createElement("canvas");
        text.width = W; text.height = H;
        const t = text.getContext("2d");
        t.fillStyle = "#fff";
        t.textAlign = "center"; t.textBaseline = "middle";
        t.font = `800 ${Math.min(H / 3, W / 9) | 0}px Inter, sans-serif`;
        t.fillText("EDUARDO", W / 2, H / 2 - H / 7);
        t.font = `700 ${Math.min(H / 6, W / 14) | 0}px Inter, sans-serif`;
        t.fillText("HERNÁNDEZ VERGARA", W / 2, H / 2 + H / 7);
        const pixels = t.getImageData(0, 0, W, H).data;
        for (let i = 0; i < W * H; i++) cells[i] = pixels[i * 4 + 3] > 120 ? 1 : 0;

        const glider = () => {
            const x = Math.random() * (W - 4) | 0, y = Math.random() < .5 ? 1 : H - 4;
            [[1, 0], [2, 1], [0, 2], [1, 2], [2, 2]].forEach(([dx, dy]) => cells[(y + dy) * W + x + dx] = 1);
        };
        const step = () => {
            const next = new Uint8Array(W * H);
            for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
                let n = 0;
                for (let dy = -1; dy <= 1; dy++) for (let dx = -1; dx <= 1; dx++)
                    if ((dx || dy) && cells[((y + dy + H) % H) * W + (x + dx + W) % W]) n++;
                const i = y * W + x;
                next[i] = n === 3 || (n === 2 && cells[i]) ? 1 : 0;
            }
            cells = next;
        };
        const draw = () => {
            ctx.fillStyle = bg; ctx.globalAlpha = .35; ctx.fillRect(0, 0, canvas.width, canvas.height); // estela
            ctx.globalAlpha = 1; ctx.fillStyle = accent;
            for (let i = 0; i < W * H; i++) if (cells[i]) ctx.fillRect((i % W) * CELL, ((i / W) | 0) * CELL, CELL - 1, CELL - 1);
        };

        let frame = 0, holdName = 40; // el nombre se sostiene un momento antes de empezar a vivir
        const timer = setInterval(() => {
            if (!paused && --holdName < 0 && !reduced) {
                step();
                if (++frame % 60 === 0) glider();
            }
            draw();
        }, 70);

        const paint = e => {
            if (!(e.buttons & 1)) return;
            const x = (e.clientX / CELL) | 0, y = (e.clientY / CELL) | 0;
            for (let dy = -1; dy <= 1; dy++) for (let dx = -1; dx <= 1; dx++)
                if (Math.random() < .6) cells[((y + dy + H) % H) * W + (x + dx + W) % W] = 1;
        };
        const close = () => { clearInterval(timer); overlay.remove(); removeEventListener("keydown", keys, true); open = false; };
        const keys = e => {
            if (e.key === "Escape") { e.preventDefault(); close(); }
            else if (e.key === " ") { e.preventDefault(); paused = !paused; }
        };
        canvas.addEventListener("pointerdown", paint);
        canvas.addEventListener("pointermove", paint);
        addEventListener("keydown", keys, true);
    };

    // Detecta el código Konami o la palabra "life" en las últimas teclas.
    const konami = ["ArrowUp", "ArrowUp", "ArrowDown", "ArrowDown", "ArrowLeft", "ArrowRight", "ArrowLeft", "ArrowRight", "b", "a"];
    let recent = [];
    addEventListener("keydown", e => {
        if (open || e.target.closest?.("input, textarea, [contenteditable]")) return;
        recent = [...recent, e.key.length === 1 ? e.key.toLowerCase() : e.key].slice(-konami.length);
        if (konami.every((k, i) => recent[i] === k) || recent.slice(-4).join("") === "life") { recent = []; play(); }
    });
    window.cvLife = play; // también desde la consola: cvLife()
})();
