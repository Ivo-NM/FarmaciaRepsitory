/*
    <div id="divCardHeader" class="card-header border-0"> ------
        <h3 class="card-title">Movimientos de inventario</h3> -------
    
        <div id="divCardTools" class="card-tools"> ------
            <a href="#" class="btn btn-tool btn-sm" aria-label="Download"> ----
                <i class="bi bi-download"></i> ----
            </a> ---

            <a href="#" class="btn btn-tool btn-sm" aria-label="More options"> ----
                <i class="bi bi-list"></i> ----
            </a> ----
        </div> -----
    </div> -----

    <div id="divCardBody" class="card-body table-responsive p-0"> -----
        <table class="table table-striped align-middle"> ------
            <thead> ------
                <tr> -----
                    <th>Producto</th> -----
                    <th>Stock</th> -----
                    <th>Ver Movimiento</th> -----
                </tr> -----
            </thead> -----

        <tbody> -----
            <tr> ----
                <td> -----
                    <p>Producto/Medicamento</p> ----
                </td> ---

                <td> ----
                    <p>Stock</p> -----
                </td> ----

                <td> -----
                    <a href="#" class="text-body-secondary" aria-label="View product details"> ----
                        <i class="bi bi-search"></i> ----
                    </a> ----
                </td> -----
            </tr> ----
        </tbody> -----
    </table> ---
</div> ---
*/


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

    let $tablaEncabezadoCeldaVerMovimiento = document.createElement("th");
    $tablaEncabezadoCeldaVerMovimiento.textContent = "Ver Movimiento";

    $tablaEncabezadoFilas.appendChild($tablaEncabezadoCeldaProducto);
    $tablaEncabezadoFilas.appendChild($tablaEncabezadoCeldaStock);
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

    let url = "https://localhost:7032/api/Inventarios/Inventarios";

    fetch(url)
        .then(r => {

            if (!r.ok)
            {
                return null;
            }

            return r.json();
        })
        .then(d => {

            if (d === null)
            {
                Swal.fire("Lo sentimos, hubo un error");
                return;
            }

            console.log(d);

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

                // Celda Ver Movimiento

                let $filaBusquedaTabla = document.createElement("td");

                let $contenidoFilaBusquedaTabla = document.createElement("a");
                $contenidoFilaBusquedaTabla.href = "#";
                $contenidoFilaBusquedaTabla.className = "text-body-secondary";
                $contenidoFilaBusquedaTabla.ariaLabel = "View product details";

                let $iconoLupa = document.createElement("i");
                $iconoLupa.className = "bi bi-search";

                $contenidoFilaBusquedaTabla.appendChild($iconoLupa);

                $filaBusquedaTabla.appendChild($contenidoFilaBusquedaTabla);

                // Armar fila

                $filasTablas.appendChild($filaProductoTabla);
                $filasTablas.appendChild($filaStockTabla);
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


document.addEventListener("DOMContentLoaded", cargarMovimiento)