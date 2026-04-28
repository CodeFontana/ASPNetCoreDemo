const STORAGE_KEY = "theme";

function storedTheme() {
    return localStorage.getItem(STORAGE_KEY);
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
    document.querySelectorAll(".app-theme-picker").forEach(picker => {
        // Mark the active dropdown item, hide the check on the others.
        picker.querySelectorAll("[data-bs-theme-value]").forEach(btn => {
            const isActive = btn.getAttribute("data-bs-theme-value") === value;
            btn.classList.toggle("active", isActive);
            const check = btn.querySelector("[data-theme-active]");
            if (check) {
                check.classList.toggle("opacity-0", !isActive);
            }
        });

        // Update the toggle button's icon + label.
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
    localStorage.setItem(STORAGE_KEY, value);
    applyTheme(value);
    syncAllPickers(value);
}

// Track which buttons we've already bound so onUpdate is idempotent across
// renders and enhanced navigations.
const boundButtons = new WeakSet();

function bindClickHandlers() {
    document.querySelectorAll(".app-theme-picker [data-bs-theme-value]").forEach(btn => {
        if (boundButtons.has(btn)) return;
        boundButtons.add(btn);
        btn.addEventListener("click", () => {
            setTheme(btn.getAttribute("data-bs-theme-value"));
        });
    });
}

let osPreferenceListenerAttached = false;
function ensureOsPreferenceListener() {
    if (osPreferenceListenerAttached) return;
    osPreferenceListenerAttached = true;
    window.matchMedia("(prefers-color-scheme: dark)").addEventListener("change", () => {
        if ((storedTheme() || "auto") === "auto") applyTheme("auto");
    });
}

// -----------------------------------------------------------------------------
// Lifecycle hooks invoked by BlazorPageScript.
// -----------------------------------------------------------------------------

export function onLoad() {
    ensureOsPreferenceListener();
}

export function onUpdate() {
    const value = storedTheme() || "auto";
    applyTheme(value);
    syncAllPickers(value);
    bindClickHandlers();
}

export function onDispose() {
    // Click handlers live on the picker buttons in the DOM; they go away with
    // the elements. The OS-preference listener intentionally remains for the
    // lifetime of the page so background re-resolution keeps working even if a
    // picker remounts.
}
