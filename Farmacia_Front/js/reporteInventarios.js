const API = "https://localhost:7032/api";

// Filas ya combinadas (una por producto) con lo vendido y lo recibido en el rango
let filasMovimientos = [];

// Gráfico de barras de los productos con más movimiento
let graficoTop = null;

// ---------------- Fechas

// Por defecto: del primer día del mes actual hasta hoy
function cargarFechasPorDefecto()
{
    const hoy = dayjs();

    document.getElementById("fecha1").value = hoy.startOf("month").format("YYYY-MM-DD");
    document.getElementById("fecha2").value = hoy.format("YYYY-MM-DD");
}

// Devuelve { desde, hasta } o null si las fechas no son válidas
function leerFechas()
{
    const desde = document.getElementById("fecha1").value;
    const hasta = document.getElementById("fecha2").value;

    if (desde === "" || hasta === "")
    {
        Swal.fire("Complete las dos fechas");
        return null;
    }

    // Las fechas tienen formato YYYY-MM-DD, se pueden comparar como texto
    if (desde > hasta)
    {
        Swal.fire("La fecha desde no puede ser mayor a la fecha hasta");
        return null;
    }

    return { desde, hasta };
}

// ---------------- API (MovimientosController)

// tipo: "vendidos" o "recibidos"
// El controller compara con <= fechaFin, por eso la fecha hasta se manda al final del día
function obtenerMovimientos(tipo, fechas)
{
    const fecha1 = encodeURIComponent(`${fechas.desde}T00:00:00`);
    const fecha2 = encodeURIComponent(`${fechas.hasta}T23:59:59`);

    let url = `${API}/Movimientos/productos/${tipo}/${fecha1}/${fecha2}`;

    return fetch(url)
        .then(r => r.ok ? r.json() : Promise.reject(new Error(`HTTP ${r.status}`)));
}

// Une vendidos y recibidos en una sola fila por producto
function combinarMovimientos(vendidos, recibidos)
{
    const mapa = new Map();

    const sumar = (lista, campo) => {
        lista.forEach(m => {
            const nombre = m.producto ?? "Sin nombre";

            if (!mapa.has(nombre))
            {
                mapa.set(nombre, { producto: nombre, vendidos: 0, recibidos: 0 });
            }

            mapa.get(nombre)[campo] += m.cantidad;
        });
    };

    sumar(vendidos, "vendidos");
    sumar(recibidos, "recibidos");

    return Array.from(mapa.values())
        .sort((x, y) => (y.vendidos + y.recibidos) - (x.vendidos + x.recibidos) ||
                        x.producto.localeCompare(y.producto));
}

// ---------------- Balance

// El balance es lo que entró (entregas) menos lo que salió (ventas)
function formatearBalance(valor)
{
    return valor > 0 ? `+${valor}` : `${valor}`;
}

function crearIndicadorBalance(valor)
{
    let $indicador = document.createElement("span");

    if (valor === 0)
    {
        $indicador.className = "text-body-secondary";
        $indicador.textContent = "0";
        return $indicador;
    }

    const sube = valor > 0;

    $indicador.className = sube ? "text-success-emphasis" : "text-danger-emphasis";

    let $flecha = document.createElement("i");
    $flecha.className = sube ? "bi bi-arrow-up" : "bi bi-arrow-down";

    $indicador.appendChild($flecha);
    $indicador.append(` ${Math.abs(valor)}`);

    return $indicador;
}

// ---------------- Resumen (tarjetas)

function mostrarResumen()
{
    const vendidos = filasMovimientos.reduce((acc, f) => acc + f.vendidos, 0);
    const recibidos = filasMovimientos.reduce((acc, f) => acc + f.recibidos, 0);

    document.getElementById("total-vendidos").textContent = vendidos;
    document.getElementById("total-recibidos").textContent = recibidos;
    document.getElementById("total-balance").textContent = formatearBalance(recibidos - vendidos);
}

// ---------------- Tabla

function crearCelda(contenido, clase)
{
    let $celda = document.createElement("td");

    if (clase)
    {
        $celda.className = clase;
    }

    if (contenido instanceof Node)
    {
        $celda.appendChild(contenido);
    }
    else
    {
        $celda.textContent = contenido;
    }

    return $celda;
}

function mostrarTabla()
{
    const filtro = document.getElementById("input-producto").value.trim().toLowerCase();

    const $cuerpo = document.getElementById("cuerpo_tabla");
    $cuerpo.replaceChildren();

    const filas = filasMovimientos.filter(f => f.producto.toLowerCase().includes(filtro));

    if (filas.length === 0)
    {
        let $filaVacia = document.createElement("tr");

        let $celdaVacia = crearCelda("No hay movimientos para las fechas elegidas", "text-center text-body-secondary");
        $celdaVacia.colSpan = 4;

        $filaVacia.appendChild($celdaVacia);
        $cuerpo.appendChild($filaVacia);
    }

    filas.forEach(f => {
        let $fila = document.createElement("tr");

        $fila.appendChild(crearCelda(f.producto));
        $fila.appendChild(crearCelda(f.vendidos, "text-end"));
        $fila.appendChild(crearCelda(f.recibidos, "text-end"));
        $fila.appendChild(crearCelda(crearIndicadorBalance(f.recibidos - f.vendidos), "text-end"));

        $cuerpo.appendChild($fila);
    });

    document.getElementById("texto-productos").textContent =
        `Mostrando ${filas.length} de ${filasMovimientos.length} productos`;
}

// ---------------- Gráfico (productos con más movimiento)

function mostrarGrafico()
{
    // La lista ya viene ordenada por movimiento total
    const top = filasMovimientos.slice(0, 8);

    document.getElementById("grafico-vacio").classList.toggle("d-none", top.length > 0);

    if (graficoTop !== null)
    {
        graficoTop.destroy();
        graficoTop = null;
    }

    if (top.length === 0)
    {
        return;
    }

    graficoTop = new Chart(document.getElementById("grafico-top"), {
        type: "bar",
        data: {
            labels: top.map(f => f.producto),
            datasets: [
                {
                    label: "Vendidos",
                    data: top.map(f => f.vendidos),
                    backgroundColor: "#0d6efd"
                },
                {
                    label: "Recibidos",
                    data: top.map(f => f.recibidos),
                    backgroundColor: "#adb5bd"
                }
            ]
        },
        options: {
            indexAxis: "y",
            plugins: { legend: { position: "bottom" } },
            scales: { x: { beginAtZero: true, ticks: { precision: 0 } } }
        }
    });
}

// ---------------- Buscar

function buscarMovimientos()
{
    const fechas = leerFechas();

    if (fechas === null)
    {
        return;
    }

    const $btnBuscar = document.getElementById("btn-buscar");
    $btnBuscar.disabled = true;

    Promise.all([
        obtenerMovimientos("vendidos", fechas),
        obtenerMovimientos("recibidos", fechas)
    ])
        .then(([vendidos, recibidos]) => {

            filasMovimientos = combinarMovimientos(vendidos, recibidos);

            document.getElementById("texto-fechas").textContent =
                `Movimientos del ${dayjs(fechas.desde).format("DD/MM/YYYY")} al ${dayjs(fechas.hasta).format("DD/MM/YYYY")}`;

            mostrarResumen();
            mostrarTabla();
            mostrarGrafico();
        })
        .catch(error => {
            console.error(error);
            Swal.fire("Lo sentimos, hubo un problema cargando los movimientos");
        })
        .finally(() => {
            $btnBuscar.disabled = false;
        });
}

document.addEventListener("DOMContentLoaded", () => {
    cargarFechasPorDefecto();
    buscarMovimientos();
});

document.getElementById("btn-buscar").addEventListener("click", buscarMovimientos);

document.getElementById("btn-restablecer").addEventListener("click", () => {
    cargarFechasPorDefecto();
    buscarMovimientos();
});

document.getElementById("input-producto").addEventListener("input", mostrarTabla);