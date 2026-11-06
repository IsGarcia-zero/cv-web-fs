// La selección de texto se vuelve un autómata celular: en cada renglón seleccionado corre la regla 30
// (la de Fundamentos), con la generación nueva entrando por abajo. El resaltado nativo (::selection)
// sigue debajo; el lienzo encima sólo oscurece las células vivas en el tema oscuro, o las aclara en el
// claro, así que el texto, que ya es el tono extremo, no pierde contraste. Con "reducir movimiento"
// el patrón queda quieto.
(() => {
    // MIX: cuánto se corren las células hacia el color del texto. Texto oscuro sobre cian aguanta más;
    // texto blanco sobre terracota necesita poco para seguir en AA (≥ 4.5:1 en las células: 6.4 y 4.7).
    const CELL = 4, RULE = 30, STEP = 120, MIX_DARK_TEXT = 0.32, MIX_LIGHT_TEXT = 0.18;
    const reducedMotion = matchMedia("(prefers-reduced-motion: reduce)").matches;
    const canvas = Object.assign(document.createElement("canvas"), { className: "selection-cells" });
    canvas.setAttribute("aria-hidden", "true");
    const ctx = canvas.getContext("2d");
    let bands = [], timer = 0, frame = 0;

    // Color de las células: el acento corrido un poco hacia el color del texto seleccionado.
    const rgb = hex => [1, 3, 5].map(i => parseInt(hex.trim().slice(i, i + 2), 16));
    const paint = () => {
        const css = getComputedStyle(document.documentElement);
        const accent = rgb(css.getPropertyValue("--accent")), text = rgb(css.getPropertyValue("--on-accent"));
        const textIsDark = text[0] * 0.299 + text[1] * 0.587 + text[2] * 0.114 < 128;
        const mix = textIsDark ? MIX_DARK_TEXT : MIX_LIGHT_TEXT;
        const [r, g, b] = accent.map((c, i) => Math.round(c + (text[i] - c) * mix));
        canvas.style.mixBlendMode = textIsDark ? "darken" : "lighten";
        ctx.fillStyle = `rgb(${r} ${g} ${b})`;
    };

    const nextRow = row => row.map((c, i) => {
        const l = row[(i - 1 + row.length) % row.length], r = row[(i + 1) % row.length];
        return (RULE >> ((l << 2) | (c << 1) | r)) & 1;
    });

    // Un renglón nuevo empieza al azar y ya lleno de generaciones, para que no aparezca vacío.
    const band = rect => {
        const cols = Math.floor(rect.width / CELL), rows = Math.floor(rect.height / CELL);
        const history = [Array.from({ length: cols }, () => (Math.random() < 0.5 ? 1 : 0))];
        while (history.length < rows) history.push(nextRow(history[history.length - 1]));
        return { rect, cols, history };
    };

    const draw = () => {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        for (const { rect, cols, history } of bands) {
            const x0 = rect.left + (rect.width - cols * CELL) / 2;
            const y0 = rect.top + (rect.height - history.length * CELL) / 2;
            history.forEach((row, y) => row.forEach((alive, x) => {
                if (alive) ctx.fillRect(x0 + x * CELL, y0 + y * CELL, CELL, CELL);
            }));
        }
    };

    const step = () => {
        for (const b of bands) b.history = [...b.history.slice(1), nextRow(b.history[b.history.length - 1])];
        draw();
    };

    // Relee los renglones; uno del mismo tamaño en la misma posición de la lista conserva su historia,
    // así que extender la selección o desplazar la página no reinicia el patrón.
    const refresh = () => {
        frame = 0;
        const sel = getSelection();
        const rects = [];
        if (sel && !sel.isCollapsed)
            for (let i = 0; i < sel.rangeCount; i++)
                for (const r of sel.getRangeAt(i).getClientRects())
                    if (r.width >= CELL && r.height >= CELL) rects.push(r);
        bands = rects.map((r, i) => {
            const old = bands[i];
            return old && old.rect.width === r.width && old.rect.height === r.height ? { ...old, rect: r } : band(r);
        });
        if (!bands.length) {
            clearInterval(timer);
            timer = 0;
            canvas.remove();
            return;
        }
        if (!canvas.isConnected) {
            resize();
            document.body.append(canvas);
        }
        paint();
        draw();
        if (!reducedMotion && !timer) timer = setInterval(step, STEP);
    };

    const resize = () => {
        const dpr = devicePixelRatio || 1;
        canvas.width = innerWidth * dpr;
        canvas.height = innerHeight * dpr;
        ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
        paint();
    };

    const schedule = () => { if (!frame) frame = requestAnimationFrame(refresh); };
    document.addEventListener("selectionchange", schedule);
    addEventListener("scroll", () => bands.length && schedule(), { passive: true, capture: true });
    addEventListener("resize", () => { if (bands.length) { resize(); schedule(); } });
    // Cambiar de tema con texto seleccionado recalcula el color y la mezcla.
    new MutationObserver(() => { if (bands.length) { paint(); draw(); } })
        .observe(document.documentElement, { attributes: true, attributeFilter: ["data-theme"] });
})();
