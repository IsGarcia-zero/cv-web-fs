// Laboratorio de autómatas: F# decide cuál corre (data-kind), si está en pausa (data-running)
// y cuándo reiniciar (data-seed); aquí sólo se simula y se dibuja en una rejilla de W×H.
(() => {
    const W = 160, H = 80;
    const reducedMotion = matchMedia("(prefers-reduced-motion: reduce)").matches;
    const at = (x, y) => ((y + H) % H) * W + ((x + W) % W);
    const neighbors8 = (cells, x, y, state) => {
        let n = 0;
        for (let dy = -1; dy <= 1; dy++) for (let dx = -1; dx <= 1; dx++)
            if ((dx || dy) && cells[at(x + dx, y + dy)] === state) n++;
        return n;
    };
    const random = (density, states = 1) => Uint8Array.from({ length: W * H }, () => Math.random() < density ? 1 + (Math.random() * states | 0) : 0);

    // ── Juego de la Vida, con estela que se desvanece ──
    const life = () => {
        let cells = random(.22), trail = new Float32Array(W * H), stale = 0, last = 0;
        return {
            step() {
                const next = new Uint8Array(W * H);
                let population = 0;
                for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
                    const i = at(x, y), n = neighbors8(cells, x, y, 1);
                    next[i] = n === 3 || (n === 2 && cells[i]) ? 1 : 0;
                    trail[i] = next[i] ? 1 : trail[i] * .82;
                    population += next[i];
                }
                cells = next;
                stale = population === last ? stale + 1 : 0;
                last = population;
                if (stale > 40) { cells = random(.22); stale = 0; }
            },
            paint: (px, c) => { for (let i = 0; i < W * H; i++) px(i, c.accent, trail[i]); },
            click(x, y) { for (let k = 0; k < 40; k++) cells[at(x + (Math.random() * 9 | 0) - 4, y + (Math.random() * 9 | 0) - 4)] = 1; }
        };
    };

    // ── Hormigas de Langton: blanco → derecha, negro → izquierda, voltear, avanzar ──
    const ants = () => {
        const grid = new Uint8Array(W * H), owner = new Uint8Array(W * H);
        const dirs = [[0, -1], [1, 0], [0, 1], [-1, 0]];
        const list = [[W * .35, H * .5], [W * .5, H * .4], [W * .65, H * .6]].map(([x, y], c) => ({ x: x | 0, y: y | 0, d: c, c }));
        return {
            steps: 45,
            step() {
                for (const a of list) {
                    const i = at(a.x, a.y);
                    a.d = (a.d + (grid[i] ? 3 : 1)) % 4;
                    grid[i] ^= 1;
                    owner[i] = a.c;
                    a.x = (a.x + dirs[a.d][0] + W) % W;
                    a.y = (a.y + dirs[a.d][1] + H) % H;
                }
            },
            paint(px, c) {
                const colors = [c.accent, c.accent2, c.accent3];
                for (let i = 0; i < W * H; i++) px(i, colors[owner[i]], grid[i] ? .9 : 0);
                for (const a of list) px(at(a.x, a.y), c.ink, 1);
            },
            click(x, y) { if (list.length < 12) list.push({ x, y, d: 0, c: list.length % 3 }); }
        };
    };

    // ── Cerebro de Brian: apagada → encendida con 2 vecinas; encendida → reposo → apagada ──
    const brain = () => {
        let cells = random(.12);
        return {
            step() {
                const next = new Uint8Array(W * H);
                for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
                    const i = at(x, y), s = cells[i];
                    next[i] = s === 1 ? 2 : s === 2 ? 0 : neighbors8(cells, x, y, 1) === 2 ? 1 : 0;
                }
                cells = next;
                let alive = 0;
                for (let i = 0; i < W * H; i++) alive += cells[i] === 1;
                if (alive < 30) cells = random(.12);
            },
            paint(px, c) { for (let i = 0; i < W * H; i++) px(i, cells[i] === 1 ? c.accent : c.accent2, cells[i] === 1 ? 1 : cells[i] === 2 ? .45 : 0); },
            click(x, y) { for (let k = 0; k < 30; k++) cells[at(x + (Math.random() * 7 | 0) - 3, y + (Math.random() * 7 | 0) - 3)] = 1; }
        };
    };

    // ── Autómata cíclico (vecindad de von Neumann, umbral 1): del ruido salen espirales ──
    const cyclic = () => {
        const N = 12;
        let cells = Uint8Array.from({ length: W * H }, () => Math.random() * N | 0);
        return {
            step() {
                const next = cells.slice();
                for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
                    const i = at(x, y), want = (cells[i] + 1) % N;
                    if (cells[at(x + 1, y)] === want || cells[at(x - 1, y)] === want || cells[at(x, y + 1)] === want || cells[at(x, y - 1)] === want) next[i] = want;
                }
                cells = next;
            },
            paint(px, c) {
                const ramp = [c.accent, c.accent2, c.accent3];
                for (let i = 0; i < W * H; i++) {
                    const t = cells[i] / N * ramp.length;
                    px(i, ramp[t | 0], .35 + .65 * (t % 1));
                }
            },
            click(x, y) { for (let k = 0; k < 60; k++) cells[at(x + (Math.random() * 11 | 0) - 5, y + (Math.random() * 11 | 0) - 5)] = Math.random() * N | 0; }
        };
    };

    // ── Wireworld: cabeza → cola → cable; un cable se vuelve cabeza con 1 o 2 cabezas vecinas ──
    const wireworld = () => {
        const EMPTY = 0, HEAD = 1, TAIL = 2, WIRE = 3;
        let cells = new Uint8Array(W * H);
        const loop = (x0, y0, w, h, wireTo) => {
            for (let x = x0; x < x0 + w; x++) { cells[at(x, y0)] = WIRE; cells[at(x, y0 + h - 1)] = WIRE; }
            for (let y = y0; y < y0 + h; y++) { cells[at(x0, y)] = WIRE; cells[at(x0 + w - 1, y)] = WIRE; }
            cells[at(x0 + 2, y0)] = HEAD;
            cells[at(x0 + 1, y0)] = TAIL;
            // Los circuitos grandes llevan un segundo electrón en el lado opuesto.
            if (w + h > 28) { cells[at(x0 + w - 3, y0 + h - 1)] = HEAD; cells[at(x0 + w - 2, y0 + h - 1)] = TAIL; }
            const ym = y0 + (h >> 1);
            for (let x = x0 + w; x < wireTo; x++) cells[at(x, ym)] = WIRE;
            return ym;
        };
        // Relojes de distintos periodos que alimentan un bus vertical.
        const outs = [[6, 6, 14, 8], [6, 30, 22, 10], [6, 56, 10, 14], [40, 14, 18, 18], [40, 50, 12, 12]].map(([x, y, w, h]) => loop(x, y, w, h, 120));
        for (let y = Math.min(...outs); y <= Math.max(...outs); y++) cells[at(120, y)] = WIRE;
        for (let x = 121; x < W - 4; x++) { cells[at(x, 40)] = WIRE; cells[at(x, 20)] = WIRE; cells[at(x, 60)] = WIRE; }
        return {
            step() {
                const next = cells.slice();
                for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
                    const i = at(x, y), s = cells[i];
                    if (s === HEAD) next[i] = TAIL;
                    else if (s === TAIL) next[i] = WIRE;
                    else if (s === WIRE) { const n = neighbors8(cells, x, y, HEAD); if (n === 1 || n === 2) next[i] = HEAD; }
                }
                cells = next;
            },
            paint(px, c) {
                for (let i = 0; i < W * H; i++) {
                    const s = cells[i];
                    px(i, s === HEAD ? c.accent3 : s === TAIL ? c.accent : c.ink3, s === EMPTY ? 0 : s === WIRE ? .35 : 1);
                }
            },
            click(x, y) { for (let dy = -2; dy <= 2; dy++) for (let dx = -2; dx <= 2; dx++) if (cells[at(x + dx, y + dy)] === WIRE) { cells[at(x + dx, y + dy)] = HEAD; return; } }
        };
    };

    // ── Elementales de Wolfram: la historia baja como una cascada; clic cambia de regla ──
    const elementary = () => {
        const rules = [30, 90, 110, 184];
        let r = 0, rows = new Uint8Array(W * H), row;
        const seed = () => {
            row = rules[r] === 90 ? Uint8Array.from({ length: W }, (_, i) => i === W >> 1) : Uint8Array.from({ length: W }, () => Math.random() < (rules[r] === 184 ? .45 : .3));
            rows.fill(0);
        };
        seed();
        return {
            step() {
                rows.copyWithin(0, W);
                rows.set(row, (H - 1) * W);
                row = row.map((_, i) => (rules[r] >> ((row[(i + W - 1) % W] << 2) | (row[i] << 1) | row[(i + 1) % W])) & 1);
            },
            paint: (px, c) => { for (let i = 0; i < W * H; i++) px(i, [c.accent, c.accent2, c.accent3, c.accent][r], rows[i] ? .9 : 0); },
            click() { r = (r + 1) % rules.length; seed(); }
        };
    };

    const kinds = { life, ant: ants, brain, cyclic, wireworld, eca: elementary };

    const run = canvas => {
        const ctx = canvas.getContext("2d");
        const off = Object.assign(document.createElement("canvas"), { width: W, height: H });
        const offCtx = off.getContext("2d"), image = offCtx.createImageData(W, H);
        const rgb = name => {
            offCtx.fillStyle = getComputedStyle(document.documentElement).getPropertyValue(name).trim() || "#888";
            const hex = offCtx.fillStyle;
            return [1, 3, 5].map(i => parseInt(hex.slice(i, i + 2), 16));
        };
        let sim, key = "";
        const draw = () => {
            const c = { accent: rgb("--accent"), accent2: rgb("--accent-2"), accent3: rgb("--accent-3"), ink: rgb("--ink"), ink3: rgb("--ink-3") };
            image.data.fill(0);
            sim.paint((i, [r, g, b], a) => { const p = i * 4; image.data[p] = r; image.data[p + 1] = g; image.data[p + 2] = b; image.data[p + 3] = a * 255; }, c);
            offCtx.putImageData(image, 0, 0);
            const ratio = devicePixelRatio || 1;
            if (canvas.width !== canvas.clientWidth * ratio) { canvas.width = canvas.clientWidth * ratio; canvas.height = canvas.clientHeight * ratio; }
            ctx.imageSmoothingEnabled = false;
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            ctx.drawImage(off, 0, 0, canvas.width, canvas.height);
        };
        const sync = () => {
            const next = canvas.dataset.kind + "|" + canvas.dataset.seed;
            if (next === key) return;
            key = next;
            sim = (kinds[canvas.dataset.kind] || life)();
            if (reducedMotion) for (let i = 0; i < 120 * (sim.steps || 1); i++) sim.step();
            draw();
        };
        canvas.addEventListener("click", e => {
            const r = canvas.getBoundingClientRect();
            sim.click(((e.clientX - r.left) / r.width * W) | 0, ((e.clientY - r.top) / r.height * H) | 0);
            draw();
        });
        let visible = false;
        new IntersectionObserver(([e]) => visible = e.isIntersecting).observe(canvas);
        sync();
        const timer = setInterval(() => {
            if (!canvas.isConnected) return clearInterval(timer);
            sync();
            if (!visible || document.hidden || canvas.dataset.running !== "true" || reducedMotion) return;
            for (let i = 0; i < (sim.steps || 1); i++) sim.step();
            draw();
        }, 70);
    };

    const start = () => document.querySelectorAll(".lab canvas:not([data-started])").forEach(c => { c.dataset.started = "1"; run(c); });
    new MutationObserver(start).observe(document.body, { childList: true, subtree: true });
    start();
})();
