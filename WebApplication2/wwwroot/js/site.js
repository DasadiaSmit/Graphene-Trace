/* ===========================================================
   GrapheneTrace - Global JavaScript
   =========================================================== */

/*
  This file is automatically loaded in _Layout.cshtml.
  It enhances UX with validation highlighting, prevents double submissions,
  and enables simple fade transitions between pages.
*/

// ✅ Wait until DOM is fully loaded
document.addEventListener("DOMContentLoaded", function () {

    // ---------- Smooth Page Fade Animation ----------
    document.body.classList.add("fade-in");

    // ---------- Disable Double Form Submit ----------
    const forms = document.querySelectorAll("form");
    forms.forEach(form => {
        form.addEventListener("submit", function () {
            const submitButtons = form.querySelectorAll("button[type='submit']");
            submitButtons.forEach(btn => {
                btn.disabled = true;
                btn.innerHTML = "Please wait...";
            });
        });
    });

    // ---------- Real-time Validation Feedback ----------
    const inputs = document.querySelectorAll(".form-control");
    inputs.forEach(input => {
        input.addEventListener("invalid", () => {
            input.classList.add("is-invalid");
        });
        input.addEventListener("input", () => {
            if (input.checkValidity()) {
                input.classList.remove("is-invalid");
                input.classList.add("is-valid");
            } else {
                input.classList.remove("is-valid");
            }
        });
    });
});

// ---------- Optional Fade-out Transition ----------
window.addEventListener("beforeunload", () => {
    document.body.classList.add("fade-out");
});
