function cargarEntregasPorNro()
{
    const $nroEntrega = document.getElementById("input-nro-entrega");
    let nroEntrega = Number($nroEntrega.value);

    const $lstEntregas = document.getElementById("cuerpo_tabla");
    $lstEntregas.replaceChildren();

    let url = `https://localhost:7032/api/Entregas/entregas/${nroEntrega}`;

    fetch(url)
        .then(r => {
            if (!r.ok)
            {
                return null;
            }

            return r.json();
        })
        .then(e => {
            if (e === null)
            {
                return Swal.fire("Lo sentimos, hubo un problema cargando la entrega");
            }

            e.detalleEntregasDtos.forEach(de => {
                    let $fila = document.createElement("tr");

                    //Nro. Entrega
                    let $nroEntrega = document.createElement("td");
                    $nroEntrega.textContent = e.nroEntrega;

                    //Fecha
                    let $fecha = document.createElement("td");
                    $fecha.textContent = dayjs(e.fecha).format("DD/MM/YYYY");

                    //Respuesta Entrega
                    let $respuestaEntrega = document.createElement("td");
                    $respuestaEntrega.textContent = e.respuestaEntrega;

                    //Cod. Empleado
                    let $codEmpleado = document.createElement("td");
                    $codEmpleado.textContent = e.idEmpleado;

                    //Med/Producto
                    let $medProd = document.createElement("td");
                    $medProd.textContent = de.nombreMedicamento != null ? de.nombreMedicamento : de.nombreProducto;

                    //Cantidad
                    let $cant = document.createElement("td");
                    $cant.textContent = de.cantidad;

                    $fila.appendChild($nroEntrega);
                    $fila.appendChild($fecha);
                    $fila.appendChild($respuestaEntrega);
                    $fila.appendChild($codEmpleado);
                    $fila.appendChild($medProd);
                    $fila.appendChild($cant);

                    $lstEntregas.appendChild($fila);
            })
        })
}

function cargarEntregasPorSucursal()
{
    const $nroSucursal = document.getElementById("opciones-sucursales");
    let nroSucursal = Number($nroSucursal.value);

    const $lstEntregas = document.getElementById("cuerpo_tabla");
    $lstEntregas.replaceChildren();

    let url = `https://localhost:7032/api/Entregas/entregas/sedes/${nroSucursal}`;

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
                Swal.fire("Lo sentimos, hubo un problema cargando las entregas");
                return;
            }

            d.forEach(e => {
                e.detalleEntregasDtos.forEach(de => {
                    let $fila = document.createElement("tr");

                    //Nro. Entrega
                    let $nroEntrega = document.createElement("td");
                    $nroEntrega.textContent = e.nroEntrega;

                    //Fecha
                    let $fecha = document.createElement("td");
                    $fecha.textContent = dayjs(e.fecha).format("DD/MM/YYYY");

                    //Respuesta Entrega
                    let $respuestaEntrega = document.createElement("td");
                    $respuestaEntrega.textContent = e.respuestaEntrega;

                    //Cod. Empleado
                    let $codEmpleado = document.createElement("td");
                    $codEmpleado.textContent = e.idEmpleado;

                    //Med/Producto
                    let $medProd = document.createElement("td");
                    $medProd.textContent = de.nombreMedicamento != null ? de.nombreMedicamento : de.nombreProducto;

                    //Cantidad
                    let $cant = document.createElement("td");
                    $cant.textContent = de.cantidad;

                    $fila.appendChild($nroEntrega);
                    $fila.appendChild($fecha);
                    $fila.appendChild($respuestaEntrega);
                    $fila.appendChild($codEmpleado);
                    $fila.appendChild($medProd);
                    $fila.appendChild($cant);

                    $lstEntregas.appendChild($fila);
                })
            });
        });
}

function cargarEntregas()
{
    const $lstEntregas = document.getElementById("cuerpo_tabla");
    $lstEntregas.replaceChildren();

    let url = "https://localhost:7032/api/Entregas/entregas";

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
                Swal.fire("Lo sentimos, hubo un problema cargando las entregas");
                return;
            }

            d.forEach(e => {
                e.detalleEntregasDtos.forEach(de => {
                    let $fila = document.createElement("tr");

                    //Nro. Entrega
                    let $nroEntrega = document.createElement("td");
                    $nroEntrega.textContent = e.nroEntrega;

                    //Fecha
                    let $fecha = document.createElement("td");
                    $fecha.textContent = dayjs(e.fecha).format("DD/MM/YYYY");

                    //Respuesta Entrega
                    let $respuestaEntrega = document.createElement("td");
                    $respuestaEntrega.textContent = e.respuestaEntrega;

                    //Cod. Empleado
                    let $codEmpleado = document.createElement("td");
                    $codEmpleado.textContent = e.idEmpleado;

                    //Med/Producto
                    let $medProd = document.createElement("td");
                    $medProd.textContent = de.nombreMedicamento != null ? de.nombreMedicamento : de.nombreProducto;

                    //Cantidad
                    let $cant = document.createElement("td");
                    $cant.textContent = de.cantidad;

                    $fila.appendChild($nroEntrega);
                    $fila.appendChild($fecha);
                    $fila.appendChild($respuestaEntrega);
                    $fila.appendChild($codEmpleado);
                    $fila.appendChild($medProd);
                    $fila.appendChild($cant);

                    $lstEntregas.appendChild($fila);
                })
            });
        });
}

document.addEventListener("DOMContentLoaded", () =>  {
    cargarSucursales("opciones-sucursales");
    cargarEntregas();
});

document.getElementById("opciones-sucursales").addEventListener("change", cargarEntregasPorSucursal);

document.getElementById("icono_lupa").addEventListener("click", cargarEntregasPorNro);