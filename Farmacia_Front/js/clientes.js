// Datos del empleado guardados por login.js (incluye su sucursal)
function obtenerSesion()
{
    try
    {
        return JSON.parse(sessionStorage.getItem("sesion"));
    }
    catch
    {
        return null;
    }
}

const sesion = obtenerSesion();

function mostrarDatosEmpleado()
{
    const nombreCompleto = `${sesion.nombre} ${sesion.apellido}`;

    document.getElementById("nombre-empleado").textContent = nombreCompleto;

    const $datos = document.getElementById("datos-empleado");
    $datos.replaceChildren();
    $datos.append(`${nombreCompleto} - ${sesion.ocupacion}`);

    document.getElementById("btn-cerrar-sesion").addEventListener("click", e => {
        e.preventDefault();
        sessionStorage.removeItem("sesion");
        window.location.href = "../html/login.html";
    });
}


// ================== CONFIGURACIÓN ==================
const API_BASE = "https://localhost:7032/api";
const URL_CLIENTES = `${API_BASE}/Clientes/clientes`;   // GET, POST, PUT /{id}, DELETE /{id}
const URL_BARRIOS = `${API_BASE}/Barrios/Barrios`;                     // BarriosController -> GET Barrios
const URL_OBRAS_SOCIALES = `${API_BASE}/Obras_Sociales/ObrasSociales`; // Obras_SocialesController -> GET ObrasSociales
// ===================================================

const REGEX_NOMBRE = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/;

document.addEventListener("DOMContentLoaded", () => {
  if (sesion === null)
    {
        window.location.replace("../html/login.html");
        return;
    }
    mostrarDatosEmpleado();

  const tbody = document.getElementById("clientesTbody");
  const searchInput = document.getElementById("user-search");
  const modalEl = document.getElementById("modal-add-user");
  const modalTitle = document.getElementById("modal-add-user-label");
  const form = document.getElementById("formRegistrarCliente");
  const submitBtn = form.querySelector('button[type="submit"]');
  const deleteModalEl = document.getElementById("modal-delete-user");
  const deleteName = document.getElementById("borrarNombre");
  const deleteBtn = document.getElementById("btnConfirmarBorrar");

  const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
  const deleteModal = bootstrap.Modal.getOrCreateInstance(deleteModalEl);

  const f = {
    nombre: document.getElementById("nombre"),
    apellido: document.getElementById("apellido"),
    documento: document.getElementById("documento"),
    email: document.getElementById("email"),
    calle: document.getElementById("calle"),
    altura: document.getElementById("altura"),
    nroTelefono: document.getElementById("nroTelefono"),
    idBarrio: document.getElementById("idBarrio"),
    codObraSocial: document.getElementById("codObraSocial"),
  };

  let clientes = [];
  let editandoId = null; // null = creando
  let borrandoId = null;

  // ---------- Mensajes ----------
  const pageAlert = document.createElement("div");
  pageAlert.className = "d-none";
  document.querySelector(".app-content .card").before(pageAlert);

  const formAlert = document.createElement("div");
  formAlert.className = "d-none";
  form.querySelector(".modal-body").prepend(formAlert);

  let alertTimer;
  function showAlert(box, text, type = "danger", autoHide = false) {
    box.className = `alert alert-${type}`;
    box.setAttribute("role", "alert");
    box.textContent = text;
    clearTimeout(alertTimer);
    if (autoHide) alertTimer = setTimeout(() => hideAlert(box), 4000);
  }
  function hideAlert(box) {
    box.className = "d-none";
    box.textContent = "";
  }

  // Los errores del back vienen con formas distintas: {message}, {mensaje}, {errores:[...]}, {errors:{...}}
  function extraerError(data, status) {
    if (typeof data === "string" && data.trim()) return data;
    if (data && typeof data === "object") {
      if (data.message) return data.message;
      if (data.mensaje) return data.mensaje;
      const lista = data.errores || data.Errores;
      if (Array.isArray(lista)) return lista.join(" ");
      if (data.errors) return Object.values(data.errors).flat().join(" ");
    }
    return `Error inesperado (código ${status}).`;
  }

  async function llamarApi(url, options) {
    let response;
    try {
      response = await fetch(url, options);
    } catch {
      throw new Error("No se pudo conectar con el servidor.");
    }
    let data = null;
    const texto = await response.text();
    try {
      data = texto ? JSON.parse(texto) : null;
    } catch {
      data = texto; // texto plano
    }
    if (!response.ok) throw new Error(extraerError(data, response.status));
    return data;
  }

  // ---------- Utilidades ----------
  const escapeHtml = (v) =>
    String(v ?? "").replace(/[&<>"']/g, (c) => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c]));

  const aEntero = (v) => (v === "" || v == null ? null : parseInt(v, 10));
  const aTexto = (v) => (v.trim() === "" ? null : v.trim());

  // ---------- Listado ----------
  function filaMensaje(texto) {
    tbody.innerHTML = `<tr><td colspan="4" class="text-center text-secondary py-4">${escapeHtml(texto)}</td></tr>`;
  }

  function renderClientes() {
    const q = searchInput.value.trim().toLowerCase();
    const lista = clientes.filter((c) =>
      [c.nombre, c.apellido, c.documento, c.email].some((v) => String(v ?? "").toLowerCase().includes(q))
    );

    if (lista.length === 0) {
      filaMensaje(clientes.length === 0 ? "No hay clientes registrados." : "Sin resultados para la búsqueda.");
      return;
    }

    tbody.innerHTML = lista
      .map((c) => {
        const iniciales = `${(c.nombre || "?")[0]}${(c.apellido || "")[0] || ""}`.toUpperCase();
        const obra = c.obraSocialNombre
          ? `<span class="badge text-bg-success">${escapeHtml(c.obraSocialNombre)}</span>`
          : `<span class="badge text-bg-secondary">Sin obra social</span>`;
        return `
          <tr>
            <td>
              <div class="d-flex align-items-center">
                <span class="rounded-circle bg-primary text-white d-inline-flex align-items-center justify-content-center me-2"
                      style="width:32px;height:32px;font-size:.8rem">${escapeHtml(iniciales)}</span>
                <div>
                  <span class="fw-medium">${escapeHtml(c.nombre)} ${escapeHtml(c.apellido)}</span>
                  <div class="small text-secondary">Doc: ${escapeHtml(c.documento)}</div>
                </div>
              </div>
            </td>
            <td>${escapeHtml(c.email) || '<span class="text-secondary">-</span>'}</td>
            <td>${obra}</td>
            <td class="text-end">
              <div class="btn-group btn-group-sm">
                <button type="button" class="btn btn-outline-secondary" data-action="editar" data-id="${escapeHtml(c.idCliente)}" aria-label="Editar">
                  <i class="bi bi-pencil" aria-hidden="true"></i>
                </button>
                <button type="button" class="btn btn-outline-danger" data-action="borrar" data-id="${escapeHtml(c.idCliente)}" aria-label="Eliminar">
                  <i class="bi bi-trash" aria-hidden="true"></i>
                </button>
              </div>
            </td>
          </tr>`;
      })
      .join("");
  }

  async function cargarClientes() {
    filaMensaje("Cargando clientes...");
    try {
      const data = await llamarApi(URL_CLIENTES);
      clientes = Array.isArray(data) ? data : [];
      hideAlert(pageAlert);
      renderClientes();
    } catch (err) {
      clientes = [];
      filaMensaje("No se pudieron cargar los clientes.");
      showAlert(pageAlert, err.message);
    }
  }

  // ---------- Catálogos (barrios y obras sociales) ----------
  function leerItem(item, clavesId, clavesNombre) {
    const claves = Object.keys(item);
    const buscar = (candidatas) => {
      const k = claves.find((c) => candidatas.includes(c.toLowerCase()));
      return k !== undefined ? item[k] : undefined;
    };
    const id = buscar(clavesId) ?? Object.values(item).find((v) => typeof v === "number");
    const nombre = buscar(clavesNombre) ?? Object.values(item).find((v) => typeof v === "string");
    return { id, nombre };
  }
  function llenarSelect(select, items, getValue, getLabel) {
    const primera = select.options[0]; // "Seleccione..." / "Sin obra social"
    select.innerHTML = "";
    select.appendChild(primera);
    items.forEach((it) => {
      const opt = document.createElement("option");
      opt.value = getValue(it);
      opt.textContent = getLabel(it);
      select.appendChild(opt);
    });
  }

  async function cargarCatalogos() {
    const errores = [];
    try {
      const barrios = await llamarApi(URL_BARRIOS);
      const items = barrios.map((b) => leerItem(b, ["idbarrio", "id"], ["nombrebarrio", "nombre", "barrio"]));
      llenarSelect(f.idBarrio, items, (b) => b.id, (b) => b.nombre);
    } catch {
      errores.push("barrios");
    }
    try {
      const obras = await llamarApi(URL_OBRAS_SOCIALES);
      const items = obras.map((o) => leerItem(o, ["codobrasocial", "id"], ["detalle", "nombre", "nombreobrasocial"]));
      llenarSelect(f.codObraSocial, items, (o) => o.id, (o) => o.nombre);
    } catch {
      errores.push("obras sociales");
    }
    if (errores.length) {
      showAlert(pageAlert, `No se pudieron cargar: ${errores.join(" y ")}. Revisa que la API esté corriendo.`, "warning");
    }
  }

  // Asigna el valor de un select; si la opción no existe, la agrega para no perder el dato
  function setSelect(select, valor) {
    if (valor == null || valor === ""|| Number(valor) === 0) {
      select.value = "";
      return;
    }
    const v = String(valor);
    if (![...select.options].some((o) => o.value === v)) {
      const opt = document.createElement("option");
      opt.value = v;
      opt.textContent = `ID ${v}`;
      select.appendChild(opt);
    }
    select.value = v;
  }

  // ---------- Crear / Editar ----------
  function abrirModalNuevo() {
    editandoId = null;
    form.reset();
    hideAlert(formAlert);
    modalTitle.textContent = "Registrar Nuevo Cliente";
    submitBtn.textContent = "Guardar Cliente";
  }

  function abrirModalEditar(c) {
    editandoId = c.idCliente;
    form.reset();
    hideAlert(formAlert);
    modalTitle.textContent = "Editar Cliente";
    submitBtn.textContent = "Guardar Cambios";
    f.nombre.value = c.nombre ?? "";
    f.apellido.value = c.apellido ?? "";
    f.documento.value = c.documento ?? "";
    f.email.value = c.email ?? "";
    f.calle.value = c.calle ?? "";
    f.altura.value = c.altura ?? "";
    f.nroTelefono.value = c.nroTelefono ?? "";
    setSelect(f.idBarrio,c.id_barrio ?? c.idBarrio);
    setSelect(f.codObraSocial, c.cod_ObraSocial ?? c.codObraSocial);
    modal.show();
  }

  // El botón "Nuevo cliente" abre el modal con data-bs-toggle (trae relatedTarget): se resetea a modo crear
  modalEl.addEventListener("show.bs.modal", (e) => {
    if (e.relatedTarget) abrirModalNuevo();
  });

  form.addEventListener("submit", async (e) => {
    e.preventDefault();
    hideAlert(formAlert);

    if (!REGEX_NOMBRE.test(f.nombre.value.trim()) || !REGEX_NOMBRE.test(f.apellido.value.trim())) {
      showAlert(formAlert, "El nombre y el apellido solo pueden contener letras y espacios.");
      return;
    }
    if (!form.checkValidity()) {
      form.reportValidity();
      return;
    }

    const payload = {
      nombre: f.nombre.value.trim(),
      apellido: f.apellido.value.trim(),
      documento: aEntero(f.documento.value),
      calle: aTexto(f.calle.value),
      altura: aEntero(f.altura.value),
      idBarrio: aEntero(f.idBarrio.value),
      nroTelefono: aEntero(f.nroTelefono.value),
      email: aTexto(f.email.value),
      codObraSocial: editandoId !== null
        ? (aEntero(f.codObraSocial.value) ?? 0)
        : aEntero(f.codObraSocial.value),
    };

    const esEdicion = editandoId !== null;
    submitBtn.disabled = true;
    try {
      const data = await llamarApi(esEdicion ? `${URL_CLIENTES}/${editandoId}` : URL_CLIENTES, {
        method: esEdicion ? "PUT" : "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });
      modal.hide();
      await cargarClientes();
      showAlert(pageAlert, data?.message || "Cliente guardado correctamente.", "success", true);
    } catch (err) {
      showAlert(formAlert, err.message);
    } finally {
      submitBtn.disabled = false;
    }
  });

  // ---------- Borrar ----------
  tbody.addEventListener("click", (e) => {
    const btn = e.target.closest("button[data-action]");
    if (!btn) return;
    const cliente = clientes.find((c) => String(c.idCliente) === btn.dataset.id);
    if (!cliente) return;

    if (btn.dataset.action === "editar") {
      abrirModalEditar(cliente);
    } else if (btn.dataset.action === "borrar") {
      borrandoId = cliente.idCliente;
      deleteName.textContent = `${cliente.nombre} ${cliente.apellido}`;
      deleteModal.show();
    }
  });

  deleteBtn.addEventListener("click", async () => {
    if (borrandoId === null) return;
    deleteBtn.disabled = true;
    try {
      const data = await llamarApi(`${URL_CLIENTES}/${borrandoId}`, { method: "DELETE" });
      deleteModal.hide();
      await cargarClientes();
      showAlert(pageAlert, data?.message || "Cliente eliminado.", "success", true);
    } catch (err) {
      deleteModal.hide();
      showAlert(pageAlert, err.message);
    } finally {
      deleteBtn.disabled = false;
      borrandoId = null;
    }
  });

  searchInput.addEventListener("input", renderClientes);

  // ---------- Inicio ----------
  cargarClientes();
  cargarCatalogos();
});
