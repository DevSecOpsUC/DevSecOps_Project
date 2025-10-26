document.addEventListener("DOMContentLoaded", () => {
  const loginBtn = document.getElementById("login-btn");
  const loadBtn = document.getElementById("load-btn");
  const loginSection = document.getElementById("login-section");
  const roomsSection = document.getElementById("rooms-section");
  const loginMsg = document.getElementById("login-msg");
  const userLabel = document.getElementById("user-label");
  const roomsDiv = document.getElementById("rooms");

  loginBtn.addEventListener("click", async () => {
    const username = document.getElementById("username").value.trim();
    const password = document.getElementById("password").value.trim();
    loginMsg.textContent = "Verificando...";

    try {
      const res = await fetch("/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password })
      });
      if (!res.ok) throw new Error("Credenciales inválidas");
      const data = await res.json();
      localStorage.setItem("token", data.token);
      userLabel.textContent = data.user;
      loginSection.classList.add("hidden");
      roomsSection.classList.remove("hidden");
    } catch (err) {
      loginMsg.textContent = err.message;
    }
  });

  loadBtn.addEventListener("click", async () => {
    roomsDiv.innerHTML = "<p>Cargando habitaciones...</p>";
    try {
      const res = await fetch("/api/rooms");
      if (!res.ok) throw new Error("Error al obtener habitaciones");
      const data = await res.json();
      roomsDiv.innerHTML = data.map(r => `
        <div class="room-card">
          <h3>${r.Name}</h3>
          <p>💰 $${Number(r.Price).toLocaleString('es-CO')}</p>
        </div>
      `).join("");
    } catch {
      roomsDiv.innerHTML = "<p style='color:red;'>No se pudieron cargar las habitaciones</p>";
    }
  });
});
