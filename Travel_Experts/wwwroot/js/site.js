// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//David Grant

// Login form
function displayForm() {
    document.getElementById("myForm").style.display = "block";
}
function closeForm() {
    document.getElementById("myForm").style.display = "none";
}
// Register form
function displayRegisterForm() {
    document.getElementById("myRegisterForm").style.display = "block";
}
function closeRegisterForm() {
    document.getElementById("myRegisterForm").style.display = "none";
}






//Regex Validation
function Validate() {

    emailD = document.getElementById("Email").value
    passwordD = document.getElementById("Password").value
    passwordDB = document.getElementById("cPassword").value
    fnameD = document.getElementById("Firstname").value
    lnameD = document.getElementById("Lastname").value


    var formData = {
        email: emailD,
        password: passwordD,
        fname: fnameD,
        lname: lnameD,

    }


    regexEmail = /^[^\\s@]+@[^\s@]+\.[^\s@]+$/
    regexPassword = /^(?=.*[!@#$%^&*])(?=.*[A-Z])(?=.*[a-z])/


    returnString = ""
    for (x in formData) {
        if (formData[x].length < 1) {
            returnString = "All elements must be filled out."
        }
    }
    if (passwordD != passwordDB) {
        returnString = "Passwords must match."
    }
    if (regexEmail.test(formData["email"]) == false) {
        returnString = "Must use a valid email address."
    }
    else if (regexPassword.test(formData["password"]) == false) {
        returnString = "Must use a valid email address."
    }



    if (returnString != "") {
        document.getElementById("errors").innerHTML = returnString
    }
    else {
        document.getElementById("errors").innerHTML = "Thank you for registering!"
    }
}



