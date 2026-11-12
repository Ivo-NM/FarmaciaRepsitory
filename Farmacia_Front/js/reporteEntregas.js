function buscarEntregasPorSucursal()
{
    const $lstSucursales = document.getElementById("opciones-sucursales");
    let idSucursal = $lstSucursales.value;
    let $cuerpoTabla = document.getElementById("cuerpo_tabla");
    $cuerpoTabla.replaceChildren();

    let url = `https://localhost:7032/api/Entregas/entregas/sedes/${idSucursal}`;

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
                Swal.fire("Lo siento, hubo un problema mientras se cargaban las entregas");
                return;
            }

            d.forEach(e => {
                
                e.detalleEntregasDtos.forEach(de => {
                    let $fila = document.createElement("tr");
                    
                    //Nro. Entrega
                    let $celdaNroEntrega = document.createElement("td");
                    $celdaNroEntrega.textContent = e.nroEntrega;

                    //Fecha
                    let $celdaFecha = document.createElement("td");
                    $celdaFecha.textContent = dayjs(e.fecha).format("DD/MM/YYYY");

                    //Respuesta Entrega
                    let $celdaRespuestaEntrega = document.createElement("td");
                    $celdaRespuestaEntrega.textContent = e.respuestaEntrega;

                    //Cod. Empleado
                    let $celdaCodEmpleado = document.createElement("td");
                    $celdaCodEmpleado.textContent = e.idEmpleado;

                    //Med/Prod
                    let $celdaMedProd = document.createElement("td");
                    $celdaMedProd.textContent = de.nombreMedicamento != null ? de.nombreMedicamento : de.nombreProducto;

                    //Cant
                    let $celdaCant = document.createElement("td");
                    $celdaCant.textContent = de.cantidad;

                    $fila.appendChild($celdaNroEntrega);
                    $fila.appendChild($celdaFecha);
                    $fila.appendChild($celdaRespuestaEntrega);
                    $fila.appendChild($celdaCodEmpleado);
                    $fila.appendChild($celdaMedProd);
                    $fila.appendChild($celdaCant);

                    $cuerpoTabla.appendChild($fila);
                });
            })
        })
}

function BuscarEntregaPorNro()
{
    const $inputIdEntrega = document.getElementById("input-nro-entrega");
    let idEntrega = Number($inputIdEntrega.value);
    
    let $cuerpoTabla = document.getElementById("cuerpo_tabla");
    $cuerpoTabla.replaceChildren();

    let url = `https://localhost:7032/api/Entregas/entregas/${idEntrega}`;

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
                Swal.fire("Lo siento, hubo un error mientras se cargaba la entrega");
                return;
            }

            d.detalleEntregasDtos.forEach(de => {
                let $fila = document.createElement("tr");
                
                //Nro. Entrega
                let $celdaNroEntrega = document.createElement("td");
                $celdaNroEntrega.textContent = d.nroEntrega;

                //Fecha
                let $celdaFecha = document.createElement("td");
                $celdaFecha.textContent = dayjs(d.fecha).format("DD/MM/YYYY");
                
                //Respuesta Entrega
                let $celdaRespuestaEntrega = document.createElement("td");
                $celdaRespuestaEntrega.textContent = d.respuestaEntrega;

                //Cod. Empleado
                let $celdaCodEmpleado = document.createElement("td");
                $celdaCodEmpleado.textContent = d.idEmpleado;

                //Med/Prod
                let $celdaMedProd = document.createElement("td");
                $celdaMedProd.textContent = de.nombreMedicamento != null ? de.nombreMedicamento : de.nombreProducto;

                //Cantidad
                let $celdaCant = document.createElement("td");
                $celdaCant.textContent = de.cantidad;

                $fila.appendChild($celdaNroEntrega);
                $fila.appendChild($celdaFecha);
                $fila.appendChild($celdaRespuestaEntrega);
                $fila.appendChild($celdaCodEmpleado);
                $fila.appendChild($celdaMedProd);
                $fila.appendChild($celdaCant);

                $cuerpoTabla.appendChild($fila);
            })
        })
}

function cargarEntregas()
{    
    let $cuerpoTabla = document.getElementById("cuerpo_tabla");
    $cuerpoTabla.replaceChildren();

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
                Swal.fire("Lo sentimos, hubo problemas cargando las entregas");
                return;
            }

            d.forEach(e => {
                
                e.detalleEntregasDtos.forEach(de => {
                    let $fila = document.createElement("tr");

                    //Nro. Entrega
                    let $celdaNroEntrega = document.createElement("td");
                    $celdaNroEntrega.textContent = e.nroEntrega;

                    //Fecha
                    let $celdaFecha = document.createElement("td");
                    $celdaFecha.textContent = dayjs(e.fecha).format("DD/MM/YYYY");

                    //Respuesta Entrega
                    let $celdaRespuestaEntrega = document.createElement("td");
                    $celdaRespuestaEntrega.textContent = e.respuestaEntrega;

                    //Cod. Empleado
                    let $celdaCodEmpleado = document.createElement("td");
                    $celdaCodEmpleado.textContent = e.idEmpleado;

                    //Medicamento/Producto
                    let $celdaMedProd = document.createElement("td");
                    $celdaMedProd.textContent =
                    de.nombreMedicamento != null ? de.nombreMedicamento : de.nombreProducto;

                    //Cantidad
                    let $celdaCant = document.createElement("td");
                    $celdaCant.textContent = de.cantidad;

                    //Agregar celdas a la fila
                    $fila.appendChild($celdaNroEntrega);
                    $fila.appendChild($celdaFecha);
                    $fila.appendChild($celdaRespuestaEntrega);
                    $fila.appendChild($celdaCodEmpleado);
                    $fila.appendChild($celdaMedProd);
                    $fila.appendChild($celdaCant);

                    $cuerpoTabla.appendChild($fila);
                });
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

document.addEventListener("DOMContentLoaded", () =>  {
    cargarSucursales(), cargarEntregas()
});

document.getElementById("icono_lupa").addEventListener("click", BuscarEntregaPorNro);

document.getElementById("opciones-sucursales").addEventListener("change", buscarEntregasPorSucursal);