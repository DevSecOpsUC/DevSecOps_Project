document.addEventListener("DOMContentLoaded", () => {
  const loginPage = document.getElementById("login-page");
  const roomsPage = document.getElementById("rooms-page");
  const loginBtn = document.getElementById("login-btn");
  const logoutBtn = document.getElementById("logout-btn");
  const loadBtn = document.getElementById("load-btn");
  const loginMsg = document.getElementById("login-msg");
  const userLabel = document.getElementById("user-label");
  const roomsDiv = document.getElementById("rooms");

  // Si ya hay sesión, entra directo
  if (localStorage.getItem("token")) {
    showRoomsPage();
  }

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
      userLabel.textContent = `👤 ${data.user}`;
      showRoomsPage();
    } catch (err) {
      loginMsg.textContent = err.message;
    }
  });

  logoutBtn.addEventListener("click", () => {
    localStorage.removeItem("token");
    showLoginPage();
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

  function showRoomsPage() {
    loginPage.classList.add("hidden");
    roomsPage.classList.remove("hidden");
    loginPage.classList.remove("active");
    roomsPage.classList.add("active");
  }

  function showLoginPage() {
    loginPage.classList.remove("hidden");
    roomsPage.classList.add("hidden");
    roomsPage.classList.remove("active");
    loginPage.classList.add("active");
  }
});
