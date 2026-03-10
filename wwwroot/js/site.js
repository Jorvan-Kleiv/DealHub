// Write your JavaScript code.
// Notifications ui
const btnToggleNotif = document.getElementById('btnToggleNotif');
const notifPanel = document.getElementById('notifPanel');
const notifBadge = document.getElementById('notifBadge');

btnToggleNotif.addEventListener('click', (e) => {
    e.stopPropagation();
    notifPanel.classList.toggle('hidden');
});

document.addEventListener('click', (e) => {
    if (!notifPanel.contains(e.target) && !btnToggleNotif.contains(e.target)) {
        notifPanel.classList.add('hidden');
    }
});
function markNotifsRead() {
    notifBadge.style.display = 'none';
    document.getElementById('notif1').classList.remove('opacity-100');
    document.getElementById('notif1').classList.add('opacity-60');
    setTimeout(() => { notifPanel.classList.add('hidden'); }, 300);
}

window.addEventListener('click', function (event) {
    const dropdown = document.getElementById('user-dropdown');
    const btn = document.getElementById('user-menu-btn');
    if (!btn.contains(event.target) && !dropdown.classList.contains('hidden')) {
        dropdown.classList.add('hidden');
    }
});

function toggleSidebar() {
    const sb = document.getElementById('sidebar');
    const ov = document.getElementById('mob-overlay');
    if (window.innerWidth < 768) {
        sb.classList.toggle('mob-open');
        ov.classList.toggle('show', sb.classList.contains('mob-open'));
    } else {
        sb.classList.toggle('col');
    }
}

function closeMobile() {
    document.getElementById('sidebar').classList.remove('mob-open');
    document.getElementById('mob-overlay').classList.remove('show');
}

function toggleRole(el) {
    const classes = [...el.classList];
    const type = classes.find(c => c.startsWith('rt-') && !c.endsWith('-on') && !c.endsWith('-off'))?.replace('rt-', '');
    if (!type) return;
    const isOn = el.classList.contains(`rt-${type}-on`);
    el.classList.toggle(`rt-${type}-on`, !isOn);
    el.classList.toggle(`rt-${type}-off`, isOn);
    const labels = { admin: 'Admin', modo: 'Modérateur', march: 'Marchand', user: 'Utilisateur' };
    showToast(isOn ? `Rôle "${labels[type]}" retiré` : `Rôle "${labels[type]}" ajouté`, isOn ? 'orange' : 'green');
}

function filterUsers(q = '') {
    const term = q.toLowerCase();
    document.querySelectorAll('#users-tbody tr').forEach(row => {
        const match = !term || (row.dataset.name || '').toLowerCase().includes(term) || (row.dataset.email || '').toLowerCase().includes(term);
        row.style.display = match ? '' : 'none';
    });
}

function openModal(id, label) {
    if (label) {
        const el = document.getElementById(id === 'modal-warn-user' ? 'warn-name' : 'ban-name');
        if (el) el.textContent = label;
    }
    document.getElementById(id).classList.remove('hidden');
}

function closeModal(id) { document.getElementById(id).classList.add('hidden'); }

document.addEventListener('keydown', e => {
    if (e.key === 'Escape')
        document.querySelectorAll('.modal-wrap:not(.hidden)').forEach(m => m.classList.add('hidden'));
});

function selectDuree(btn) {
    document.querySelectorAll('#duree-btns button').forEach(b => b.style.cssText = '');
    btn.style.background = '#1c1917';
    btn.style.color = '#fff';
    btn.style.borderColor = '#1c1917';
}

function checkDel(input) {
    const btn = document.getElementById('btn-del');
    if (btn) btn.disabled = input.value !== 'SUPPRIMER';
}

function showToast(msg, color) {
    const t = document.getElementById('toast');
    const ic = document.getElementById('toast-icon');
    const m = document.getElementById('toast-msg');
    const cfg = {
        green: { icon: 'solar:check-circle-bold', color: '#34d399' },
        red: { icon: 'solar:close-circle-bold', color: '#f87171' },
        orange: { icon: 'solar:danger-triangle-bold', color: '#fb923c' },
    };
    const c = cfg[color] || cfg.green;
    ic.setAttribute('icon', c.icon);
    ic.style.color = c.color;
    m.textContent = msg;
    t.classList.add('show');
    clearTimeout(t._t);
    t._t = setTimeout(() => t.classList.remove('show'), 3000);
}
// ── Aperçu image ───────────────────────────────────────────────────────
const imgInput = document.getElementById("imageUrlInput");
const imgPreview = document.getElementById("imgPreview");
const imgSrc = document.getElementById("imgPreviewSrc");

function updatePreview() {
    const url = imgInput.value.trim();
    if (url) {
        imgSrc.src = url;
        imgPreview.classList.remove("hidden");
        imgPreview.classList.add("flex");
    } else {
        imgPreview.classList.add("hidden");
        imgPreview.classList.remove("flex");
    }
}
imgInput.addEventListener("input", updatePreview);
updatePreview();

// ── Calcul réduction ─────────────────────────────────────────────────
const priceOriginal = document.getElementById("priceOriginal");
const priceFinal = document.getElementById("priceFinal");
const discountBadge = document.getElementById("discountBadge");
const discountText = document.getElementById("discountText");

function updateDiscount() {
    const orig = parseFloat(priceOriginal.value);
    const final = parseFloat(priceFinal.value);
    if (orig > 0 && final >= 0 && final < orig) {
        const pct = Math.round((orig - final) / orig * 100);
        discountText.textContent = `-${pct}% d'économie — vous économisez ${(orig - final).toFixed(2)} €`;
        discountBadge.classList.remove("hidden");
        discountBadge.classList.add("flex");
    } else {
        discountBadge.classList.add("hidden");
        discountBadge.classList.remove("flex");
    }
}
priceOriginal.addEventListener("input", updateDiscount);
priceFinal.addEventListener("input", updateDiscount);