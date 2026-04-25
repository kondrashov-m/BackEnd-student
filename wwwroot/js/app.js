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
        if (!response.ok) throw new Error('Failed to load items');
        const items = await response.json();
        displayItems(items);
    } catch (error) {
        showError('Error loading items: ' + error.message);
    }
}

function displayItems(items) {
    const container = document.getElementById('itemsList');
    
    if (!items || items.length === 0) {
        container.innerHTML = '<div class="empty">No items yet. Create your first item!</div>';
        return;
    }
    
    container.innerHTML = items.map(item => `
        <div class="item-card" data-id="${item.id}">
            <div class="item-info">
                <h3>${escapeHtml(item.name)}</h3>
                <p>${escapeHtml(item.description) || 'No description'}</p>
                <small>Created: ${new Date(item.createdAt).toLocaleString()}</small>
            </div>
            <div class="item-actions">
                <button class="btn-edit" onclick="editItem(${item.id})">Edit</button>
                <button class="btn-delete" onclick="deleteItem(${item.id})">Delete</button>
            </div>
        </div>
    `).join('');
}

async function createItem() {
    const name = document.getElementById('itemName').value;
    const description = document.getElementById('itemDesc').value;
    
    try {
        const response = await fetch(API_BASE, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name, description })
        });
        
        if (!response.ok) throw new Error('Failed to create item');
        
        resetForm();
        await loadItems();
    } catch (error) {
        showError('Error creating item: ' + error.message);
    }
}

async function editItem(id) {
    try {
        const response = await fetch(`${API_BASE}/${id}`);
        if (!response.ok) throw new Error('Item not found');
        
        const item = await response.json();
        
        document.getElementById('itemName').value = item.name;
        document.getElementById('itemDesc').value = item.description;
        document.getElementById('submitBtn').textContent = 'Update Item';
        document.getElementById('cancelBtn').style.display = 'inline-block';
        
        currentEditId = item.id;
    } catch (error) {
        showError('Error loading item: ' + error.message);
    }
}

async function updateItem() {
    const name = document.getElementById('itemName').value;
    const description = document.getElementById('itemDesc').value;
    
    try {
        const response = await fetch(`${API_BASE}/${currentEditId}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: currentEditId, name, description })
        });
        
        if (!response.ok) throw new Error('Failed to update item');
        
        resetForm();
        await loadItems();
    } catch (error) {
        showError('Error updating item: ' + error.message);
    }
}

async function deleteItem(id) {
    if (!confirm('Are you sure you want to delete this item?')) return;
    
    try {
        const response = await fetch(`${API_BASE}/${id}`, { method: 'DELETE' });
        if (!response.ok) throw new Error('Failed to delete item');
        
        await loadItems();
    } catch (error) {
        showError('Error deleting item: ' + error.message);
    }
}

function resetForm() {
    document.getElementById('itemForm').reset();
    document.getElementById('submitBtn').textContent = 'Create Item';
    document.getElementById('cancelBtn').style.display = 'none';
    currentEditId = null;
}

function showLoading() {
    document.getElementById('itemsList').innerHTML = '<div class="loading">Loading...</div>';
}

function showError(message) {
    const container = document.getElementById('itemsList');
    container.innerHTML = `<div class="error">${escapeHtml(message)}</div>`;
    setTimeout(() => {
        if (container.innerHTML.includes(message)) loadItems();
    }, 3000);
}

function escapeHtml(text) {
    if (!text) return '';
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}