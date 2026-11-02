// Physarum polycephalum por agentes (modelo de Jeff Jones, 2010): cada agente sensa el rastro
// delante, a la izquierda y a la derecha, gira hacia el más fuerte, avanza y deposita.
// Unas "fuentes de comida" fijas depositan siempre, y la red crece para unirlas.
(() => {
    const W = 200, H = 100, AGENTS = 4000;
    const SENSOR_ANGLE = Math.PI / 4, TURN = Math.PI / 4, SENSOR_DIST = 9, DEPOSIT = 5, DECAY = 0.9, DIFFUSE = 0.35;
    const reducedMotion = matchMedia("(prefers-reduced-motion: reduce)").matches;

    const simulate = canvas => {
        const trail = new Float32Array(W * H), next = new Float32Array(W * H);
        // Agentes repartidos por todo el lienzo: así la red se forma en vez de una sola mancha.
        const agents = Array.from({ length: AGENTS }, () => ({
            x: Math.random() * W, y: Math.random() * H, a: Math.random() * Math.PI * 2
        }));
        const food = Array.from({ length: 6 }, () => [4 + Math.random() * (W - 8) | 0, 4 + Math.random() * (H - 8) | 0]);
        const at = (x, y) => trail[((y + H) % H | 0) * W + ((x + W) % W | 0)];

        const step = () => {
            for (const g of agents) {
                const sense = da => at(g.x + Math.cos(g.a + da) * SENSOR_DIST, g.y + Math.sin(g.a + da) * SENSOR_DIST);
                const f = sense(0), l = sense(-SENSOR_ANGLE), r = sense(SENSOR_ANGLE);
                if (f < l && f < r) g.a += (Math.random() < .5 ? -1 : 1) * TURN;
                else if (l > r) g.a -= TURN;
                else if (r > l) g.a += TURN;
                g.x = (g.x + Math.cos(g.a) + W) % W;
                g.y = (g.y + Math.sin(g.a) + H) % H;
                trail[(g.y | 0) * W + (g.x | 0)] += DEPOSIT;
            }
            for (const [fx, fy] of food) trail[fy * W + fx] += 25;
            // Difusión parcial 3×3 (mezcla con el valor propio para conservar venas finas) y evaporación.
            for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
                let sum = 0;
                for (let dy = -1; dy <= 1; dy++) for (let dx = -1; dx <= 1; dx++) sum += at(x + dx, y + dy);
                const i = y * W + x;
                next[i] = (trail[i] * (1 - DIFFUSE) + (sum / 9) * DIFFUSE) * DECAY;
            }
            trail.set(next);
        };

        const ctx = canvas.getContext("2d");
        const off = Object.assign(document.createElement("canvas"), { width: W, height: H });
        const offCtx = off.getContext("2d");
        const image = offCtx.createImageData(W, H);
        const rgb = () => {
            offCtx.fillStyle = getComputedStyle(document.documentElement).getPropertyValue("--accent-3").trim() || "#6ee7b7";
            const hex = offCtx.fillStyle;
            return [1, 3, 5].map(i => parseInt(hex.slice(i, i + 2), 16));
        };
        const draw = () => {
            if (!canvas.clientWidth) return;
            const [r, g, b] = rgb();
            for (let i = 0; i < W * H; i++) {
                const p = i * 4;
                image.data[p] = r; image.data[p + 1] = g; image.data[p + 2] = b;
                image.data[p + 3] = Math.min(255, Math.sqrt(trail[i]) * 60);
            }
            offCtx.putImageData(image, 0, 0);
            canvas.width = canvas.clientWidth * (devicePixelRatio || 1);
            canvas.height = canvas.clientHeight * (devicePixelRatio || 1);
            ctx.imageSmoothingEnabled = true;
            ctx.drawImage(off, 0, 0, canvas.width, canvas.height);
        };

        if (reducedMotion) { for (let i = 0; i < 300; i++) step(); draw(); return; }
        let visible = false;
        new IntersectionObserver(([e]) => visible = e.isIntersecting).observe(canvas);
        // Si Blazor quita la tarjeta (por ejemplo, al filtrar), la simulación se detiene.
        const timer = setInterval(() => {
            if (!canvas.isConnected) return clearInterval(timer);
            if (visible && !document.hidden) { step(); draw(); }
        }, 50);
        for (let i = 0; i < 120; i++) step();
        draw();
    };

    const start = () => document.querySelectorAll(".physarum canvas:not([data-running])").forEach(c => {
        c.dataset.running = "1";
        simulate(c);
    });
    new MutationObserver(start).observe(document.body, { childList: true, subtree: true });
    start();
})();
