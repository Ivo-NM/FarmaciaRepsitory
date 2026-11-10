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
                $contenidoCeldaFecha.textContent = f.fecha;

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

document.addEventListener("DOMContentLoaded", cargarFacturas);