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

function buscarFacturaPorSucursal()
{
    let $cuerpoTabla = document.getElementById("cuerpo_tabla");
    $cuerpoTabla.replaceChildren();

    const $lstOpciones = document.getElementById("opciones-sucursales");

    let idSucursal = parseInt($lstOpciones.value);

    let url = `https://localhost:7032/api/Facturas/facturas/sucursal/${idSucursal}`;

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
                Swal.fire("Lo sentimos, hubo problemas cargando las facturas");
                return;
            }

            d.forEach(f => {
                
                f.detalleFacturasDto.forEach(df => {
                    let $fila = document.createElement("tr");

                    //Nro. Factura
                    let $celdaNroFactura = document.createElement("td");
                    $celdaNroFactura.textContent = f.nroFactura;

                    //Sucursal
                    let $celdaSucursal = document.createElement("td");
                    $celdaSucursal.textContent = f.nombsede;

                    //Fecha
                    let $celdaFecha = document.createElement("td");
                    $celdaFecha.textContent = dayjs(f.fecha).format("DD/MM/YYYY");

                    //Cod. Empleado
                    let $celdaCodEmpleado = document.createElement("td");
                    $celdaCodEmpleado.textContent = f.idEmpleado;

                    //Cod. Forma Pago
                    let $celdaCodFormaPago = document.createElement("td");
                    $celdaCodFormaPago.textContent = f.idFormaPago;

                    //Medicamento/Producto
                    let $celdaMedProd = document.createElement("td");
                    $celdaMedProd.textContent =
                    df.nombreMed != null? df.nombreMed : df.nombreProd;

                    //Cantidad
                    let $celdaCant = document.createElement("td");
                    $celdaCant.textContent = df.cantidad;

                    //Agregar celdas a la fila
                    $fila.appendChild($celdaNroFactura);
                    $fila.appendChild($celdaSucursal);
                    $fila.appendChild($celdaFecha);
                    $fila.appendChild($celdaCodEmpleado);
                    $fila.appendChild($celdaCodFormaPago);
                    $fila.appendChild($celdaMedProd);
                    $fila.appendChild($celdaCant);

                    $cuerpoTabla.appendChild($fila);
                });
            });
        })
}

function cargarFactura()
{
    let $cuerpoTabla = document.getElementById("cuerpo_tabla");
    $cuerpoTabla.replaceChildren();

    const $inputNroFactura = document.getElementById("input-nro-factura");
    let nroFactura = $inputNroFactura.value;

    //isNaN devuelve true si nroFactura no es un número
    if (isNaN(nroFactura))
    {
        Swal.fire("El número de factura debe ser un número entero positivo");
        return;
    }

    let url = `https://localhost:7032/api/Facturas/facturas/${nroFactura}`;

    fetch(url)
        .then(r => {

            if (!r.ok)
            {
                return null;
            }

            return r.json();
        })
        .then(d => {

            if (d === null || d === undefined)
            {
                Swal.fire("Lo sentimos, hubo problemas cargando la factura");
                return;
            }

            d.detalleFacturasDto.forEach(df => {

                let $fila = document.createElement("tr");

                //Nro. Factura
                let $celdaNroFactura = document.createElement("td");
                $celdaNroFactura.textContent = d.nroFactura;

                //Sucursal
                let $celdaSucursal = document.createElement("td");
                $celdaSucursal.textContent = d.nombsede;

                //Fecha
                let $celdaFecha = document.createElement("td");
                $celdaFecha.textContent = dayjs(d.fecha).format("DD/MM/YYYY");

                //Cod. Empleado
                let $celdaCodEmpleado = document.createElement("td");
                $celdaCodEmpleado.textContent = d.idEmpleado;

                //Cod. Forma Pago
                let $celdaCodFormaPago = document.createElement("td");
                $celdaCodFormaPago.textContent = d.idFormaPago;

                //Medicamento/Producto
                let $celdaMedProd = document.createElement("td");
                $celdaMedProd.textContent =
                    df.nombreMed != null ? df.nombreMed : df.nombreProd;

                //Cantidad
                let $celdaCant = document.createElement("td");
                $celdaCant.textContent = df.cantidad;

                $fila.appendChild($celdaNroFactura);
                $fila.appendChild($celdaSucursal);
                $fila.appendChild($celdaFecha);
                $fila.appendChild($celdaCodEmpleado);
                $fila.appendChild($celdaCodFormaPago);
                $fila.appendChild($celdaMedProd);
                $fila.appendChild($celdaCant);

                $cuerpoTabla.appendChild($fila);
            });
        })
        .catch(error => {
            console.error(error);
            Swal.fire("Lo sentimos, hubo problemas cargando la factura");
        });
}

function cargarFacturas()
{
    let $cuerpoTabla = document.getElementById("cuerpo_tabla");
    $cuerpoTabla.replaceChildren();

    let url = "https://localhost:7032/api/Facturas/facturas";

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
                Swal.fire("Lo sentimos, hubo problemas cargando las facturas");
                return;
            }

            d.forEach(f => {
                
                f.detalleFacturasDto.forEach(df => {
                    let $fila = document.createElement("tr");

                    //Nro. Factura
                    let $celdaNroFactura = document.createElement("td");
                    $celdaNroFactura.textContent = f.nroFactura;

                    //Sucursal
                    let $celdaSucursal = document.createElement("td");
                    $celdaSucursal.textContent = f.nombsede;

                    //Fecha
                    let $celdaFecha = document.createElement("td");
                    $celdaFecha.textContent = dayjs(f.fecha).format("DD/MM/YYYY");

                    //Cod. Empleado
                    let $celdaCodEmpleado = document.createElement("td");
                    $celdaCodEmpleado.textContent = f.idEmpleado;

                    //Cod. Forma Pago
                    let $celdaCodFormaPago = document.createElement("td");
                    $celdaCodFormaPago.textContent = f.idFormaPago;

                    //Medicamento/Producto
                    let $celdaMedProd = document.createElement("td");
                    $celdaMedProd.textContent =
                    df.nombreMed != null ? df.nombreMed : df.nombreProd;

                    //Cantidad
                    let $celdaCant = document.createElement("td");
                    $celdaCant.textContent = df.cantidad;

                    //Agregar celdas a la fila
                    $fila.appendChild($celdaNroFactura);
                    $fila.appendChild($celdaSucursal);
                    $fila.appendChild($celdaFecha);
                    $fila.appendChild($celdaCodEmpleado);
                    $fila.appendChild($celdaCodFormaPago);
                    $fila.appendChild($celdaMedProd);
                    $fila.appendChild($celdaCant);

                    $cuerpoTabla.appendChild($fila);
                });
            });
        })
}

document.getElementById("icono_lupa").addEventListener("click", cargarFactura)
document.addEventListener("DOMContentLoaded", () => {
    
    if (sesion === null)
    {
        window.location.replace("../html/login.html");
        return;
    }
    mostrarDatosEmpleado();
    cargarSucursales("opciones-sucursales");
    cargarFacturas();
});

document.getElementById("opciones-sucursales").addEventListener("change", buscarFacturaPorSucursal);
