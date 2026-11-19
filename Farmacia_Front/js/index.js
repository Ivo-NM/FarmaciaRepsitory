
const API = "https://localhost:7032/api";

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

    document.getElementById("titulo-home").textContent = `Home - ${sesion.nomSede}`;
    document.getElementById("nombre-empleado").textContent = nombreCompleto;

    const $datos = document.getElementById("datos-empleado");
    $datos.replaceChildren();
    $datos.append(`${nombreCompleto} - ${sesion.ocupacion}`);

    const $sede = document.createElement("small");
    $sede.textContent = sesion.nomSede;
    $datos.appendChild($sede);

    document.getElementById("btn-cerrar-sesion").addEventListener("click", e => {
        e.preventDefault();
        sessionStorage.removeItem("sesion");
        window.location.href = "../html/login.html";
    });
}

// Movimientos del mes de la sucursal por producto:
// las entregas suben el stock y las ventas (facturas) lo bajan.

function obtenerMovimientosDelMes()
{
    const hoy = new Date();
    const esDelMes = fecha => {
        const f = new Date(fecha);
        return f.getFullYear() === hoy.getFullYear() && f.getMonth() === hoy.getMonth();
    };
    const leer = r => r.ok ? r.json() : Promise.reject(new Error(`HTTP ${r.status}`));

    return Promise.all([
        fetch(`${API}/Facturas/facturas/sucursal/${sesion.nroSucursal}`).then(leer),
        fetch(`${API}/Entregas/entregas/sedes/${sesion.nroSucursal}`).then(leer)
    ]).then(([facturas, entregas]) => {

        const mapa = new Map();
        const obtener = nombre => {
            if (!mapa.has(nombre))
            {
                mapa.set(nombre, { entradas: 0, salidas: 0, detalle: [] });
            }
            return mapa.get(nombre);
        };

        facturas.filter(f => esDelMes(f.fecha)).forEach(f => {
            f.detalleFacturasDto.forEach(df => {
                const m = obtener(df.nombreMed ?? df.nombreProd ?? "Sin nombre");
                m.salidas += df.cantidad;
                m.detalle.push({ tipo: "Venta", ref: `Factura #${f.nroFactura}`, fecha: f.fecha, cantidad: -df.cantidad });
            });
        });

        entregas.filter(e => esDelMes(e.fecha)).forEach(e => {
            e.detalleEntregasDtos.forEach(de => {
                const m = obtener(de.nombreMedicamento ?? de.nombreProducto ?? "Sin nombre");
                m.entradas += de.cantidad;
                m.detalle.push({ tipo: "Entrega", ref: `Entrega #${e.nroEntrega}`, fecha: e.fecha, cantidad: de.cantidad });
            });
        });

        return mapa;
    });
}

function crearIndicadorMovimiento(mov)
{
    const $span = document.createElement("span");

    if (!mov || (mov.entradas === 0 && mov.salidas === 0))
    {
        $span.className = "text-body-secondary";
        $span.textContent = "Sin movimientos";
        return $span;
    }

    const neto = mov.entradas - mov.salidas;
    const $icono = document.createElement("i");

    if (neto > 0)
    {
        $span.className = "text-success fw-semibold";
        $icono.className = "bi bi-arrow-up";
    }
    else if (neto < 0)
    {
        $span.className = "text-danger fw-semibold";
        $icono.className = "bi bi-arrow-down";
    }
    else
    {
        $span.className = "text-body-secondary fw-semibold";
        $icono.className = "bi bi-dash";
    }

    $span.title = `Recibido: ${mov.entradas} - Vendido: ${mov.salidas}`;
    $span.appendChild($icono);
    $span.append(` ${neto > 0 ? "+" : ""}${neto}`);

    return $span;
}

function mostrarDetalleMovimiento(nombre, mov)
{
    const $contenido = document.createElement("div");

    if (!mov || mov.detalle.length === 0)
    {
        $contenido.textContent = "No hubo movimientos de este producto este mes.";
    }
    else
    {
        const $tabla = document.createElement("table");
        $tabla.className = "table table-sm text-start mb-0";

        const $cabecera = $tabla.createTHead().insertRow();
        ["Fecha", "Tipo", "Comprobante", "Cantidad"].forEach(t => {
            const $th = document.createElement("th");
            $th.textContent = t;
            $cabecera.appendChild($th);
        });

        const $cuerpo = $tabla.createTBody();

        [...mov.detalle]
            .sort((a, b) => new Date(b.fecha) - new Date(a.fecha))
            .forEach(d => {
                const $fila = $cuerpo.insertRow();
                $fila.insertCell().textContent = new Date(d.fecha).toLocaleDateString("es-AR");
                $fila.insertCell().textContent = d.tipo;
                $fila.insertCell().textContent = d.ref;

                const $cant = $fila.insertCell();
                $cant.textContent = d.cantidad > 0 ? `+${d.cantidad}` : `${d.cantidad}`;
                $cant.className = d.cantidad > 0 ? "text-success" : "text-danger";
            });

        $contenido.appendChild($tabla);
    }

    Swal.fire({
        title: nombre,
        html: $contenido,
        confirmButtonText: "Cerrar"
    });
}

function cargarMovimiento()
{
    // Reconstruyendo dinámicamente cartas
    // Estoy sufriendo...

    let $cartas = document.getElementById("cartas");

    $cartas.replaceChildren();

    // Encabezado

    let $divCardHeader = document.createElement("div");
    $divCardHeader.className = "card-header border-0";

    let $tituloCarta = document.createElement("h3");
    $tituloCarta.className = "card-title";
    $tituloCarta.textContent = "Movimiento De Inventario";

    $divCardHeader.appendChild($tituloCarta);

    let $divCardTools = document.createElement("div");
    $divCardTools.className = "card-tools";

    let $enlaceDownload = document.createElement("a");
    $enlaceDownload.href = "#";
    $enlaceDownload.className = "btn btn-tool btn-sm";
    $enlaceDownload.ariaLabel = "Download";

    let $iconoDownload = document.createElement("i");
    $iconoDownload.className = "bi bi-download";

    let $enlaceOpciones = document.createElement("a");
    $enlaceOpciones.href = "#";
    $enlaceOpciones.className = "btn btn-tool btn-sm";
    $enlaceOpciones.ariaLabel = "More options";

    let $iconoOpciones = document.createElement("i");
    $iconoOpciones.className = "bi bi-list";

    $enlaceDownload.appendChild($iconoDownload);
    $enlaceOpciones.appendChild($iconoOpciones);

    $divCardTools.appendChild($enlaceDownload);
    $divCardTools.appendChild($enlaceOpciones);

    $divCardHeader.appendChild($divCardTools);

    $cartas.appendChild($divCardHeader);

    // Cuerpo Tarjeta

    let $divCardBody = document.createElement("div");
    $divCardBody.className = "card-body table-responsive p-0";
    $divCardBody.style.maxHeight = "420px";
    $divCardBody.style.overflowY = "auto";

    // Tabla

    let $tabla = document.createElement("table");
    $tabla.className = "table table-striped align-middle";

    // Encabezado Tabla

    let $tablaEncabezado = document.createElement("thead");

    let $tablaEncabezadoFilas = document.createElement("tr");

    let $tablaEncabezadoCeldaProducto = document.createElement("th");
    $tablaEncabezadoCeldaProducto.textContent = "Producto";

    let $tablaEncabezadoCeldaStock = document.createElement("th");
    $tablaEncabezadoCeldaStock.textContent = "Stock";

    let $tablaEncabezadoCeldaMovimiento = document.createElement("th");
    $tablaEncabezadoCeldaMovimiento.textContent = "Movimiento del mes";

    let $tablaEncabezadoCeldaVerMovimiento = document.createElement("th");
    $tablaEncabezadoCeldaVerMovimiento.textContent = "Ver Movimiento";

    $tablaEncabezadoFilas.appendChild($tablaEncabezadoCeldaProducto);
    $tablaEncabezadoFilas.appendChild($tablaEncabezadoCeldaStock);
    $tablaEncabezadoFilas.appendChild($tablaEncabezadoCeldaMovimiento);
    $tablaEncabezadoFilas.appendChild($tablaEncabezadoCeldaVerMovimiento);

    $tablaEncabezado.appendChild($tablaEncabezadoFilas);

    $tabla.appendChild($tablaEncabezado);

    // Cuerpo Tabla

    let $cuerpoTabla = document.createElement("tbody");

    $tabla.appendChild($cuerpoTabla);

    // Insertar tabla dentro del cuerpo de la tarjeta

    $divCardBody.appendChild($tabla);

    $cartas.appendChild($divCardBody);

    // Obtener inventarios

    let url = `${API}/Inventarios/Inventarios/sucursal/${sesion.nroSucursal}`;

    Promise.all([
        fetch(url).then(r => r.ok ? r.json() : null),
        obtenerMovimientosDelMes()
    ])
        .then(([d, movimientos]) => {

            if (d === null)
            {
                Swal.fire("Lo sentimos, hubo un error");
                return;
            }

            d.forEach(inventario => {

            inventario.detalleInventarios.forEach(i => {

                let $filasTablas = document.createElement("tr");

                // Celda Producto

                let $filaProductoTabla = document.createElement("td");

                let $contenidoFilaProductoTabla = document.createElement("p");

                $contenidoFilaProductoTabla.textContent =
                    i.nomProducto ?? i.nomMedicamento ?? "Sin nombre";

                $filaProductoTabla.appendChild($contenidoFilaProductoTabla);

                // Celda Stock

                let $filaStockTabla = document.createElement("td");

                let $contenidoFilaStockTabla = document.createElement("p");
                $contenidoFilaStockTabla.textContent = `${i.stock}`;

                $filaStockTabla.appendChild($contenidoFilaStockTabla);

                // Celda Movimiento del mes (subida / bajada)

                let nombreProducto = i.nomProducto ?? i.nomMedicamento ?? "Sin nombre";
                let mov = movimientos.get(nombreProducto);

                let $filaMovimientoTabla = document.createElement("td");
                $filaMovimientoTabla.appendChild(crearIndicadorMovimiento(mov));

                // Celda Ver Movimiento

                let $filaBusquedaTabla = document.createElement("td");

                let $contenidoFilaBusquedaTabla = document.createElement("a");
                $contenidoFilaBusquedaTabla.href = "#";
                $contenidoFilaBusquedaTabla.className = "text-body-secondary";
                $contenidoFilaBusquedaTabla.ariaLabel = "View product details";

                let $iconoLupa = document.createElement("i");
                $iconoLupa.className = "bi bi-search";

                $contenidoFilaBusquedaTabla.appendChild($iconoLupa);
                $contenidoFilaBusquedaTabla.addEventListener("click", e => {
                    e.preventDefault();
                    mostrarDetalleMovimiento(nombreProducto, mov);
                });

                $filaBusquedaTabla.appendChild($contenidoFilaBusquedaTabla);

                // Armar fila

                $filasTablas.appendChild($filaProductoTabla);
                $filasTablas.appendChild($filaStockTabla);
                $filasTablas.appendChild($filaMovimientoTabla);
                $filasTablas.appendChild($filaBusquedaTabla);

                // Agregar fila al tbody

                $cuerpoTabla.appendChild($filasTablas);

            });

        });

    })
    .catch(error => {
        console.error(error);
        Swal.fire("Lo sentimos, hubo un error al cargar los inventarios");
    });
}


// ---------------- Ventas (facturas de la sucursal)

function cargarVentas()
{
    fetch(`${API}/Facturas/facturas/sucursal/${sesion.nroSucursal}`)
        .then(r => r.ok ? r.json() : null)
        .then(facturas => {

            if (facturas === null)
            {
                Swal.fire("Lo sentimos, hubo un error al cargar las facturas");
                return;
            }

            const hoy = new Date();
            const anio = hoy.getFullYear();
            const mes = hoy.getMonth();

            const diasMesActual = new Date(anio, mes + 1, 0).getDate();
            const diasMesPasado = new Date(anio, mes, 0).getDate();
            const mesPasado = new Date(anio, mes - 1, 1);

            const cantidadDias = Math.max(diasMesActual, diasMesPasado);

            const porDiaActual = new Array(cantidadDias).fill(0);
            const porDiaPasado = new Array(cantidadDias).fill(0);

            let totalActual = 0;
            let totalPasado = 0;

            facturas.forEach(f => {
                const fecha = new Date(f.fecha);

                if (fecha.getFullYear() === anio && fecha.getMonth() === mes)
                {
                    porDiaActual[fecha.getDate() - 1]++;
                    totalActual++;
                }
                else if (fecha.getFullYear() === mesPasado.getFullYear() &&
                         fecha.getMonth() === mesPasado.getMonth())
                {
                    porDiaPasado[fecha.getDate() - 1]++;
                    totalPasado++;
                }
            });

            // Los días que todavía no llegaron no se dibujan
            for (let i = hoy.getDate(); i < cantidadDias; i++)
            {
                porDiaActual[i] = null;
            }

            // Total y variación respecto al mes pasado

            document.getElementById("Ventas_totales").textContent = totalActual;

            const $variacion = document.getElementById("subida_ventas");
            $variacion.replaceChildren();

            if (totalPasado === 0)
            {
                $variacion.className = "text-body-secondary";
                $variacion.textContent = "Sin datos";
            }
            else
            {
                const porcentaje = ((totalActual - totalPasado) / totalPasado) * 100;
                const sube = porcentaje >= 0;

                $variacion.className = sube ? "text-success-emphasis" : "text-danger-emphasis";

                const $flecha = document.createElement("i");
                $flecha.className = sube ? "bi bi-arrow-up" : "bi bi-arrow-down";

                $variacion.appendChild($flecha);
                $variacion.append(` ${Math.abs(porcentaje).toFixed(1)}%`);
            }

            // Gráfico de líneas

            const { areaFill } = globalThis.lteChartTheme;

            new Chart(document.getElementById("visitors-chart"), {
                type: "line",
                data: {
                    labels: Array.from({ length: cantidadDias }, (_, i) => i + 1),
                    datasets: [
                        {
                            label: "Este mes",
                            data: porDiaActual,
                            borderColor: "#0d6efd",
                            backgroundColor: areaFill("#0d6efd"),
                            pointBackgroundColor: "#0d6efd",
                            fill: true,
                            tension: 0.4
                        },
                        {
                            label: "Mes pasado",
                            data: porDiaPasado,
                            borderColor: "#adb5bd",
                            backgroundColor: areaFill("#adb5bd"),
                            pointBackgroundColor: "#adb5bd",
                            fill: true,
                            tension: 0.4
                        }
                    ]
                },
                options: {
                    plugins: { legend: { display: false } },
                    scales: { y: { beginAtZero: true, ticks: { precision: 0 } } }
                }
            });
        })
        .catch(error => {
            console.error(error);
            Swal.fire("Lo sentimos, hubo un error al cargar las ventas");
        });
}

// ---------------- Entregas (gráfico circular de productos recibidos este mes)

function cargarEntregas()
{
    const hoy = new Date();

    document.getElementById("entregas-periodo").textContent =
        hoy.toLocaleDateString("es-AR", { month: "long", year: "numeric" });

    fetch(`${API}/Entregas/entregas/sedes/${sesion.nroSucursal}`)
        .then(r => r.ok ? r.json() : null)
        .then(entregas => {

            if (entregas === null)
            {
                Swal.fire("Lo sentimos, hubo un error al cargar las entregas");
                return;
            }

            // Cantidad recibida por producto en el mes actual

            const porProducto = new Map();

            entregas.forEach(e => {
                const fecha = new Date(e.fecha);

                if (fecha.getFullYear() !== hoy.getFullYear() || fecha.getMonth() !== hoy.getMonth())
                {
                    return;
                }

                e.detalleEntregasDtos.forEach(de => {
                    const nombre = de.nombreMedicamento ?? de.nombreProducto ?? "Sin nombre";
                    porProducto.set(nombre, (porProducto.get(nombre) ?? 0) + de.cantidad);
                });
            });

            if (porProducto.size === 0)
            {
                document.getElementById("entregas-chart").classList.add("d-none");
                document.getElementById("entregas-vacio").classList.remove("d-none");
                return;
            }

            // Los 5 productos con más cantidad; el resto se agrupa en "Otros"

            let items = [...porProducto.entries()].sort((a, b) => b[1] - a[1]);

            if (items.length > 6)
            {
                const resto = items.slice(5).reduce((suma, [, cant]) => suma + cant, 0);
                items = [...items.slice(0, 5), ["Otros", resto]];
            }

            new Chart(document.getElementById("entregas-chart"), {
                type: "doughnut",
                data: {
                    labels: items.map(([nombre]) => nombre),
                    datasets: [
                        {
                            label: "Unidades",
                            data: items.map(([, cant]) => cant),
                            backgroundColor: ["#0d6efd", "#20c997", "#ffc107", "#d63384", "#6f42c1", "#adb5bd"]
                        }
                    ]
                },
                options: {
                    cutout: "65%",
                    plugins: { legend: { position: "bottom" } }
                }
            });
        })
        .catch(error => {
            console.error(error);
            Swal.fire("Lo sentimos, hubo un error al cargar las entregas");
        });
}

document.addEventListener("DOMContentLoaded", () => {

    // Sin sesión no hay sucursal: volvemos al login

    if (sesion === null)
    {
        window.location.replace("../html/login.html");
        return;
    }

    mostrarDatosEmpleado();
    cargarMovimiento();
    cargarVentas();
    cargarEntregas();
});