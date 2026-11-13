function crearFactura()
{
    let exito = validarDatos();

    if (!exito)
    {
        return;
    }
    
    let url = "https://localhost:7032/api/Facturas/facturas/crear";

    let putaFactura = obtenerFactura();

    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(putaFactura)
    })
    .then(r => {
        
        if (r.status === 404 || r.status === 400)
        {
            //Para que aparezcan mensajes personalizados que manda la API
            //ARREGLAR

            return r.json()
                .then(d => ({
                error: true,
                message: d.message ||
                Object.values(d.errors ?? {}).flat().join("\n") ||
                "Error al procesar la solicitud"
            }));
        }

        if (!r.ok)
        {
            return false;
        }

        return r.json();
    })
    .then(d => {
        if (d === false)
        {
            Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: `Lo sentimos, no se pudo guardar la factura`
            });

            return;
        }

        if (d.error)
        {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: d.message
            });

            return;
        }

        return Swal.fire({
            icon: "success",
            title: "Exito",
            text: "Se pudo guardar la factura"
        });
    });
}

function validarDatos()
{
    let putaFactura = obtenerFactura();
    
    if (!putaFactura.fecha)
    {
        Swal.fire("Ingrese una fecha");
        return false;
    }

    let fechaActual = dayjs();

    if (dayjs(putaFactura.fecha).isAfter(fechaActual))
    {
        Swal.fire("La fecha y hora deben ser anteriores o iguales a las actuales");
        return false;
    }

    if (!putaFactura.idEmpleado)
    {
        Swal.fire("Ingrese el ID del empleado");
        return false;
    }

    if (putaFactura.idEmpleado <= 0)
    {
        Swal.fire("El ID del empleado debe ser mayor a 0");
        return false;
    }

    if (!Number.isInteger(putaFactura.idEmpleado))
    {
        Swal.fire("El ID del empleado debe ser un entero");
        return false;
    }

    if (!putaFactura.idCliente)
    {
        Swal.fire("Ingrese el ID del cliente");
        return false;
    }

    if (putaFactura.idCliente <= 0)
    {
        Swal.fire("El ID del cliente debe ser mayor a 0");
        return false;
    }

    if (!Number.isInteger(putaFactura.idCliente))
    {
        Swal.fire("El ID del cliente debe ser un entero");
        return false;
    }

    if (!putaFactura.idFormaPago)
    {
        Swal.fire("Ingrese el ID de la forma de pago");
        return false;
    }

    if (putaFactura.idFormaPago <= 0)
    {
        Swal.fire("El id de la forma de pago debe ser mayor a 0");
        return false;
    }

    if (!Number.isInteger(putaFactura.idFormaPago))
    {
        Swal.fire("El ID de la forma de pago debe ser un entero");
        return false;
    }

    let mensaje = "";

    putaFactura.detalleFacturasDtos.forEach(df => {
        if ((df.codMedicamento === null && df.codProducto === null) ||
            (df.codMedicamento !== null && df.codProducto !== null))
        {
            mensaje = "Elija medicamento o producto";
            return;
        }

        if (df.preUnitario <= 0 || isNaN(df.preUnitario))
        {
            mensaje = "El precio unitario debe ser un número positivo";
            return;
        }

        if (df.cantidad <= 0 || !Number.isInteger(df.cantidad))
        {
            mensaje = "La cantidad debe ser un número positivo entero";
            return;
        }

        if (df.descuentoPorObraSocial < 0 || isNaN(df.descuentoPorObraSocial))
        {
            mensaje = "El descuento debe ser un número positivo o 0";
            return;
        }
    });

    if (mensaje !== "")
    {
        Swal.fire(mensaje);
        return false;
    }

    return true;
}

function obtenerFactura()
{
    const $inputSucursal = document.getElementById("opciones-sucursales");
    const $inputFechaHora = document.getElementById("fechaFactura");
    const $inputIdEmpleado = document.getElementById("idEmpleado");
    const $inputIdCliente = document.getElementById("idCliente");
    const $inputIdFormaPago = document.getElementById("idFormaPago");
    const $inputCodMed = document.getElementById("opciones-medicamentos");
    const $inputCodProd = document.getElementById("opciones-productos");
    const $inputPreUnitario = document.getElementById("preUnitario");
    const $inputCant = document.getElementById("cantidad");
    const $inputDescuento = document.getElementById("descuento");
    const $cbkReembolsado = document.getElementById("ckbReembolsado");

    let idSucursal = Number($inputSucursal.value);
    let fechaHora = $inputFechaHora.value;
    let idEmpleado = Number($inputIdEmpleado.value);
    let idCliente = Number($inputIdCliente.value);
    let idFormaPago = Number($inputIdFormaPago.value);

    let codMed = null;

    if ($inputCodMed.value !== "")
    {
        codMed = Number($inputCodMed.value);
    };
    
    let codProd = null;

    if ($inputCodProd.value !== "")
    {
        codProd = Number($inputCodProd.value);
    }

    let preUnitario = Number($inputPreUnitario.value);
    let cant = Number($inputCant.value);
    let descuento = Number($inputDescuento.value);
    let ckbReembolsado = $cbkReembolsado.checked;

    let detalleFacturasDtos = {
        "codMedicamento": codMed,
        "codProducto": codProd,
        "preUnitario": preUnitario,
        "cantidad": cant,
        "descuentoPorObraSocial": descuento,
        "reembolsado": ckbReembolsado
    };
    
    let putaFactura = {
        "nroSucursal": idSucursal,
        "fecha": fechaHora,
        "idEmpleado": idEmpleado,
        "idCliente": idCliente,
        "idFormaPago": idFormaPago,
        detalleFacturasDtos: [detalleFacturasDtos]
    };

    return putaFactura;
}

document.getElementById("btn-crear-factura").addEventListener("click", crearFactura);
document.addEventListener("DOMContentLoaded", () => {
    cargarSucursales("opciones-sucursales"),
    buscarMedicamentos("opciones-medicamentos"),
    buscarProductos("opciones-productos")
});