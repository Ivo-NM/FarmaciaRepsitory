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

                let $fila = document.createElement("tr");

                // Nro. Factura
                let $celdaNroFactura = document.createElement("td");
                let $contenidoCeldaNroFactura = document.createElement("p");
                $contenidoCeldaNroFactura.textContent = f.nroFactura;

                // Sucursal
                let $celdaSucursal = document.createElement("td");
                let $contenidoCeldaSucursal = document.createElement("p");
                $contenidoCeldaSucursal.textContent = f.nombsede;

                // Fecha
                let $celdaFecha = document.createElement("td");
                let $contenidoCeldaFecha = document.createElement("p");
                let fecha = dayjs(f.fecha);
                let fechaFormateada = dayjs(fecha).format("DD/MM/YYYY");
                $contenidoCeldaFecha.textContent = fechaFormateada;

                // Cod. Empleado
                let $celdaCodEmpleado = document.createElement("td");
                let $contenidoCeldaEmpleado = document.createElement("p");
                $contenidoCeldaEmpleado.textContent = f.idEmpleado;

                // Cod. Forma Pago
                let $celdaCodFormaPago = document.createElement("td");
                let $contenidoCeldaFormaPago = document.createElement("p");
                $contenidoCeldaFormaPago.textContent = f.idFormaPago;

                $celdaNroFactura.appendChild($contenidoCeldaNroFactura);
                $celdaSucursal.appendChild($contenidoCeldaSucursal);
                $celdaFecha.appendChild($contenidoCeldaFecha);
                $celdaCodEmpleado.appendChild($contenidoCeldaEmpleado);
                $celdaCodFormaPago.appendChild($contenidoCeldaFormaPago);

                $fila.appendChild($celdaNroFactura);
                $fila.appendChild($celdaSucursal);
                $fila.appendChild($celdaFecha);
                $fila.appendChild($celdaCodEmpleado);
                $fila.appendChild($celdaCodFormaPago);

                // Detalles de factura
                f.detalleFacturasDto.forEach(df => {

                    // Medicamento / Producto
                    let $celdaMedProd = document.createElement("td");
                    let $contenidoCeldaMedProd = document.createElement("p");

                    $contenidoCeldaMedProd.textContent =
                        df.nombreMed != null ? df.nombreMed : df.nombreProd;

                    $celdaMedProd.appendChild($contenidoCeldaMedProd);

                    // Cantidad
                    let $celdaCant = document.createElement("td");
                    let $contenidoCeldaCant = document.createElement("p");
                    $contenidoCeldaCant.textContent = df.cantidad;

                    $celdaCant.appendChild($contenidoCeldaCant);

                    $fila.appendChild($celdaMedProd);
                    $fila.appendChild($celdaCant);
                });

                $cuerpoTabla.appendChild($fila);
            });
        })
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

function cargarFactura()
{
    let $cuerpoTabla = document.getElementById("cuerpo_tabla");
    $cuerpoTabla.replaceChildren();

    const $inputNroFactura = document.getElementById("input-nro-factura");
    let nroFactura = $inputNroFactura.value;

    //isNan devuelve true si nroFactura no es un número
    if (isNaN(nroFactura))
    {
        Swal.fire("El número de factura debe ser un número entero positivo");
        return;
    }

    let url = `https://localhost:7032/api/Facturas/facturas/${nroFactura}`;

    fetch(url)
        .then(r => {

            if (r.status === 404)
            {
                Swal.fire("No se encontró la factura");
                return;
            }

            if (!r.ok)
            {
                return null;
            }

            return r.json();
        })
        .then(d => {

            if (d === null)
            {
                Swal.fire("Lo sentimos, hubo problemas cargando la factura");
                return;
            }

            let $fila = document.createElement("tr");

            // Nro. Factura
            let $celdaNroFactura = document.createElement("td");
            let $contenidoCeldaNroFactura = document.createElement("p");
            $contenidoCeldaNroFactura.textContent = d.nroFactura;

            // Sucursal
            let $celdaSucursal = document.createElement("td");
            let $contenidoCeldaSucursal = document.createElement("p");
            $contenidoCeldaSucursal.textContent = d.nombsede;

            // Fecha
            let $celdaFecha = document.createElement("td");
            let $contenidoCeldaFecha = document.createElement("p");
            let fecha = dayjs(d.fecha);
            let fechaFormateada = dayjs(fecha).format("DD/MM/YYYY");
            $contenidoCeldaFecha.textContent = fechaFormateada;

            // Cod. Empleado
            let $celdaCodEmpleado = document.createElement("td");
            let $contenidoCeldaEmpleado = document.createElement("p");
            $contenidoCeldaEmpleado.textContent = d.idEmpleado;

            // Cod. Forma Pago
            let $celdaCodFormaPago = document.createElement("td");
            let $contenidoCeldaFormaPago = document.createElement("p");
            $contenidoCeldaFormaPago.textContent = d.idFormaPago;

            $celdaNroFactura.appendChild($contenidoCeldaNroFactura);
            $celdaSucursal.appendChild($contenidoCeldaSucursal);
            $celdaFecha.appendChild($contenidoCeldaFecha);
            $celdaCodEmpleado.appendChild($contenidoCeldaEmpleado);
            $celdaCodFormaPago.appendChild($contenidoCeldaFormaPago);

            $fila.appendChild($celdaNroFactura);
            $fila.appendChild($celdaSucursal);
            $fila.appendChild($celdaFecha);
            $fila.appendChild($celdaCodEmpleado);
            $fila.appendChild($celdaCodFormaPago);

            // Detalles de factura
            d.detalleFacturasDto.forEach(df => {

                // Medicamento / Producto
                let $celdaMedProd = document.createElement("td");
                let $contenidoCeldaMedProd = document.createElement("p");

                $contenidoCeldaMedProd.textContent =
                    df.nombreMed != null ? df.nombreMed : df.nombreProd;

                $celdaMedProd.appendChild($contenidoCeldaMedProd);

                // Cantidad
                let $celdaCant = document.createElement("td");
                let $contenidoCeldaCant = document.createElement("p");
                $contenidoCeldaCant.textContent = df.cantidad;

                $celdaCant.appendChild($contenidoCeldaCant);

                $fila.appendChild($celdaMedProd);
                $fila.appendChild($celdaCant);
            });

            $cuerpoTabla.appendChild($fila);
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

                let $fila = document.createElement("tr");

                // Nro. Factura
                let $celdaNroFactura = document.createElement("td");
                let $contenidoCeldaNroFactura = document.createElement("p");
                $contenidoCeldaNroFactura.textContent = f.nroFactura;

                // Sucursal
                let $celdaSucursal = document.createElement("td");
                let $contenidoCeldaSucursal = document.createElement("p");
                $contenidoCeldaSucursal.textContent = f.nombsede;

                // Fecha
                let $celdaFecha = document.createElement("td");
                let $contenidoCeldaFecha = document.createElement("p");
                let fecha = dayjs(f.fecha);
                let fechaFormateada = dayjs(fecha).format("DD/MM/YYYY");
                $contenidoCeldaFecha.textContent = fechaFormateada;

                // Cod. Empleado
                let $celdaCodEmpleado = document.createElement("td");
                let $contenidoCeldaEmpleado = document.createElement("p");
                $contenidoCeldaEmpleado.textContent = f.idEmpleado;

                // Cod. Forma Pago
                let $celdaCodFormaPago = document.createElement("td");
                let $contenidoCeldaFormaPago = document.createElement("p");
                $contenidoCeldaFormaPago.textContent = f.idFormaPago;

                $celdaNroFactura.appendChild($contenidoCeldaNroFactura);
                $celdaSucursal.appendChild($contenidoCeldaSucursal);
                $celdaFecha.appendChild($contenidoCeldaFecha);
                $celdaCodEmpleado.appendChild($contenidoCeldaEmpleado);
                $celdaCodFormaPago.appendChild($contenidoCeldaFormaPago);

                $fila.appendChild($celdaNroFactura);
                $fila.appendChild($celdaSucursal);
                $fila.appendChild($celdaFecha);
                $fila.appendChild($celdaCodEmpleado);
                $fila.appendChild($celdaCodFormaPago);

                // Detalles de factura
                f.detalleFacturasDto.forEach(df => {

                    // Medicamento / Producto
                    let $celdaMedProd = document.createElement("td");
                    let $contenidoCeldaMedProd = document.createElement("p");

                    $contenidoCeldaMedProd.textContent =
                        df.nombreMed != null ? df.nombreMed : df.nombreProd;

                    $celdaMedProd.appendChild($contenidoCeldaMedProd);

                    // Cantidad
                    let $celdaCant = document.createElement("td");
                    let $contenidoCeldaCant = document.createElement("p");
                    $contenidoCeldaCant.textContent = df.cantidad;

                    $celdaCant.appendChild($contenidoCeldaCant);

                    $fila.appendChild($celdaMedProd);
                    $fila.appendChild($celdaCant);
                });

                $cuerpoTabla.appendChild($fila);
            });
        })
}

document.getElementById("icono_lupa").addEventListener("click", cargarFactura)
document.addEventListener("DOMContentLoaded", () => {
    cargarFacturas();
    cargarSucursales();
});

document.getElementById("opciones-sucursales").addEventListener("change", buscarFacturaPorSucursal);