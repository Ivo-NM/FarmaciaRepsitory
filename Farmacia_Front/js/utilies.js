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