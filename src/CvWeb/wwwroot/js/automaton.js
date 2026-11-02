// Decorative cellular automaton in the sidebar.
(() => {
    const root = document.documentElement;
    const reducedMotion = matchMedia("(prefers-reduced-motion: reduce)").matches;
    // Franja con un autómata celular elemental que crece hacia arriba y cambia de regla cada tanto.
    const automaton = canvas => {
        const ctx = canvas.getContext("2d");
        const rules = [30, 110, 90, 150, 45, 105];
        const cell = 4;
        let rule = 0, gen = 0, row = [];
        const resize = () => {
            const ratio = devicePixelRatio || 1;
            canvas.width = canvas.clientWidth * ratio;
            canvas.height = canvas.clientHeight * ratio;
            ctx.setTransform(ratio, 0, 0, ratio, 0, 0);
            row = Array.from({ length: Math.ceil(canvas.clientWidth / cell) }, () => Math.random() < .25);
            ctx.clearRect(0, 0, canvas.clientWidth, canvas.clientHeight);
        };
        const next = cells => cells.map((_, i) => {
            const n = cells.length;
            const index = (cells[(i + n - 1) % n] << 2) | (cells[i] << 1) | cells[(i + 1) % n];
            return ((rules[rule] >> index) & 1) === 1;
        });
        const draw = () => {
            const w = canvas.clientWidth, h = canvas.clientHeight;
            if (!w || !h || !canvas.width || !canvas.height) return; // oculto o sin diseño todavía
            const color = getComputedStyle(root).getPropertyValue("--accent").trim();
            // Sube todo una fila (copiando el lienzo sobre sí mismo) y dibuja la nueva generación abajo.
            const shift = cell * (canvas.height / h);
            ctx.globalCompositeOperation = "copy";
            ctx.drawImage(canvas, 0, shift, canvas.width, canvas.height - shift, 0, 0, w, h - cell);
            ctx.globalCompositeOperation = "source-over";
            ctx.clearRect(0, h - cell, w, cell);
            ctx.fillStyle = color;
            row.forEach((alive, x) => { if (alive) ctx.fillRect(x * cell, h - cell, cell - 1, cell - 1); });
            row = next(row);
            if (++gen % 160 === 0) rule = (rule + 1) % rules.length;
        };
        resize();
        new ResizeObserver(resize).observe(canvas);
        if (reducedMotion) { for (let i = 0; i < canvas.clientHeight / cell; i++) draw(); return; }
        let visible = true;
        new IntersectionObserver(([e]) => visible = e.isIntersecting).observe(canvas);
        setInterval(() => { if (visible && !document.hidden) draw(); }, 90);
    };
    const startAutomaton = () => {
        const canvas = document.querySelector(".automaton canvas");
        if (!canvas || canvas.dataset.running) return;
        canvas.dataset.running = "1";
        automaton(canvas);
    };

    new MutationObserver(startAutomaton).observe(document.body, { childList: true, subtree: true });
    startAutomaton();
})();
