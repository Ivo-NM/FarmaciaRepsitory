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
        
        if (r.status === 400)
        {
            return false;
        }
        
        if (!r.ok)
        {
            return null;
        }

        return r.json();
    })
    .then(d => {
        
        console.log(d);
        
        if (d === false)
        {
            return Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: d.message
                    });
        }

        if (d === null)
        {
            return Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: "Lo sentimos, no se pudo guardar la factura"
            });
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
    const $inputFechaHora = document.getElementById("fechaFactura");
    const $inputIdEmpleado = document.getElementById("idEmpleado");
    const $inputIdCliente = document.getElementById("idCliente");
    const $inputIdFormaPago = document.getElementById("idFormaPago");
    const $inputCodMed = document.getElementById("codMedicamento");
    const $inputCodProd = document.getElementById("codProducto");
    const $inputPreUnitario = document.getElementById("preUnitario");
    const $inputCant = document.getElementById("cantidad");
    const $inputDescuento = document.getElementById("descuento");
    const $cbkReembolsado = document.getElementById("ckbReembolsado");

    let fechaHora = dayjs($inputFechaHora.value);
    let fechaActual = dayjs();

    if ($inputFechaHora.value.trim() === "")
    {
        Swal.fire("Debe ingresar una fecha");
        return false;
    }

    if (fechaHora.isAfter(fechaActual))
    {
        Swal.fire("La fecha de factura no puede ser posterior a la fecha actual");
        return false;
    }

    let idEmpleado = Number($inputIdEmpleado.value);

    //Number.isInteger, comprueba que el nro sea entero

    if ($inputIdEmpleado.value.trim() === "")
    {
        Swal.fire("Es necesario el empleado");
        return false;
    }

    if (!Number.isInteger(idEmpleado) || idEmpleado <= 0)
    {
        Swal.fire("El Id de empleado debe ser un número entero positivo");
        return false;
    }

    let idCliente = Number($inputIdCliente.value);

    if ($inputIdCliente.value.trim() === "")
    {
        Swal.fire("Es necesario el cliente");
        return false;
    }
    //Number.isInteger, comprueba que el nro sea entero

    if (!Number.isInteger(idCliente) || idCliente <= 0)
    {
        Swal.fire("El Id del cliente debe ser un número entero positivo");
        return false;
    }
    
    let idFormaPago = Number($inputIdFormaPago.value);

    //Number.isInteger, comprueba que el nro sea entero

    if ($inputIdFormaPago.value.trim() === "")
    {
        Swal.fire("Es necesario la forma de pago");
        return false;
    }

    if (!Number.isInteger(idFormaPago) || idFormaPago <= 0)
    {
        Swal.fire("El de la forma de pago debe ser un número entero positivo");
        return false;
    }

    let codMed = Number($inputCodMed.value);
    let codProd = Number($inputCodProd.value);

    //Number.isInteger comprueba que el número sea entero

    if (($inputCodMed.value.trim() !== "" && (!Number.isInteger(codMed) || codMed <= 0)) ||
        ($inputCodProd.value.trim() !== "" && (!Number.isInteger(codProd) || codProd <= 0)))
    {
        Swal.fire("Los códigos de los productos y medicamentos deben ser números enteros positivos");
        return false;
    }

    if (($inputCodMed.value.trim() === "" && $inputCodProd.value.trim() === "") ||
        ($inputCodMed.value.trim() !== "" && $inputCodProd.value.trim() !== ""))
    {
        Swal.fire("Coloque un producto o medicamento");
        return false;
    }

    let preUnitario = Number($inputPreUnitario.value);

    if ($inputPreUnitario.value.trim() === "")
    {
        Swal.fire("Es necesario el precio unitario");
        return false;
    }

    if (isNaN(preUnitario))
    {
        Swal.fire("El precio unitario debe ser un número entero positivo");
        return false;
    }

    let cant = Number($inputCant.value);

    //Number.isInteger, comprueba que el nro sea entero

    if ($inputCant.value.trim() === "")
    {
        Swal.fire("Es necesaria la cantidad");
        return false;
    }

    if (!Number.isInteger(cant) || cant <= 0)
    {
        Swal.fire("La cantidad debe ser un número entero positivo");
        return false;
    }

    let descuento = Number($inputDescuento.value);

    if ($inputDescuento.value.trim() === "")
    {
        Swal.fire("Es necesario el descuento por obra social, aunque sea 0");
        return false;
    }

    if (isNaN(descuento) || descuento < 0)
    {
        Swal.fire("El descuento debe ser un número entero positivo");
        return false;
    }
}

function cargarSucursales()
{
    const $lstSucursales = document.getElementById("opciones-sucursales");
    $lstSucursales.replaceChildren();

    let url = "https://localhost:7032/api/Sucursales/sucursales";

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
                Swal.fire("Lo sentimos, hubo un problema cargando las sucursales");
                return;
            }

            d.forEach(s => {
                let $opcionSucursal = document.createElement("option");
                $opcionSucursal.value = s.nroSucursal;
                $opcionSucursal.textContent = s.nomSede;

                $lstSucursales.appendChild($opcionSucursal);
            })
        })
}

function obtenerFactura()
{
    const $inputSucursal = document.getElementById("opciones-sucursales");
    const $inputFechaHora = document.getElementById("fechaFactura");
    const $inputIdEmpleado = document.getElementById("idEmpleado");
    const $inputIdCliente = document.getElementById("idCliente");
    const $inputIdFormaPago = document.getElementById("idFormaPago");
    const $inputCodMed = document.getElementById("codMedicamento");
    const $inputCodProd = document.getElementById("codProducto");
    const $inputPreUnitario = document.getElementById("preUnitario");
    const $inputCant = document.getElementById("cantidad");
    const $inputDescuento = document.getElementById("descuento");
    const $cbkReembolsado = document.getElementById("ckbReembolsado");

    let idSucursal = Number($inputSucursal.value);
    let fechaHora = $inputFechaHora.value;
    let idEmpleado = Number($inputIdEmpleado.value);
    let idCliente = Number($inputIdCliente.value);
    let idFormaPago = Number($inputIdFormaPago.value);
    let codMed = Number($inputCodMed.value);
    let codProd = Number($inputCodProd.value);
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
document.addEventListener("DOMContentLoaded", cargarSucursales);