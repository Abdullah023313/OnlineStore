//document.getElementById("btnBuy").onclick = function() {
//    Console.log(1);
//};

let img = document.getElementById("main-img");

function SelectImage(imgItem) {
    console.log(imgItem);
    img.src = "/images/" + imgItem;
}

function clickingtoggle_menu() {
    let StylePage = document.getElementById("page").style.display.toString();
    if (StylePage != "none") {

        document.getElementById("page").style.display = "none";
    } else {
        document.getElementById("page").style.display = "flex";
    }
}

function clickingFilter() {
    document.getElementById("aside").style.display = "block";
    document.getElementById("insulator").style.display = "block";
}

function clickingLeftArrow() {
    document.getElementById("aside").style.display = "none";
    document.getElementById("insulator").style.display = "none";
}

function clickingCategories() {
    if (document.getElementById("ulCategories").style.display != "block") {
        document.getElementById("ulCategories").style.display = "block";
        document.getElementById("fa-angle-down").style.transform = "rotate(180deg)";
    } else {
        document.getElementById("ulCategories").style.display = "none";
        document.getElementById("fa-angle-down").style.transform = "rotate(0)";
    }

}
let slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);

}

function currentSlide(n) {
    showSlides(slideIndex = n);

}

function currentSlide(n) {
    showSlides(slideIndex = n);

}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("dot");

    if (n > slides.length) {
        slideIndex = 1;
    }
    if (n < 1) {
        slideIndex = slides.length;
    }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";

    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}