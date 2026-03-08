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
