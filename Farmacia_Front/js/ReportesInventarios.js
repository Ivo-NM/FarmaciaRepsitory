cargarMovimientoPorFecha();

function cargarMovimientoPorFecha()
{
        let $cartas = document.getElementById("cartas");

        if ($cartas) {
        Swal.fire("Lo sentimos, hubo un error al cargar los inventarios");
    }

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

}

const API = "https://localhost:7032/api";

let url = `${API}/Movimientos/Productos/Vendidos/${$fecha1}/${$fecha2}`;

function cargarMovimientosPorFecha()
{
   const  mov =  fetch(url)

}