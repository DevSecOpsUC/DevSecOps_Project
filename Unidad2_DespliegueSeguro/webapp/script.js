document.getElementById('btn').addEventListener('click', async () => {
  const out = document.getElementById('out');
  out.innerHTML = '<p>Cargando habitaciones...</p>';

  try {
    // Usa el proxy del Nginx del compose: /api/rooms
    const res = await fetch('/api/rooms');
    if (!res.ok) throw new Error(`Error HTTP ${res.status}`);

    const data = await res.json();

    out.innerHTML = data.map(r => `
      <div class="room-card">
        <h3>${(r.Name || r.nombre || 'Habitación')}</h3>
        <p><strong>Precio:</strong> $${Number(r.Price ?? r.precio ?? 0).toLocaleString('es-CO')}</p>
      </div>
    `).join('');
  } catch (e) {
    // Fallback local si la API no responde
    const mock = [
      { Name: 'Habitación 101', Price: 680000 },
      { Name: 'Habitación 202', Price: 550000 },
      { Name: 'Habitación 303', Price: 750000 },
      { Name: 'Habitación 404', Price: 600000 }
    ];
    out.innerHTML = mock.map(r => `
      <div class="room-card">
        <h3>${r.Name}</h3>
        <p><strong>Precio:</strong> $${Number(r.Price).toLocaleString('es-CO')}</p>
      </div>
    `).join('') + `<p style="color:#f87171;margin-top:8px">API no disponible: ${e.message}</p>`;
  }
});
