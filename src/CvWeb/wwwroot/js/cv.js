// Lo que el sitio hace fuera de F#: preferencias, portapapeles, impresión y animaciones de presentación.
(() => {
    const read = key => { try { return localStorage.getItem(key); } catch { return null; } };
    const write = (key, value) => { try { localStorage.setItem(key, value); } catch { } };
    const reducedMotion = matchMedia("(prefers-reduced-motion: reduce)").matches;
    const systemTheme = () => matchMedia("(prefers-color-scheme: light)").matches ? "light" : "dark";
    const root = document.documentElement;

    // Tema antes del primer render para que no parpadee; la primera visita sigue al sistema.
    const applyTheme = value => {
        root.dataset.theme = value;
        const meta = document.querySelector('meta[name="theme-color"]');
        if (meta) meta.content = getComputedStyle(root).getPropertyValue("--bg").trim();
    };
    applyTheme(read("cv-theme") || systemTheme());

    // Transición de vista donde el navegador la soporta: el tema nuevo se expande en círculo
    // desde el último punto donde se presionó (motion.css lee --vt-x / --vt-y).
    addEventListener("pointerdown", e => {
        root.style.setProperty("--vt-x", e.clientX + "px");
        root.style.setProperty("--vt-y", e.clientY + "px");
    }, { passive: true });
    const transition = change =>
        document.startViewTransition && !reducedMotion ? document.startViewTransition(change) : change();

    // `?lang=en` gana a lo guardado: es el enlace que se manda a quien lee en inglés.
    const linkedLang = () => {
        const lang = new URLSearchParams(location.search).get("lang");
        return lang === "es" || lang === "en" ? lang : null;
    };

    window.cvSite = {
        preferredLang: () => linkedLang() || read("cv-lang") || (navigator.language || "es").toLowerCase(),
        saveLang(lang) { root.lang = lang; write("cv-lang", lang); },
        preferredTheme: () => read("cv-theme") || systemTheme(),
        saveTheme(value) {
            if (root.dataset.theme !== value) transition(() => applyTheme(value));
            write("cv-theme", value);
        },
        async copy(value) {
            try { await navigator.clipboard.writeText(value); return true; }
            catch {
                // Respaldo para navegadores o contextos sin Clipboard API.
                const area = Object.assign(document.createElement("textarea"), { value, readOnly: true });
                area.style.cssText = "position:fixed;opacity:0";
                document.body.append(area);
                area.select();
                const ok = document.execCommand("copy");
                area.remove();
                if (!ok) throw new Error("copy failed");
                return true;
            }
        },
        print: () => window.print()
    };

    // Aparición de secciones y conteo de cifras al entrar en pantalla.
    const countUp = el => {
        const target = Number(el.dataset.count);
        if (reducedMotion || "prerendered" in root.dataset || !target || !el.firstChild) return;
        const start = performance.now();
        const tick = now => {
            const t = Math.min((now - start) / 1400, 1);
            // nodeValue y no textContent: el nodo de texto sigue siendo el que Blazor conoce.
            el.firstChild.nodeValue = Math.round((1 - Math.pow(1 - t, 3)) * target);
            if (t < 1) requestAnimationFrame(tick);
        };
        requestAnimationFrame(tick);
    };
    const revealer = new IntersectionObserver(entries => entries.forEach(e => {
        if (!e.isIntersecting) return;
        e.target.classList.add("visible");
        e.target.querySelectorAll("[data-count]").forEach(countUp);
        revealer.unobserve(e.target);
    }), { rootMargin: "0px 0px -8% 0px" }); // sin umbral: hay secciones más altas que la pantalla

    // Marca en la navegación la sección visible (aria-current, también para lectores de pantalla).
    const spy = new IntersectionObserver(entries => entries.forEach(e => {
        if (!e.isIntersecting) return;
        document.querySelectorAll(".side-nav a, .mobile-nav a").forEach(a => {
            const on = a.getAttribute("href") === "#" + e.target.id;
            if (on) a.setAttribute("aria-current", "true"); else a.removeAttribute("aria-current");
        });
        // En el índice de celular, centra la sección actual sin mover la página.
        const list = document.querySelector(".mobile-nav ul");
        const current = list && list.querySelector('[aria-current="true"]');
        if (current && list.offsetParent) {
            const li = current.parentElement;
            list.scrollTo({ left: li.offsetLeft - (list.clientWidth - li.offsetWidth) / 2, behavior: reducedMotion ? "instant" : "smooth" });
        }
    }), { rootMargin: "-45% 0px -50% 0px" });

    const watched = new WeakSet();
    // Blazor ya montó su árbol cuando aparece la barra lateral que él mismo dibuja (no la copia estática).
    let firstShell = null;
    const booted = () => {
        const shell = document.querySelector(".shell");
        if (!firstShell) firstShell = shell;
        return shell !== firstShell;
    };
    const watch = () => document.querySelectorAll(".reveal, .block").forEach(el => {
        if (watched.has(el)) return;
        watched.add(el);
        // Con HTML pre-renderizado, Blazor reemplaza lo que ya se ve: si eso volviera a aparecer
        // con transición parpadearía, así que lo que ya está en pantalla se muestra de inmediato.
        if (el.classList.contains("reveal") && "prerendered" in root.dataset && booted() && el.getBoundingClientRect().top < innerHeight)
            el.classList.add("visible", "instant");
        if (el.classList.contains("reveal")) revealer.observe(el);
        if (el.id) spy.observe(el);
    });

    // Foco de luz que sigue al puntero; sólo con ratón y sin "reducir movimiento".
    if (!reducedMotion && matchMedia("(hover: hover) and (pointer: fine)").matches) {
        let frame = 0;
        addEventListener("pointermove", e => {
            cancelAnimationFrame(frame);
            frame = requestAnimationFrame(() => {
                root.style.setProperty("--x", e.clientX + "px");
                root.style.setProperty("--y", e.clientY + "px");
            });
        }, { passive: true });
    }

    new MutationObserver(watch).observe(document.body, { childList: true, subtree: true });
    root.classList.add("anim");
    watch();
})();
