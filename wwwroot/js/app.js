function saveToLocalStorage() {
    const value = document.getElementById('lsValue').value;
    if (!value) {
        alert('Введите значение');
        return;
    }
    localStorage.setItem('userData', value);
    displayLocalStorage();
    document.getElementById('lsValue').value = '';
}

function displayLocalStorage() {
    const value = localStorage.getItem('userData');
    const container = document.getElementById('lsDisplay');
    if (value) {
        container.innerHTML = `<div class="info-text">📦 Значение: <strong>${escapeHtml(value)}</strong></div>`;
    } else {
        container.innerHTML = '<div class="empty-text">Нет данных в localStorage</div>';
    }
}

function clearLocalStorage() {
    localStorage.removeItem('userData');
    displayLocalStorage();
}


function saveToSessionStorage() {
    const value = document.getElementById('ssValue').value;
    if (!value) {
        alert('Введите значение');
        return;
    }
    sessionStorage.setItem('tempData', value);
    displaySessionStorage();
    document.getElementById('ssValue').value = '';
}

function displaySessionStorage() {
    const value = sessionStorage.getItem('tempData');
    const container = document.getElementById('ssDisplay');
    if (value) {
        container.innerHTML = `<div class="info-text">📦 Значение: <strong>${escapeHtml(value)}</strong></div>`;
    } else {
        container.innerHTML = '<div class="empty-text">Нет данных в sessionStorage</div>';
    }
}

function clearSessionStorage() {
    sessionStorage.removeItem('tempData');
    displaySessionStorage();
}

async function setCookie() {
    const value = document.getElementById('cookieValue').value;
    if (!value) {
        alert('Введите значение');
        return;
    }
    
    try {
        const response = await fetch('/api/state/set-cookie', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: value })
        });
        const data = await response.json();
        if (response.ok) {
            displayCookie();
            document.getElementById('cookieValue').value = '';
        } else {
            alert('Ошибка: ' + data.message);
        }
    } catch (error) {
        alert('Ошибка: ' + error.message);
    }
}

async function displayCookie() {
    try {
        const response = await fetch('/api/state/get-cookie');
        const data = await response.json();
        const container = document.getElementById('cookieDisplay');
        if (data.exists) {
            container.innerHTML = `<div class="info-text">🍪 Значение: <strong>${escapeHtml(data.value)}</strong></div>`;
        } else {
            container.innerHTML = '<div class="empty-text">Нет данных в Cookie</div>';
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

async function deleteCookie() {
    try {
        await fetch('/api/state/delete-cookie', { method: 'DELETE' });
        displayCookie();
    } catch (error) {
        alert('Ошибка: ' + error.message);
    }
}

async function setSession() {
    const value = document.getElementById('sessionValue').value;
    if (!value) {
        alert('Введите значение');
        return;
    }
    
    try {
        const response = await fetch('/api/state/set-session', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ value: value })
        });
        const data = await response.json();
        if (response.ok) {
            displaySession();
            document.getElementById('sessionValue').value = '';
        } else {
            alert('Ошибка: ' + data.message);
        }
    } catch (error) {
        alert('Ошибка: ' + error.message);
    }
}

async function displaySession() {
    try {
        const response = await fetch('/api/state/get-session');
        const data = await response.json();
        const container = document.getElementById('sessionDisplay');
        if (data.exists) {
            container.innerHTML = `<div class="info-text">🔒 Значение: <strong>${escapeHtml(data.value)}</strong></div>`;
        } else {
            container.innerHTML = '<div class="empty-text">Нет данных в сессии</div>';
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

async function clearSession() {
    try {
        await fetch('/api/state/clear-session', { method: 'DELETE' });
        displaySession();
    } catch (error) {
        alert('Ошибка: ' + error.message);
    }
}

function escapeHtml(text) {
    if (!text) return '';
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}

document.addEventListener('DOMContentLoaded', () => {
    displayLocalStorage();
    displaySessionStorage();
    displayCookie();
    displaySession();
});