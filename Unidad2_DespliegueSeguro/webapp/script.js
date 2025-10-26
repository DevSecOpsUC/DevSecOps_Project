const loginBtn = document.getElementById('login-btn');
const loadBtn = document.getElementById('load-btn');
const msg = document.getElementById('login-msg');
const loginSection = document.getElementById('login-section');
const dashboard = document.getElementById('dashboard');

loginBtn.addEventListener('click', async () => {
  const username = document.getElementById('username').value;
  const password = document.getElementById('password').value;

  msg.textContent = "Verificando credenciales...";
  try {
    const res = await fetch('/api/auth/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username, password })
    });
    if (!res.ok) throw new Error('Credenciales inválidas');
    const data = await res.json();
    localStorage.setItem('token', data.token);
    msg.textContent = "Inicio de sesión exitoso ✅";
    loginSection.style.display = 'none';
    dashboard.style.display = 'block';
  } catch (e) {
    msg.textContent = "Error: " + e.message;
  }
});

loadBtn.addEventListener('click', async () => {
  const token = localStorage.getItem('token');
  const roomsDiv = document.getElementById('rooms');
  roomsDiv.innerHTML = "<p>Cargando habitaciones...</p>";

  try {
    const res = await fetch('/api/rooms');
    const data = await res.json();
    roomsDiv.innerHTML = data.map(r => `
      <div class="room-card">
        <h3>${r.Name}</h3>
        <p>💰 $${Number(r.Price).toLocaleString('es-CO')}</p>
      </div>
    `).join('');
  } catch {
    roomsDiv.innerHTML = "<p style='color:red;'>No se pudieron cargar las habitaciones</p>";
  }
});
