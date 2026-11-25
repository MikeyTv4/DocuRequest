(function () {
    // Scroll-reveal animation
    const io = new IntersectionObserver((entries) => {
        entries.forEach((e) => {
            if (e.isIntersecting) {
                e.target.classList.add('in');
                io.unobserve(e.target);
            }
        });
    }, { threshold: 0.12 });
    document.querySelectorAll('.reveal').forEach((el) => io.observe(el));

    // Animated counters on the home page
    document.querySelectorAll('.count').forEach((el) => {
        const target = parseInt(el.dataset.to || '0', 10);
        const duration = 1200;
        const start = performance.now();
        const tick = (now) => {
            const p = Math.min((now - start) / duration, 1);
            el.textContent = Math.round(target * (1 - Math.pow(1 - p, 3)));
            if (p < 1) requestAnimationFrame(tick);
        };
        requestAnimationFrame(tick);
    });

    // Copy reference number button (Details page)
    const copyBtn = document.getElementById('copyRef');
    if (copyBtn) {
        copyBtn.addEventListener('click', async () => {
            const text = document.getElementById('refNo').textContent.trim();
            try {
                await navigator.clipboard.writeText(text);
                copyBtn.innerHTML = '<i class="bi bi-check2 me-1"></i> Copied';
                setTimeout(() => (copyBtn.innerHTML = '<i class="bi bi-clipboard me-1"></i> Copy'), 1800);
            } catch { /* clipboard unavailable, ignore */ }
        });
    }
})();
