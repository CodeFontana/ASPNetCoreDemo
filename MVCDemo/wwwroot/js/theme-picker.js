// =============================================================================
//  ThemePicker — owns all theme-switching behavior for the app.
// =============================================================================
//  - Reads the user's preference from localStorage and resolves "auto" against
//    the OS color-scheme (prefers-color-scheme).
//  - Writes [data-bs-theme] on <html> so Bootstrap 5.3's CSS variables flip.
//  - Keeps every ThemePicker on the page in sync (icon, label, active item).
//  - Binds click handlers on the dropdown items.
//  - Listens for OS preference changes (and re-resolves "auto" when they flip).
//
//  This script must be referenced from <head> WITHOUT defer/async so the
//  initial applyTheme() call runs before <body> paints — that's how we avoid
//  FOUC on each full-page navigation in this server-rendered stack.
//  (The DOM-touching parts wait for DOMContentLoaded.)
// =============================================================================
(function () {
    "use strict";

    const STORAGE_KEY = "theme";

    function storedTheme() {
        try { return localStorage.getItem(STORAGE_KEY); } catch (e) { return null; }
    }

    function preferredTheme() {
        return window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light";
    }

    function resolveTheme(value) {
        return value === "auto" ? preferredTheme() : value;
    }

    function applyTheme(value) {
        document.documentElement.setAttribute("data-bs-theme", resolveTheme(value));
    }

    function syncAllPickers(value) {
        document.querySelectorAll(".app-theme-picker").forEach(function (picker) {
            picker.querySelectorAll("[data-bs-theme-value]").forEach(function (btn) {
                const isActive = btn.getAttribute("data-bs-theme-value") === value;
                btn.classList.toggle("active", isActive);
                const check = btn.querySelector("[data-theme-active]");
                if (check) {
                    check.classList.toggle("opacity-0", !isActive);
                }
            });

            const icon = picker.querySelector("[data-theme-icon]");
            const label = picker.querySelector("[data-theme-label]");
            if (icon) {
                icon.className = "fs-5 bi " + (
                    value === "dark" ? "bi-moon-stars-fill" :
                    value === "light" ? "bi-sun-fill" :
                    "bi-circle-half"
                );
            }
            if (label) {
                label.textContent =
                    value === "dark" ? "Dark" :
                    value === "light" ? "Light" :
                    "Auto";
            }
        });
    }

    function setTheme(value) {
        try { localStorage.setItem(STORAGE_KEY, value); } catch (e) { /* ignore */ }
        applyTheme(value);
        syncAllPickers(value);
    }

    // -------------------------------------------------------------------------
    // 1) Apply theme synchronously, before paint, to avoid FOUC.
    // -------------------------------------------------------------------------
    applyTheme(storedTheme() || "auto");

    // -------------------------------------------------------------------------
    // 2) Once the DOM is parsed, sync picker chrome and bind click handlers.
    // -------------------------------------------------------------------------
    function onReady() {
        const value = storedTheme() || "auto";
        syncAllPickers(value);
        document.querySelectorAll(".app-theme-picker [data-bs-theme-value]").forEach(function (btn) {
            btn.addEventListener("click", function () {
                setTheme(btn.getAttribute("data-bs-theme-value"));
            });
        });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", onReady);
    } else {
        onReady();
    }

    // -------------------------------------------------------------------------
    // 3) Re-resolve "auto" whenever the OS color-scheme preference flips.
    // -------------------------------------------------------------------------
    window.matchMedia("(prefers-color-scheme: dark)").addEventListener("change", function () {
        if ((storedTheme() || "auto") === "auto") applyTheme("auto");
    });
})();
