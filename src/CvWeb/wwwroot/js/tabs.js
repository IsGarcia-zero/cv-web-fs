// Pestañas desplazables: muestra ‹ › y el desvanecido sólo hacia donde hay más pestañas,
// desplaza con la rueda vertical, mantiene visible la pestaña elegida y le pasa el foco
// cuando se cambió con el teclado.
(() => {
    const behavior = () => matchMedia("(prefers-reduced-motion: reduce)").matches ? "instant" : "smooth";
    const update = list => {
        const wrap = list.parentElement;
        const max = list.scrollWidth - list.clientWidth;
        // Atributos propios (no `class`): Blazor no los conoce y no los pisa al volver a dibujar.
        wrap.toggleAttribute("data-left", list.scrollLeft > 2);
        wrap.toggleAttribute("data-right", list.scrollLeft < max - 2);
    };

    const reveal = (list, behavior) => {
        const tab = list.querySelector('[aria-selected="true"]');
        if (!tab) return;
        // Primero el foco (si se cambió con el teclado): enfocar después cancelaría el desplazamiento suave.
        if (list.contains(document.activeElement) && document.activeElement !== tab) tab.focus({ preventScroll: true });
        const pad = 40; // deja ver un poco de la vecina para indicar que hay más
        const left = tab.offsetLeft - list.offsetLeft - pad;
        const right = tab.offsetLeft - list.offsetLeft + tab.offsetWidth + pad - list.clientWidth;
        // Se espera un cuadro para que Blazor termine de aplicar sus cambios al DOM.
        requestAnimationFrame(() => {
            if (list.scrollLeft > left) list.scrollTo({ left: Math.max(0, left), behavior });
            else if (list.scrollLeft < right) list.scrollTo({ left: right, behavior });
        });
    };

    const setup = list => {
        if (list.dataset.tabsReady) return;
        list.dataset.tabsReady = "1";
        list.addEventListener("scroll", () => update(list), { passive: true });
        list.addEventListener("keydown", e => {
            if (["ArrowLeft", "ArrowRight", "Home", "End"].includes(e.key)) e.preventDefault();
        });
        list.addEventListener("wheel", e => {
            if (Math.abs(e.deltaY) <= Math.abs(e.deltaX) || list.scrollWidth <= list.clientWidth) return;
            const max = list.scrollWidth - list.clientWidth;
            const goingPast = (e.deltaY < 0 && list.scrollLeft <= 0) || (e.deltaY > 0 && list.scrollLeft >= max);
            if (goingPast) return; // en los extremos la rueda vuelve a mover la página
            e.preventDefault();
            list.scrollBy({ left: e.deltaY, behavior: "auto" });
        }, { passive: false });
        new ResizeObserver(() => update(list)).observe(list);
        new MutationObserver(() => { reveal(list, behavior()); update(list); })
            .observe(list, { subtree: true, attributes: true, attributeFilter: ["aria-selected"] });
        reveal(list, "auto");
        update(list);
    };

    document.addEventListener("click", e => {
        const button = e.target.closest(".tabs-btn");
        if (!button) return;
        const list = button.parentElement.querySelector(".tabs");
        list.scrollBy({ left: Number(button.dataset.scroll) * list.clientWidth * .7, behavior: behavior() });
    });

    const scan = () => document.querySelectorAll(".tabs").forEach(setup);
    new MutationObserver(scan).observe(document.body, { childList: true, subtree: true });
    scan();
})();
