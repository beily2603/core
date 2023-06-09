
const uri= '/User';

function login(){
    const name = document.getElementById('name');
    const password = document.getElementById('password');
    const item = {
        Name: name.value.trim(),
        Password:password.value.trim()
    };
    fetch(`${uri}/Login`, {
      
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
       
        .then((response)=>{response.text();alert("55555555");})
        .then((data) => {
            sessionStorage.setItem("token",data.token);
            name.value = '';
            password.value = '';
            if(data.isAdmin)
                  location.href="../html/users.html";
            else
                  location.href="../html/tasks.html";
          
          


        })
        .catch(error => console.error('User not valid!'));
}
