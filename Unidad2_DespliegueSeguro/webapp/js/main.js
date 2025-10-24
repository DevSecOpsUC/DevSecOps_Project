document.getElementById('btn').addEventListener('click', async () => {
  const out = document.getElementById('out');
  out.innerHTML = '<p>Cargando habitaciones...</p>';

  try {
    const res = await fetch('https://devsecopsuc-api.azurewebsites.net/rooms');
    if (!res.ok) throw new Error(`Error HTTP ${res.status}`);

    const data = await res.json();

    out.innerHTML = data.map(r => `
      <div class="room-card">
        <h3>${r.Name}</h3>
        <p><strong>Precio:</strong> $${r.Price.toLocaleString()}</p>
      </div>
    `).join('');
  } catch (e) {
    out.innerHTML = `<p style="color:red;">Error: ${e.message}</p>`;
  }
});
