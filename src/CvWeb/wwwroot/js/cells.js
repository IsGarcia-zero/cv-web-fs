// Al presionar cualquier control brota una explosión de células: el autómata "Seeds" (B2/S),
// donde una célula nace con exactamente 2 vecinas y ninguna sobrevive. Unas cuantas semillas
// junto al puntero crecen y se desvanecen en medio segundo.
(() => {
    if (matchMedia("(prefers-reduced-motion: reduce)").matches) return;
    const N = 30, CELL = 4, GENERATIONS = 9;
    const targets = "button, a, [role=tab], .chip, .tag, .row";

    const burst = (clientX, clientY) => {
        const canvas = Object.assign(document.createElement("canvas"), { className: "cell-burst", width: N, height: N });
        canvas.style.left = clientX - (N * CELL) / 2 + "px";
        canvas.style.top = clientY - (N * CELL) / 2 + "px";
        document.body.append(canvas);
        const ctx = canvas.getContext("2d");
        const color = getComputedStyle(document.documentElement).getPropertyValue("--accent").trim();
        let cells = new Uint8Array(N * N);
        for (let k = 0; k < 4; k++) cells[(N / 2 - 1 + (Math.random() * 3 | 0)) * N + (N / 2 - 1 + (Math.random() * 3 | 0))] = 1;
        let gen = 0;
        const tick = () => {
            ctx.clearRect(0, 0, N, N);
            ctx.globalAlpha = 1 - gen / GENERATIONS;
            ctx.fillStyle = color;
            for (let i = 0; i < N * N; i++) if (cells[i]) ctx.fillRect(i % N, (i / N) | 0, 1, 1);
            const next = new Uint8Array(N * N);
            for (let y = 1; y < N - 1; y++) for (let x = 1; x < N - 1; x++) {
                let n = 0;
                for (let dy = -1; dy <= 1; dy++) for (let dx = -1; dx <= 1; dx++) if ((dx || dy) && cells[(y + dy) * N + x + dx]) n++;
                next[y * N + x] = n === 2 ? 1 : 0;
            }
            cells = next;
            if (++gen <= GENERATIONS) setTimeout(tick, 50);
            else canvas.remove();
        };
        tick();
    };

    addEventListener("pointerdown", e => {
        if (e.button !== 0 || !e.target.closest(targets) || e.target.closest(".lab-stage")) return;
        burst(e.clientX, e.clientY);
    }, { passive: true });
})();
