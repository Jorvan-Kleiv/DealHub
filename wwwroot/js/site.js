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
const fileInput = document.getElementById('imageFile');
const uploadZone = document.getElementById('uploadZone');
const uploadPlaceholder = document.getElementById('uploadPlaceholder');
const imgPreview = document.getElementById('imgPreview');
const fileInfo = document.getElementById('fileInfo');
const fileNameEl = document.getElementById('fileName');
const removeBtn = document.getElementById('removeFile');

function applyFile(file) {
    if (!file || !file.type.startsWith('image/')) return;
    const reader = new FileReader();
    reader.onload = (e) => {
        imgPreview.src = e.target.result;
        imgPreview.classList.remove('hidden');
        uploadPlaceholder.classList.add('hidden');
        uploadZone.classList.replace('border-dashed', 'border-solid');
        uploadZone.classList.add('border-brand');
        fileInfo.classList.replace('hidden', 'flex');
        fileNameEl.textContent = file.name;
    };
    reader.readAsDataURL(file);
}

fileInput.addEventListener('change', () => { if (fileInput.files[0]) applyFile(fileInput.files[0]); });

uploadZone.addEventListener('dragover', (e) => { e.preventDefault(); uploadZone.classList.add('border-brand', 'bg-brand/[.04]'); });
uploadZone.addEventListener('dragleave', () => { uploadZone.classList.remove('border-brand', 'bg-brand/[.04]'); });
uploadZone.addEventListener('drop', (e) => {
    e.preventDefault();
    const file = e.dataTransfer.files[0];
    if (file) { const dt = new DataTransfer(); dt.items.add(file); fileInput.files = dt.files; applyFile(file); }
});

removeBtn.addEventListener('click', (e) => {
    e.preventDefault(); e.stopPropagation();
    fileInput.value = '';
    imgPreview.src = '';
    imgPreview.classList.add('hidden');
    uploadPlaceholder.classList.remove('hidden');
    uploadZone.classList.replace('border-solid', 'border-dashed');
    uploadZone.classList.remove('border-brand');
    fileInfo.classList.replace('flex', 'hidden');
    fileNameEl.textContent = '';
});
function toggleDropdown(event) {
    event.stopPropagation();
    const dropdown = document.getElementById('user-dropdown');
    dropdown.classList.toggle('hidden');
}

window.addEventListener('click', function (event) {
    const dropdown = document.getElementById('user-dropdown');
    const btn = document.getElementById('user-menu-btn');
    if (!btn.contains(event.target) && !dropdown.classList.contains('hidden')) {
        dropdown.classList.add('hidden');
    }
});

