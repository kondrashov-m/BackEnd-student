const API_BASE = '/api/items';

let currentEditId = null;

document.addEventListener('DOMContentLoaded', () => {
    loadItems();
    
    document.getElementById('itemForm').addEventListener('submit', (e) => {
        e.preventDefault();
        if (currentEditId) {
            updateItem();
        } else {
            createItem();
        }
    });
});

async function loadItems() {
    showLoading();
    try {
        const response = await fetch(API_BASE);
        if (!response.ok) throw new Error('Ошибка загрузки элементов');
        const items = await response.json();
        displayItems(items);
    } catch (error) {
        showError('Ошибка загрузки: ' + error.message);
    }
}

function displayItems(items) {
    const container = document.getElementById('itemsList');
    
    if (!items || items.length === 0) {
        container.innerHTML = '<div class="empty">Нет элементов. Создайте первый элемент!</div>';
        return;
    }
    
    container.innerHTML = items.map(item => `
        <div class="item-card" data-id="${item.id}">
            <div class="item-info">
                <h3>${escapeHtml(item.name)}</h3>
                <p>${escapeHtml(item.description) || 'Нет описания'}</p>
                <small>Создан: ${new Date(item.createdAt).toLocaleString('ru-RU')}</small>
            </div>
            <div class="item-actions">
                <button class="btn-edit" onclick="editItem(${item.id})">Редактировать</button>
                <button class="btn-delete" onclick="deleteItem(${item.id})">Удалить</button>
            </div>
        </div>
    `).join('');
}

async function createItem() {
    const name = document.getElementById('itemName').value;
    const description = document.getElementById('itemDesc').value;
    
    if (!name.trim()) {
        showError('Название элемента не может быть пустым');
        return;
    }
    
    try {
        const response = await fetch(API_BASE, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name, description })
        });
        
        if (!response.ok) throw new Error('Ошибка создания элемента');
        
        resetForm();
        await loadItems();
        showSuccess('Элемент успешно создан');
    } catch (error) {
        showError('Ошибка создания: ' + error.message);
    }
}

async function editItem(id) {
    try {
        const response = await fetch(`${API_BASE}/${id}`);
        if (!response.ok) throw new Error('Элемент не найден');
        
        const item = await response.json();
        
        document.getElementById('itemName').value = item.name;
        document.getElementById('itemDesc').value = item.description;
        document.getElementById('submitBtn').textContent = 'Обновить элемент';
        document.getElementById('cancelBtn').style.display = 'inline-block';
        
        currentEditId = item.id;
    } catch (error) {
        showError('Ошибка загрузки элемента: ' + error.message);
    }
}

async function updateItem() {
    const name = document.getElementById('itemName').value;
    const description = document.getElementById('itemDesc').value;
    
    if (!name.trim()) {
        showError('Название элемента не может быть пустым');
        return;
    }
    
    try {
        const response = await fetch(`${API_BASE}/${currentEditId}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: currentEditId, name, description })
        });
        
        if (!response.ok) throw new Error('Ошибка обновления элемента');
        
        resetForm();
        await loadItems();
        showSuccess('Элемент успешно обновлён');
    } catch (error) {
        showError('Ошибка обновления: ' + error.message);
    }
}

async function deleteItem(id) {
    if (!confirm('Вы уверены, что хотите удалить этот элемент?')) return;
    
    try {
        const response = await fetch(`${API_BASE}/${id}`, { method: 'DELETE' });
        if (!response.ok) throw new Error('Ошибка удаления элемента');
        
        await loadItems();
        showSuccess('Элемент успешно удалён');
    } catch (error) {
        showError('Ошибка удаления: ' + error.message);
    }
}

function resetForm() {
    document.getElementById('itemForm').reset();
    document.getElementById('submitBtn').textContent = 'Создать элемент';
    document.getElementById('cancelBtn').style.display = 'none';
    currentEditId = null;
}

function showLoading() {
    document.getElementById('itemsList').innerHTML = '<div class="loading">Загрузка...</div>';
}

function showError(message) {
    const container = document.getElementById('itemsList');
    container.innerHTML = `<div class="error">❌ ${escapeHtml(message)}</div>`;
    setTimeout(() => {
        if (container.innerHTML.includes(message)) loadItems();
    }, 3000);
}

function showSuccess(message) {
    console.log('✅ ' + message);
}

function escapeHtml(text) {
    if (!text) return '';
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}