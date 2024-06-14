// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    showQuantiyCart();
});


$(".addtocart").click(function (evt) {
    evt.preventDefault();
    let id = $(this).attr("data-productId");

    $.ajax({
        url: "/customer/cart/addtocartapi",
        data: { "productId": id },
        success: function (data) {
            //thong bao ket qua
            Swal.fire({
                title: "Product added to cart",
                text: "You clicked the button!",
                icon: "success"
            });
            //hien thi so luong san pham trong gio trong _FrontEnd.cshtml
            showQuantiyCart();
        }
    });
})


let showQuantiyCart = () => {
    $.ajax({
        url: "/customer/cart/GetQuantityOfCart",
        success: function (data) {
            //hien thi so luong san pham trong gio trong _FrontEnd.cshtml
            $(".showcart").text(data.qty);
        }
    });
}