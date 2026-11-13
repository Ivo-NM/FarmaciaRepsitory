function obtenerEntrega()
{
    const $nroSucursal = document.getElementById("opciones-sucursales");
    const $fecha = document.getElementById("fechaEntrega");
    const $firmaEmpresa = document.getElementById("firmaEmpresa");
    const $respuestaEntrega = document.getElementById("respuestaEntrega");
    const $idEmpleado = document.getElementById("idEmpleado");
    const $codMed = document.getElementById("opciones-medicamentos");
    const $codProd = document.getElementById("opciones-productos");
    const $cant = document.getElementById("cantidad");

    let nroSucursal = Number($nroSucursal.value);
    let fecha = $fecha.value;
    let firmaEmpresa = $firmaEmpresa.value;
    let respuestaEntrega = $respuestaEntrega.value;
    let idEmpleado = Number($idEmpleado.value);

    let codMed = null;
    if ($codMed.value !== "") 
    {
        codMed = Number($codMed.value);
    }

    let codProd = null;
    if ($codProd.value !== "") 
    {
        codProd = Number($codProd.value);
    }

    let cant = Number($cant.value);

    let detalleEntrega = 
    {
        codMed: codMed,
        codProd: codProd,
        cant: cant
    };

    let putaEntrega = 
    {
        nroSucursal: nroSucursal,
        fecha: fecha,
        firmaEmpresa: firmaEmpresa,
        respuestaEntrega: respuestaEntrega,
        idEmpleado: idEmpleado,
        detalleEntregas: [detalleEntrega]
    };

    return putaEntrega;
}

document.addEventListener("DOMContentLoaded", () => {
    cargarSucursales("opciones-sucursales");
    buscarMedicamentos("opciones-medicamentos");
    buscarProductos("opciones-productos");
    }
)