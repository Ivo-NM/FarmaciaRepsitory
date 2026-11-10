function Login()
{
    const $inputEmail = document.getElementById("loginEmail");
    const $inputPassword = document.getElementById("loginPassword");

    let email = $inputEmail.value;
    let password = $inputPassword.value;

    //También evita que el usuario pueda dejar espacios
    
    if (email.trim() === "")
    {
        Swal.fire({
            title: "Por favor, coloque un correo",
            icon: "warning",
            confirmButtonText: "Aceptar"
        });

        return;
    };

    if (password.trim() === "")
    {
        Swal.fire({
            title: "Por favor, coloque la contraseña",
            icon: "warning",
            confirmButtonText: "Aceptar"
        });
        
        return;
    }

    let usuario = {
        "Correo": email,
        "Contrasenia": password
    };

    let url = "https://localhost:7032/api/Usuarios/login";

    fetch (url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(usuario)
    })
        .then(r => {

            if (!r.ok)
            {
                Swal.fire("Contraseña y/o password incorrecto");
                return;
            }

            return r.json();
        })
        .then(d => {
                console.log(d);
                Swal.fire(`${d.message}`);
                return;
        });
}

document.getElementById("loginButton").addEventListener("click", Login);