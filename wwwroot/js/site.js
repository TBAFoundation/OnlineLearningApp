document.addEventListener("DOMContentLoaded", function () {
    fetch('/Orders/GetCartItemCount', {
        method: 'GET'
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('cart-item-count').innerHTML = data;
        })
        .catch(error => console.error('Error fetching cart item count:', error));
});
