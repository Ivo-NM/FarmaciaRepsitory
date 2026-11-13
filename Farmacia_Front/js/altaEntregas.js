function validarDatos()
{
    let putaEntrega = obtenerEntrega();

    if (!putaEntrega.fecha)
    {
        Swal.fire("Ingrese una fecha");
        return false;
    }

    let fechaActual = dayjs();

    if (dayjs(putaEntrega.fecha).isAfter(fechaActual))
    {
        Swal.fire("La fecha y hora deben ser anteriores o iguales a las actuales");
        return false;
    }

    if (!putaEntrega.firmaEmpresa) 
    {
        Swal.fire("Ingrese firma de empresa");
        return false;
    }

    if (!/^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$/.test(putaEntrega.firmaEmpresa)) 
    {
        Swal.fire("La firma de la empresa solo debe contener letras y espacios");
        return false;
    }

    //Validar respuesta de entrega
    if (!putaEntrega.respuestaEntrega) 
    {
        Swal.fire("Ingrese respuesta de entrega");
        return false;
    }

    //Validar que respuestaEntrega contenga solamente letras y espacios
    if (!/^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$/.test(putaEntrega.respuestaEntrega)) 
    {
        Swal.fire("La respuesta de entrega solo debe contener letras y espacios");
        return false;
    }

    //Validar ID de empleado
    if (!Number.isInteger(putaEntrega.idEmpleado)) 
    {
        Swal.fire("El ID del empleado debe ser un número entero");
        return false;
    }

    if (putaEntrega.idEmpleado <= 0)
    {
        Swal.fire("El ID del empleado debe ser mayor a 0");
        return false;
    }

    let mensaje = "";

    putaEntrega.detalleEntregas.forEach(de => {
        if ((de.codMed.value === "" && de.codProd.value === "") ||
            (de.codMed.value !== "" && de.codProd.value !== ""))
        {
            mensaje = "Coloque un medicamento o un producto";
            return;
        }

        if (de.cant <= 0)
        {
            mensaje = "La cantidad debe ser un número mayor a 0";
            return;
        }

        if (!Number.isInteger(de.cant))
        {
            mensaje = "La cantidad debe ser un número entero positivo";
            return;
        }
    })

    if (mensaje !== "")
    {
        Swal.fire(mensaje);
        return false;
    }

    return true;
}

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