function buscarSucursales(idLstSucursales)
{
    const $lstSucursales = document.getElementById(idLstSucursales);
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
                $opcionSucursal.textContent = s.nomSede;
                $opcionSucursal.value = s.nroSucursal;
                $lstSucursales.appendChild($opcionSucursal);
            })
        })
}

function buscarMedicamentos(idLstMed)
{
    const $lstMedicamentos = document.getElementById(idLstMed);
    $lstMedicamentos.replaceChildren();
    let $opcionSinMed = document.createElement("option");
    $opcionSinMed.textContent = "Sin medicamento";
    $opcionSinMed.value = "";
    $lstMedicamentos.appendChild($opcionSinMed);

    let url = "https://localhost:7032/api/Medicamentos/medicamentos";

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
                Swal.fire("Lo sentimos, hubo un problema cargando los medicamentos");
                return;
            }

            d.forEach(m => {
                let $opcionMed = document.createElement("option");
                $opcionMed.textContent = m.nombreMedicamento;
                $opcionMed.value = m.nombreMedicamento;
                $lstMedicamentos.appendChild($opcionMed);
            })
        })
}

function buscarProductos(idLstProd)
{
    const $lstProductos = document.getElementById(idLstProd);
    $lstProductos.replaceChildren();
    let $opcionSinProd = document.createElement("option");
    $opcionSinProd.textContent = "Sin producto";
    $opcionSinProd.value = "";
    $lstProductos.appendChild($opcionSinProd);

    let url = "https://localhost:7032/api/Productos/productos";

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
                Swal.fire("Lo sentimos, hubo un problema cargando los productos");
                return;
            }

            d.forEach(p => {
                let $opcionProd = document.createElement("option");
                $opcionProd.textContent = p.codProducto;
                $opcionProd.value = p.detalle;
                $lstProductos.appendChild($opcionProd);
            })
        })
}